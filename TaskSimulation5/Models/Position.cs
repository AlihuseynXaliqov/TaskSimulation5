using TaskSimulation5.Models.Base;

namespace TaskSimulation5.Models
{
    public class Position : BaseEntity
    {

        public string Name { get; set; }
        public int Count { get; set; }

        public ICollection<Agent> Agents { get; set; }
    }
}
