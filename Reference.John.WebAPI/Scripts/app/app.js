var referenceModule = angular.module("AngularJohnReference", ['ngRoute', 'ngResource'])
    .config(function($routeProvider, $locationProvider) {
        $routeProvider.when('/Forms', { templateUrl: '/templates/forms.html', controller: 'FormController' });
        $routeProvider.when('/Registration/Instructors', { templateUrl: '/templates/instructors.html', controller: 'InstructorsController' });
        $routeProvider.when('/Registration/CreateAccount', { templateUrl: '/templates/create-account.html', controller: 'AccountController' });
        $routeProvider.otherwise({redirectTo: '/angulardefault.html'}); 
        $locationProvider.html5Mode(true);
    });