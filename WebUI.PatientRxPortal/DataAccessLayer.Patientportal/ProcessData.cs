using AutoMapper;
using DataContracts.PatientPortal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Patientportal
{
    public class ProcessData
    {
        //static bool isMapperInitialized = false;

        static ProcessData()
        {
            //if (!isMapperInitialized)
            //{
            //    Mapper.Initialize(cfg => cfg.CreateMap<TB_Patient, Patient>());
            //    Mapper.Initialize(cfg => cfg.CreateMap<TB_RxData, RxData>());
            //    Mapper.Initialize(cfg => cfg.CreateMap<TB_PortalUser, PortalUser>());
            //    isMapperInitialized = true;
            //}
        }

        private DateTime GetMaxof(DateTime? upDate, DateTime cDate)
        {
            return (upDate != null && Convert.ToDateTime(upDate) > cDate ? Convert.ToDateTime(upDate) : cDate);
        }

        public CommonStatus GetPatients(string FirstName, string LastName, int LoggedInuserID)
        {

            CommonStatus cs = new CommonStatus(false);

            try
            {

                using (PatientPortalEntities ent = new PatientPortalEntities())
                {
                    List<Patient> patients = new List<Patient>();
                    RxData rdata = new RxData();

                    var pts = from p in ent.TB_Patient /*where (p.First_Name.Contains(FirstName) || p.Last_Name.Contains(LastName))*/ select p;
                    foreach (TB_Patient dbp in pts)
                    {
                        //Patient p = Mapper.Map<Patient>(dbp);
                        Patient p = new Patient();
                        p.Patient_ID = dbp.Patient_ID;
                        p.First_Name = dbp.First_Name;
                        p.Last_Name = dbp.Last_Name;
                        p.DateOfBirth = dbp.DateOfBirth;
                        p.PhoneNumber = dbp.PhoneNumber;
                        p.DisplayDate = (dbp.UpdateDate != null && dbp.UpdateDate > dbp.CreateDate ? dbp.UpdateDate : dbp.CreateDate);

                        p.LastestRxData = new RxData();

                        //select all Rxs for patient
                        var rx = from prd in ent.TB_PatientRxData
                                 join rd in ent.TB_RxData on prd.RxData_ID equals rd.RxData_ID
                                 join pt in ent.TB_Patient on prd.Patient_ID equals pt.Patient_ID
                                 where (prd.Patient_ID == p.Patient_ID)
                                 //orderby rd.CreateDate descending, rd.UpdateDate descending
                                 select new
                                 {
                                     rxid = rd.RxData_ID,
                                     uddate = (rd.UpdateDate != null && rd.UpdateDate > rd.CreateDate ? rd.UpdateDate : rd.CreateDate)
                                 };

                        if (rx != null && rx.FirstOrDefault() != null)
                        {
                            var latestRxId = (from lrid in rx orderby lrid.uddate descending select lrid.rxid).FirstOrDefault();

                            var latestRX = (from s in ent.TB_RxData where s.RxData_ID == latestRxId select s).FirstOrDefault();

                            RxData d = new RxData();
                            d.RxData_ID = latestRxId;
                            d.RxDate = latestRX.RxDate;
                            d.RxDoctor = latestRX.RxDoctor;
                            d.Prescription1 = latestRX.Prescription1;
                            d.Prescription2 = latestRX.Prescription2;
                            d.Prescription3 = latestRX.Prescription3;
                            d.Prescription4 = latestRX.Prescription4;
                            d.Prescription5 = latestRX.Prescription5;

                            p.LastestRxData = d;

                            //p.LastestRxData = Mapper.Map<RxData>(rx.FirstOrDefault());
                        }
                        patients.Add(p);
                    }

                    cs.Set(true, "", patients);

                }
            }
            catch (Exception ex)
            {
                cs.Set(false, ex.Message, null);
            }

            return cs;

        }
        public CommonStatus AddUpdatePatient(Patient patient, int LoggedInuserID)
        {
            CommonStatus cs = new CommonStatus(false);

            try
            {
                using (PatientPortalEntities ent = new PatientPortalEntities())
                {
                    var username = (from u in ent.TB_PortalUser where u.PortalUser_ID == LoggedInuserID select u.UserName).ToString();

                    if (patient.Patient_ID == 0)
                    {
                        TB_Patient tp = new TB_Patient();
                        tp.First_Name = patient.First_Name;
                        tp.Last_Name = patient.Last_Name;
                        tp.DateOfBirth = patient.DateOfBirth;
                        tp.PhoneNumber = patient.PhoneNumber;
                        tp.IsActive = true;
                        tp.CreateDate = DateTime.Now;
                        tp.CreatedBy = username;

                        ent.TB_Patient.Add(tp);
                        ent.SaveChanges();
                    }
                    else
                    {
                        var pt = (from p in ent.TB_Patient where p.Patient_ID == patient.Patient_ID select p).FirstOrDefault();

                        if (pt != null)
                        {
                            pt.First_Name = patient.First_Name;
                            pt.Last_Name = patient.Last_Name;
                            pt.DateOfBirth = patient.DateOfBirth;
                            pt.PhoneNumber = patient.PhoneNumber;
                            pt.UpdateDate = DateTime.Now;
                            pt.UpdatedBy = username;

                            ent.SaveChanges();
                        }
                    }

                    cs.Set(true, "", null);

                }
            }
            catch (Exception ex)
            {
                cs.Set(false, ex.Message, null);
            }

            return cs;
        }
        public CommonStatus AddUpdatePatientRxData(int patientID, RxData rxData, int LoggedInuserID)
        {
            CommonStatus cs = new CommonStatus(false);

            try
            {
                using (PatientPortalEntities ent = new PatientPortalEntities())
                {
                    var username = (from u in ent.TB_PortalUser where u.PortalUser_ID == LoggedInuserID select u.UserName).ToString();

                    if (rxData.RxData_ID == 0)
                    {
                        TB_RxData r = new TB_RxData();
                        r.RxDate = rxData.RxDate;
                        r.RxDoctor = rxData.RxDoctor;
                        r.Prescription1 = rxData.Prescription1;
                        r.Prescription2 = rxData.Prescription2;
                        r.Prescription3 = rxData.Prescription3;
                        r.Prescription4 = rxData.Prescription4;
                        r.Prescription5 = rxData.Prescription5;
                        r.CreateDate = DateTime.Now;
                        r.CreatedBy = username;

                        ent.TB_RxData.Add(r);
                        ent.SaveChanges();
                    }
                    else
                    {
                        var r = (from p in ent.TB_RxData where p.RxData_ID == rxData.RxData_ID select p).FirstOrDefault();

                        if (r != null)
                        {
                            r.RxDate = rxData.RxDate;
                            r.RxDoctor = rxData.RxDoctor;
                            r.Prescription1 = rxData.Prescription1;
                            r.Prescription2 = rxData.Prescription2;
                            r.Prescription3 = rxData.Prescription3;
                            r.Prescription4 = rxData.Prescription4;
                            r.Prescription5 = rxData.Prescription5;
                            r.UpdateDate = DateTime.Now;
                            r.UpdatedBy = username;

                            ent.SaveChanges();
                        }
                    }

                    cs.Set(true, "", null);

                }
            }
            catch (Exception ex)
            {
                cs.Set(false, ex.Message, null);
            }

            return cs;
        }

        public CommonStatus VerifyLoginUser(string UserName, string Password)
        {
            //password has been already hashed using MD5 in upper layers.

            CommonStatus cs = new CommonStatus(false);
            PortalUser user = null;

            try
            {

                using (PatientPortalEntities ent = new PatientPortalEntities())
                {
                    var u = (from p in ent.TB_PortalUser where p.UserName == UserName && p.HashedPassword == Password && p.IsActive == true select p).FirstOrDefault();

                    if (u != null)
                    {
                        user = new PortalUser();

                        user.PortalUser_ID = u.PortalUser_ID;
                        user.UserName = u.UserName;
                        user.DisplayName = u.DisplayName;
                        user.HashedPassword = u.HashedPassword;
                    }

                    cs.Set(true, "", user);

                }
            }
            catch (Exception ex)
            {
                cs.Set(false, ex.Message, null);
            }

            return cs;
        }
    }
}
