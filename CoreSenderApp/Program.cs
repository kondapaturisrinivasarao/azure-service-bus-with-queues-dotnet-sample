using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;

namespace CoreSenderApp
{
    class Program
    {
        /* 
         * Go to your azure service bus resource , create if does not exists
         * Go To Shared access policies 
         * Go To RootManageSharedAccessKey 
         * Copy Primary/Secondary  Connection String
         */
        const string ServiceBusConnectionString = "<your_connection_string>";
        /*
          * Go to your azure service bus resource , create if does not exists
          * Create Queue , create if does not exists and use the same queue
             */
        const string QueueName = "<your_queue_name>";
        static IQueueClient queueClient;

        public static async Task Main(string[] args)
        {
            const int numberOfMessages = 10;
            queueClient = new QueueClient(ServiceBusConnectionString, QueueName);
            Console.WriteLine("======================================================");
            Console.WriteLine("Press ENTER key to exit after sending all the messages.");
            Console.WriteLine("======================================================");

            await SendMessageAsync(numberOfMessages);

            Console.ReadKey();

            await queueClient.CloseAsync();

        }

        static async Task SendMessageAsync(int numberOfMessagesToSend)
        {
            try 
            { 
                for(var i = 0; i< numberOfMessagesToSend; i++)
                {
                    string messageBody = $"Message{i}";
                    var message = new Message(Encoding.UTF8.GetBytes(messageBody));
                    Console.WriteLine($"Sending message: {messageBody}");

                    await queueClient.SendAsync(message);
                }

            }
            catch (Exception exception) 
            {
                Console.WriteLine($"{DateTime.Now} :: Exception: {exception.Message}");
            }   
        }
    }
}
