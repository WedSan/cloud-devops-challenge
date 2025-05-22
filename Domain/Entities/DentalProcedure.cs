using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class DentalProcedure
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual DentalHistory DentalHistory { get; set; }

        public DentalProcedure()
        {
        }

        public DentalProcedure(int id, string name, DentalHistory dentalHistory)
        {
            Id = id;
            Name = name;
            DentalHistory = dentalHistory;
        }
    }
}
