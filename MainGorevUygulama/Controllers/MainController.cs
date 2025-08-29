using MainGorevUygulama.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using System.Security.Claims;
using System.Text;

namespace MainGorevUygulama.Controllers
{
    public class MainController : Controller
    {
        private readonly ApplicationDbContext _context;
        public MainController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult SignUp()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(string FUserName, string FName, string FSurname, string FEMail, string FPassword)
        {
            var mail = await _context.Users.AnyAsync(x => x.Email == FEMail); //AnyAsync metodu, belirtilen koşulu boolean bir değer olarak döner.
            if (mail)
            {
                ViewBag.Error = "Bu e-posta adresi zaten kayıtlı.";
                return View();
            }
            else
            {

                var user = new User();
                user.UserName = FUserName;
                user.Name = FName;
                user.Surname = FSurname;
                user.Email = FEMail;
                user.Password = PasswordToBase64(FPassword);

                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction("SignIn","Main");
            }
        }

        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(string FEMail, string FPassword)
        {
            var user = _context.Users.FirstOrDefault(x => x.Email == FEMail);

            if (user == null || user.Password != PasswordToBase64(FPassword))
            {
                ViewBag.Error = "Mail veya şifre yanlış";
                return View();
            }
            HttpContext.Session.SetInt32("UserId", user.Id);

            // Kullanıcı claim oluştur
            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()), // UserId
        new Claim(ClaimTypes.Name, user.Name),               // UserName
        new Claim(ClaimTypes.Surname, user.Surname),         // Surname
        new Claim(ClaimTypes.Email, user.Email),                  // Email
       
                                               
    };


            var claimsIdentity = new ClaimsIdentity(claims,"SignIn");

            

            // Kullanıcıyı login et
            await HttpContext.SignInAsync(
                new ClaimsPrincipal(claimsIdentity),
                new AuthenticationProperties
                {
                    IsPersistent = false, // Oturumun kalıcı olmasını sağlar
                    ExpiresUtc = null // 30 gün boyunca oturum açık kalır
                }
            );

            return RedirectToAction("MainPage", "Task");
        }
        [HttpPost]
        public async Task<IActionResult> SignOut()
        {
            // Kullanıcıyı oturumdan çıkart
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("SignIn","Main");
        }

        public static string PasswordToBase64(string passwordBase)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(passwordBase);
            return Convert.ToBase64String(bytes);
        }

    }
}
