using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Dto
{
    public class CityViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
