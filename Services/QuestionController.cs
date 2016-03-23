using System;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using RalphWilliams.Modules.DNNQuickSurvey.Components;
using RalphWilliams.Modules.DNNQuickSurvey.Services.ViewModels;
using DotNetNuke.Common;
using DotNetNuke.Web.Api;
using DotNetNuke.Security;
using System.Threading;
using DotNetNuke.UI.Modules;
using DotNetNuke.Common.Utilities;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace RalphWilliams.Modules.DNNQuickSurvey.Services
{
	[SupportedModules("DNNQuickSurvey")]
	[DnnModuleAuthorize(AccessLevel = SecurityAccessLevel.View)]

	public class QuestionController : DnnApiController
	{
		private readonly IQuestionRepository _repository;

		public QuestionController(IQuestionRepository repository)
		{
			Requires.NotNull(repository);

			this._repository = repository;
		}

		public QuestionController() : this(QuestionRepository.Instance) { }

		public HttpResponseMessage Delete(int questionId)
		{
			var question = _repository.GetQuestion(questionId, ActiveModule.ModuleID);

			_repository.DeleteQuestion(question);

			return Request.CreateResponse(System.Net.HttpStatusCode.NoContent);
		}

		public HttpResponseMessage Get(int questionId)
		{
			var question = new QuestionViewModel(_repository.GetQuestion(questionId, ActiveModule.ModuleID));

			return Request.CreateResponse(question);
		}

		public HttpResponseMessage GetList()
		{
			List<QuestionViewModel> questions;

			if (Globals.IsEditMode())
			{
				questions = _repository.GetQuestions(ActiveModule.ModuleID)
					   .Select(question => new QuestionViewModel(question, GetEditUrl(question.QuestionId)))
					   .ToList();
			}
			else
			{
				questions = _repository.GetQuestions(ActiveModule.ModuleID)
					   .Select(question => new QuestionViewModel(question, ""))
					   .ToList();
			}

			return Request.CreateResponse(questions);
		}

		protected string GetEditUrl(int id)
		{
			string editUrl = Globals.NavigateURL("Edit", string.Format("mid={0}", ActiveModule.ModuleID), string.Format("tid={0}", id));

			if (PortalSettings.EnablePopUps)
			{
				editUrl = UrlUtils.PopUpUrl(editUrl, PortalSettings, false, false, 550, 950);
			}
			return editUrl;
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public HttpResponseMessage Upsert(QuestionViewModel question)
		{
			if (question.Id > 0)
			{
				var t = Update(question);
				return Request.CreateResponse(System.Net.HttpStatusCode.NoContent);
			}
			else
			{
				var t = Create(question);
				return Request.CreateResponse(t.QuestionId);
			}

		}

		private Question Create(QuestionViewModel question)
		{
			Question t = new Question
			{
				QuestionName = question.Name,
				QuestionType = question.Description,
				AssignedUserId = question.AssignedUser,
				ModuleId = ActiveModule.ModuleID,
				CreatedByUserId = UserInfo.UserID,
				LastModifiedByUserId = UserInfo.UserID,
				CreatedOnDate = DateTime.UtcNow,
				LastModifiedOnDate = DateTime.UtcNow
			};
			_repository.AddQuestion(t);

			return t;
		}

		private Question Update(QuestionViewModel question)
		{

			var t = _repository.GetQuestion(question.Id, ActiveModule.ModuleID);
			if (t != null)
			{
				t.QuestionName = question.Name;
				t.QuestionType = question.Description;
				t.AssignedUserId = question.AssignedUser;
				t.LastModifiedByUserId = UserInfo.UserID;
				t.LastModifiedOnDate = DateTime.UtcNow;
			}
			_repository.UpdateQuestion(t);

			return t;
		}
	}
}
