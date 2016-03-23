angular
	.module('surveyControllers')
	.controller('surveyResultsCtrl', ['$scope', '$http', '$location',
	function ($scope, $http, $location, $sce) {
		$scope.questions = "Survey Results go here.";
	}]);