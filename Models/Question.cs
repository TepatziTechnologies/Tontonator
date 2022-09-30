using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tontonator.Models
{
	internal class Question
	{
		public string QuestionName { get; set; }
		public string QuestionCategory { get; set; }
		public string[] QuestionOptions { get; set; }

		public Question(string questionName, string questionCategory, string[] questionOptions)
		{
			this.QuestionName = questionName;
			this.QuestionCategory = questionCategory;
			this.QuestionOptions = questionOptions;
		}
	}
}
