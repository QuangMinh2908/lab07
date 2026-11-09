using System.Linq;
using System.Web.Mvc;
using _25DH110979_quangminh.Models;
using _25DH110979_quangminh.Models.ViewModel;

namespace _25DH110979_quangminh.Controllers
{
    public class CartController : Controller
    {
        private MyStoreEntities db = new MyStoreEntities();

        // Lấy giỏ hàng từ Session
        private Cart GetCart()
        {
            Cart cart = Session["Cart"] as Cart;
            if (cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }

        // 1. Xem giỏ hàng
        public ActionResult Index()
        {
            var cart = GetCart();
            return View(cart);
        }

        // 2. Thêm vào giỏ hàng khi bấm nút "Mua ngay"
        public ActionResult AddToCart(int productId, int quantity = 1)
        {
            var product = db.Product.FirstOrDefault(p => p.ProductID == productId);
            if (product != null)
            {
                GetCart().AddItem(product, quantity);
            }
            return RedirectToAction("Index");
        }

        // 3. Xóa sản phẩm khỏi giỏ
        public ActionResult RemoveFromCart(int productId)
        {
            GetCart().RemoveItem(productId);
            return RedirectToAction("Index");
        }

        // 4. Đặt hàng / Thanh toán
        public ActionResult Checkout()
        {
            var cart = GetCart();
            if (!cart.Items.Any())
            {
                return RedirectToAction("Index", "Home");
            }

            // Tạo hóa đơn mới lưu vào Database
            Order order = new Order
            {
                OrderDate = System.DateTime.Now,
                TotalAmount = cart.TotalValue(),
                PaymentStatus = "Đang xử lý"
            };

            db.Order.Add(order);
            db.SaveChanges();

            // Thêm chi tiết hóa đơn
            foreach (var item in cart.Items)
            {
                OrderDetail detail = new OrderDetail
                {
                    OrderID = order.OrderID,
                    ProductID = item.ProductID,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice
                };
                db.OrderDetail.Add(detail);
            }
            db.SaveChanges();

            // Xóa sạch giỏ hàng sau khi mua thành công
            cart.Clear();

            ViewBag.Message = "Đặt hàng thành công! Mã đơn hàng của bạn là #" + order.OrderID;
            return View("OrderSuccess", order);
        }
    }
}