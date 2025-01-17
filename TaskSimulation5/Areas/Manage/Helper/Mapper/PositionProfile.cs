using AutoMapper;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using TaskSimulation5.Areas.Manage.ViewModel.Positions;
using TaskSimulation5.Models;

namespace TaskSimulation5.Areas.Manage.Helper.Mapper
{
    public class PositionProfile:Profile
    {
        public PositionProfile()
        {
            CreateMap<CreatePositionVm,Position>().ReverseMap();
            CreateMap<UpdatePositonVm, Position>().ReverseMap();

        }
    }
}
