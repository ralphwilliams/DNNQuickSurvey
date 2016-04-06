angular
	.module('surveyControllers', [])
	.controller('surveyFormCtrl', ['$scope', '$http', 'questionsFactory', 'answersFactory', '$location',
	function ($scope, $http, questionsFactory, answersFactory, $location, $sce) {

		questionsFactory.callQuestionsData()
			.then(function (data) {
				$scope.questions = angular.fromJson(data);
			}, function (data) {
				console.log(data);
			});

		 answersFactory.callAnswersData()
		 	.then(function (data) {
		 		$scope.answers = angular.fromJson(data);
		 	}, function (data) {
		 		console.log(data);
		 	});

		// $scope.answers = 'Survey Questions go here.';

	}]);