using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tontonator.Core.Helpers;

namespace Tontonator.Core
{
    internal class App
    {
        public static void Init()
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
									Tontonator tontonator = new Tontonator();
									tontonator.Init();
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
}
