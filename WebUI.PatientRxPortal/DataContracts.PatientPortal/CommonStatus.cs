using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContracts.PatientPortal
{
    public class CommonStatus
    {
        private bool Status;
        private string Message;
        private object Payload;

        public bool OpStatus
        {
            get {
                return Status;
            }
        }

        public string OpMessage
        {
            get
            {
                return Message;
            }
        }

        public object OpPayload
        {
            get
            {
                return Payload;
            }
        }

        public CommonStatus(bool _Status)
        {
            Status = _Status;
            Message = "";
            Payload = null;
        }

        public CommonStatus(bool _Status, string _Message, object _Payload)
        {
            Status = _Status;
            Message = _Message;
            Payload = _Payload;
        }

        public void Set(bool _Status, string _Message, object _Payload)
        {
            Status = _Status;
            Message = _Message;
            Payload = _Payload;
        }
    }
}
