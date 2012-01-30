using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COMPortTerminal
{
    class DimSumSizes
    {
        public enum eSize { eNull, eSmall, eMedium, eLarge };
        private eSize ds_size;
        private double ds_price;
        private int quantity;
        private string text;

        //constructor
        public DimSumSizes() : this(eSize.eNull) { }
        public DimSumSizes(eSize size) : this(size, 1) { }
        public DimSumSizes(eSize size, int quantity)
        {
            this.quantity = quantity;
            ds_size = size;
            switch (size)
            {
                case eSize.eSmall:
                    ds_price = 1.99;
                    text = "Small";
                    break;
                case eSize.eMedium:
                    ds_price = 2.99;
                    text = "Medium";
                    break;
                case eSize.eLarge:
                    ds_price = 3.99;
                    text = "Large";
                    break;
                default:
                    ds_price = 0;
                    text = "Invalid Size";
                    break;
            }
        }
        //destructor
        ~DimSumSizes()
        {
            Console.WriteLine("Destroying DimSumClass " + ds_size);
        }

        public string getSizeString()
        {
            return text;
        }
        public eSize getSize()
        {
            return ds_size;
        }
        public double getPrice()
        {
            return ds_price;
        }
        public int getQuantity()
        {
            return quantity;
        }
    }
}
