using System.ComponentModel.DataAnnotations;

namespace Just_project.Models
{
    public class Message
    {
            [Required(ErrorMessage = "Поле имени обязательно к заполнению")]
            public string name { get; set; }
            public string Email { get; set; }
            [Required(ErrorMessage = "Поле номера обязательно к заполнению")]
            public string Phone { get; set; }

            [Required(ErrorMessage = "Поле email обязательно к заполнению")]
            [EmailAddress(ErrorMessage = "Указан не корректный email адресс")]
            public string message { get; set; }
        
    }

}
