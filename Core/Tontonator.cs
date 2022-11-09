using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tontonator.Core.Data;
using Tontonator.Core.Services;
using Tontonator.Models;

namespace Tontonator.Core
{
	public class Tontonator
	{
		public List<Question> questions;
		private double _average;
		private List<Character> _possibleCharacters;
		private List<Character> _nextPossibleCharacters;

        private readonly static Tontonator _instance = new Tontonator();

        private Tontonator()
		{
			questions = DataManager.GetBasicQuestions();
			_possibleCharacters = new List<Character>();
			_nextPossibleCharacters = new List<Character>();
			_average = 0d;
		}

		/// <summary>
        /// This property is used to access to Tontonator instance since it is a Singleton.
        /// </summary>

		public static Tontonator Instance
        {
			get
            {
				return _instance;
            }
        }

		public void Init()
		{
            for (int i = 0; i < questions.Count; i++) questions[i] = States.ShowQuestion(questions[i], i++);

			Console.WriteLine(_average);
		}

		public void ThinkOnCharacter(Question currentQuestion)
		{
			UpdateAvg();

			foreach (var question in questions)
			{
				
				Object.Equals("","");
			}
		}

		private void UpdateAvg()
		{
			double aux = 0d;
			foreach (var question in questions)
				aux += question.QuestionRate;

			aux = aux / questions.Count;
			_average = aux;
		}
	}
}
