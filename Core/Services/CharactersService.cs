using System;
using Google.Cloud.Firestore;
using Tontonator.Core.Data.BaseRepository;
using Tontonator.Models;
using static Grpc.Core.Metadata;

namespace Tontonator.Core.Services
{
    public class CharactersService : EntityBaseRepository<Character>
    {
        public CharactersService() : base("characters")
        {
        }

        public virtual Character AddCharacter(Character entity)
        {
            DocumentReference document = _firestoreDb.Collection(this.collection).Document();
            entity.Id = document.Id;
            var result = document.SetAsync(entity.ToDictionary()).GetAwaiter().GetResult();

            var questionsCollection = document.Collection("questions");

            foreach (var question in entity.Questions)
            {
                questionsCollection.Document(question.Id).SetAsync(question.ToDictionaryComplete()).GetAwaiter().GetResult();
            }

            return new Character();
        }
    }
}

