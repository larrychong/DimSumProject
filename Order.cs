using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COMPortTerminal
{
    class Order
    {
        private int orderID;
        public enum eSize { eNull, eSmall, eMedium, eLarge };
        private eSize ds_size;
        private double ds_price;
        private int quantity;
        private string sizeString;

        //constructor
       //public Order() : this(eSize.eNull) { }
       // public Order(eSize size) : this(size, 1,) { }
        public Order(eSize size, int quantity, int orderID)
        {
            this.quantity = quantity;
            ds_size = size;
            switch (size)
            {
                case eSize.eSmall:
                    ds_price = 1.99;
                    sizeString = "Small";
                    break;
                case eSize.eMedium:
                    ds_price = 2.99;
                    sizeString = "Medium";
                    break;
                case eSize.eLarge:
                    ds_price = 3.99;
                    sizeString = "Large";
                    break;
                default:
                    ds_price = 0;
                    sizeString = "Invalid Size";
                    break;
            }
        }
        //destructor
        ~Order()
        {
            Console.WriteLine("Destroying DimSumClass " + ds_size);
        }

        public string getSizeString()
        {
            return sizeString;
        }
        public eSize getSize()
        {
            return ds_size;
        }
        public double getPrice()
        {
            return ds_price*quantity;
        }
        public int getQuantity()
        {
            return quantity;
        }
        public int getOrderID()
        {
            return orderID;
        }
        public void setQuantity(int newQuantity)
        {
            quantity = newQuantity;
        }
        public override string ToString()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(quantity.ToString());
            sb.Append("\t");
            sb.Append(sizeString);
            sb.Append("\t$");
            sb.Append(ds_price.ToString());
            sb.Append(" ea.");
            return sb.ToString();
        }
    }
}