using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cake_Shop.Business
{
    class CartItem : INotifyPropertyChanged
    {
        private int _quantity;
        public CAKE CakeItem { get; set; }
        public int Quantity 
        {
            get => _quantity;
            set 
            {
                _quantity = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Quantity"));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
