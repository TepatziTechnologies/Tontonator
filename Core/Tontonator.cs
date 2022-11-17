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

		public const bool DATABASE_OFF = true;

        private readonly CharactersService _charactersService;
		private readonly QuestionsService _questionsService;

		private List<Character> _possibleCharacters;
		private List<Character> _nextPossibleCharacters;

        public List<Question> questions;
        private List<Question> checkedQuestions;
		private List<Question> liveQuestions;
		private List<Question> positiveQuestions;
        private List<Question> negativeQuestions;
        private List<Question> alreadyAskedQuestions;

        private readonly static Tontonator _instance = new Tontonator();

        private Tontonator()
		{
			questions = DataManager.GetBasicQuestions();
			checkedQuestions = new List<Question>();
			positiveQuestions = new List<Question>();
			_questionsService = new QuestionsService();
			liveQuestions = new List<Question>();
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
			// Temporary disabled.
			// UpdateAvg();

			// We add the current questions, to the live sesion history.
			liveQuestions.Add(currentQuestion);

			// Then we check if the question has been
			if (!checkedQuestions.Exists(n => n.Id == currentQuestion.Id)) this.checkedQuestions.Add(currentQuestion);

			_nextPossibleCharacters = _charactersService.ReadByQuestion(currentQuestion);

			if (currentQuestion.QuestionOption == QuestionOption.Si)
			{
				positiveQuestions.Add(currentQuestion);
			}
			else if (currentQuestion.QuestionOption == QuestionOption.No)
            {
				negativeQuestions.Add(currentQuestion);
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

		private void MoveQuestionAhead()
        {

        }

		private void RemoveAlreadyAskedQuestions()
        {
			foreach (var question in alreadyAskedQuestions) if (questions.Exists(q => q.Id == question.Id)) questions.RemoveAll(qq => qq.Id == question.Id);
        }

		private void CheckDuplicatedQuestions()
        {
            foreach (var question in questions) if (questions.Exists(q => q.Id == question.Id)) questions.RemoveAll(q => q.Id == question.Id);
        }
	}
}