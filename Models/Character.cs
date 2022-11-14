using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tontonator.Core.Data.BaseRepository;

namespace Tontonator.Models
{
	public class Character : IEntityBase
	{
		public string Id { get; set; }
		public string CharacterName { get; set; }
		public string CharacterCategory { get; set; }
		public List<Question> Questions { get; set; }

        public Character()
        {

        }

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
			dictionary.Add("Questions", this.QuestionsToArray());

			return dictionary;
        }

		/// <summary>
		/// This method should be used only when storing data into the database,
		/// </summary>
		/// <returns></returns>
		private Question[] QuestionsToArray()
		{
			Question[] questions = new Question[this.Questions.Count()];

			var counter = 0;
			
			foreach (var question in this.Questions)
            {
				questions[counter] = question;
				counter++;
            }

			return questions;
        }
	}
}