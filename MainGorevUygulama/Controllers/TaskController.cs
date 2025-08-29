using MainGorevUygulama.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.Identity.Client;
using System.Security.Claims;


namespace MainGorevUygulama.Controllers
{
    [Authorize]
    public class TaskController : Controller
    {
        private readonly ApplicationDbContext _context;
        public TaskController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult AddMission()
        {
            ViewBag.Partial = "Add Mission";
            return View();
        }
        [HttpPost]
        public IActionResult AddMission(string FMissionTitle, string FMissionDescription, string FMissionDateTime)
        {
            
            var mission = new Mission();
          
            mission.UserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value); // Get the UserId from the authenticated user

            mission.Title = FMissionTitle;
            mission.Description = FMissionDescription;
            mission.Date = DateTime.Parse(FMissionDateTime);
            mission.Statu = false;
            _context.Missions.Add(mission);
            _context.SaveChanges();

            return RedirectToAction("MainPage", "Task");
        }


        [HttpPost]
        public IActionResult DeleteMission(int id)
        {
            var mission = _context.Missions.Find(id);
            if (mission != null)
            {
                _context.Missions.Remove(mission);
                _context.SaveChanges();
                return RedirectToAction("MainPage","Task");
            }
            return View();
        }
        [HttpGet]
        public IActionResult UpdateMission(int id)
        {
            var mission = _context.Missions.Find(id);
            return View(mission);
        }
        [HttpPost]
        public IActionResult UpdateMission(int id, string FMissionTitle, string FMissionDescription, string FMissionDateTime)
        {
            var mission = _context.Missions.Find(id);
            {
                if (mission != null)
                {
                    mission.Title = FMissionTitle;
                    mission.Description = FMissionDescription;
                    mission.Date = DateTime.Parse(FMissionDateTime);
                    mission.Statu = mission.Statu;

                    _context.Missions.Update(mission);
                    _context.SaveChanges();

                    return RedirectToAction("MainPage","Task");
                }
                return View();
            }
        }

        [HttpGet]
        public IActionResult MainPage()
        {

            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var Missions = _context.Missions.Where(m => m.UserId == userId).ToList(); 
            var name = User.FindFirstValue(ClaimTypes.Name);
            var surname = User.FindFirstValue(ClaimTypes.Surname);

            ViewBag.FullName = $"{name} {surname}";
            ViewBag.Partial = "Missions";
            return View(Missions);
        }
        [HttpPost]
        public async Task<IActionResult> MissionStatus(int id, bool Statu)
        {

            var task = _context.Missions.FirstOrDefault(x => x.Id == id);
            if (task != null)
            {
                task.Statu = Statu;
                _context.SaveChanges(); // ✅ DB'ye kaydet
            }
            return RedirectToAction("MainPage","Task");
        }

    }
}
