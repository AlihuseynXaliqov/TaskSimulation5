using TaskSimulation5.Models.Base;

namespace TaskSimulation5.Models
{
    public class Agent:BaseEntity
    {
        public string Name { get; set; }

        public string ImageUrl { get; set; }    
        public int PositionId { get; set; }
        public Position Positions { get; set; }

    }
}
