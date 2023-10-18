
using CarsBg_System.Data;

using Microsoft.EntityFrameworkCore;

namespace CarsBg_System.Tests.Mock
{
    public static class DatabaseMock
    {

        public static CarsDbContext Instance
        {
            get
            {
                var dbContextOptions = new DbContextOptionsBuilder<CarsDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options;

                return new CarsDbContext(dbContextOptions);
            }
        }

    }
}
