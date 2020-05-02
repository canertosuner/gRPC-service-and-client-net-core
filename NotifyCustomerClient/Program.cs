using System;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Net.Client;
using RefundService;

namespace NotifyCustomerClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var refundClient = new RefundService.RefundService.RefundServiceClient(channel);

            Console.WriteLine("gRPC notifyCustomer client is up !");

            using (var serverStreamingCall = refundClient.NotifyCustomer(new NotifyRequest()))
            {
                while (await serverStreamingCall.ResponseStream.MoveNext())
                {
                    var notifyReply = serverStreamingCall.ResponseStream.Current;
                    if (notifyReply != null)
                    {
                        Console.WriteLine("Dear Customer your order " + notifyReply.OrderId + " is refunded.");
                    }
                }
            }
        }
    }
}
