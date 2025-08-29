using MainGorevUygulama.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;

namespace MainGorevUygulama.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;
        public UserController(ApplicationDbContext context) //Dependency Injection(Context veri tabanına sorgu atmak için)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var userMail = User.FindFirstValue(ClaimTypes.Email);
            var userName = User.FindFirstValue(ClaimTypes.Name);
            var userSurname = User.FindFirstValue(ClaimTypes.Surname);
            ViewBag.Name = userName;
            ViewBag.Surname = userSurname;
            ViewBag.Email = userMail;
            ViewBag.Partial = "Profile";
            return View(); //Model olarak user'ı view'a gönder
        }
        
        [HttpGet]
        public IActionResult EditProfile()
        {
            ViewBag.Partial = "Edit Profile";
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> EditProfile(int id, string FUserName, string FName, string FSurname, string FEMail, string FPassword)
        {
          
            //Db Change OPENED>
            var userId = HttpContext.Session.GetInt32("UserId"); // Oturumdan kullanıcı kimliğini al
            var user = _context.Users.Find(userId.Value); // Kullanıcıyı veritabanından bul
            user.Name = FName;
            user.Surname = FSurname;
            //user.UserName = FUserName;
            user.Email = FEMail;
            user.Password = Base64ToPassword(FPassword);
            _context.Users.Update(user);
            _context.SaveChanges();
            //Db Change CLOSED>

            // Claimleri güncelle
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()), // UserId
                new Claim(ClaimTypes.Name, FName),               // UserName
                new Claim(ClaimTypes.Surname, FSurname),         // Surname
                new Claim(ClaimTypes.Email, FEMail),                  // Email      

            };
            var claimsIdentity = new ClaimsIdentity(claims, "EditProfile");



            // Kullanıcıyı login et
            await HttpContext.SignInAsync(
                new ClaimsPrincipal(claimsIdentity),
                new AuthenticationProperties
                {
                    IsPersistent = false, // Oturumun kalıcı olmasını sağlar
                    ExpiresUtc = null // 30 gün boyunca oturum açık kalır
                }
            );
            return RedirectToAction("Profile", "User");
            
        }
        [HttpPost]
        public IActionResult DeleteAccount()
        {

            var userId = HttpContext.Session.GetInt32("UserId");
            var user = _context.Users.Find(userId.Value);
            var missions = _context.Missions.Where(m => m.UserId == user.Id);
            _context.Missions.RemoveRange(missions);
            _context.Users.Remove(user);
            _context.SaveChanges();
            return RedirectToAction("SignIn","Main");
        }

        public static string Base64ToPassword(string passwordBase)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(passwordBase);
            return Convert.ToBase64String(bytes);
        }

    }
}
