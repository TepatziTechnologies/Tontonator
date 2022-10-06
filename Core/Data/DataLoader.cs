using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tontonator.Models;

namespace Tontonator.Core.Data
{
	internal class DataLoader
	{
		public List<Character> characters = new List<Character>();


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
