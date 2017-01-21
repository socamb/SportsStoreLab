using System.Collections.Generic;
using System.Linq;

namespace SportsStore.Domain.Entities {


    // Class for Cart
    public class Cart {
        private List<CartLine> lineCollection = new List<CartLine>();


        /// <summary>
        /// Adds a product to the cart.
        /// </summary>
        /// <param name="product">Specifies the product object to add tot he cart.</param>
        /// <param name="quantity">Specifies the number of products to add to the cart.</param>
        public void AddItem(Product product, int quantity) {
            CartLine line = lineCollection
                .Where(p => p.Product.ProductID == product.ProductID)
                .FirstOrDefault();

            if (line == null) {
                lineCollection.Add(new CartLine {
                    Product = product,
                    Quantity = quantity
                });
            } else {
                line.Quantity += quantity;
            }
        }


        /// <summary>
        /// Removes a product from the cart,
        /// </summary>
        /// <param name="product">Specifies the product object to remove from the cart</param>
        public void RemoveLine(Product product) {
            lineCollection.RemoveAll(l => l.Product.ProductID == product.ProductID);
        }

        /// <summary>
        /// Computes the total value of the cart.
        /// </summary>
        /// <returns>Specifies the total value for the cart.</returns>
        public decimal ComputeTotalValue() {
            return lineCollection.Sum(e => e.Product.Price * e.Quantity);
        }

        /// <summary>
        /// Clears the cart of all line items.
        /// </summary>
        public void Clear() {
            lineCollection.Clear();
        }

        /// <summary>
        /// Returns all Cart Lines in the Cart.
        /// </summary>
        public IEnumerable<CartLine> Lines {
            get { return lineCollection; }
        }
    }


    // Class for a line item in a cart
    public class CartLine {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
