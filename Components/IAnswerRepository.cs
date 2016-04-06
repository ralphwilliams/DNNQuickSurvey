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

using System.Linq;
using DotNetNuke.Collections;

namespace RalphWilliams.Modules.DNNQuickSurvey.Components
{
	public interface IAnswerRepository
	{

		int AddAnswer(Answer t);

		void DeleteAnswer(int answerId, int moduleId);

		void DeleteAnswer(Answer t);

		Answer GetAnswer(int answerId, int moduleId);

		IQueryable<Answer> GetAnswers(int moduleId);

		IPagedList<Answer> GetAnswers(string searchTerm, int moduleId, int pageIndex, int pageSize);

		void UpdateAnswer(Answer t);
	}
}