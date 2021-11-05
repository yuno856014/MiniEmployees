using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AIT.DB.Models.Employees
{
    public class EditEmp
    {
        public int EmployeeId { get; set; }
        [Required(ErrorMessage = "Bạn phải điền tên nhân viên")]
        [Display(Name = "Tên Nhân Viên:")]
        public string EmployeeName { get; set; }
        [Required(ErrorMessage = "Bạn phải điền ngày sinh nhật")]
        [Display(Name = "Ngày Sinh Nhật:")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyy}", ApplyFormatInEditMode = true)]
        public DateTime? Dob { get; set; }
        [Required(ErrorMessage = "Bạn phải điền ngày vào làm")]
        [Display(Name = "Ngày Vào làm:")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateFirst { get; set; }
        [Required(ErrorMessage = "Bạn phải điền giới thiệu ngắn")]
        [Display(Name = "Giới Thiệu Ngắn:")]
        public string ShortIntro { get; set; }
        [Display(Name = "Chi Nhánh:")]
        public int? BranchId { get; set; }
        [Display(Name = "Kỹ Năng:")]
        public int? SkillId { get; set; }
        public IFormFile Avatar { get; set; }
        public string ExitAvt { get; set; }
        [Display(Name = "Trạng Thái:")]
        public bool Status { get; set; }
        [Required(ErrorMessage = "Bạn phải điền giới địa chỉ")]
        [Display(Name = "Địa Chỉ:")]
        public string Address { get; set; }

        public virtual Branch Branch { get; set; }
        public virtual Skill Skill { get; set; }
    }
}
