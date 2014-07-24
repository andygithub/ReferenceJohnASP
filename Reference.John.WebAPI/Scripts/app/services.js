'use strict';

referenceModule.factory('formRepository', ['$resource', 'FormEndpoint', 'OptionListEndpoint', function ($resource, FormEndpoint, OptionListEndpoint) {
    return {
        get: function () {
            return $resource(FormEndpoint).query();
        },
        getItem: function () {
            return $resource(FormEndpoint, { clientToken: '@ClientToken' });
        },
        optionLists: function () {
            return $resource(OptionListEndpoint).get();
        },
        save: function (form) {
            return $resource(FormEndpoint).save(form);
        },
        //look at this overload to support PUT - 
        update: function(ClientToken,Form) {
            return $resource(FormEndpoint, { ClientToken: "@ClientToken" }, { "update": { method: "PUT" } });
        }
    }
}]);

referenceModule.factory('settingsRepository', ['$resource', function ($resource) {
    return {
        getModalWindowSettings: function () {
            return {
                templateUrl: '/templates/modalloading.html',
                controller: "ModalInstanceCtrl",
                backdrop: true,
                keyboard: true,
                backdropClick: true
            };
        }
        
    }
}]);


