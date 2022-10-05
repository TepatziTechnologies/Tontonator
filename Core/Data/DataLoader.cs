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
		public static List<Question> GetQuestions()
		{
			var questions = new List<Question>();

			questions.Add(new Question("¿Su personaje es real?", "Basicas"));

			return questions;
		}
	}
}
