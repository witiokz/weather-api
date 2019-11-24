using System;

namespace Domain.Dto
{
    public class CityDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Temperature { get; set; }

        public DateTime Updated { get; set; }
    }
}
