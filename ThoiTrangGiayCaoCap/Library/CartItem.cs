using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThoiTrangGiayCaoCap
{
    public class CartItem
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Img { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal Amount { get; set; }

        public CartItem() { }
        public CartItem(int proid, string name, string img, decimal price, int quantity) {
            this.ProductId = proid;
            this.Name = name;
            this.Img = img;
            this.Price = price;
            this.Quantity = quantity;
            this.Amount = quantity * price;
        }

    }
}