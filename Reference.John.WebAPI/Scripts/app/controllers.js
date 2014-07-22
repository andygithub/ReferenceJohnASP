referenceModule.controller("FormController", function ($scope, formRepository, usSpinnerService) {
    usSpinnerService.spin('spinner-1');
    formRepository.get().$promise.then(function (response) {
        $scope.forms = response;
        usSpinnerService.stop('spinner-1');
    }, function (response) {
        // TODO: handle the error
        usSpinnerService.stop('spinner-1');
    });
});

referenceModule.controller("FormCreateController", function ($scope, $location, formRepository, usSpinnerService) {
    //$scope.selectedGender = null;
    $scope.GenderList = [];
    $scope.RaceList = [];
    $scope.RegionList = [];
    $scope.EthnicityList = [];

    usSpinnerService.spin('spinner-1');
    formRepository.optionLists().$promise.then(function (response) {
        $scope.GenderList = response.GenderList;
        $scope.RaceList = response.RaceList;
        $scope.RegionList = response.RegionList;
        $scope.EthnicityList = response.EthnicityList;
        usSpinnerService.stop('spinner-1');
    }, function (response) {
        // TODO: handle the error 
        usSpinnerService.stop('spinner-1');
    });

    $scope.save = function (form) {
        $scope.error = false;
        $scope.errors = [];
        formRepository.save(form).$promise.then(
            function () {
                console.log('success');
                $location.url('/Forms');
                $scope.$apply();
            },
            function (data) {
                debugger;
                console.log('error');
                $scope.error = true;
                $scope.errors = parseErrors(data.data);
            });
    };
});

referenceModule.controller("FormEditController", function ($scope, $location, formRepository, usSpinnerService) {
    //$scope.selectedGender = null;
    $scope.GenderList = [];
    $scope.RaceList = [];
    $scope.RegionList = [];
    $scope.EthnicityList = [];

    usSpinnerService.spin('spinner-1');
    formRepository.optionLists().$promise.then(function (response) {
        $scope.GenderList = response.GenderList;
        $scope.RaceList = response.RaceList;
        $scope.RegionList = response.RegionList;
        $scope.EthnicityList = response.EthnicityList;
        usSpinnerService.stop('spinner-1');
    }, function (response) {
        // TODO: handle the error 
        usSpinnerService.stop('spinner-1');
    });

    $scope.save = function (form) {
        $scope.error = false;
        $scope.errors = [];
        formRepository.save(form).$promise.then(
            function () {
                console.log('success');
                $location.url('/Forms');
                $scope.$apply();
            },
            function (data) {
                debugger;
                console.log('error');
                $scope.error = true;
                $scope.errors = parseErrors(data.data);
            });
    };
});

//separate method for parsing errors into a single flat array
function parseErrors(response) {
    var errors = [];
    for (var key in response.ModelState) {
        for (var i = 0; i < response.ModelState[key].length; i++) {
            errors.push(response.ModelState[key][i]);
        }
    }
    return errors;
}