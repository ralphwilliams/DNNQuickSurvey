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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotNetNuke.Collections;
using DotNetNuke.Common;
using DotNetNuke.Data;
using DotNetNuke.Framework;

namespace RalphWilliams.Modules.DNNQuickSurvey.Components
{
	public class QuestionRepository : ServiceLocator<IQuestionRepository, QuestionRepository>, IQuestionRepository
	{

		protected override Func<IQuestionRepository> GetFactory()
		{
			return () => new QuestionRepository();
		}

		public int AddQuestion(Question t)
		{
			Requires.NotNull(t);
			Requires.PropertyNotNegative(t, "ModuleId");

			using (IDataContext ctx = DataContext.Instance())
			{
				var rep = ctx.GetRepository<Question>();
				rep.Insert(t);
			}
			return t.QuestionId;
		}

		public void DeleteQuestion(Question t)
		{
			Requires.NotNull(t);
			Requires.PropertyNotNegative(t, "QuestionId");

			using (IDataContext ctx = DataContext.Instance())
			{
				var rep = ctx.GetRepository<Question>();
				rep.Delete(t);
			}
		}

		public void DeleteQuestion(int questionId, int moduleId)
		{
			Requires.NotNegative("questionId", questionId);
			Requires.NotNegative("moduleId", moduleId);

			var t = GetQuestion(questionId, moduleId);
			DeleteQuestion(t);
		}

		public Question GetQuestion(int questionId, int moduleId)
		{
			Requires.NotNegative("questionId", questionId);
			Requires.NotNegative("moduleId", moduleId);

			Question t;
			using (IDataContext ctx = DataContext.Instance())
			{
				var rep = ctx.GetRepository<Question>();
				t = rep.GetById(questionId, moduleId);
			}
			return t;
		}

		public IQueryable<Question> GetQuestions(int moduleId)
		{
			Requires.NotNegative("moduleId", moduleId);

			IQueryable<Question> t = null;

			using (IDataContext ctx = DataContext.Instance())
			{
				var rep = ctx.GetRepository<Question>();

				t = rep.Get(moduleId).AsQueryable();
			}

			return t;
		}

		public IPagedList<Question> GetQuestions(string searchTerm, int moduleId, int pageIndex, int pageSize)
		{
			Requires.NotNegative("moduleId", moduleId);

			var t = GetQuestions(moduleId).Where(c => c.QuestionName.Contains(searchTerm)
												|| c.QuestionType.Contains(searchTerm));


			return new PagedList<Question>(t, pageIndex, pageSize);
		}

		public void UpdateQuestion(Question t)
		{
			Requires.NotNull(t);
			Requires.PropertyNotNegative(t, "QuestionId");

			using (IDataContext ctx = DataContext.Instance())
			{
				var rep = ctx.GetRepository<Question>();
				rep.Update(t);
			}
		}
	}
}