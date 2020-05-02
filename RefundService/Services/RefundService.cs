using System;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace RefundService.Services
{
    public class RefundService : global::RefundService.RefundService.RefundServiceBase
    {
        private readonly IRefundManager _refundManager;
        public RefundService(IRefundManager refundManager)
        {
            _refundManager = refundManager;
        }

        public override Task<RefundReply> Refund(RefundRequest request, ServerCallContext context)
        {
            Console.WriteLine("CustomerId: " + request.Customerid + "\tOrderId: " + request.OrderId + "\tAmount:" + request.Amount + " refunded");

            //Your business logic goes here, external call etc.

            _refundManager.Add(request.OrderId);
            return Task.FromResult(new RefundReply
            {
                Message = "Reply from RefundService " + request.OrderId + " Refunded",
            });
        }

        public override async Task NotifyCustomer(NotifyRequest request, IServerStreamWriter<NotifyReply> responseStream, ServerCallContext context)
        {
            while (!context.CancellationToken.IsCancellationRequested)
            {
                var refunds = _refundManager.GetAll().ToList();

                foreach (var item in refunds)
                {
                    var reply = new NotifyReply { OrderId = item };
                    await responseStream.WriteAsync(reply);
                    Console.WriteLine(item + " notify message sent to notifyClient");
                    _refundManager.Rempve(item);
                }
            }
        }
    }
}
