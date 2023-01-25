using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_buisness.Models
{
    public class SpecialTeam
    {
        public int Id { get; set; }
        public int PositionId { get; set; }

        [StringLength(maximumLength: 100)]
        public string? ImageName { get; set; }
        [StringLength(maximumLength:50)]
        public string Fullname { get; set; }
        [StringLength(maximumLength: 200)]
        public string? FbUrl { get; set; }
        [StringLength(maximumLength: 200)]
        public string? InstUrl { get; set; }
        [StringLength(maximumLength: 200)]
        public string? TwUrl { get; set; }
        public bool IsDeleted { get; set; }

        public Position? Position { get; set; }
        [NotMapped]
        public IFormFile? ImageFile { get; set; }
    }
}
