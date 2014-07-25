var referenceModule = angular.module("AngularJohnReference", ['ngRoute', 'ngResource', 'ui.bootstrap', 'angularSpinner'])
    .config(function ($routeProvider, $locationProvider) {
        $routeProvider.when('/Forms', {
            templateUrl: '/templates/forms.html', controller: 'FormController'
        });
        $routeProvider.when('/EditForm', { templateUrl: '/templates/editform.html', controller: 'FormEditController' });
        $routeProvider.when('/CreateForm', { templateUrl: '/templates/createform.html', controller: 'FormCreateController' });
        $routeProvider.otherwise({ redirectTo: '/angulardefault.html' });
        $locationProvider.html5Mode(true);
    });

referenceModule.constant('SpinnerName', 'spinner-1');
referenceModule.constant('FormEndpoint', '/api/FormSimpleEF');
referenceModule.constant('OptionListEndpoint', '/api/OptionList');


