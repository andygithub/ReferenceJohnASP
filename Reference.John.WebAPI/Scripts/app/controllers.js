referenceModule.controller("FormController", function ($scope, $modal,$log, formRepository, usSpinnerService, settingsRepository, SpinnerName) {
    $scope.ModalOptions = settingsRepository.getModalWindowSettings();
    $scope.initialized = false;

    var modalInstance; 

    formRepository.get().$promise.then(function (response) {
        $scope.forms = response;
        $scope.initialized = true;
        $log.info('success');
    }, function (response) {
        $log.info('error');
        $scope.initialized = true;
    });

    $scope.$watch('initialized', function (newValue, oldValue) {
        if (newValue===false)
        {
            modalInstance = $modal.open($scope.ModalOptions);
            usSpinnerService.spin(SpinnerName);
        }
        else {
            modalInstance.close();
            usSpinnerService.stop(SpinnerName);
        }

    });
});

referenceModule.controller("FormCreateController", function ($scope, $location, $modal, $log, formRepository, usSpinnerService, settingsRepository, SpinnerName) {
    $scope.GenderList = [];
    $scope.RaceList = [];
    $scope.RegionList = [];
    $scope.EthnicityList = [];
    $scope.ModalOptions = settingsRepository.getModalWindowSettings();
    $scope.initialized = false;

    var modalInstance;

    formRepository.optionLists().$promise.then(function (response) {
        $scope.GenderList = response.GenderList;
        $scope.RaceList = response.RaceList;
        $scope.RegionList = response.RegionList;
        $scope.EthnicityList = response.EthnicityList;
        $scope.initialized = true;
        $log.info('success');
    }, function (response) {
        // TODO: handle the error 
        $scope.initialized = true;
        $log.info('error');
    });

    $scope.save = function (form) {
        $scope.error = false;
        $scope.errors = [];
        formRepository.save(form).$promise.then(
            function () {
                $log.info('success');
                $location.url('/Forms');
                $scope.$apply();
            },
            function (data) {
                debugger;
                $log.info('error');
                $scope.error = true;
                $scope.errors = parseErrors(data.data);
            });
    };

    $scope.$watch('initialized', function (newValue, oldValue) {
        if (newValue === false) {
            modalInstance = $modal.open($scope.ModalOptions);
            usSpinnerService.spin(SpinnerName);
        }
        else {
            modalInstance.close();
            usSpinnerService.stop(SpinnerName);
        }

    });
});

referenceModule.controller("FormEditController", function ($resource, $scope, $location, $modal, $log, formRepository, usSpinnerService, settingsRepository, SpinnerName) {
    $scope.GenderList = [];
    $scope.RaceList = [];
    $scope.RegionList = [];
    $scope.EthnicityList = [];
    $scope.ModalOptions = settingsRepository.getModalWindowSettings();
    $scope.initialized = false;

    var modalInstance;

    var _allitems = $location.search();
    $scope.Token =  $location.search().ClientToken;
    if ($location.search().ClientToken == null) { $log.error('Expected querystring parameter ClientToken not found.'); }
    //$log.info(_token);
    //$log.info(_allitems);

    formRepository.optionLists().$promise.then(function (response) {
        $scope.GenderList = response.GenderList;
        $scope.RaceList = response.RaceList;
        $scope.RegionList = response.RegionList;
        $scope.EthnicityList = response.EthnicityList;
        $scope.initialized = true;
        $log.info('success option list');
    }, function (response) {
        // TODO: handle the error 
        $scope.initialized = true;
        $log.error('error option list');
    });

    formRepository.getItem().get(_allitems).$promise.then(function (response) {
        $scope.zform = response;
        $log.info('success single record');
    }, function (response) {
        // TODO: handle the error 
        $log.info('error single record');
    });

    $scope.save = function (ClientToken, form) {
        $scope.error = false;
        $scope.errors = [];
        formRepository.update().update(_allitems,form).$promise.then(
            function () {
                $log.info('success update');
                $location.url('/Forms');
                //$scope.$apply();
            },
            function (data) {
                $log.error('error update');
                $scope.error = true;
                $scope.errors = parseErrors(data.data);
            });
    };

    $scope.$watch('initialized', function (newValue, oldValue) {
        if (newValue === false) {
            modalInstance = $modal.open($scope.ModalOptions);
            usSpinnerService.spin(SpinnerName);
        }
        else {
            modalInstance.close();
            usSpinnerService.stop(SpinnerName);
        }

    });

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

function ModalInstanceCtrl($scope, $modalInstance) {

    //$scope.items = items;
    //$scope.selected = {
    //    item: $scope.items[0]
    //};

    //$scope.ok = function () {
    //    $modalInstance.close($scope.selected.item);
    //};

    //$scope.cancel = function () {
    //    $modalInstance.dismiss('cancel');
    //};
};

