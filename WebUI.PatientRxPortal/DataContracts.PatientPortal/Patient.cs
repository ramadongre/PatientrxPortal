using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContracts.PatientPortal
{
    public class Patient: AuditBase
    {
        public int Patient_ID;
        public string First_Name;
        public string Last_Name;
        public DateTime DateOfBirth;
        public string PhoneNumber;
        public bool IsActive;
        public RxData LastestRxData;
    }
}
