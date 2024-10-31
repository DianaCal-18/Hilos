namespace Threads
{
    class Program
    {
        static bool[] isRunning;
        static Thread[] threads;
        static int[] counters;
        static int contadores = 5;

        static void Main(string[] args)
        {

            isRunning = new bool[contadores];
            counters = new int[contadores];
            threads = new Thread[contadores];

            for (int i = 0; i < contadores; i++)
            {
                int intervalo = i;
                threads[i] = new Thread(() => RunCounter(intervalo, 1000 * (intervalo + 1)));
            }

            Menu();
        }


        static void RunCounter(int index, int intervalo)
        {
            while (isRunning[index])
            {
                counters[index]++;
                Console.WriteLine($"Contador {index + 1}: {counters[index]}");
                Thread.Sleep(intervalo);
            }
        }


        static void IniciarContador(int index)
        {
            if (!isRunning[index])
            {
                isRunning[index] = true;
                threads[index] = new Thread(() => RunCounter(index, 1000 * (index + 1)));
                threads[index].Start();
                Console.WriteLine($"\n***Contador {index + 1} iniciado.***");
            }
            else
            {
                Console.WriteLine($"Contador {index + 1} ya esta en ejecucion.");
            }
        }


        static void PararContador(int index)
        {
            if (isRunning[index])
            {
                isRunning[index] = false;
                threads[index].Join();
                Console.WriteLine($"\n***Contador {index + 1} se ha desactivado.***");
            }
            else
            {
                Console.WriteLine($"Contador {index + 1} ya estaba desactivado.");
            }
        }


        static void MostrarEstado()
        {
            Console.WriteLine("\n***ESTADO DE LOS CONTADORES***");
            for (int i = 0; i < contadores; i++)
            {
                Console.WriteLine($"\nContador {i + 1}: {(isRunning[i] ? "En ejecucion" : "Desactivado")} - Valor: {counters[i]}");
            }
        }


        static void Menu()
        {
            int option;
            do
            {
                Console.WriteLine("\n- - - BIENVENIDO AL MENU - - - ");

                Console.WriteLine("\n1. Iniciar contador");
                Console.WriteLine("2. Detener contador");
                Console.WriteLine("3. Mostrar estado de contadores");
                Console.WriteLine("4. Detener todos los contadores");
                Console.WriteLine("5. Salir");
                Console.Write("\nSeleccione una opcion: ");
                option = int.Parse(Console.ReadLine());

                switch (option)
                {
                    case 1:
                        Console.Write("¿Que contador deseas iniciar 1 al 4?: ");
                        int startIndex = int.Parse(Console.ReadLine()) - 1;
                        if (startIndex >= 0 && startIndex < contadores)
                        {
                            IniciarContador(startIndex);
                        }
                        else
                        {
                            Console.WriteLine("Indice de contador invalido.");
                        }
                        break;
                    case 2:
                        Console.Write("¿Que contador deseas detener 1 al 4?: ");
                        int stopIndex = int.Parse(Console.ReadLine()) - 1;
                        if (stopIndex >= 0 && stopIndex < contadores)
                        {
                            PararContador(stopIndex);
                        }
                        else
                        {
                            Console.WriteLine("Indice de contador  invalido");
                        }
                        break;
                    case 3:
                        MostrarEstado();
                        break;
                    case 4:
                        PararContadores();
                        break;
                    case 5:
                        PararContadores();
                        Console.WriteLine("Saliendo...");
                        Console.WriteLine("Diana Calderon 2022-1921");
                        break;
                    default:
                        Console.WriteLine("Opcion invalidad.");
                        Console.WriteLine("Saliendo...");
                        break;
                }
            } while (option != 4);
        }


        static void PararContadores()
        {
            for (int i = 0; i < contadores; i++)
            {
                isRunning[i] = false;
                if (threads[i].IsAlive)
                {
                    threads[i].Join();
                }
            }
        }
    }
}
