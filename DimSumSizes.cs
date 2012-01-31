using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COMPortTerminal
{
    class DimSumSizes
    {
<<<<<<< HEAD
        public enum eSize {eNull,eSmall,eMedium,eLarge};
=======
        public enum eSize { eNull, eSmall, eMedium, eLarge };
>>>>>>> 1351fe8101a6a3639307e58c5afd6835662b74c4
        private eSize ds_size;
        private double ds_price;
        private int quantity;
        private string text;

        //constructor
<<<<<<< HEAD
        public DimSumSizes():this(eSize.eNull){}
        public DimSumSizes (eSize size): this (size, 1){}
        public DimSumSizes(eSize size, int quantity)
        {
            this.quantity= quantity;
=======
        public DimSumSizes() : this(eSize.eNull) { }
        public DimSumSizes(eSize size) : this(size, 1) { }
        public DimSumSizes(eSize size, int quantity)
        {
            this.quantity = quantity;
>>>>>>> 1351fe8101a6a3639307e58c5afd6835662b74c4
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
<<<<<<< HEAD
        public int getQuantity ()
=======
        public int getQuantity()
>>>>>>> 1351fe8101a6a3639307e58c5afd6835662b74c4
        {
            return quantity;
        }
    }
}
