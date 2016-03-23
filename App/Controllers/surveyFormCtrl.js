angular
	.module('surveyControllers', [])
	.controller('surveyFormCtrl', ['$scope', '$http', 'questionsFactory', '$location',
	function ($scope, $http, questionsFactory, $location, $sce) {

		questionsFactory.callQuestionsData()
			.then(function (data) {
				$scope.questions = angular.fromJson(data);
			}, function (data) {
				console.log(data);
			});

		// $scope.questions = 'Survey Questions go here.';

	}]);