angular
	.module('surveyControllers')
	.controller('surveyEditCtrl', ['$scope', '$http', 'questionsFactory', '$location',
	function ($scope, $http, questionsFactory, $location, $sce) {

	questionsFactory.callQuestionsData()
		.then(function (data) {
			$scope.questions = angular.fromJson(data);
		}, function (data) {
			console.log(data);
		});

	$scope.addNewQuestion = function (newQuestionName) {
		$scope.newQuestion = {
			name: newQuestionName
		};
		questionsFactory.setQuestions($scope.newQuestion)
			.success(function () {
				$scope.status = 'Inserted Video! Refreshing video list.';
				$scope.questions.push($scope.newQuestion);
			}).
			error(function (error) {
				$scope.status = 'Unable to insert video: ' + error.message;
			});
	}

		// $scope.questions = "Edit Survey goes here.";


	}]);