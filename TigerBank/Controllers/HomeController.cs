using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Cryptography;
using TigerBank.Models;
using System.Text;

namespace TigerBank.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ViewResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(Users obj)
        {
            if (ModelState.IsValid)
            {

                string password = obj.Password;
                string salt = "";
                string charset = "0123456789abcdefghijklmnopqrstuvwxyz";
                int randStrLength = 10;

                for (int i = 0; i < randStrLength; i++)
                {
                    Random random = new Random();
                    int index = random.Next(0, charset.Length);
                    salt += charset[index];
                }

                password += salt;
                string hashed = ComputeSha256Hash(password);
                obj.Password = hashed;
                obj.Salt = salt;

                _db.Users.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "New user has been created.";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public ViewResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Users obj)
        {
            if (ModelState.IsValid)
            {
                Users user = _db.Users.Where(x => x.Username == obj.Username).FirstOrDefault();
                if (user == null)
                {
                    TempData["error"] = "No user found.";
                    return View(obj);
                }
                else
                {
                    string password = obj.Password;
                    password += user.Salt;

                    string newHash = ComputeSha256Hash(password);

                    if(newHash == user.Password)
                    {
                        TempData["success"] = "Login Successful!";
                        return RedirectToAction("Index", user); //TODO: Change to correct redirect page.
                    }
                }
            }
            return View(obj);
        }

        public string ComputeSha256Hash(string str)
        {
            using(SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(str));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}