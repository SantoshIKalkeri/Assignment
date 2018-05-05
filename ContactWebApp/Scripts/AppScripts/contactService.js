(
    function () {
        var app = angular.module("contactApp");

        app.factory("contactService", function ($http) {

            //TODO: add constant for base url
            function getContactList() {
                return $http.get("http://localhost/ContactServices/contactapi/contact");
            }

            function updateContact(id, updatedContactModel) {
                return $http(
                    {
                        url: "http://localhost/ContactServices/contactapi/contact/" + id,
                        method: "PUT",
                        data: updatedContactModel
                    }
                );
            }

            function addContact(newContactModel) {
                return $http(
                    {
                        url: "http://localhost/ContactServices/contactapi/contact",
                        method: "POST",
                        data: newContactModel
                    }
                );
            }

            function deleteContact(id) {
                return $http(
                    {
                        url: "http://localhost/ContactServices/contactapi/contact/" + id,
                        method: "DELETE"
                    }
                );
            }

            return {
                getContactList: getContactList,
                addContact: addContact,
                deleteContact: deleteContact,
                updateContact: updateContact
            }
        });
    }
)();