'use strict';

registrationModule.controller("InstructorsController", function ($scope, instructorRepository) {
    $scope.instructors = instructorRepository.get();
});
