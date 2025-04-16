using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Just_project.Admin.Models
{
    //[Table("SignDB")]
    //public class SignInModel
    //{

    //    public int Id { get; set; }
    //    public string Username { get; set; }
    //    public string Email { get; set; }
    //    public string Password { get; set; }
    //}
    public class SignInModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }
}
