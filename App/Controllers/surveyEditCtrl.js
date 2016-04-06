angular
	.module('surveyControllers')
	.controller('surveyEditCtrl', ['$scope', '$http', 'questionsFactory', '$location',
	function ($scope, $http, questionsFactory, $location, $sce) {

		// Wrap in function to be able to load 
		// after new question has been asked
		$scope.loadQuestions = function() {
			questionsFactory.callQuestionsData()
				.then(function (data) {
					$scope.questions = angular.fromJson(data);
				}, function (data) {
					console.log(data);
				});
		};
		$scope.loadQuestions();

		$scope.addNewQuestion = function (newQuestion) {
			function addQuestion(newQuestionName) {
				questionsFactory.setQuestions(newQuestionName)
					.success(function() {
						$scope.status = 'Inserted question! Refreshing question list.';
						// Reload questions - pulls list from API to load questions that
						// could be added or removed by other users since page has been loaded
						$scope.loadQuestions();
					}).
					error(function(error) {
						$scope.status = 'Unable to insert question: ' + error.message;
					});
			}

			// Question object constructor
			// Properties are the same as what the QuestionController is expecting
			function Question(name) {
				// Name from input
				this.Name = name;
				// Static for now, can add variations later
				this.Description = 'Text';
			}

			// Create new object that will be passed to API
			var NewQuestion = new Question(newQuestion);
			// Pass new question object to API
			addQuestion(NewQuestion);
			// Clear out new question input
			$scope.newQuestionName = '';
		};

		$scope.removeQuestion = function (questionToDelete) {
			function deleteQuestion(questionId) {
				questionsFactory.deleteQuestion(questionId)
					.success(function () {
						$scope.status = 'Inserted Question! Refreshing question list.';
						// Reload questions - pulls list from API to load questions that
						// could be added or removed by other users since page has been loaded
						$scope.loadQuestions();
					}).
					error(function (error) {
						$scope.status = 'Unable to remove question: ' + error.message;
					});
			}

			// Pass questionId to API
			deleteQuestion(questionToDelete);
		};

	}]);