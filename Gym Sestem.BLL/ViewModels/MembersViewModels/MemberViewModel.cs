using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym_Sestem.BLL.ViewModels.MembersViewModels
{
    public class MemberViewModel
    {
        public int Id { get; set; }
        public string? Photo { get; set; }
        public string Name { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Phone { get; set; } = default!;
        public string Gender { get; set; } = default!;

        public string DateOfBirth { get; set; } = default!;
        public string Address { get; set; } = default!;

        public decimal? Height { get; set; }
        public decimal? Weight { get; set; }
        public string? BloodType { get; set; }
        public string? Note { get; set; }

        public string? PlanName { get; set; }
        public string? MembershipStartDate { get; set; }
        public string? MembershipEndDate { get; set; }
    }
}
