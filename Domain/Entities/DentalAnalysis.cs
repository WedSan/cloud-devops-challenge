using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class DentalAnalysis
    {
        public int Id { get; set; }

        public virtual User User { get; init; }

        public DateTime AnalysisDate { get; init; }

        public float ProbabilityProblem { get; set; }

        public virtual List<MonitoringData> MonitoringDataList { get; init; }

        public DentalAnalysis()
        {
        }

        public DentalAnalysis(int id, User user, DateTime analysisDate, float probabilityProblem, List<MonitoringData> monitoringDataList)
        {
            Id = id;
            User = user;
            AnalysisDate = analysisDate;
            ProbabilityProblem = probabilityProblem;
            MonitoringDataList = monitoringDataList;
        }
    }
}
