using System.Collections.Generic;

namespace RefundService
{
    public interface IRefundManager
    {
        void Add(string orderId);
        IList<string> GetAll();
        void Rempve(string orderId);
    }
}