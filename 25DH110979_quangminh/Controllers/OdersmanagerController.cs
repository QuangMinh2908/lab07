using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using _25DH110979_quangminh.Models;

namespace _25DH110979_quangminh.Areas.Admin.Controllers
{
    public class OdersmanagerController : Controller
    {
        private MyStoreEntities db = new MyStoreEntities();

        // 1. Xem danh sách đơn hàng
        public ActionResult Index(string statusSearch, string searchString)
        {
            var orders = db.Order.Include(o => o.Customer).AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString.Trim();
                int orderId;
                if (int.TryParse(searchString, out orderId))
                {
                    orders = orders.Where(o => o.OrderID == orderId || (o.Customer != null && o.Customer.CustomerName.Contains(searchString)));
                }
                else
                {
                    orders = orders.Where(o => o.Customer != null && o.Customer.CustomerName.Contains(searchString));
                }
            }

            if (!string.IsNullOrEmpty(statusSearch))
            {
                orders = orders.Where(o => o.PaymentStatus == statusSearch);
            }

            return View(orders.OrderByDescending(o => o.OrderDate).ToList());
        }

        // 2. Xem chi tiết đơn hàng
        public ActionResult Details(int id)
        {
            var order = db.Order.Include(o => o.OrderDetail.Select(d => d.Product))
                               .Include(o => o.Customer)
                               .FirstOrDefault(o => o.OrderID == id);
            if (order == null) return HttpNotFound();
            return View(order);
        }

        // 3. Danh sách Khách hàng
        public ActionResult Customers()
        {
            var customers = db.Customer.ToList();
            return View(customers);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) db.Dispose();
            base.Dispose(disposing);
        }
    }
}
