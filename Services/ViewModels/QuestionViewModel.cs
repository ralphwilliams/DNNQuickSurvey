/*
' Copyright (c) 2016 Ralph Williams
'  All rights reserved.
' 
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
' TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
' THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
' CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
' DEALINGS IN THE SOFTWARE.
' 
*/

using RalphWilliams.Modules.DNNQuickSurvey.Components;
using Newtonsoft.Json;

namespace RalphWilliams.Modules.DNNQuickSurvey.Services.ViewModels
{
	[JsonObject(MemberSerialization.OptIn)]
	public class QuestionViewModel
	{
		public QuestionViewModel(Question t)
		{
			Id = t.QuestionId;
			Name = t.QuestionName;
			Description = t.QuestionType;
			AssignedUser = t.AssignedUserId;
		}

		public QuestionViewModel(Question t, string editUrl)
		{
			Id = t.QuestionId;
			Name = t.QuestionName;
			Description = t.QuestionType;
			EditUrl = editUrl;
		}


		public QuestionViewModel() { }

		[JsonProperty("id")]
		public int Id { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("description")]
		public string Description { get; set; }

		[JsonProperty("assignedUser")]
		public int AssignedUser { get; set; }

		[JsonProperty("editUrl")]
		public string EditUrl { get; }
	}
}