using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MeetManager.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace MeetManager.Controllers
{
    public class HomeController : Controller
    {
        private MeetManagerContext context;

        public HomeController(MeetManagerContext c)
        {
            context = c;
        }

        public IActionResult Index()
        {

            var meets = context.Meets
                .Include(sh => sh.Team)
                .Include(sh => sh.TeamMeets)
                    .ThenInclude(sh => sh.Meet)
                .Include(sh => sh.TeamMeets)
                    .ThenInclude(sh => sh.Team)
                .Include(sh => sh.Races)
                    .ThenInclude(sh => sh.Event)
                .Include(sh => sh.Races)
                    .ThenInclude(sh => sh.Runner)
                .ToList();


            var teams = context.Teams.ToList();

            ViewBag.Teams = teams;
            return View(meets);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }


        public IActionResult AllTeams()
        {
            var conferences = context.Teams.Select(s=> s.Conference).Distinct().ToList();

            var listOfTeams = new List<List<Team>>();

            foreach(string conference in conferences)
            {
                var teamadd = context.Teams.Where(w => w.Conference == conference).ToList();
                listOfTeams.Add(teamadd);
            }

            ViewBag.Teams = listOfTeams;
            return View();
        }


        [Route("Home/MeetView/{MeetId}")]
        public IActionResult MeetView(int MeetId)
        {
            int Id = MeetId;

            var races = context.Races
                .Include(sh => sh.Runner)
                    .ThenInclude(sh => sh.Team)
                .Include(sh => sh.Event)
                .Include(sh => sh.Meet)
                .Where(sh => sh.Meet.Id == Id)
            .ToList();

            var events = context.Events.ToList();
            var runners = context.Runners
                .Include(sh => sh.Team)
                .Where(sh => context.Meets.Where(m => m.Id == Id).First().TeamMeets.Any(tm => tm.Team.Runners.Any(te => te.Id == sh.Id)))
                .ToList();
            

            ViewBag.Teams = context.Teams.Where(t => t.TeamMeets.Where(tm => tm.MeetId == Id).Any(tm => tm.TeamId == t.Id)).ToList();
            ViewBag.MeetName = context.Meets.Where(m => m.Id == MeetId).Single().Name;
            ViewBag.Location = context.Meets.Where(m => m.Id == MeetId).Single().Location;
            ViewBag.MeetId = Id;
            ViewBag.EventsList = events;
            ViewBag.RunnersList = runners;

            return View(races);
        }



        [Route("Home/RunnerView/{RunnerId}")]
        public IActionResult RunnerView(int RunnerId)
        {
            int Id = RunnerId;

            var runner = context.Runners.Where(r => r.Id == Id)
                .Include(i => i.Races)
                    .ThenInclude(i => i.Event)
                .Include(i => i.Races)
                    .ThenInclude(i => i.Meet)
                .Include(i => i.Team).Single();

            ViewBag.Runner = runner;
            return View();
        }

        [Route("Home/TeamView/{TeamId}")]
        public IActionResult TeamView(int TeamId)
        {
            int Id = TeamId;

            var runners = context.Runners
                .Include(sh => sh.Team)
                .Where(sh => sh.Team.Id == TeamId)
                .ToList();

            var events = new List<string>();
            events.Add("Distance");
            events.Add("Sprints");
            events.Add("Mid-Distance");

            var years = new List<string>();
            years.Add("Freshman");
            years.Add("Sophomore");
            years.Add("Junior");
            years.Add("Senior");

            ViewBag.Events = events;
            ViewBag.Years = years;

            ViewBag.Team = context.Teams.Where(m => m.Id == Id).Single();
            ViewBag.TeamId = Id;

            return View(runners);
        }


        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult HandleNewRace(int MeetId, int RunnerId, int EventId, string time)
        {
            Meet meet = context.Meets.Where(m => m.Id == MeetId).Single();
            Runner runner = context.Runners.Where(r => r.Id == RunnerId).Single();
            Event even = context.Events.Where(e => e.Id == EventId).Single();
            Race newRace  = new Race () { Meet = meet, Runner = runner, Event = even, Time = time};
            context.Races.Add(newRace);
            context.SaveChanges();

            return RedirectToAction("MeetView", "Home", new { @MeetId = MeetId });
        }


        [HttpPost]
        public IActionResult HandleNewRunner(int TeamId, string eventGroup, string year, string firstName, string lastName)
        {
            Team team = context.Teams.Where(t => t.Id == TeamId).Single();
            Runner newRunner = new Runner() { EventGroup = eventGroup, Year = year, FirstName = firstName, LastName = lastName, Team = team };
            context.Runners.Add(newRunner);
            context.SaveChanges();

            return RedirectToAction("TeamView", "Home", new { @TeamId = TeamId });
        }

    }
}
