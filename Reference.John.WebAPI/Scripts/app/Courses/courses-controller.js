registrationModule.controller("CoursesController", function ($scope, courseRepository) {
    $scope.courses = courseRepository.get();
});