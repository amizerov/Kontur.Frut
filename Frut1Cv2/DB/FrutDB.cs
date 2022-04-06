using Microsoft.EntityFrameworkCore;

namespace Frut1Cv2
{
    public class FrutDB : DbContext
    {
        public DbSet<Оплата> Оплаты { get; set; }
        public DbSet<Машина> Машины { get; set; }
        public DbSet<Товар> Товары { get; set; }
        public DbSet<Вагон> Вагоны { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer($"Server=.;Database=MoscowCity;UID=city;PWD=!QAZ1qaz");

    }
}
