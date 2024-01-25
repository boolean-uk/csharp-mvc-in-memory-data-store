using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exercise.Model.Models
{
    public abstract class DatabaseItem
    {
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}
