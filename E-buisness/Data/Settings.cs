using System.ComponentModel.DataAnnotations;

namespace E_buisness.Data
{
    public class Settings
    {
        public int Id { get; set; }
        [StringLength(maximumLength:50)]
        public string Key { get; set; }
        [StringLength(maximumLength: 50)]
        public string Value { get; set; }
    }
}
