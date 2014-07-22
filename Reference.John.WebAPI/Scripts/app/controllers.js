referenceModule.controller("FormController", function ($scope,formRepository) {
    $scope.forms = formRepository.get();
});