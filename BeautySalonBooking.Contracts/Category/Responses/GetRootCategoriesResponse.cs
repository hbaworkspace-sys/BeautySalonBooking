using BeautySalonBooking.Contracts.Category.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonBooking.Contracts.Category.Responses
{
    public class GetRootCategoriesResponse
    {
        public List<CategoryDto> Categories { get; set; } = new();
    }
}
