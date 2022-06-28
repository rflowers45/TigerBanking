using Microsoft.AspNetCore.Mvc;
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
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger,  IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public ViewResult Bank(Users obj)
        {
            Users user = obj;
            return View(user);
        }

        [HttpGet]
        public ViewResult Deposit(int userId)
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
            //int UserId = userId;
           // Accounts account = _unitOfWork.Account.GetFirstOrDefault(u => u.UserId == UserId, includeProperties: "User,AccountType");
            return View(accountVM);

        }

        [HttpPost]
        public IActionResult Deposit(AccountVM obj)
        {
            if (ModelState.IsValid)
            {


                Accounts account = _unitOfWork.Account.GetFirstOrDefault(u => u.UserId == obj.Account.UserId, includeProperties: "User,AccountType");
                obj.Account.Balance += account.Balance;

                _unitOfWork.Account.Update(obj.Account);
                _unitOfWork.Save();
                Users user = _unitOfWork.Users.GetFirstOrDefault(u => u.userId == obj.Account.UserId);
                TempData["success"] = "Balance Updated.";
                return RedirectToAction("Bank", user);
            }

            obj.UsersList = _unitOfWork.Users.GetAll().Select(i => new SelectListItem
            {
                Text = i.Username,
                Value = i.userId.ToString()
            });

            obj.AccountTypeList = _unitOfWork.AccountType.GetAll().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.AccountTypeId.ToString()
            });
           
            return View(obj);
        }
           
               // Accounts account = _unitOfWork.Account.GetFirstOrDefault(u => u.UserId == obj.UserId, includeProperties: "User,AccountType");
               // obj.Balance += account.Balance;

               

        [HttpGet]
        public ViewResult Withdraw(int userId)
        {
            int UserId = userId;
            Accounts account = _unitOfWork.Account.GetFirstOrDefault(u => u.UserId == UserId);
            return View(account);
        }

        [HttpPost]
        public IActionResult Withdraw(Accounts obj)
        {
            if (ModelState.IsValid)
            {
                Accounts account = _unitOfWork.Account.GetFirstOrDefault(u => u.UserId == obj.UserId, includeProperties: "User,AccountType");
                account.Balance -= obj.Balance;
                obj.Balance = account.Balance;

                _unitOfWork.Account.Update(obj);
                _unitOfWork.Save();
                Users user = _unitOfWork.Users.GetFirstOrDefault(u => u.userId == obj.UserId);
                TempData["success"] = "Balance Updated.";
                return RedirectToAction("Bank", user);
            }
            return View(obj);
        }

        public ViewResult Transactions(int userId)
        {
            int UserId = userId;

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
                Users user = _unitOfWork.Users.GetFirstOrDefault(u => u.Username == obj.Username);

                if (user == null)
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

                    _unitOfWork.Users.Add(obj);
                    _unitOfWork.Save();
                    TempData["success"] = "New user has been created.";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["error"] = "User already exists!";
                }
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
                Users user = _unitOfWork.Users.GetFirstOrDefault(x => x.Username == obj.Username);
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
        public IActionResult AddAccount(AccountVM obj)
        {
            if (ModelState.IsValid)
            {

                //string AccountType = obj.AccountType;
                //int balance = obj.Balance;

                _unitOfWork.Account.Add(obj.Account);
                _unitOfWork.Save();  
                TempData["success"] = "New Account has been created.";

                return RedirectToAction("Bank");
            }

            obj.UsersList = _unitOfWork.Users.GetAll().Select(i => new SelectListItem
            {
                Text = i.Username,
                Value = i.userId.ToString()
            });

            obj.AccountTypeList = _unitOfWork.AccountType.GetAll().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.AccountTypeId.ToString()
            });


            return View(obj);
        }

        [HttpGet]
        public ViewResult AddAccountType()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddAccountType(AccountType obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.AccountType.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "New Account Type has been created.";
                return RedirectToAction("Index");
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