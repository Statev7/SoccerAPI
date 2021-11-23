using SoccerAPI.Database.Models.BaseModels;

namespace SoccerAPI.Database.Models.Teams
{
    public class Footballer : Person
    {
        public Footballer()
            :base()
        {
            
        }

        public string Position { get; set; }

        public string StrongLeg { get; set; }

        public virtual Team Team { get; set; }
    }
}
