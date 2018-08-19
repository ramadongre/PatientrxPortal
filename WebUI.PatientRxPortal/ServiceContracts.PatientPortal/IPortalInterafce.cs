﻿using DataContracts.PatientPortal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.PatientPortal
{
    public interface IPortalInterafce
    {
        //Get all patients for hospital while patient has one latest RxData
        CommonStatus GetPatients(string FirstName, string LastName, int LoggedInuserID);
        CommonStatus AddUpdatePatient(Patient patient, int LoggedInuserID);
        CommonStatus AddUpdatePatientRxData(int patientID, RxData rxData, int LoggedInuserID);
        CommonStatus GetAllPatientRxs(int patientID, int LoggedInuserID);
        CommonStatus VerifyLoginUser(string UserName, string Password);
        CommonStatus GetPatient(int PatientID, int LoggedInuserID);
        CommonStatus GetRxData(int RxDataID, int LoggedInuserID);
    }
}
