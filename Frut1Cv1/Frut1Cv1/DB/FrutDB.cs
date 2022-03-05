using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frut1Cv1
{
    public class FrutDB : DbContext
    {
        public FrutDB() : base("DBConnection") 
        { 
        }

        public DbSet<Оплата> Оплаты { get; set; } 
        public DbSet<Машина> Машины { get; set; }
        public DbSet<Товар> Товары { get; set; }
    }
}
