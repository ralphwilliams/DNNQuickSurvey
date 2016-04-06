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

	public class AnswerController : DnnApiController
	{
		private readonly IAnswerRepository _repository;

		public AnswerController(IAnswerRepository repository)
		{
			Requires.NotNull(repository);

			this._repository = repository;
		}

		public AnswerController() : this(AnswerRepository.Instance) { }

		[HttpPost]
		[ValidateAntiForgeryToken]
		public HttpResponseMessage Delete(int answerId)
		{
			var answer = _repository.GetAnswer(answerId, ActiveModule.ModuleID);

			_repository.DeleteAnswer(answer);

			return Request.CreateResponse(System.Net.HttpStatusCode.NoContent);
		}

		public HttpResponseMessage Get(int answerId)
		{
			var answer = new AnswerViewModel(_repository.GetAnswer(answerId, ActiveModule.ModuleID));

			return Request.CreateResponse(answer);
		}

		public HttpResponseMessage GetList()
		{
			List<AnswerViewModel> answers;

			if (Globals.IsEditMode())
			{
				answers = _repository.GetAnswers(ActiveModule.ModuleID)
					   .Select(answer => new AnswerViewModel(answer, GetEditUrl(answer.AnswerId)))
					   .ToList();
			}
			else
			{
				answers = _repository.GetAnswers(ActiveModule.ModuleID)
					   .Select(answer => new AnswerViewModel(answer, ""))
					   .ToList();
			}

			return Request.CreateResponse(answers);
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
		public HttpResponseMessage Upsert(AnswerViewModel answer)
		{
			if (answer.Id > 0)
			{
				var t = Update(answer);
				return Request.CreateResponse(System.Net.HttpStatusCode.NoContent);
			}
			else
			{
				var t = Create(answer);
				return Request.CreateResponse(t.AnswerId);
			}

		}

		private Answer Create(AnswerViewModel answer)
		{
			Answer t = new Answer
			{
				AnswerValue = answer.Value,
				QuestionId = answer.QuestionId,
				ModuleId = ActiveModule.ModuleID,
				CreatedByUserId = UserInfo.UserID,
				LastModifiedByUserId = UserInfo.UserID,
				CreatedOnDate = DateTime.UtcNow,
				LastModifiedOnDate = DateTime.UtcNow
			};
			_repository.AddAnswer(t);

			return t;
		}

		private Answer Update(AnswerViewModel answer)
		{

			var t = _repository.GetAnswer(answer.Id, ActiveModule.ModuleID);
			if (t != null)
			{
				t.AnswerValue = answer.Value;
				t.QuestionId = answer.QuestionId;
				t.AssignedUserId = answer.AssignedUser;
				t.LastModifiedByUserId = UserInfo.UserID;
				t.LastModifiedOnDate = DateTime.UtcNow;
			}
			_repository.UpdateAnswer(t);

			return t;
		}
	}
}
