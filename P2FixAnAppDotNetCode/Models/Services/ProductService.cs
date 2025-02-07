using System.Collections.Generic;
using P2FixAnAppDotNetCode.Models.Repositories;

namespace P2FixAnAppDotNetCode.Models.Services
{
    /// <summary>
    /// This class provides services to manages the products
    /// </summary>
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;

        public ProductService(IProductRepository productRepository, IOrderRepository orderRepository)
        {
            _productRepository = productRepository;
            _orderRepository = orderRepository;
        }

        /// <summary>
        /// Get all product from the inventory
        /// </summary>
        public List<Product> GetAllProducts()
        {
            // Conversion du tableau en List<T>
            return _productRepository.GetAllProducts();
        }

        /// <summary>
        /// Get a product form the inventory by its id
        /// </summary>
        public Product GetProductById(int id)
        {
            // On utilise GetAllProducts() pour récupérer tous les produits
            List<Product> products = GetAllProducts();

            // Recherche du produit par l'id
            foreach (var product in products)
            {
                if (product.Id == id)
                {
                    return product; // On retourne le produit dès qu'on le trouve
                }
            }

            return null; // Aucun produit trouvé on retourne null
        }

        /// <summary>
        /// Update the quantities left for each product in the inventory depending of ordered the quantities
        /// </summary>
        public void UpdateProductQuantities(Cart cart)
        {
            if (cart == null)
            {
                return; // Sécurité si le panier est null
            }

            // Récupération de la liste de tous les produits depuis le repository
            List<Product> products = GetAllProducts();

            // Pour chaque ligne du panier, on met à jour le stock du produit correspondant
            foreach (var cartLine in cart.Lines)
            {
                foreach (var product in products)
                {
                    if (product.Id == cartLine.Product.Id)
                    {
                        _productRepository.UpdateProductStocks(product.Id, cartLine.Quantity); // On arrête la recherche pour le produit trouvé
                    }
                }
            }
        }
    }
}
