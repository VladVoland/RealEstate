'use strict';

var app = angular.module('myApp', [
    'ngRoute',
    'ngResource'
]);
app.config(function ($routeProvider) {
    $routeProvider.when('/signIn', { templateUrl: 'app/signIn.html', controller: 'signIn' });
    $routeProvider.when('/realEstatesView', { templateUrl: 'app/realEstatesView.html', controller: 'realEstatesView' });
    $routeProvider.when('/register', { templateUrl: 'app/register.html', controller: 'register' });
    $routeProvider.when('/manager', { templateUrl: 'app/managerMenu.html', controller: 'managerMenu' });
    $routeProvider.when('/admin', { templateUrl: 'app/adminMenu.html', controller: 'adminMenu' });
    $routeProvider.when('/realEstateAdd', { templateUrl: 'app/realEstateAdd.html', controller: 'realEstateAdd' });
    $routeProvider.otherwise({ redirectTo: '/signIn' });
});