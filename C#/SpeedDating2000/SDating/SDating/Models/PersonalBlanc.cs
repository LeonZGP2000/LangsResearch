using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

/*
 Validation (client side):
https://docs.microsoft.com/en-us/aspnet/core/mvc/models/validation?view=aspnetcore-3.1
 */

namespace SDating.Models
{
    public class PersonalBlanc
    {
        [Required]
        [MinLength(1)]
        /// <summary>
        /// # Personal number in current Session
        /// </summary>
        public int TableId { get; set; }

        [Required]
        /// <summary>
        /// true - Man
        /// false - Girl
        /// </summary>
        public bool isMan { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Имя введено не полностью")]
        public string Name { get; set; }

        /// <summary>
        /// Root + "//Sessions//Session_{session ID}"/{TableId}_{Name}.jpg
        /// </summary>
        [DataType(DataType.Url)]
        public string Picture { get; set; }

        [DataType(DataType.ImageUrl)]
        public string PictureFullPath{ get; set; }

        /// <summary>
        /// Participants
        /// </summary>
        public List<PersonalBlanc> PersonalChoose { get; set; }

        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email участника не валидный")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Номер телефона в формате +380XXYYYYYY")]
        public string Phone { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Возраст введен не корректно")]
        public int Age { get; set; }

    }
}
