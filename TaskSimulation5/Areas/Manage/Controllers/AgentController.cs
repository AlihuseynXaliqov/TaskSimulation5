using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskSimulation5.Areas.Manage.Helper.Exception;
using TaskSimulation5.Areas.Manage.ViewModel.Agents;
using TaskSimulation5.Areas.Manage.ViewModel.Positions;
using TaskSimulation5.DAL.Context;
using TaskSimulation5.Helper;
using TaskSimulation5.Models;

namespace TaskSimulation5.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize]
    public class AgentController : Controller
    {
        private readonly AppDbContext dbContext;
        private readonly IMapper mapper;
        private readonly IWebHostEnvironment web;

        public AgentController(AppDbContext dbContext, IMapper mapper, IWebHostEnvironment web)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.web = web;
        }
        public async Task<IActionResult> Index()
        {
            var agents = await dbContext.Agents.Include(x => x.Positions).ToListAsync();
            return View(agents);
        }
        public IActionResult Create()
        {
            ViewBag.Positions = dbContext.Positions.ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAgentVm vm)
        {
            ViewBag.Positions = dbContext.Positions.ToList();
            if (!ModelState.IsValid) return View(vm);

            if (!vm.formFile.ContentType.Contains("image"))
            {
                ModelState.AddModelError("formFile", "Sekil duzgun deyil");
                return View();
            }
            if (vm.formFile.Length > 2097152)
            {
                ModelState.AddModelError("formFile", "Sekil duzgun deyil");
                return View();
            }
            vm.ImageUrl = vm.formFile.Upload(web.WebRootPath, "Upload/Agent");

            var position = mapper.Map<Agent>(vm);
            await dbContext.Agents.AddAsync(position);

            await dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Update(int id)
        {
            ViewBag.Positions = dbContext.Positions.ToList();
            if (id <= 0)
            {
                return RedirectToAction("Error","Home");
            }
            var agent=await dbContext.Agents.FirstOrDefaultAsync(x => x.Id == id);
            if (agent == null)
            {
                throw new NotFoundException();
            }
            var newAgent=mapper.Map<UpdateAgentVm>(agent);
            return View(newAgent);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateAgentVm vm)
        {
            ViewBag.Positions = dbContext.Positions.ToList();
            var agent= await dbContext.Agents.FirstOrDefaultAsync(x=>x.Id == vm.Id);
            if (!ModelState.IsValid) return View(vm);
            if (vm.formFile != null)
            {
                if (!vm.formFile.ContentType.Contains("image"))
                {
                    ModelState.AddModelError("formFile", "Sekil duzgun deyil");
                    return View();
                }
                if (vm.formFile.Length > 2097152)
                {
                    ModelState.AddModelError("formFile", "Sekil duzgun deyil");
                    return View();
                }
                if (!string.IsNullOrEmpty(agent.ImageUrl))
                {
                    FileExtention.Delete(web.WebRootPath, "Upload/Agent", agent.ImageUrl);

                }
                agent.ImageUrl = vm.formFile.Upload(web.WebRootPath, "Upload/Agent");
            }
            agent.Name = vm.Name;
            agent.PositionId = vm.PositionId;

            var position = mapper.Map<Agent>(vm);


            await dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
           
            if (id <= 0)
            {
                throw new NegativeIdException();
            }
            var agent = await dbContext.Agents.FirstOrDefaultAsync(x => x.Id == id);
            if (agent == null)
            {
                throw new NotFoundException();
            }
            var newAgent = mapper.Map<Agent>(agent);
            FileExtention.Delete(web.WebRootPath, "Upload/Agent", agent.ImageUrl);
            dbContext.Agents.Remove(newAgent);  
            await dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
