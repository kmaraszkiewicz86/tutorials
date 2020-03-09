using System;
using System.Threading.Tasks;
using QueueApp.Core;

namespace QueueApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                var cloudQueueService = new CloudQueueService();

                await cloudQueueService.CreateNewQueueIfNotExistsAsync();
                
                if (args.Length > 0)
                {
                    var value = string.Join(" ", args);

                    await cloudQueueService.SendArticleAsync(value);

                    Console.WriteLine($"Sent {value}");

                }
                else
                {
                    var result = await cloudQueueService.ReceiveArticleAsync();
                    Console.WriteLine($"Result: {result}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {

            }
        }
    }
}
