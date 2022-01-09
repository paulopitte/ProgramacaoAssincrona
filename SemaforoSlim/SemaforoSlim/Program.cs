using System;
using System.Threading;
using static System.Console;
using static System.Threading.Thread;
namespace SemaforoSlim
{
    internal class Program
    {

        private static SemaphoreSlim semaphoreSlim = new SemaphoreSlim(3);
        static void Main(string[] args)
        {
            WriteLine("Demo Controlando Threads com semáforos SLIM.");

            for (int i = 0; i < 10; i++)
            {
                var name = "Thread : " + i;
                int wait = 2 + 2 * 1;
                Thread threadObj = new(() => Processaamento(name, wait));

                threadObj.Start();
            }
            ReadKey();
        }



        private static void Processaamento(string name, int seconds)
        {

             semaphoreSlim.Wait();


            WriteLine($"Thread {name} processando algo crítico....");   
            Sleep(TimeSpan.FromSeconds(seconds));

            // esse cara, deve ser chamado  sempre que uma thread for concluida  ou quando desejara liberar a thread para que outra entreem execução.
            // esse Release decrementa a contagem no semaforo.
            semaphoreSlim.Release();

            WriteLine($"Thread {name} foi liberada....");

        }


    }
}
