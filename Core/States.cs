using System;
using System.Diagnostics.Metrics;
using Tontonator.Core.Helpers;
using Tontonator.Models;

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

        /// <summary>
        /// This state shows the question and its options. It also waits for user's response and evaluate the question.
        /// </summary>
        /// <param name="question">The question to be shown.</param>
        /// <param name="index">This index is only for message purposes.</param>
        /// <returns>Returns a question with all its properties filled.</returns>
        public static Question ShowQuestion(Question question, int index)
        {
            if (IsQuestionReady(question))
            {
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

            return question;
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
                        
                    }
                    else if (int.Parse(opt) == 2)
                    {

                    }
                }
            }

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