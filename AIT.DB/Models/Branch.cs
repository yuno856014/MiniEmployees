using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace AIT.DB.Models
{
    public partial class Branch
    {
        public Branch()
        {
            Employees = new HashSet<Employee>();
        }

        public int BranchId { get; set; }
        [Required(ErrorMessage ="Bạn phải điền chi nhánh")]
        [RegularExpression("([^0-9~`!@#$%^&*()-=+{}\\|':;>.<,?]+\\S)", ErrorMessage = "Không được chứa các ký tự đặc biệt ngoại trừ dấu cách(khoảng trống)")]
        public string BranchName { get; set; }
        [Required(ErrorMessage ="Bạn phải điền địa chỉ")]
        public string Address { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
