using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using _25DH110979_quangminh.Models;
using _25DH110979_quangminh.Models.ViewModel;

namespace _25DH110979_quangminh.Controllers
{
    public class HomeController : Controller
    {
        private MyStoreEntities db = new MyStoreEntities();

        // 1. Trang chủ bán hàng
        public ActionResult Index()
        {
            var products = db.Product.Include(p => p.Category).ToList();
            return View(products);
        }

        // 2. Chi tiết sản phẩm
        public ActionResult ProductDetail(int id)
        {
            var product = db.Product.Include(p => p.Category).FirstOrDefault(p => p.ProductID == id);
            if (product == null) return HttpNotFound();

            var vm = new ProductDetailVM
            {
                Product = product,
                RelatedProducts = db.Product.Where(p => p.CategoryID == product.CategoryID && p.ProductID != id).Take(8).ToList(),
                TopProducts = db.Product.OrderByDescending(p => p.OrderDetail.Count).Take(8).ToList()
            };

            return View(vm);
        }
    }
}