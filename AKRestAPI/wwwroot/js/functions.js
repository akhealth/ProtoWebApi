/*jshint esversion: 6 */
/*jslint browser: true*/
/*global  $*/

$(document).ready(function () {

    $("#search").submit(function (event) {

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
        var url = "http://protowebapi-development.azurewebsites.net/mci/people/findByName?firstName=" +
            encodeURIComponent(name.first) + "&lastName=" + encodeURIComponent(name.last);

        // Clear any prior results from the results table.
        $("#result-list td").remove();

        // Show a spinner while data is being loaded.
        $("#loading").show();

        $.get(url, function (data) {

            // We'll build up a list of ARIES IDs, to query ARIES for more information.
            var AriesIDs = [];

            // We'll build up a list of results to iterate through and display.
            var MCIData = [];

            // Iterate through the returned names and list them all.
            $.each(data, function (i, val) {

                var person = val.SearchResponsePerson;

                var result = {
                    Name: null,
                    clientID: null,
                    DateOfBirth: null,
                    Program: null,
                    CaseNumber: null,
                    CaseStatus: null,
                    CaseAction: null,
                    EISID: null
                };

                // Name
                result.Name = person.FirstName + ""  + person.LastName;

                // Date of birth
                result.DateOfBirth = person.DateOfBirth.substring(5,7) + "/" +
                    person.DateOfBirth.substring(8,10) + "/" + person.DateOfBirth.substring(0,4);

                // List all registration IDs.
                $.each(person.Registrations.Registration, function (j, registration) {

                    // If there's just one registration, it's not an object, and is handled differently.
                    if (!$.isPlainObject(registration)) {
                        registration = person.Registrations.Registration;
                    }

                    // Indicate whether application has an EIS record.
                    if (registration.RegistrationName == "EIS_ID") {
                        result.EISID = registration.RegistrationValue;
                    }
                    else {
                        result.EISID = "N/A";
                    }

                    // If there's a non-empty ARIES ID, indicate s/he's in ARIES.
                    if (registration.RegistrationName == "ARIES_ID" && registration.RegistrationValue != "") {
                        
                        result.clientID = registration.RegistrationValue;

                        // MAGI Medicaid is the only program listed in ARIES, so we know the program.
                        result.Program = "MAGI Medicaid";
                        
                        // Add this to our list of ARIES IDs.
                        AriesIDs.push(registration.RegistrationValue);

                    }

                });

                // Save this result to our list.
                MCIData.push(result);

            });

            // Query ARIES for information about these results.
            var AriesURL = "http://protowebapi-development.azurewebsites.net/aries?ids=" + AriesIDs.join(",");
            $.getJSON(AriesURL, function (AriesData) {

                // Merge our ARIES data into our MCI results (if there are any).

                // Create a blank array where we can track which ARIES IDs (unique people) we have
                // already matched. We'll use that to build up an array of additional cases for each
                // person (e.g., a person's case #2, case #3, etc.), which we'll merge back in at
                // the end.
                var MatchedRecords = [];
                var DuplicateRecords = [];

                if(AriesData.length > 0) {
                    // Test data, for returning multiple ARIES records for a single person.
                    //AriesData = $.parseJSON('[{"app_num":"T12345678","application_status_cd":"DI","indv_id":2400000003},{"app_num":"T87654321","application_status_cd":"FG","indv_id":2400000003}]');
                    for (AriesRecord of AriesData) {
                        for (var i of MCIData) {
                            if (AriesRecord.indv_id == i.clientID) {
                                
                                // If we've already seen this ARIES ID, make a copy of it and
                                // append the ARIES case information.
                                if ($.inArray(String(i.clientID), MatchedRecords) >= 0)
                                {

                                    // Make a copy of -- NOT a reference to -- i.
                                    var MovedRecord = $.extend({}, i);

                                    MovedRecord.CaseNumber = AriesRecord.app_num;
                                    MovedRecord.CaseStatus = AriesRecord.application_status_cd;
                                    DuplicateRecords.push(MovedRecord);

                                }

                                else {
                                    
                                    // Append the ARIES case information to this person's record.
                                    i.CaseNumber = AriesRecord.app_num;
                                    i.CaseStatus = AriesRecord.application_status_cd;

                                    // Record that we have seen this ARIES ID.
                                    MatchedRecords.push(String(i.clientID));

                                }

                                // Stop searching.
                                break;

                            }
                        }
                    }

                    // If we found any individual with more than one case, merge the additional
                    // cases into our list of matched recors.
                    if (DuplicateRecords.length > 0) {
                        $.merge(MCIData, DuplicateRecords);
                    }

                }

                // Inject our results into the DOM.
                for (var MCIRecord of MCIData) {

                    var html = "<tr>";
                    $.each(MCIRecord, function(key, val) {
                        if (key == "clientID") {
                            val = "<a href=\"https://uat.myaries.alaska.gov/wp/ControllerServlet?PAGE_ID=IQISU&ACTION=ClickOnLink&cin=" +
                                val + "&CS=Y&token=Random&FWPOPUP=N&WF_NAV_ID=CS\">" +
                                val + "</a>";
                        }
                        if (val == null) {
                            val = " ";
                        }
                        html += "<td>" + val + "</td>";
                    });
                    html += "</tr>";

                    $("#result-list > tbody:last-child").append(html);
                }

                // Remove the spinner overlay.
                $("#loading").hide();

                // Reveal the results.
                $("#results").show();

            });

            });

        return false;
    });
});
