using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tontonator.Core.Data;
using Tontonator.Core.Services;
using Tontonator.Models;
using Tontonator.Models.Enums;

namespace Tontonator.Core
{
	public class Tontonator
	{
        private double _average;

		private const bool DATABASE = true;

        private readonly CharactersService _charactersService;
		private readonly QuestionsService _questionsService;

		private List<Character> _possibleCharacters;
		private List<Character> _nextPossibleCharacters;

        public List<Question> questions;
        private List<Question> _checkedQuestions;
		private List<Question> _liveQuestions;
		private List<Question> _toCharacter;
		private List<Question> alreadyAskedQuestions;

        private readonly static Tontonator _instance = new Tontonator();

        private Tontonator()
		{
			questions = DataManager.GetBasicQuestions();
			_checkedQuestions = new List<Question>();
			_toCharacter = new List<Question>();
			_questionsService = new QuestionsService();
			_liveQuestions = new List<Question>();
			_charactersService = new CharactersService();
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
            for (int i = 0; i < questions.Count; i++) questions[i] = States.ShowQuestion(questions[i], i);

			Console.WriteLine(_average);
		}

		public void ThinkOnCharacter(Question currentQuestion)
		{
			UpdateAvg();
			_liveQuestions.Add(currentQuestion);
			if (!_checkedQuestions.Exists(n => n.Id == currentQuestion.Id)) this._checkedQuestions.Add(currentQuestion);

            _nextPossibleCharacters = _charactersService.ReadByQuestions(_liveQuestions);

			if (currentQuestion.QuestionOption == QuestionOption.Si || currentQuestion.QuestionOption == QuestionOption.Probablemente) _toCharacter.Add(currentQuestion);

			//if ()
		}

		private void UpdateAvg()
		{
			double aux = 0d;
			foreach (var question in questions)
				aux += question.QuestionRate;

			aux = aux / questions.Count;
			_average = aux;
		}

		private void MoveQuestionAhead()
        {

        }

		private void ComparateQuestions()
        {
			foreach (var question in alreadyAskedQuestions)
            {
				if (questions.Exists(q => q.Id == question.Id)) questions.RemoveAll(qq => qq.Id == question.Id);
            }
        }
	}
}