﻿using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Cryptography;
using TigerBank.Models;
using System.Text;
using TigerBank.Models.ViewModels;
using TigerBank.Repository.IRepository;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TigerBank.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _db = db;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public ViewResult Bank(Users obj)
        {
            Users user = obj;
            return View();
        }

        public ViewResult Deposit()
        {
            return View();
        }

        public ViewResult Withdraw()
        {
            return View();
        }

        public ViewResult Transactions()
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

        [HttpGet]
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
                        return RedirectToAction("Bank", user); //TODO: Change to correct redirect page.
                    }
                }
            }
            return View(obj);
        }

        [HttpGet]
        public ViewResult AddAccount()
        {
            AccountVM accountVM = new()
            {
                Account = new(),
                UsersList = _unitOfWork.Users.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Username,
                    Value = i.userId.ToString()
                }),
                AccountTypeList = _unitOfWork.AccountType.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.AccountTypeId.ToString()
                })
            };
            return View(accountVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddAccount(Accounts obj)
        {
            if (ModelState.IsValid)
            {

                //string AccountType = obj.AccountType;
                int balance = obj.Balance;

                _db.Accounts.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "New Account has been created.";

                return RedirectToAction("Bank");
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