"use strict";
var CEApp = CEApp || {};

CEApp.Json = function () {
    var
        individualEntryJson = function (options) {
            var contactName = (options && options['contactName']) ? options['contactName'] : '';
            var contactEmail = (options && options['contactEmail']) ? options['contactEmail'] : '';
            var contactPhone = (options && options['contactPhone']) ? options['contactPhone'] : '';
            var category = (options && options['category']) ? options['category'] : '';
            var division = (options && options['division']) ? options['division'] : '';
            var ceClass = (options && options['class']) ? options['class'] : '';
            var subcategory = (options && options['subcategory']) ? options['subcategory'] : '';
            var lastName = (options && options['lastName']) ? options['lastName'] : '';
            var firstName = (options && options['firstName']) ? options['firstName'] : '';
            var chineseName = (options && options['chineseName']) ? options['chineseName'] : '';
            var birthday = (options && options['birthday']) ? options['birthday'] : '';
            var school = (options && options['school']) ? options['school'] : '';
            var otherSchool = (options && options['otherSchool']) ? options['otherSchool'] : '';
            var email = (options && options['email']) ? options['email'] : contactEmail;
            var grade = (options && options['grade']) ? options['grade'] : '';
            var lunchProgram = (options && options['lunchProgram']) ? options['lunchProgram'] : false;

            var jsonobj = {
                'contactName': contactName,
                'contactEmail': contactEmail,
                'contactPhone': contactPhone,
                'category': category,
                'division': division,
                'class': ceClass,
                'subcategory': subcategory,
                'lastName': lastName,
                'firstName': firstName,
                'chineseName': chineseName,
                'birthday': birthday,
                'school': school,
                'otherSchool': otherSchool,
                'email': email,
                'grade': grade,
                'lunchProgram': lunchProgram ? '1' : '0'
            };
            return jsonobj; // use JSON.stringify(jsonobj) to convert it to string
        },

        teamEntryJson = function (options) {
            var contactName = (options && options['contactName']) ? options['contactName'] : undefined;
            var contactEmail = (options && options['contactEmail']) ? options['contactEmail'] : undefined;
            var contactPhone = (options && options['contactPhone']) ? options['contactPhone'] : undefined;
            var team = (options && options['team']) ? options['team'] : undefined;
            var category = (options && options['category']) ? options['category'] : undefined;
            var division = (options && options['division']) ? options['division'] : undefined;
            var subcategory = (options && options['subcategory']) ? options['subcategory'] : '';
            var teammates = (options && options['teammates']) ? options['teammates'] : null;

            if (teammates == null) teammates = [];
            var jsonobj = {
                'contactName': contactName,
                'contactEmail': contactEmail,
                'contactPhone': contactPhone,
                'team': team,
                'category': category,
                'division': division,
                'subcategory': subcategory,
                'teammates': teammates
            }
            return jsonobj; // use JSON.stringify(jsonobj) to convert it to string
        },

        tourEntryJson = function (options) {
            // page 1
            var firstName = (options && options['firstName']) ? options['firstName'] : '';
            var lastName = (options && options['lastName']) ? options['lastName'] : '';
            var email = (options && options['email']) ? options['email'] : '';
            var phone = (options && options['phone']) ? options['phone'] : '';
            var cellphone = (options && options['cellphone']) ? options['cellphone'] : '';
            var district = (options && options['district']) ? options['district'] : '';
            var school = (options && options['school']) ? options['school'] : '';
            var grade = (options && options['grade']) ? options['grade'] : '';
            var subject = (options && options['subject']) ? options['subject'] : '';
            var gender = (options && options['gender']) ? options['gender'] : '';
            // page 2
            var learnProgram = (options && options['learnProgram']) ? options['learnProgram'] : '';
            var specialty = (options && options['specialty']) ? options['specialty'] : '';
            var reference1 = (options && options['reference1']) ? options['reference1'] : '';
            var reference2 = (options && options['reference2']) ? options['reference2'] : '';
            var reference3 = (options && options['reference3']) ? options['reference3'] : '';
            // page 3 - move to individual cookie to workaround 4K cookie length limit

            var jsonobj = {
                // page 1
                'firstName': firstName,
                'lastName': lastName,
                'email': email,
                'phone': phone,
                'cellphone': cellphone,
                'district': district,
                'school': school,
                'grade': grade,
                'subject': subject,
                'gender': gender,
                // page 2
                'learnProgram': learnProgram,
                'specialty': specialty,
                'reference1': reference1,
                'reference2': reference2,
                'reference3': reference3
                // page 3 - potentially too large to saved to cookie
            }
            return jsonobj; // use JSON.stringify(jsonobj) to convert it to string
        }

        return {
            individualEntryJson: individualEntryJson,
            teamEntryJson: teamEntryJson,
            tourEntryJson: tourEntryJson
        }
}();
