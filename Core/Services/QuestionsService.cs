using System;
using Tontonator.Core.Data.BaseRepository;
using Tontonator.Models;

namespace Tontonator.Core.Services
{
    public class QuestionsService : BaseRepository<Question>
    {
        public QuestionsService() : base("questions")
        {

        }
    }
}

