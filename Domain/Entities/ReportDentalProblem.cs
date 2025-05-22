using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ReportDentalProblem
    {
        public int Id { get; set; }

        public string Problem { get; set; }

        public virtual MonitoringData MonitoringData { get; set; }

        public ReportDentalProblem()
        {
        }

        public ReportDentalProblem(int id, string problem, MonitoringData dentalHistory)
        {
            Id = id;
            Problem = problem;
            MonitoringData = dentalHistory;
        }
    }
}
