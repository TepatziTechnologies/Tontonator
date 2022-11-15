using System;
using Google.Cloud.Firestore;
using Tontonator.Core.Data.BaseRepository;
using Tontonator.Models;
using static Grpc.Core.Metadata;

namespace Tontonator.Core.Services
{
    public class CharactersService : EntityBaseRepository<Character>
    {
        private QuestionsService _questionsService;

        public CharactersService() : base("characters")
        {
            _questionsService = new QuestionsService();
        }

        public virtual Character AddCharacter(Character entity)
        {
            //
            DocumentReference document = _firestoreDb.Collection(this.collection).Document();
            entity.Id = document.Id;

            var questionsCollection = document.Collection("questions");

            foreach (var question in entity.Questions)
            {
                if (!string.IsNullOrEmpty(question.Id))
                {
                    questionsCollection.Document(question.Id).CreateAsync(question.ToDictionaryComplete()).GetAwaiter().GetResult();
                }
                else
                {
                    var questionFromDb = _questionsService.Read(nameof(Question.QuestionName), question.QuestionName);
                    if (string.IsNullOrEmpty(questionFromDb.Id))
                    {
                        var newQuestionCreated = _questionsService.Add(question);
                        question.Id = newQuestionCreated.Id;
                        //questionsCollection.Document(question.Id).SetAsync(question.ToDictionaryComplete()).GetAwaiter().GetResult();
                    }
                    else
                    {
                        question.Id = questionFromDb.Id;
                        //questionsCollection.Document(question.Id).SetAsync(question.ToDictionaryComplete()).GetAwaiter().GetResult();
                    }
                }
            }

            var result = document.SetAsync(entity.ToDictionary()).GetAwaiter().GetResult();

            return new Character();
        }

        public List<Character> ReadByQuestions(List<Question> questions)
        {

            //
            List<Character> characters = new List<Character>();
            List<string> values = new List<string>();


            var parentCollection = _firestoreDb.Collection(this.collection);

            foreach (var question in questions)
            {
                values.Add(question.QuestionName);
            }

            parentCollection.WhereArrayContainsAny(nameof(Character.Questions), values.ToArray()).GetSnapshotAsync().GetAwaiter().GetResult();

            

            return characters;
        }
    }
}