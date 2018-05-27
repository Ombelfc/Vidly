using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Vidly.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter a name for the customer.")]
        [StringLength(255)]
        public string Name { get; set; }

        public bool IsSubscribedToNewsletter { get; set; }

        public MembershipType MembershipType { get; set; }

        [Display(Name = "Membership Type")]
        // A property of type byte is implicitly required
        public byte MembershipTypeId { get; set; }

        [Display(Name = "Date of birth")]
        [Min18YearsIfMember]
        public DateTime? Birthday { get; set; }
    }
}
