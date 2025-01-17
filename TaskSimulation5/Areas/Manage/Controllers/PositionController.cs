using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskSimulation5.Areas.Manage.Helper.Exception;
using TaskSimulation5.Areas.Manage.ViewModel.Positions;
using TaskSimulation5.DAL.Context;
using TaskSimulation5.Models;

namespace TaskSimulation5.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class PositionController : Controller
    {
        private readonly AppDbContext dbContext;
        private readonly IMapper mapper;

        public PositionController(AppDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var positions = await dbContext.Positions.Include(x=>x.Agents).ToListAsync();
            return View(positions);
        }

        public IActionResult Create()
        {
           
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePositionVm vm)
        {
            if (!ModelState.IsValid) return View(vm);

            if (await dbContext.Positions.AnyAsync(a => a.Name == vm.Name) is false)
            {
                var position = mapper.Map<Position>(vm);
                await dbContext.Positions.AddAsync(position);
                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {
            if(id<=0)
            {
                throw new NegativeIdException();
            }
            var position=await dbContext.Positions.FirstOrDefaultAsync(x=>x.Id==id);
            if (position == null)
            {
                throw new NotFoundException();
            }
            var newPositon=mapper.Map<UpdatePositonVm>(position);
            return View(newPositon);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdatePositonVm vm)
        {
            if (!ModelState.IsValid) return View(vm);
            var position = await dbContext.Positions.FirstOrDefaultAsync(x => x.Id == vm.Id);
            if (position == null)
            {
                throw new NotFoundException();
            }
            position.Name = vm.Name;
            dbContext.Positions.Update(position);
            await dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                throw new NegativeIdException();
            }
            var position = await dbContext.Positions.FirstOrDefaultAsync(x => x.Id == id);
            if (position == null)
            {
                throw new NotFoundException();
            }
            dbContext.Positions.Remove(position);
            await dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
