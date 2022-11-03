using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tontonator.Models;
using Tontonator.Models.Enums;

namespace Tontonator.Core.Data
{
	internal class DataLoader
	{
		public static List<Character> characters = new List<Character>();

		public static void InitCharacters()
		{
			characters.Add(new Character("El que siempre dice que si", "random", new List<Question>() {
				new Question("¿Su personaje es real?","Basicas", QuestionOption.Si, 1),
				new Question("¿Su personaje es hombre?", "Basicas", QuestionOption.Si, 1),
				new Question("¿Su personaje vive en mexico?", "Basicas", QuestionOption.Si, 1),
				new Question("¿Su personaje habla ingles?", "Basicas", QuestionOption.Si, 1),
				new Question("¿Su personaje tiene 3 ojos?", "Basicas", QuestionOption.Si, 1),
				new Question("¿Su personaje vive en corea?", "Basicas", QuestionOption.Si, 1),
				new Question("¿Su personaje nacio en guatemala?", "Basicas", QuestionOption.Si, 1)
			}));
		}


		public static List<Question> GetQuestions()
		{
			var questions = new List<Question>();

			questions.Add(new Question("¿Su personaje es real?", "Basicas"));
			questions.Add(new Question("¿Su personaje es hombre?", "Basicas"));
			questions.Add(new Question("¿Su personaje es un youtuber famoso?", "Basicas"));
			questions.Add(new Question("¿Su personaje es un cantante?", "Basicas"));

			return questions;
		}

	}
}
