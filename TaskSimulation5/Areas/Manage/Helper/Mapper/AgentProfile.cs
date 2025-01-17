using AutoMapper;
using TaskSimulation5.Areas.Manage.ViewModel.Agents;
using TaskSimulation5.Areas.Manage.ViewModel.Positions;
using TaskSimulation5.Models;

namespace TaskSimulation5.Areas.Manage.Helper.Mapper
{
    public class AgentProfile: Profile
    {
        public AgentProfile()
        {
            CreateMap<CreateAgentVm, Agent>().ReverseMap();
            CreateMap<UpdateAgentVm, Agent>().ReverseMap();
        }
    }
}
