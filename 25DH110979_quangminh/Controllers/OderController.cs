using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using _25DH110979_quangminh.Models;
using _25DH110979_quangminh.Models.ViewModel;

namespace _25DH110979_quangminh.Controllers
{
    public class OrderController : Controller
    {
        private MyStoreEntities db = new MyStoreEntities();

        private Cart GetCartService()
        {
            Cart cart = Session["Cart"] as Cart;
            if (cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }

        // Xem lịch sử mua hàng
        [Authorize]
        public ActionResult MyOrder(string status)
        {
            string username = User.Identity.Name;
            var customer = db.Customer.FirstOrDefault(c => c.Username == username);
            if (customer == null) return RedirectToAction("Login", "Account");

            var orders = db.Order.Where(o => o.CustomerID == customer.CustomerID).AsQueryable();

            if (!string.IsNullOrEmpty(status))
            {
                orders = orders.Where(o => o.PaymentStatus == status);
            }

            return View(orders.OrderByDescending(o => o.OrderDate).ToList());
        }

        // Mua lại đơn hàng cũ
        public ActionResult ReOrder(int orderId)
        {
            var orderDetails = db.OrderDetail.Where(od => od.OrderID == orderId).ToList();
            var cart = GetCartService();

            foreach (var item in orderDetails)
            {
                var product = db.Product.Find(item.ProductID);
                if (product != null)
                {
                    cart.AddItem(product, item.Quantity);
                }
            }
            return RedirectToAction("Index", "Cart");
        }
    }
}