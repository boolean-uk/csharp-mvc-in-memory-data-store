using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace exercise.wwwapi.Model
{
    public class InternalProduct : Product
    {
        public int Id { get; set; }

        public InternalProduct(string name, string category, int price) : base(name, category, price)
        {
            
        }
    }
}
