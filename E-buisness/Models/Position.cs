using System.ComponentModel.DataAnnotations;

namespace E_buisness.Models
{
    public class Position
    {
        public int Id { get; set; }

        [StringLength(maximumLength: 50)]
        public string Name { get; set; }
        public bool IsDeleted { get; set; }

        public List<SpecialTeam> SpecialTeams { get; set; }
    }
}
