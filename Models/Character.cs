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
		public List<Question> Questions { get; set; }

		public Character(string characterName, string characterCategory, List<Question> questions)
		{
			CharacterName = characterName;
			CharacterCategory = characterCategory;
			Questions = questions;
		}

		public Dictionary<string, object> ToDictionary()
		{
			var dictionary = new Dictionary<string, object>();

			dictionary.Add("Id", this.Id);
			dictionary.Add("CharacterName", this.CharacterName);
			dictionary.Add("CharacterCategory", this.CharacterCategory);
			dictionary.Add("Questions", this.Questions);

			return dictionary;
        }
	}
}