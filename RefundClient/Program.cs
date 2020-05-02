using System;
using System.Threading.Tasks;
using Grpc.Net.Client;
using RefundService;

namespace RefundClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var refundClient = new RefundService.RefundService.RefundServiceClient(channel);

            Console.WriteLine("gRPC client is up !");

            for (int i = 5; i < 8; i++)
            {
                var orderid = i + "43243546";

                Console.WriteLine("Request started for orderid: " + orderid);
                var reply = await refundClient.RefundAsync(new RefundRequest
                {
                    OrderId = orderid,
                    Amount = 54.98,
                    Customerid = new Random().Next(10, 100).ToString()
                });
                Console.WriteLine(reply.Message);

                await Task.Delay(1000);
            }

            Console.ReadLine();
        }
    }
}
