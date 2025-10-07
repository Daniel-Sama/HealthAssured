using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckoutKata.Interface
{
    public interface ICheckout
    {
        public void Scan(string item);
        public int GetTotalPrice();

    }
}
