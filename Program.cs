using Tontonator.Core;
using Tontonator.Core.Data;
using Tontonator.Core.Services;

namespace Tontonator
{
	internal class Program
	{
		static void Main(string[] args)
		{
            // Feed first time database.

            //DataLoader.FeedDatabaseQuestions();
            //DataLoader.FeedDatabaseCharacters();
            App.Init();
		}
	}
}