using exercise.Model.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exercise.Model.Data
{
    public class DataContext<T> : DbContext where T : DatabaseItem
    {
        public DataContext(DbContextOptions<DataContext<T>> options) : base(options)
        {
            
        }

        public DbSet<T> Data { get; set; }
    }
}
