﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tontonator.Core.Services;
using Tontonator.Models;
using Tontonator.Models.Enums;

namespace Tontonator.Core.Data
{
	internal class DataManager
	{
		private static QuestionsService _questionsService = new QuestionsService();

		public static List<Question> GetBasicQuestions()
		{
			var questions = new List<Question>();

			_questionsService.Read(nameof(Question.QuestionCategory), nameof(QuestionCategory.Basic));


			return questions;
		}
	}
}
