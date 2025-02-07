using System.Collections.Generic;
using System.Linq;

namespace P2FixAnAppDotNetCode.Models
{
    /// <summary>
    /// The Cart class
    /// </summary>
    public class Cart : ICart
    {
        /// <summary>
        /// Read-only property for display only
        /// </summary>
        public IEnumerable<CartLine> Lines => cartLines;
        private readonly List<CartLine> cartLines = new();



        /// <summary>
        /// Adds a product in the cart or increment its quantity in the cart if already added
        /// </summary>//
        public void AddItem(Product product, int quantity)
        {
            foreach (var line in cartLines)
            {
                if (line.Product.Id == product.Id)
                {
                    line.Quantity += quantity;
                    return; // Produit trouvé, quantité mise à jour
                }
            }

            // Si il n'y a pas de produit, on le rajoute
            cartLines.Add(new CartLine { Product = product, Quantity = quantity });
        }

        /// <summary>
        /// Removes a product form the cart
        /// </summary>
        public void RemoveLine(Product product) =>
            cartLines.RemoveAll(l => l.Product.Id == product.Id);

        /// <summary>
        /// Get total value of a cart
        /// </summary>
        public double GetTotalValue()
        {
            double total = 0.0;

            foreach (var line in cartLines)
            {
                total += line.Product.Price * line.Quantity;
            }

            return total;
        }

        /// <summary>
        /// Get average value of a cart
        /// </summary>
        public double GetAverageValue()
        {
            if (cartLines.Count == 0)
                return 0.0;

            double totalValue = GetTotalValue();
            int totalQuantity = 0;

            foreach (var line in cartLines)
            {
                totalQuantity += line.Quantity;
            }

            if (totalQuantity > 0)
                return totalValue / totalQuantity;
            else
                return 0.0;
        }

        /// <summary>
        /// Looks after a given product in the cart and returns if it finds it
        /// </summary>
        public Product FindProductInCartLines(int productId)
        {
            foreach (var line in cartLines)
            {
                if (line.Product.Id == productId)
                {
                    return line.Product;
                }
            }
            return null; // Produit non trouvé 
        }

        /// <summary>
        /// Get a specific cartline by its index
        /// </summary>
        public CartLine GetCartLineByIndex(int index)
        {
            return Lines.ToArray()[index];
        }

        /// <summary>
        /// Clears a the cart of all added products
        /// </summary>
        public void Clear()
        {
            cartLines.Clear();
        }
    }

    public class CartLine
    {
        public int OrderLineId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
