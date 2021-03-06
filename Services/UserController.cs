﻿using System.Linq;
using System.Net.Http;
using System.Collections.Generic;
using RalphWilliams.Modules.DNNQuickSurvey.Services.ViewModels;
using DotNetNuke.Web.Api;
using DotNetNuke.Security;
using DotNetNuke.Entities.Users;

namespace RalphWilliams.Modules.DNNQuickSurvey.Services
{
	[SupportedModules("DNNQuickSurvey")]
	[DnnModuleAuthorize(AccessLevel = SecurityAccessLevel.View)]
	public class UserController : DnnApiController
	{
		public UserController() { }

		public HttpResponseMessage GetList()
		{

			var userlist = DotNetNuke.Entities.Users.UserController.GetUsers(this.PortalSettings.PortalId);
			var users = userlist.Cast<UserInfo>().ToList()
				   .Select(user => new UserViewModel(user))
				   .ToList();

			return Request.CreateResponse(users);
		}
	}
}
