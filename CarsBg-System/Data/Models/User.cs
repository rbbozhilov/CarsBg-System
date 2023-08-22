using Microsoft.AspNetCore.Identity;

namespace CarsBg_System.Data.Models
{
    public class User : IdentityUser
    {

        public User()
        {
            this.Cars = new HashSet<Car>();
        }

        public ICollection<Car> Cars { get; set; }
    }
}
