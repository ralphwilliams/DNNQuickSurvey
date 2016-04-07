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
	public class AnswerRepository : ServiceLocator<IAnswerRepository, AnswerRepository>, IAnswerRepository
	{

		protected override Func<IAnswerRepository> GetFactory()
		{
			return () => new AnswerRepository();
		}

		public int AddAnswer(Answer t)
		{
			Requires.NotNull(t);
			Requires.PropertyNotNegative(t, "ModuleId");

			using (IDataContext ctx = DataContext.Instance())
			{
				var rep = ctx.GetRepository<Answer>();
				rep.Insert(t);
			}
			return t.AnswerId;
		}

		public void DeleteAnswer(Answer t)
		{
			Requires.NotNull(t);
			Requires.PropertyNotNegative(t, "AnswerId");

			using (IDataContext ctx = DataContext.Instance())
			{
				var rep = ctx.GetRepository<Answer>();
				rep.Delete(t);
			}
		}

		public void DeleteAnswer(int answerId, int moduleId)
		{
			Requires.NotNegative("answerId", answerId);
			Requires.NotNegative("moduleId", moduleId);

			var t = GetAnswer(answerId, moduleId);
			DeleteAnswer(t);
		}

		public Answer GetAnswer(int answerId, int moduleId)
		{
			Requires.NotNegative("answerId", answerId);
			Requires.NotNegative("moduleId", moduleId);

			Answer t;
			using (IDataContext ctx = DataContext.Instance())
			{
				var rep = ctx.GetRepository<Answer>();
				t = rep.GetById(answerId, moduleId);
			}
			return t;
		}

		public IEnumerable<Answer> GetAnswers(int moduleId)
		{
			Requires.NotNegative("moduleId", moduleId);

            IEnumerable<Answer> t = null;

			using (IDataContext ctx = DataContext.Instance())
			{
				var rep = ctx.GetRepository<Answer>();

				t = rep.Get(moduleId);
			}

			return t;
		}

		public IPagedList<Answer> GetAnswers(string searchTerm, int moduleId, int pageIndex, int pageSize)
		{
			Requires.NotNegative("moduleId", moduleId);

			var t = GetAnswers(moduleId).Where(c => c.AnswerValue.ToString().Contains(searchTerm)
												|| c.QuestionId.ToString().Contains(searchTerm));


			return new PagedList<Answer>(t, pageIndex, pageSize);
		}

		public void UpdateAnswer(Answer t)
		{
			Requires.NotNull(t);
			Requires.PropertyNotNegative(t, "AnswerId");

			using (IDataContext ctx = DataContext.Instance())
			{
				var rep = ctx.GetRepository<Answer>();
				rep.Update(t);
			}
		}
	}
}