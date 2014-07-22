'use strict';

referenceModule.factory('formRepository', function ($resource) {
    return {
        get: function () {
            return $resource('/api/FormSimpleEF').query();
        },
        optionLists: function () {
            return $resource('/api/OptionList').get();
        },
        save: function (form) {
            return $resource('/api/FormSimpleEF').save(form);
        }
    }
});