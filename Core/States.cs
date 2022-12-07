using System;
using System.Diagnostics.Metrics;
using Tontonator.Core.Helpers;
using Tontonator.Models;
using Tontonator.Models.Enums;

namespace Tontonator.Core
{
    public class States
    {

        /// <summary>
        /// This method shows the main menu. Should be called on App.Init() only.
        /// </summary>
        public static void ShowMainMenu()
        {
            var opt = 0;

            while (opt != 2)
            {
                if (Tontonator.Instance.IsActive)
                {
                    Console.WriteLine("===== Bienvenido a tontonator =====");
                    Console.WriteLine("Seleccione una opción");
                    Console.WriteLine("1. Jugar");
                    Console.WriteLine("2. Salir");

                    string? aux = Console.ReadLine();

                    if (!string.IsNullOrEmpty(aux))
                    {
                        if (aux.Length == 1)
                        {
                            if (char.IsDigit(aux[0]))
                            {
                                opt = int.Parse(aux);

                                Console.Clear();

                                switch (opt)
                                {
                                    case 1:
                                        Tontonator.Instance.Init();
                                        break;
                                    case 2:
                                        Console.WriteLine("Saliendo...");
                                        break;
                                    default:
                                        MessageHelper.WriteError("ERROR: Ingrese un valor valido");
                                        break;
                                }
                            }
                            else
                            {
                                Console.Clear();
                                MessageHelper.WriteError("ERROR: Ingrese un valor númerico");
                            }

                        }
                        else
                        {
                            Console.Clear();
                            MessageHelper.WriteError("ERROR: Ingrese un valor valido");
                        }
                    }
                    else
                    {
                        Console.Clear();
                        MessageHelper.WriteError("ERROR: El campo no puede estar vacio.");
                    }
                }
            }
        }

        /// <summary>
        /// This state shows the question and its options. It also waits for user's response and evaluate the question.
        /// </summary>
        /// <param name="question">The question to be shown.</param>
        /// <param name="index">This index is only for message purposes.</param>
        /// <returns>Returns a question with all its properties filled.</returns>
        public static Question ShowQuestion(Question question, int index)
        {
            if (Tontonator.Instance.IsActive)
            {
                if (IsQuestionReady(question))
                {
                    Tontonator.Instance.IncreaseCurrentIndex();
                    while (!question.IsCorrect)
                    {
                        Console.Clear();
                        Console.WriteLine(index++ + ". " + question.QuestionName);
                        question.ShowOptions();
                        var opt = Console.ReadLine();
                        question.EvaluateOption(opt);
                        Tontonator.Instance.ThinkOnCharacter(question);
                    }
                }
            }

            return question;
        }

        /// <summary>
        /// This shows the character, must pass a character as parameter.
        /// </summary>
        /// <param name="character">The character to display.</param>
        public static void ShowCharacter(Character character)
        {
            Console.WriteLine("Su personaje es: " + character.CharacterName);
            Console.WriteLine("1. Si");
            Console.WriteLine("2. No");

            var opt = Console.ReadLine();

            if (!string.IsNullOrEmpty(opt))
            {
                if (char.IsDigit(opt[0]))
                {
                    if (int.Parse(opt) == 1)
                    {
                        Tontonator.Instance.Dispose();
                        MessageHelper.WriteSuccess("He adivinado su personaje. Presione cualquier tecla para salir.");
                        Console.ReadKey();
                        App.Exit();
                    }
                    else if (int.Parse(opt) == 2)
                    {
                        if (Tontonator.Instance.CanRerol)
                        {
                            // Here does something
                        }
                        else
                        {
                            CreateNewCharacterMenu(Tontonator.Instance.QuestionsRequired);
                        }
                    }
                }
            }
        }

        public static void CreateNewCharacterMenu(bool questionsRequired)
        {
            Console.WriteLine("No pude adivinar su personaje, ¿Desea añadirlo?");
            Console.WriteLine("1. Si");
            Console.WriteLine("2. No");
            var opt = Console.ReadLine();
            if (!string.IsNullOrEmpty(opt))
            {
                if (char.IsDigit(opt[0]))
                {
                    if (int.Parse(opt) == 1)
                    {
                        if (questionsRequired)
                        {
                            
                        }
                        else
                        {

                        }
                    }
                    else if (int.Parse(opt) == 2)
                    {
                        Tontonator.Instance.Dispose();
                        Console.Clear();
                        MessageHelper.WriteSuccess("Presiona cualquier tecla para terminar.");
                        Console.ReadKey();
                        App.Exit();
                    }
                }
            }
        }

        private Question FormQuestion()
        {
            var question = new Question();

            Console.WriteLine("Ingrese una pregunta");

            var questionTxt = Console.ReadLine();

            if (!string.IsNullOrEmpty(questionTxt))
            {
                if (questionTxt.StartsWith('¿') && questionTxt.EndsWith('?'))
                {
                    question = new Question(questionTxt, QuestionCategory.Character, Status.Disabled);
                    
                }
                else
                {
                    MessageHelper.WriteError("Intentalo de nuevo.");
                }
            }

            return question;
        }

        /// <summary>
        /// This method checks if  the question is ready to be showed. This means when doing a call to the database should not return a null object.
        /// So this method should be used first before checking the question.
        /// </summary>
        /// <param name="question">The question to be checked.</param>
        /// <returns>Returns either true if the question is not null or false if it is null.</returns>
        private static bool IsQuestionReady(Question question) => question != null ? true : false;   
    }
}