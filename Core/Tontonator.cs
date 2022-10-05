using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tontonator.Core.Data;
using Tontonator.Models;

namespace Tontonator.Core
{
	public class Tontonator
	{
		private List<Question> questions;

		public Tontonator()
		{
			questions = DataLoader.GetQuestions();
		}

		public void Init()
		{
			foreach (var question in questions)
			{
				while (!question.IsCorrect)
				{
					Console.Clear();
					Console.WriteLine(question.QuestionName);
					question.ShowOptions();
					var opt = Console.ReadLine();
					question.EvaluateOption(opt);
				}
			}
		}
	}
}
