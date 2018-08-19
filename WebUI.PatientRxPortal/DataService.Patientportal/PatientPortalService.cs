using DataAccessLayer.Patientportal;
using DataContracts.PatientPortal;
using ServiceContracts.PatientPortal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Patientportal
{
    public class PatientPortalService : IPortalInterafce
    {
        public CommonStatus GetPatients(string FirstName, string LastName, int LoggedInuserID)
        {
            ProcessData p = new ProcessData();
            CommonStatus cs = new CommonStatus(false);

            try
            {
                cs = p.GetPatients(FirstName, LastName, LoggedInuserID);
            }
            catch (Exception ex)
            {
                cs.Set(false, ex.Message, null);
            }
            finally
            {
                p = null;
            }

            return cs;
        }
        public CommonStatus AddUpdatePatient(Patient patient, int LoggedInuserID)
        {
            ProcessData p = new ProcessData();
            CommonStatus cs = new CommonStatus(false);

            try
            {
                cs = p.AddUpdatePatient(patient, LoggedInuserID);
            }
            catch (Exception ex)
            {
                cs.Set(false, ex.Message, null);
            }
            finally
            {
                p = null;
            }

            return cs;
        }
        public CommonStatus AddUpdatePatientRxData(int patientID, RxData rxData, int LoggedInuserID)
        {
            ProcessData p = new ProcessData();
            CommonStatus cs = new CommonStatus(false);

            try
            {
                cs = p.AddUpdatePatientRxData(patientID, rxData, LoggedInuserID);
            }
            catch (Exception ex)
            {
                cs.Set(false, ex.Message, null);
            }
            finally
            {
                p = null;
            }

            return cs;
        }

        public CommonStatus VerifyLoginUser(string UserName, string Password)
        {
            ProcessData p = new ProcessData();
            CommonStatus cs = new CommonStatus(false);

            try
            {
                cs = p.VerifyLoginUser(UserName, Password);
            }
            catch (Exception ex)
            {
                cs.Set(false, ex.Message, null);
            }
            finally
            {
                p = null;
            }

            return cs;
        }

        public CommonStatus GetAllPatientRxs(int patientID, int LoggedInuserID)
        {
            ProcessData p = new ProcessData();
            CommonStatus cs = new CommonStatus(false);

            try
            {
                cs = p.GetAllPatientRxs(patientID, LoggedInuserID);
            }
            catch (Exception ex)
            {
                cs.Set(false, ex.Message, null);
            }
            finally
            {
                p = null;
            }

            return cs;
        }

        public CommonStatus GetPatient(int PatientID, int LoggedInuserID)
        {
            ProcessData p = new ProcessData();
            CommonStatus cs = new CommonStatus(false);

            try
            {
                cs = p.GetPatient(PatientID, LoggedInuserID);
            }
            catch (Exception ex)
            {
                cs.Set(false, ex.Message, null);
            }
            finally
            {
                p = null;
            }

            return cs;
        }
        public CommonStatus GetRxData(int RxDataID, int LoggedInuserID)
        {
            ProcessData p = new ProcessData();
            CommonStatus cs = new CommonStatus(false);

            try
            {
                cs = p.GetRxData(RxDataID, LoggedInuserID);
            }
            catch (Exception ex)
            {
                cs.Set(false, ex.Message, null);
            }
            finally
            {
                p = null;
            }

            return cs;
        }
    }
}
