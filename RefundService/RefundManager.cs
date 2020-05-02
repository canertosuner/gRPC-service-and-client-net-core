using System.Collections.Generic;

namespace RefundService
{
    public class RefundManager : IRefundManager
    {
        private IList<string> NotifyList = new List<string>();
        public void Add(string orderId)
        {
            NotifyList.Add(orderId);
        }

        public IList<string> GetAll()
        {
            return NotifyList;
        }

        public void Rempve(string orderId)
        {
            NotifyList.Remove(orderId);
        }
    }
}
