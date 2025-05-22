using System.Runtime.CompilerServices;
using Castle.DynamicProxy.Internal;
using Microsoft.VisualBasic.CompilerServices;

namespace web.DTO.DataMonitoring
{
    public record UpdateMonitoringDataRequest(int UserId)
    {
        public UpdateMonitoringDataRequest() : this(0)
        {
            
        }
    }
}
