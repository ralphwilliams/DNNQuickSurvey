'use strict';
angular
	.module('DNNQuickSurvey', [
		'ngRoute',
		'surveyControllers',
		'ui.bootstrap'
	])
	.config(['$routeProvider', '$httpProvider',
		function ($routeProvider) {
			console.log('app loaded');
			$routeProvider
			.when('/results', {
				templateUrl: '/DesktopModules/DNNQuickSurvey/app/Views/surveyResultsView.html',
				controller: 'surveyResultsCtrl'
			})
			.when('/edit', {
				templateUrl: '/DesktopModules/DNNQuickSurvey/app/Views/surveyEditView.html',
				controller: 'surveyEditCtrl'
			})
			.otherwise({
				templateUrl: '/DesktopModules/DNNQuickSurvey/app/Views/surveyFormView.html',
				controller: 'surveyFormCtrl'
			});
		}
	]);