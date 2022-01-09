using System;
using System.Threading;

namespace SemaforoThread
{
    internal class Program
    {

        /*
         * Controlando Threads com semáforos,
         *  Para Limitar o número de thread que podem ter acesso a um recurso compartilhado de forma simultanea.
         * 
         * É usada quando temos um numero limitado de recursos e queremos limitar o numero de thread que podem usar o recurso de forma simultanea com segurança.
         * 
         */

        //InitialCount define um numero de entrada para requisições(Request) para o semaforo que podem ser processadas simultaneamente.
        //maximumCount define um numero máximo de entrada(Request) para o semaforo que podem ser processadas simultaneamente.
        // cria um semaforo que podem atender  até 5 requests simultaneamente
        //  contagem inicia é 3 e assim estamos reservando 3 entradas para outras threads, e 2 entradas para as threads atual
        public static Semaphore treadPoll = new(3, 5);
        static void Main(string[] args)
        {

            Console.WriteLine("Demo Controlando Threads com semáforos.");

            for (int i = 0; i < 10; i++)
            {
                Thread threadObj = new(() => Processaamento());
                threadObj.Name = "Thread : " + i;
                threadObj.Start();
            }
            Console.ReadKey();
        }





        private static void Processaamento()
        {

            // WaitOne() - permite a entrada das thread no semáforo, ou seja na verdade ela inclui a thread
            // esse medoto indica ao semaforo que existe uma thread aguardando a execução do processamento critico e incremente a contagem dentro do semaforo.
            treadPoll.WaitOne();


            Console.WriteLine($"Thread {Thread.CurrentThread.Name} processando algo crítico....");

            Thread.Sleep(3000);

            // esse cara, deve ser chamado  sempre que uma thread for concluida  ou quando desejara liberar a thread para que outra entreem execução.
            // esse Release decrementa a contagem no semaforo.
            treadPoll.Release();

            Console.WriteLine($"Thread {Thread.CurrentThread.Name} foi liberada....");

        }
    }
}
