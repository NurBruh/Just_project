using System.ComponentModel.DataAnnotations;

namespace Just_project.Models
{
    public class ContactModel
    {
        [Required(ErrorMessage = "Имя обязательно")]
        [StringLength(30, ErrorMessage = "Имя не может быть длиннее 30 символов")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Электронная почта обязательна")]
        [EmailAddress(ErrorMessage = "Неверный адрес электронной почты")]
        public string Email { get; set; }

        [Phone(ErrorMessage = "Неверный номер телефона")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Сообщение обязательно")]
        [StringLength(5000, ErrorMessage = "Сообщение не может быть длиннее 5000 символов")]
        public string Message { get; set; }

        public bool IsSuccess { get; set; }
    }
}
