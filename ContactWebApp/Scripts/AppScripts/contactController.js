(
    function () {
        var app = angular.module("contactApp");
        app.controller("contactController", contactControllerFunction);

        function contactControllerFunction(contactService) {
            vm = this;
            vm.contacts = [];
            vm.newContact = {};
            vm.editMode = false;

            getUpdateContactList();

            function getUpdateContactList() {
                contactService.getContactList()
                    .then(function successFn(response) {
                        if (response.status) {
                            vm.contacts = response.data;
                        }
                    }, function errorFn(response) {
                        vm.contacts = [];
                    });
            }

            this.addContact = function (newContact) {
                if (newContact) {
                    contactService.addContact(newContact)
                        .then(function successFn(response) {
                            if (response.status) {
                                //alert("Contact data added successfully.");
                                vm.newContact = {};
                                vm.contacts.push(newContact);
                                getUpdateContactList();
                            }
                        }, function errorFn() {
                            //TODO - create error service
                            //alert("error in adding contact data");
                            vm.newContact = {};
                        })
                }
            }

            this.updateContact = function (id, newContact) {
                if (newContact) {
                    contactService.updateContact(id, newContact)
                        .then(function successFn(response) {
                            if (response.status) {
                                //alert("Contact data updated successfully.");
                                vm.newContact = {};
                                vm.editMode = false;
                                getUpdateContactList();
                            }
                        }, function errorFn() {
                            //TODO - create error service
                            //alert("error in updating contact data");
                            vm.newContact = {};
                        })
                }
            }

            this.deleteContact = function (id) {
                contactService.deleteContact(id)
                    .then(function successFn(response) {
                        if (response.status) {
                            //alert("Delete Contact data successful.");
                            getUpdateContactList();
                        }
                    }, function errorFn() {
                        //TODO - create error service
                        //alert("error in deleting contact data");
                    })
            }

            function getContact(id) {

                for (i in vm.contacts) {
                    if (vm.contacts[i].Id == id) {
                        return vm.contacts[i];
                    }
                }

                //angular.forEach(vm.contacts, function (value) {
                //    if (id === contact.id) {
                //        return contact;
                //    }
                //});
            }

            this.editContact = function (id) {
                vm.editMode = true;
                vm.newContact = angular.copy(getContact(id));
            }
        }
    }
)();