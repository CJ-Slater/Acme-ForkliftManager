using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class ForkliftDto
    {
        public string Name { get; init; }
        public string ModelNumber { get; init; }
        public DateTime ManufacturingDate { get; init; }

        public int Age => CalculateAge();

        private int CalculateAge()
        {
            var today = DateTime.Today;
            var age = today.Year - ManufacturingDate.Year;
            if (ManufacturingDate.Date > today.AddYears(-age)) age--;
            return age;
        }
    }
}
