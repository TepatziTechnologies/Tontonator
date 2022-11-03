using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tontonator.Core.Data.BaseRepository;

namespace Tontonator.Models
{
	internal class Character : IEntityBase
	{
		public string Id { get; set; }
		public string CharacterName { get; set; }
		public string CharacterCategory { get; set; }
		public List<Question> QuestionPairs { get; set; }

		public Character(string characterName, string characterCategory, List<Question> questionPairs)
		{
			CharacterName = characterName;
			CharacterCategory = characterCategory;
			QuestionPairs = questionPairs;
		}

		public Dictionary<object, object> ToDictionary()
		{
			var dictionary = new Dictionary<object, object>();

			dictionary.Add("Id", this.Id);
			dictionary.Add("CharacterName", this.CharacterName);
			dictionary.Add("CharacterCategory", this.CharacterCategory);

			return dictionary;
        }
	}
}