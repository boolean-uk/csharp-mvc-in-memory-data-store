using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace exercise.wwwapi.Model
{
    public class InternalProduct : Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public InternalProduct(string name, string category, int price) : base(name, category, price)
        {
            
        }
    }
}
