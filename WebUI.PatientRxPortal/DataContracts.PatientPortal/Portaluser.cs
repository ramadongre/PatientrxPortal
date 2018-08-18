using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContracts.PatientPortal
{
    public class PortalUser : AuditBase
    {
        public int PortalUser_ID;
        public string UserName;
        public string DisplayName;
        public string HashedPassword;
        public bool IsActive;
    }
}
