using System;

namespace Domain.Filtering
{
    public class CityFilterModel
    {
        public string Name { get; set; }
        public DateTime? Updated { get; set; }
        public decimal? Temperature { get; set; }
        public int PageNumber { get; set; } = 0;
        public int PageSize { get; set; } = 5;
    }
}
