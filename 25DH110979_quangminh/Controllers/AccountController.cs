using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using _25DH110979_quangminh.Models;

namespace _25DH110979_quangminh.Controllers
{
    public class AccountController : Controller
    {
        private MyStoreEntities db = new MyStoreEntities();

        // 1. Đăng ký (GET)
        public ActionResult Register()
        {
            return View();
        }

        // 1. Đăng ký (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                var check = db.User.FirstOrDefault(s => s.Username == user.Username);
                if (check == null)
                {
                    db.User.Add(user);
                    db.SaveChanges();
                    return RedirectToAction("Login");
                }
                else
                {
                    ViewBag.error = "Tên đăng nhập đã tồn tại!";
                }
            }
            return View();
        }

        // 2. Đăng nhập (GET)
        public ActionResult Login()
        {
            return View();
        }

        // 2. Đăng nhập (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string username, string password)
        {
            if (ModelState.IsValid)
            {
                var user = db.User.FirstOrDefault(s => s.Username.Equals(username) && s.Password.Equals(password));
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(user.Username, false);
                    Session["Username"] = user.Username;
                    Session["UserRole"] = user.UserRole;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.error = "Tên đăng nhập hoặc mật khẩu không đúng!";
                }
            }
            return View();
        }

        // 3. Đăng xuất
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        // 4. Đổi mật khẩu (GET)
        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }

        // 4. Đổi mật khẩu (POST)
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(string currentPassword, string newPassword, string confirmPassword)
        {
            if (newPassword != confirmPassword)
            {
                ViewBag.Error = "Mật khẩu mới và xác nhận mật khẩu không khớp!";
                return View();
            }

            string username = User.Identity.Name;
            var user = db.User.FirstOrDefault(u => u.Username == username && u.Password == currentPassword);

            if (user == null)
            {
                ViewBag.Error = "Mật khẩu hiện tại không chính xác!";
                return View();
            }

            user.Password = newPassword;
            db.SaveChanges();
            ViewBag.Success = "Đổi mật khẩu thành công!";
            return View();
        }
    }
}