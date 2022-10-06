using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tontonator.Models
{
	internal class Character
	{
		public string CharacterName { get; set; }
		public string CharacterCategory { get; set; }
		public List<Question> QuestionPairs { get; set; }

		public Character(string characterName, string characterCategory, List<Question> questionPairs)
		{
			CharacterName = characterName;
			CharacterCategory = characterCategory;
			QuestionPairs = questionPairs;
		}
	}
}