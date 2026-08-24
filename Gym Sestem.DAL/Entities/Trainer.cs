using Gym_Sestem.DAL.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym_Sestem.DAL.Entities
{
    public class Trainer : GymUser
    {
        public Specialties specialize { get; set; }
        public DateTime HiringDate { get; set; }
        public ICollection<Session> Sessions { get; set; } = new List<Session>();
    }
}
