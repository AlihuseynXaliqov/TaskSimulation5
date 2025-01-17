using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using TaskSimulation5.DAL.Context;
using TaskSimulation5.Models;

namespace TaskSimulation5.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext dbContext;

        public HomeController(AppDbContext dbContext)
        {

            this.dbContext = dbContext;
        }
        public IActionResult Index()
        {
            var agent = dbContext.Agents.Include(x=>x.Positions).ToList();
            return View(agent);
        }
    }
}
