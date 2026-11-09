using System.Collections.Generic;

namespace _25DH110979_quangminh.Models.ViewModel
{
    public class ProductDetailVM
    {
        public Product Product { get; set; }
        public IEnumerable<Product> RelatedProducts { get; set; }
        public IEnumerable<Product> TopProducts { get; set; }
        public int Quantity { get; set; } = 1;
    }
}