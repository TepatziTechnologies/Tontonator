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
		private List<Question> _questions;
		private double _average;
		private List<Character> _possibleCharacters;
		private List<Character> _nextPossibleCharacters;

		public Tontonator()
		{
			_questions = DataLoader.GetQuestions();
			_possibleCharacters = new List<Character>();
			_nextPossibleCharacters = new List<Character>();
			_average = 0d;
		}

		public void Init()
		{
			var counter = 0;
			foreach (var question in _questions)
			{
				counter++;

				while (!question.IsCorrect)
				{
					Console.Clear();
					Console.WriteLine(counter + ". " +question.QuestionName);
					question.ShowOptions();
					var opt = Console.ReadLine();
					question.EvaluateOption(opt);
				}
			}

			ThinkOnCharacter();

			Console.WriteLine(_average);
		}

		public void ThinkOnCharacter()
		{
			UpdateAvg();
		}

		private void UpdateAvg()
		{
			double aux = 0d;
			foreach (var question in _questions)
				aux += question.QuestionRate;

			aux = aux / _questions.Count;
			_average = aux;
		}
	}
}
