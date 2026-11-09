using System.Collections.Generic;
using System.Linq;

namespace _25DH110979_quangminh.Models.ViewModel
{
    public class CartItem
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public decimal Total => Quantity * UnitPrice;
    }

    public class Cart
    {
        private List<CartItem> items = new List<CartItem>();
        public IEnumerable<CartItem> Items => items;

        public void AddItem(Product product, int quantity = 1)
        {
            var item = items.FirstOrDefault(p => p.ProductID == product.ProductID);
            if (item == null)
            {
                items.Add(new CartItem
                {
                    ProductID = product.ProductID,
                    ProductName = product.ProductName,
                    ProductImage = product.ProductImage,
                    UnitPrice = product.ProductPrice,
                    Quantity = quantity,
                    CategoryID = product.CategoryID,
                    CategoryName = product.Category != null ? product.Category.CategoryName : ""
                });
            }
            else
            {
                item.Quantity += quantity;
            }
        }

        public void UpdateQuantity(int productId, int quantity)
        {
            var item = items.FirstOrDefault(p => p.ProductID == productId);
            if (item != null)
            {
                if (quantity > 0)
                    item.Quantity = quantity;
                else
                    items.Remove(item);
            }
        }

        public void RemoveItem(int productId) => items.RemoveAll(i => i.ProductID == productId);
        public void Clear() => items.Clear();
        public decimal TotalValue() => items.Sum(e => e.Total);

        // Nhóm sản phẩm theo danh mục
        public IEnumerable<IGrouping<string, CartItem>> GroupedItems => items.GroupBy(i => i.CategoryName);
    }
}