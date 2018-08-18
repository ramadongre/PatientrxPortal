using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContracts.PatientPortal
{
    public class RxData : AuditBase
    {
        public int RxData_ID;
        public DateTime RxDate;
        public string RxDoctor;
        public string Prescription1;
        public string Prescription2;
        public string Prescription3;
        public string Prescription4;
        public string Prescription5;        
    }
}
