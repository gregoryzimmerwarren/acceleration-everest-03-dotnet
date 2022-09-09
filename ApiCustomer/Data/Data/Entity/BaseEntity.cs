using System.ComponentModel.DataAnnotations;

namespace Data.Entity
{
    public class BaseEntity
    {
        [Key]
        public long Id { get; set; }
    }
}
