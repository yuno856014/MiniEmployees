using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace AIT.DB.Models
{
    public partial class Skill
    {
        public Skill()
        {
            Employees = new HashSet<Employee>();
        }

        public int SkillId { get; set; }
        [Required(ErrorMessage = "Bạn phải điền tên kỹ năng")]
        [Display(Name ="Tên kỹ năng:")]
        public string SkillName { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
