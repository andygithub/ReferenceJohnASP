registrationModule.factory('instructorRepository', function ($resource) {
    return {
        get: function () {
            return $resource('/api/Instructors').query();
        }
    }
});