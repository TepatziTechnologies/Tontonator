using System;
using System.Collections;
using Google.Cloud.Firestore;

namespace Tontonator.Core.Data.BaseRepository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class, IEntityBase, new()
    {
        private FirestoreDb _firestoreDb;
        private string collection;
        private string filepath = "tontonatoruaq-firebase-adminsdk-oww74-029b8e3492.json";

        public BaseRepository(string collectionName)
        {
            this.collection = collectionName;
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", filepath);
            _firestoreDb = FirestoreDb.Create("tontonatoruaq");
        }

        public async Task Add(T entity)
        {
            DocumentReference document = _firestoreDb.Collection(this.collection).Document();
            entity.Id = document.Id;
            await document.CreateAsync(entity.ToDictionary());
        }

        public Task Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public Task Read(T entity)
        {
            throw new NotImplementedException();
        }

        public Task Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}

