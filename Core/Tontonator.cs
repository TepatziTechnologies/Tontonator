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
		private readonly CharactersService _charactersService;
		private readonly QuestionsService _questionsService;

		private double _average;

		private int currentIndex = 1;

		public bool DATABASE_OFF;
		public bool CanRerol { get; set; }
        public bool IsActive { get; set; }
		public bool QuestionsRequired { get; set; }

		private bool refillNeeded = true;
		private bool alreadySet = false;

		private Character currentCharacter = new Character();

		private List<Character> possibleCharacters;
		private List<Character> nextPossibleCharacters;
		private List<Character> deletedCharacters;

		public List<Question> questions;
		private List<Question> checkedQuestions;
		private List<Question> liveQuestions;
		private List<Question> positiveQuestions;
		private List<Question> negativeQuestions;
		private List<Question> alreadyAskedQuestions;
		private List<Question> charactersInheritedQuestions;

		private readonly static Tontonator _instance = new Tontonator();

		private Tontonator()
		{
            EnableDatabase();

            // We start the services to connect with the database.
            _charactersService = new CharactersService();
			_questionsService = new QuestionsService();

			questions = DataManager.GetBasicQuestions();

			// We initialize the question lists that we are going to use.
			checkedQuestions = new List<Question>();
			positiveQuestions = new List<Question>();
			negativeQuestions = new List<Question>();
			alreadyAskedQuestions = new List<Question>();
			liveQuestions = new List<Question>();
			charactersInheritedQuestions = new List<Question>();

			possibleCharacters = new List<Character>();
			nextPossibleCharacters = new List<Character>();
			deletedCharacters = new List<Character>();
			
			_average = 0d;
			IsActive = true;
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
			ChangeQuestion();
			//Console.WriteLine(_average);
		}

		/// <summary>
		/// This method prints the info of the given question as parameter and it is the starting point of this Tontonator.
		/// </summary>
		/// <param name="currentQuestion">The question to be shown.</param>
		public void ThinkOnCharacter(Question currentQuestion)
		{
			// Temporary disabled.
			// UpdateAvg();

			if (IsActive)
			{
				// We add the current questions, to the live sesion history.
				liveQuestions.Add(currentQuestion);
				alreadyAskedQuestions.Add(currentQuestion);

				// Then we check if the question has been answered
				if (!checkedQuestions.Exists(n => n.Id == currentQuestion.Id)) this.checkedQuestions.Add(currentQuestion);

				RetrieveMoreCharacters(currentQuestion);
				RemoveAlreadyAskedQuestions();
				CalculatePossibleCharacters();

				if (currentQuestion.QuestionOption == QuestionOption.Si)
				{
					positiveQuestions.Add(currentQuestion);

					if (possibleCharacters.Count == 0) States.CreateNewCharacterMenu(QuestionsRequired);

					CheckCharacter();
					ChangeQuestion();
				}
				else if (currentQuestion.QuestionOption == QuestionOption.No)
				{
					negativeQuestions.Add(currentQuestion);

					if (possibleCharacters.Count == 0) States.CreateNewCharacterMenu(QuestionsRequired);

					CheckCharacter();
					ChangeQuestion();
				}
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

		private void RetrieveMoreCharacters(Question currentQuestion)
		{
			nextPossibleCharacters = _charactersService.ReadByQuestion(currentQuestion);
			SetCurrentCharacter();
		}

		private void FillQuestions()
		{
			/*foreach (var character in possibleCharacters)
			{
				questions.AddRange(character.Questions);
			}*/
			CheckDuplicatedQuestions();
			RemoveAlreadyAskedQuestions();
		}

		private void ChangeQuestion()
		{
			if (IsActive)
			{
				if (charactersInheritedQuestions.Count > 0) States.ShowQuestion(charactersInheritedQuestions[0], currentIndex);
				else if (questions.Count > 0) States.ShowQuestion(questions[0], currentIndex);
				else if (!(questions.Count > 0)) States.CreateNewCharacterMenu(QuestionsRequired);
				//else if (questions.Count == 0) FillQuestions();
			}
        }

		private void CheckCharacter()
		{
			var hits = 0;
			var breaks = 0;
			var remaining = 0;
			var count = 0;

			foreach (var question in liveQuestions)
			{
				if (currentCharacter.Questions.Find(val => val.QuestionName == question.QuestionName) != null) count++;
            }

			remaining = currentCharacter.Questions.Count - count;

			foreach (var question in liveQuestions)
			{
				foreach (var cQuestion in currentCharacter.Questions)
				{
					if (question.QuestionName == cQuestion.QuestionName && question.QuestionOption == cQuestion.QuestionOption) hits++;				
					else if (question.QuestionName == cQuestion.QuestionName && question.QuestionOption != cQuestion.QuestionOption) breaks++;
				}
			}

			if (remaining > 0 && breaks == 0) DisableDatabase();
			else if (DATABASE_OFF && breaks > 0) EnableDatabase();


			if (breaks > 0)
			{
				deletedCharacters.Add(currentCharacter);
				ChangeCharacter();
			}
			if (hits == currentCharacter.Questions.Count)
			{
				States.ShowCharacter(currentCharacter);
			}
			else
			{
				
			}
		}

		private void ChangeCharacter(){
			charactersInheritedQuestions.Clear();
			var max = 0;
			var count = 0;

            foreach (var character in nextPossibleCharacters)
            {
				max = character.Questions.Count;
                foreach (var question in character.Questions)
                {
                    foreach (var questionn in liveQuestions)
                    {

                    }
					//if (question.QuestionOption)
                }
            }
		}

		private void SetCurrentCharacter()
		{
			var hits = 0;

			if (nextPossibleCharacters.Count > 0)
			{
				foreach (var character in nextPossibleCharacters)
				{
					foreach (var question in liveQuestions)
					{
						if (character.Questions.Exists(q => q.QuestionName == question.QuestionName)) hits++;

						if (hits == liveQuestions.Count)
						{
							currentCharacter = character;
							SetCharacterQuestions(currentCharacter);
							DisableDatabase();
							alreadySet = true;
						}
                        else
                        {
                            if(!alreadySet)
                            {
								if (possibleCharacters.Count > 0)
								{
									currentCharacter = possibleCharacters[0];
									DisableDatabase();
									SetCharacterQuestions(currentCharacter);
								}
                            }
                        }
					}
				}
				alreadySet = false;
			}
		}

		private void CalculatePossibleCharacters()
        {
			var characters = new List<Character>();

            foreach (var question in liveQuestions)
            {
                characters = nextPossibleCharacters.FindAll(n => n.Questions.FindAll(q => q.Id == question.Id).Count > 0);
            }

			if (characters.Count > 0)
			{
				this.possibleCharacters = characters;
				nextPossibleCharacters.Clear();
			}
        }

		private void RemoveAlreadyAskedQuestions()
		{
			foreach (var question in alreadyAskedQuestions)
			{
				if (questions.Exists(q => q.Id == question.Id)) questions.RemoveAll(qq => qq.Id == question.Id);
				if (charactersInheritedQuestions.Exists(q => q.Id == question.Id)) charactersInheritedQuestions.RemoveAll(qq => qq.Id == question.Id);
			}
        }

		private void CheckDuplicatedQuestions()
        {
            foreach (var question in questions) if (questions.Exists(q => q.Id == question.Id)) questions.RemoveAll(q => q.Id == question.Id);
        }

		private void SetCharacterQuestions(Character character)
		{
			var list = new List<Question>();

            foreach (var question in character.Questions)
            {
				list.Add(new Question(question));
            }

			charactersInheritedQuestions = list;
		}

        public void AddQuestion(Question question) => _questionsService.Add(question);
		public void Dispose() => IsActive = false;
        public void IncreaseCurrentIndex() => currentIndex++;
        public void DisableDatabase() => DATABASE_OFF = true;
        public void EnableDatabase() => DATABASE_OFF = false;
		
    }
}