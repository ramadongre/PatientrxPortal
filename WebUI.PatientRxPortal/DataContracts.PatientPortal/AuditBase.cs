using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContracts.PatientPortal
{
    public class AuditBase
    {
        public string CreatedBy;
        public DateTime CreateDate;
        public string UpdatedBy;
        public DateTime? UpdatedOn;
    }
}
