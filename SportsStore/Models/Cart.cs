using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    public class Cart
    {
        private List<CartLine> lineConnection = new List<CartLine>();

        public virtual void AddItem(Product product, int quantity)
        {
            var line = lineConnection.FirstOrDefault(x => x.Product.ProductID == product.ProductID);

            if (line == null)
            {
                lineConnection.Add(new CartLine()
                {
                    Product = product,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }


        public virtual void RemoveItem(Product product)
        {
            lineConnection.RemoveAll(x => x.Product.ProductID == product.ProductID);
        }

        public virtual decimal ComputeTotalValue()
        {
            return lineConnection.Sum(x => x.Quantity * x.Product.Price);
        }

        public virtual IEnumerable<CartLine> Lines => lineConnection;
    }

    public class CartLine
    {
        public int CartLineID { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
