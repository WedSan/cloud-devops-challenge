using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class DentalHistory
    {
        public int ID { get; set; }

        public virtual User User { get; init; }

        public virtual List<DentalProcedure> Procedures { get; set; }

        public DateTime ConsultationDate { get; init; }

        public string ToothCondition { get; init; }

        public DentalHistory()
        {
        }

        public DentalHistory(int iD, User user, List<DentalProcedure> procedures, DateTime consultationDate, string toothCondition)
        {
            ID = iD;
            User = user;
            Procedures = procedures;
            ConsultationDate = consultationDate;
            ToothCondition = toothCondition;
        }
    }
}
