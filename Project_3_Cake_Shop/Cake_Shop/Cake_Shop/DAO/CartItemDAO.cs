﻿using Cake_Shop.Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cake_Shop.DAO
{
    class CartItemDAO
    {
        public static BindingList<CartItem> CreateList()
        {
            return new BindingList<CartItem>();
        }
        public static void AddCakeToCart(ref BindingList<CartItem> cart, CAKE cake, int quantity = 1)
        {
            for(int i = 0; i < cart.Count; i++)
            {
                if(cart[i].CakeItem.CakeID == cake.CakeID)
                {
                    cart[i].Quantity += quantity;
                    return;
                }
            }

            cart.Add(new CartItem()
            {
                CakeItem = cake,
                Quantity = quantity
            });
        }
        public static int CalcCakePay(BindingList<CartItem> cart)
        {
            int result = 0;
            foreach(var item in cart)
            {
                result += item.Quantity * (int)item.CakeItem.Price;
            }
            return result;
        }

        public static int CountTotalItems(BindingList<CartItem> cartList)
        {
            int result = 0;
            foreach (var item in cartList)
            {
                result += item.Quantity;
            }
            return result;
        }
    }
    
}
