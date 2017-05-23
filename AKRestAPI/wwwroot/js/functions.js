$(document).ready(function () {

    $("#search").submit(function (event) {

        // Split the name on a space, which is terrible, but fine for a demo.
        var first_name = $("#name-first").val();
        var last_name = $("#name-last").val();

        // Performing no validation or filtering -- because it's a demo! -- query the URL.
        var url = "http://localhost:5000/mci/people/findByName?firstName=" + first_name + "&lastName=" + last_name;
        
        $.get(url, function (data) {

            var total = '<span class="number">' + data.length + ' results found</span>';
            $("#results").html(total);

            // Iterate through the returned names and list them all.
            $.each(data, function (i, val) {
                var person = val.SearchResponsePerson;
                var result = '<div class="result">';
                result += "<strong>" + person.FirstName + " " + person.LastName + "</strong><ul>";
                
                // List all registration IDs.
                $.each(person.Registrations.Registration, function (j, registration) {

                    if ($.isPlainObject(registration)) {
                        registration.RegistrationName = registration.RegistrationName.replace("_", " ");
                        result += "<li>" + registration.RegistrationName + ": " + registration.RegistrationValue + "</li>";
                    } else {
                        // If there's just one registration, it's not an object, and is handled differently.
                        registration = person.Registrations.Registration;
                        registration.RegistrationName = registration.RegistrationName.replace("_", " ");
                        result += "<li>" + registration.RegistrationName + ": " + registration.RegistrationValue + "</li>";
                    }
                });
                result += "</ul></div>";

                $("#results").append(result);
            });
        });

        return false;
    });
});
