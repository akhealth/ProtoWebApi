/*jshint esversion: 6 */
/*jslint browser: true*/
/*global  $*/

// Define the protocol, domain, and port for our request. If this is being deployed
// locally, use the local machine. Otherwise, use the staging URL (thus forcing HTTPS).
if (window.location.origin.search("localhost") > -1) {
    var MCIEndpoint = window.location.origin;
}
else {
    var MCIEndpoint = "https://protowebapi-staging.azurewebsites.net";
}
var ARIESEndPoint = "https://protowebapi-staging.azurewebsites.net";
var EISEndPoint = "https://protowebapi-staging.azurewebsites.net";

// Append detailed ARIES information to the MCI records.
function GetARIESRecords(AriesIDs, MCIData, callback) {

    // Query ARIES for information about these results.
    var AriesURL = ARIESEndPoint + "/aries?ids=" + AriesIDs.join(",");

    $.getJSON(AriesURL, function (AriesData) {

        // Merge our ARIES data into our MCI results (if there are any).
        if(AriesData.length > 0) {
            for (var AriesRecord of AriesData) {
                for (var MCIRecord of MCIData) {
                    for (var Registration of MCIRecord.Registrations) {
                        if (AriesRecord.indv_id == Registration.ID) {

                            // Append the ARIES case information to this person's record.
                            Registration.IndividualID = AriesRecord.app_num;
                            Registration.ApplicationStatus = AriesRecord.application_status_cd;

                            // Stop searching.
                            break;

                        }
                    }
                }
            }
        }
    })
    .always(function() {
        callback(MCIData);
    });

}

// Append detailed EIS information to the MCI records.
function GetEISRecords(MCIData, callback) {

    // Iterate through all of our MCI results.
    for (var MCIRecord of MCIData) {

        // If there is a known EIS record.
        if (MCIRecord.EISID === undefined) {

            // Query EIS for information about this result.
            var EISURL = EISEndPoint + "/eis?ids=" + MCIRecord.EISID;
            
            // Send a query, but only retrieve the HTTP status code, which is all we need.
            $.ajax(EISURL, {
                type: "GET",
                statusCode: {
                    200: function (response) {
                        MCIRecord.InEIS = true;
                    },
                    404: function (response) {
                        MCIRecord.InEIS = false;
                    }
                }
            });

        }

    }
    
    callback(MCIData);

}

// Send records to the browser.
function DisplayRecords(MCIData) {
    // Inject our results into the DOM.
    for (var MCIRecord of MCIData) {

        // The row in the search results.
        var TableHTML = '<tr data-virtualid="' + MCIRecord.VirtualID + '">';
        TableHTML += "<td>" + MCIRecord.Name + "</td>";
        TableHTML += "<td>" + MCIRecord.DateOfBirth + "</td>";
        TableHTML += "<td>" + MCIRecord.SSN + "</td>";
        TableHTML += "</tr>";

        // The detailed information, available upon clicking.
        var DetailsHTML = '<div class="record-detail" id="record-' + MCIRecord.VirtualID + '">';        
        DetailsHTML += "<h1>" + MCIRecord.Name + "</h1>";
        DetailsHTML += MCIRecord.DateOfBirth + "<br>";
        if (MCIRecord.SSN !== "") {
            DetailsHTML += MCIRecord.SSN + "<br>";
        }

        for (var Registration of MCIRecord.Registrations) {
            DetailsHTML += '<div class="registration">' +
                "<li>Individual ID: " + Registration.IndividualID + "</li>";
            $.each(Registration, function (key, value) {
                DetailsHTML += "<li>" + key + ": " + value + "</li>";
            });
            DetailsHTML += "</div>";
        }
        DetailsHTML += "</div>";

        $("#result-list > tbody:last-child").append(TableHTML);
        $("#result-details").append(DetailsHTML);
    }

    // Remove the spinner overlay.
    $("#loading").hide();

    // Reveal the results.
    $("#results").show();

    // When a record is selected, show details about it.
    $("#result-list tbody tr").on("click", function() {
        $("#loading").show();
        $("#record-" + $(this).attr("data-virtualid")).toggle();
    });

    return true;

}


$(document).ready(function () {

    $("#search").submit(function (event) {

        // Clear any prior results from the results table.
        $("#result-list td").remove();

        // Show a spinner while data is being loaded.
        $("#loading").show();

        // Fetch the search terms.
        var name = {};
        name.first = $("#name-first").val();
        name.last = $("#name-last").val();

        // Cap first and last names at plausible lengths. We don't validate names any further,
        // because http://www.kalzumeus.com/2010/06/17/falsehoods-programmers-believe-about-names/.
        // However, further filtering of these fields would be necessary on a non-prototype system.
        if (name.first.length > 32) {
            name.first = name.first.substring(0, 31);
        }
        if (name.last.length > 32) {
            name.last = name.last.substring(0, 31);
        }

        // Query the URL, encoding the first and last names as URL fragments.
        var url = MCIEndpoint + "/mci/people/findByName?firstName=" +
            encodeURIComponent(name.first) + "&lastName=" + encodeURIComponent(name.last);

        $.ajax({
            url: url,
            success: function (data) {

                // We'll build up a list of ARIES IDs, to query ARIES for more information.
                var AriesIDs = [];

                // We'll build up a list of results to iterate through and display.
                var MCIData = [];

                // Iterate through the returned names and list them all.
                $.each(data, function (i, val) {

                    var person = val.SearchResponsePerson;

                    var result = {
                        Name: null,
                        VirtualID: null,
                        DateOfBirth: null,
                        Registrations: []
                    };

                    // Name
                    result.Name = person.FirstName + " " + person.LastName;

                    // Date of birth
                    result.DateOfBirth = person.DateOfBirth.substring(5,7) + "/" +
                        person.DateOfBirth.substring(8,10) + "/" + person.DateOfBirth.substring(0,4);

                    // VirtualID
                    result.VirtualID = person.VirtualId;

                    // List all registration IDs.
                    $.each(person.Registrations.Registration, function (j, registration) {

                        // If there's just one registration, it's not an object, and is handled differently.
                        if (!$.isPlainObject(registration)) {
                            registration = person.Registrations.Registration;
                        }

                        // Turn the registration information into a temporary object, and append it to our
                        // registration information.
                        var tmp = {};
                        tmp.System = registration.RegistrationName.replace("_ID", "");
                        tmp.ID = registration.RegistrationValue;
                        result.Registrations.push(tmp);

                        // If we have an ARIES ID, add it to our list of ARIES IDs.
                        if (registration.RegistrationName == "ARIES_ID") {
                            AriesIDs.push(registration.RegistrationValue);
                        }

                    });

                    // Save this result to our list.
                    MCIData.push(result);
                    
                });

                // Add ARIES, EIS details to every MCI record.
                GetARIESRecords(AriesIDs, MCIData, function(MCIData) {
                    GetEISRecords(MCIData, function(MCIData) {
                        // Send data to the browser.
                        DisplayRecords(MCIData);
                    });
                });
            },

            error: function() {
                alert("Azure returned an error. Please reload the page and try again.");
            }

        });

        return false;

    });

});
