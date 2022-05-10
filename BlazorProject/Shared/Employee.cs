using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorProject.Shared
{
    public class Participant
    {
        public int ParticipantId { get; set; }
        [Required]
        [MinLength(2, ErrorMessage = "FirstName must contains at least 2 charcters")]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBrith { get; set; }
        public Gender Gender { get; set; }
        public int CourseId { get; set; }
        public string PhotoPath { get; set; }
        public Course Course { get; set; }
    }
}
