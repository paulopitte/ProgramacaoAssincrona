using System;
using System.Threading.Tasks;

namespace ValueTask
{
    internal sealed class Program
    {
        /*
         * A Partir do C# 7 temos os novos tipos estendidos         * 
         * ValueTask - Fornece um resultado esperado em uma operação async
         * ValueTask<T> - Fornece um tipo de valor que encapsula T como Task<T>
         * 
         * Motivação:?
         * Task/Task<T> - sao tipos que referência que são ALOCADOS NA "HEAP" e por conseguencia usa o GC.
         * Isso Impacta no consumo de memória e desencpenho se for muito usado.
         * 
         * ValueTask/ValueTask<T> - é um tipo de valor STRUCT que fica na STACK e não usa o GC, isso evita alocações desnecessarias e leva ganho no desenpenho.
         * 
         * 
         * Quando usar ?
         *  1 - o resultado da operação executada pelo seu metodo assuncrono já estiver disponivel no momento da espera.
         *  2 - Para muitos cenários assíncronos em que o armazenamento em buffer está envolvido.
         *  3 - O resultado da operação for concluída de forma síncrona.
         * */
        static async Task Main(string[] args)
        {
            Console.WriteLine("ValueTask!");

            var result1 = await GetHtml().ConfigureAwait(true);
            Console.WriteLine($"Valor retornado do Get {result1}");
            Console.ReadKey();

            int soma = await CalcularSoma();
            Console.WriteLine(soma);
         

        }

        private async static ValueTask<string> GetHtml()
        {
            Console.WriteLine("Aguarde em execução.....");
            await Task.Delay(2000);
            Console.WriteLine("Execução concluida.....");
            return "<html></html>";
        }


        // Solução é retornar um ValueTask ao invés de retornar um Task<int>
        // Se a e b forem iguais a 0, queremos retornar imediatamente o evitando a execução da solução usando Task.
        // ou seja caso tenhamos várias chamadas para esse metodo todas as com retorn diferente de 0 serãm alocada na heap para execução da Task efetivamente.
        private static async ValueTask<int> CalcularSoma(int a = 0, int b = 0) =>
            a == 0 && b == 0 ? 0 : await Task.Run(() => a + b);

    }
}
