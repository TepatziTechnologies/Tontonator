using System;
using Tontonator.Core.Services;
using Tontonator.Models;

namespace Tontonator.Core.Helpers
{
    public class CharactersManager
    {
        public static CharactersService _charactersService = new CharactersService();

        public List<Character> GetCharacters(List<Question> answeredQuestions)
        {
            List<Character> characters = new List<Character>();

            //_charactersService.Read();

            return characters;
        }
    }
}