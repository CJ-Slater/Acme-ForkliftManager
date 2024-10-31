using CsvHelper.Configuration;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.File.Mappings
{
    public sealed class ForkliftMap : ClassMap<Domain.Entities.Forklift>
    {
        public ForkliftMap()
        {
            Map(m => m.Name).Name("name");
            Map(m => m.ModelNumber).Name("model_number");
            Map(m => m.ManufacturingDate).Name("manufacturing_date");
        }
    }
}
