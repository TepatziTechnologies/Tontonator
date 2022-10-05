using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tontonator.Models;
using Tontonator.Models.Enums;

namespace Tontonator.Core.Helpers
{
	internal class QuestionManager
	{
		public static double EvaluateQuestion(Question question)
		{
			if (question.QuestionOption == QuestionOption.Si) return 1;
			else if (question.QuestionOption == QuestionOption.No) return 0;
			else if (question.QuestionOption == QuestionOption.Probablemente) return 0.75;
			else if (question.QuestionOption == QuestionOption.ProbablementeNo) return 0.25;
			else if (question.QuestionOption == QuestionOption.Nose) return 0.50;

			return 0;
		}
	}
}
