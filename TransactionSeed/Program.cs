using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Application.TransactionProducer.Seeds;

namespace Application.TransactionProducer
{
    class Program
    {
        static void Main(string[] args)
        {
            var stopwatch = new Stopwatch();
            try
            {
                stopwatch.Start();
                SeedUsers.SeedPlayers().GetAwaiter().GetResult();
                stopwatch.Stop();
                Console.WriteLine($"User seed took {stopwatch.ElapsedMilliseconds}ms");
                stopwatch.Reset();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            try
            {
                stopwatch.Start();
                SeedTransactions.SeedTx().GetAwaiter().GetResult();
                stopwatch.Stop();
                Console.WriteLine($"txSeed took {stopwatch.ElapsedMilliseconds}ms");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }


            Console.WriteLine("Done");
        }
    }
}
