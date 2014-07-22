'use strict';

referenceModule.factory('formRepository', function ($resource) {
    return {
        get: function () {
            return $resource('/api/FormSimpleEF').query();
        }
    }
});