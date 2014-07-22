'use strict';

registrationModule.factory('accountRepository', function($resource) {
    return {
        save: function (student) {
            return $resource('/api/Account').save(student);
        }
    };
});