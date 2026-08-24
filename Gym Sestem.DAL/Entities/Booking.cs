using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym_Sestem.DAL.Entities
{
    public class Booking : BaseEntity
    {

        public bool IsAttended { get; set; } = false;
        public Member Member { get; set; } = null!;
        public int MemberId { get; set; }

        public Session Session { get; set; } = null!;
        public int SessionId { get; set; }

    }
}
