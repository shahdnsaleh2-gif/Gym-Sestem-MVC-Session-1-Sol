using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym_Sestem.DAL.Entities
{
    public class Member : GymUser
    {
        public string? Photo { get; set; } = null!;
        public HealthRecord? HealthRecord { get; set; } = null!;
        public ICollection<Membership> Memberships = new List<Membership>();
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
