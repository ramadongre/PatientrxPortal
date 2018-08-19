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

        public DateTime DisplayDate
        {
            get
            {
                return (UpdatedOn != null && UpdatedOn > CreateDate ? Convert.ToDateTime(UpdatedOn) : CreateDate);
            }
        }

        public string GetAllPrescriptions
        {
            get
            {

                StringBuilder sb = new StringBuilder();

                if (!string.IsNullOrEmpty(Prescription1))
                    sb.Append(Prescription1);

                if (!string.IsNullOrEmpty(Prescription2))
                {
                    if (sb.ToString().Length > 0)
                        sb.Append(Environment.NewLine);
                    sb.Append(Prescription2);
                }

                if (!string.IsNullOrEmpty(Prescription3))
                {
                    if (sb.ToString().Length > 0)
                        sb.Append(Environment.NewLine);
                    sb.Append(Prescription3);
                }

                if (!string.IsNullOrEmpty(Prescription4))
                {
                    if (sb.ToString().Length > 0)
                        sb.Append(Environment.NewLine);
                    sb.Append(Prescription4);
                }

                if (!string.IsNullOrEmpty(Prescription5))
                {
                    if (sb.ToString().Length > 0)
                        sb.Append(Environment.NewLine);
                    sb.Append(Prescription5);
                }

                return sb.ToString();
            }
        }
    }
}
