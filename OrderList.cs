using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COMPortTerminal
{
    class OrderList : List <Order>
    {
        public String[] toStringList()
        {
            String[] s = new String[this.Count];
            int i = Count;
            foreach (Order n in this)
            {
                i = i - 1;
                s[i] = n.ToString();
                
            }
            return s;
        }
        public int getTotalQuantity()
        {
            int quantity = 0;
            foreach (Order n in this)
            {
                quantity = quantity + n.getQuantity();
            }
            return quantity;
        }
        public double getTotalPrice()
        {
            double price = 0;
            foreach (Order n in this)
            {
                price = price + n.getPrice();
            }
            return price;
        }
    }
}
