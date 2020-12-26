using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cake_Shop.Builder
{
    class CakeBuilder
    {
        //Properties
        public string CakeName { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string Image { get; set; }
        public int CategoryID { get; set; }
        // Methods
        public CakeBuilder SetCakeName(string cakename)
        {
            this.CakeName = cakename;
            return this;
        }
        public CakeBuilder SetDescription(string description)
        {
            this.Description = description;
            return this;
        }
        public CakeBuilder SetPrice(int price)
        {
            this.Price = price;
            return this;
        }
        public CakeBuilder SetImage(string image)
        {
            this.Image = image;
            return this;
        }
        public CakeBuilder SetCategoryID(int categoryID)
        {
            this.CategoryID = categoryID;
            return this;
        }
        public CAKE Build()
        {
            return new CAKE()
            {
                CakeName = this.CakeName,
                CategoryID = this.CategoryID,
                Description = this.Description,
                Image = this.Image,
                Price = this.Price
            };
        }
    }
}
