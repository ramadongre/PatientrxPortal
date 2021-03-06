﻿using DataContracts.PatientPortal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUI.PatientRxPortal.Models;

namespace WebUI.PatientRxPortal.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Landing()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult ManagePatients()
        {
            return View();
        }
        public ActionResult ManagePatientList()
        {
            List<Patient> lstPatients = new List<Patient>();

            DataService.Patientportal.PatientPortalService cls = new DataService.Patientportal.PatientPortalService();

            try
            {
                //the UI grid would allow filter by firstname & lastname and not from the DB
                CommonStatus cs = cls.GetPatients("", "", base.GetLoggedinUserID());

                if (cs != null && cs.OpStatus)
                {
                    if (cs.OpPayload != null && ((List<Patient>)cs.OpPayload).Count() > 0)
                        lstPatients.AddRange(((List<Patient>)cs.OpPayload).OrderByDescending(a => a.DisplayDate).ToList());
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "An error occured");
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                cls = null;
            }

            return PartialView(lstPatients);
        }

        [HttpPost]
        public ActionResult AddNewPatient(PatientViewModel input)
        {
            string status = string.Empty;
            string message = string.Empty;

            if (ModelState.IsValid)
            {
                DataService.Patientportal.PatientPortalService cls = new DataService.Patientportal.PatientPortalService();

                try
                {
                    Patient p = new Patient();
                    p.First_Name = input.First_Name;
                    p.Last_Name = input.Last_Name;
                    p.DateOfBirth = Convert.ToDateTime(input.DateOfBirth);
                    p.PhoneNumber = input.PhoneNumber;
                    p.Patient_ID = 0;

                    CommonStatus cs = cls.AddUpdatePatient(p, base.GetLoggedinUserID());

                    if (cs != null && cs.OpStatus)
                    {
                        status = "OK"; message = "";
                    }
                    else
                    {
                        status = "ERROR"; message = cs.OpMessage;
                    }
                }
                catch (Exception ex)
                {
                    status = "ERROR";
                    message = ex.Message;
                }
                finally
                {
                    cls = null;
                }
            }

            var jsonData = new
            {
                status = status,
                message = message
            };


            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AddNewPatient()
        {
            return PartialView(new PatientViewModel());
        }


        [HttpGet]
        public ActionResult EditPatient(int PatientID)
        {
            PatientViewModel model = new PatientViewModel();

            DataService.Patientportal.PatientPortalService cls = new DataService.Patientportal.PatientPortalService();

            try
            {

                CommonStatus cs = cls.GetPatient(PatientID, base.GetLoggedinUserID());

                if (cs != null && cs.OpStatus)
                {
                    Patient p = (Patient)cs.OpPayload;

                    model.PatientID = p.Patient_ID;
                    model.First_Name = p.First_Name;
                    model.Last_Name = p.Last_Name;
                    model.DateOfBirth = p.DateOfBirth;
                    model.PhoneNumber = p.PhoneNumber;
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                cls = null;
            }

            ViewBag.FirstName = model.First_Name;
            ViewBag.LastName = model.Last_Name;

            return PartialView(model);
        }

        [HttpPost]
        public ActionResult EditPatient(PatientViewModel input)
        {
            string status = string.Empty;
            string message = string.Empty;

            if (ModelState.IsValid)
            {
                DataService.Patientportal.PatientPortalService cls = new DataService.Patientportal.PatientPortalService();

                try
                {
                    Patient p = new Patient();
                    p.Patient_ID = input.PatientID;
                    p.First_Name = input.First_Name;
                    p.Last_Name = input.Last_Name;
                    p.DateOfBirth = Convert.ToDateTime(input.DateOfBirth);
                    p.PhoneNumber = input.PhoneNumber;

                    CommonStatus cs = cls.AddUpdatePatient(p, base.GetLoggedinUserID());

                    if (cs != null && cs.OpStatus)
                    {
                        status = "OK"; message = "";
                    }
                    else
                    {
                        status = "ERROR"; message = cs.OpMessage;
                    }
                }
                catch (Exception ex)
                {
                    status = "ERROR";
                    message = ex.Message;
                }
                finally
                {
                    cls = null;
                }
            }

            var jsonData = new
            {
                status = status,
                message = message
            };


            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ManageRx(int PatientID, string FirstName, string LastName)
        {
            ViewBag.FirstName = FirstName;
            ViewBag.LastName = LastName;
            ViewBag.PatientID = PatientID;

            return View();
        }
        public ActionResult ManageRxList(int PatientID, string FirstName, string LastName)
        {
            List<RxData> lstRx = new List<RxData>();

            DataService.Patientportal.PatientPortalService cls = new DataService.Patientportal.PatientPortalService();

            try
            {
                //the UI grid would allow filter by firstname & lastname and not from the DB
                CommonStatus cs = cls.GetAllPatientRxs(PatientID, base.GetLoggedinUserID());

                if (cs != null && cs.OpStatus)
                {
                    if (cs.OpPayload != null && ((List<RxData>)cs.OpPayload).Count() > 0)
                        lstRx.AddRange(((List<RxData>)cs.OpPayload).OrderByDescending(a => a.DisplayDate).ToList());
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "An error occured");
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                cls = null;
            }

            return PartialView(lstRx);
        }

        [HttpPost]
        public ActionResult AddNewrx(int PatientID, RxViewModel input)
        {
            string status = string.Empty;
            string message = string.Empty;

            if (ModelState.IsValid)
            {
                DataService.Patientportal.PatientPortalService cls = new DataService.Patientportal.PatientPortalService();

                try
                {
                    RxData p = new RxData();
                    p.RxDate = input.rxDate;
                    p.RxDoctor = input.rxDoctor;
                    p.Prescription1 = input.Prescription1;
                    p.Prescription2 = input.Prescription2;
                    p.Prescription3 = input.Prescription3;
                    p.Prescription4 = input.Prescription4;
                    p.Prescription5 = input.Prescription5;
                    p.RxData_ID = 0;

                    CommonStatus cs = cls.AddUpdatePatientRxData(PatientID, p, base.GetLoggedinUserID());

                    if (cs != null && cs.OpStatus)
                    {
                        status = "OK"; message = "";
                    }
                    else
                    {
                        status = "ERROR"; message = cs.OpMessage;
                    }
                }
                catch (Exception ex)
                {
                    status = "ERROR";
                    message = ex.Message;
                }
                finally
                {
                    cls = null;
                }
            }

            var jsonData = new
            {
                status = status,
                message = message
            };


            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AddNewRx(int PatientID)
        {
            return PartialView(new RxViewModel());
        }

        [HttpGet]
        public ActionResult Editrx(int PatientID, int RxDataID)
        {
            RxViewModel p = new RxViewModel();

            DataService.Patientportal.PatientPortalService cls = new DataService.Patientportal.PatientPortalService();

            try
            {
                CommonStatus cs = cls.GetRxData(RxDataID, base.GetLoggedinUserID());

                if (cs != null && cs.OpStatus)
                {
                    RxData input = (RxData)cs.OpPayload;

                    p.RxDataID = input.RxData_ID;
                    p.rxDate = input.RxDate;
                    p.rxDoctor = input.RxDoctor;
                    p.Prescription1 = input.Prescription1;
                    p.Prescription2 = input.Prescription2;
                    p.Prescription3 = input.Prescription3;
                    p.Prescription4 = input.Prescription4;
                    p.Prescription5 = input.Prescription5;
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                cls = null;
            }

            return PartialView(p);
        }

        [HttpPost]
        public ActionResult EditRx(int PatientID, RxViewModel input)
        {
            string status = string.Empty;
            string message = string.Empty;

            if (ModelState.IsValid)
            {
                DataService.Patientportal.PatientPortalService cls = new DataService.Patientportal.PatientPortalService();

                try
                {
                    RxData p = new RxData();
                    p.RxData_ID = input.RxDataID;
                    p.RxDate = input.rxDate;
                    p.RxDoctor = input.rxDoctor;
                    p.Prescription1 = input.Prescription1;
                    p.Prescription2 = input.Prescription2;
                    p.Prescription3 = input.Prescription3;
                    p.Prescription4 = input.Prescription4;
                    p.Prescription5 = input.Prescription5;

                    CommonStatus cs = cls.AddUpdatePatientRxData(PatientID, p, base.GetLoggedinUserID());

                    if (cs != null && cs.OpStatus)
                    {
                        status = "OK"; message = "";
                    }
                    else
                    {
                        status = "ERROR"; message = cs.OpMessage;
                    }
                }
                catch (Exception ex)
                {
                    status = "ERROR";
                    message = ex.Message;
                }
                finally
                {
                    cls = null;
                }
            }

            var jsonData = new
            {
                status = status,
                message = message
            };


            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
    }
}