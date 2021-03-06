﻿angular
.module('DNNQuickSurvey')
.factory('questionsFactory', function ($http, $q) {
	var service = {};

	// DNN Services Framework
	var $self = this;
	if (sf) {
		$self.ServiceRoot = sf.getServiceRoot(moduleName); // Gets path to API
		$self.ServicePath = $self.ServiceRoot + 'Question/'; // Adds Controller name
		$self.Headers = {
			"ModuleId": sf.getModuleId(), // Gets ModuleId
			"TabId": sf.getTabId(), // Gets current TabId
			"RequestVerificationToken": sf.getAntiForgeryValue() // Generates AntiForgery Value
		};
	}

	// ADD - UPDATE
	service.setQuestions = function (question) {
		return $http({
			method: 'POST',
			url: $self.ServicePath + 'Upsert?moduleId=' + sf.getModuleId(),
			headers: $self.Headers,
			data: JSON.stringify(question)
		});
	};

	// Delete
	service.deleteQuestion = function (questionId) {
		return $http({
			method: 'POST',
			url: $self.ServicePath + 'Delete?questionId=' + questionId,
			headers: $self.Headers,
			data: JSON.stringify(questionId)
		});
	};
	service.callQuestionsData = function () {
		var deferred = $q.defer();
		$http({
			method: 'GET',
			url: $self.ServicePath + 'GetList?moduleId=' + sf.getModuleId(),
			headers: $self.Headers
		}).success(function (data) {
			deferred.resolve(data);
		}).error(function () {
			console.log('There was an error getting the questions.');
		});
		return deferred.promise;
	}
	return service;
})
.factory('answersFactory', function ($http, $q) {
	var service = {};

	// DNN Services Framework
	var $self = this;
	if (sf) {
		$self.ServiceRoot = sf.getServiceRoot(moduleName); // Gets path to API
		$self.ServicePath = $self.ServiceRoot + 'Answer/'; // Adds Controller name
		$self.Headers = {
			"ModuleId": sf.getModuleId(), // Gets ModuleId
			"TabId": sf.getTabId(), // Gets current TabId
			"RequestVerificationToken": sf.getAntiForgeryValue() // Generates AntiForgery Value
		};
	}

	// ADD - UPDATE
	service.setAnswers = function (answer) {
		return $http({
			method: 'POST',
			url: $self.ServicePath + 'Upsert?moduleId=' + sf.getModuleId(),
			headers: $self.Headers,
			data: JSON.stringify(answer)
		});
	};

	service.callAnswersData = function () {
		var deferred = $q.defer();
		$http({
			method: 'GET',
			url: $self.ServicePath + 'GetList?moduleId=' + sf.getModuleId(),
			headers: $self.Headers
		}).success(function (data) {
			deferred.resolve(data);
		}).error(function () {
			console.log('There was an error getting the answers.');
		});
		return deferred.promise;
	}
	return service;
})
.factory('localizationFactory', function ($http, $q) {
	var service = {};

	var dataUrl = "/DesktopModules/DNNQuickSurvey/API/DNNQuickSurvey/ResxData";


	// DNN Services Framework
	var $self = this;
	if ($.ServicesFramework) {
		$self.ServiceRoot = sf.getServiceRoot(moduleName);
		$self.ServicePath = $self.ServiceRoot + "Event/";
		$self.Headers = {
			"ModuleId": moduleId,
			"TabId": sf.getTabId(),
			"RequestVerificationToken": sf.getAntiForgeryValue()
		};
	}

	// GET 
	service.callResx = function () {
		var deferred = $q.defer();
		$http({
			method: 'GET',
			url: dataUrl,
			headers: $self.Headers
		}).success(function (data) {
			deferred.resolve(data);
		}).error(function () {
			console.log('There was an error getting the categories');
		});
		return deferred.promise;
	}


	return service;
})
;