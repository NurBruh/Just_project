using System.ComponentModel.DataAnnotations;

namespace Just_project.Models
{
    public class Message
    {
            [Required(ErrorMessage = "Поле имени обязательно к заполнению")]
            public required string name { get; set; }
            public required string Email { get; set; }
            [Required(ErrorMessage = "Поле номера обязательно к заполнению")]
            public required string Phone { get; set; }

            [Required(ErrorMessage = "Поле email обязательно к заполнению")]
            [EmailAddress(ErrorMessage = "Указан не корректный email адресс")]
            public required string message { get; set; }
        
    }

}
