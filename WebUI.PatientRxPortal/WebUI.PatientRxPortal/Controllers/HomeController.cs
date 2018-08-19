using DataContracts.PatientPortal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
        /*
        [HttpPost]
        public ActionResult AddNewUser(FennecUserModel input)
        {
            string status = string.Empty;
            string message = string.Empty;
             
            if (ModelState.IsValid)
            {
                try
                {

                    FennecUser user = new FennecUser();
                    user.User_Name = input.UserName;
                    user.Display_Name = input.DisplayName;
                    user.Domain_Name = input.DomainName;
                    user.IsActive = true;
                    user.User_Password = input.UserPassword;
                    user.User_Type = new FennecAuthBase();
                    user.User_Type.ID = input.UserTypeID;

                    user.User_Roles = new List<FennecAuthBase>();
                    foreach (var s in input.UserRoles.Where(a => a.IsSelected))
                    {
                        user.User_Roles.Add(new FennecAuthBase() { ID = s.ID });
                    }

                    user.User_Systems = new List<FennecAuthBase>();
                    foreach (var s in input.UserSystems.Where(a => a.IsSelected))
                    {
                        user.User_Systems.Add(new FennecAuthBase() { ID = s.ID });
                    }

                    CommonStatus cs = _wcfService.InvokeService<IFennecWebUIDataService, CommonStatus>
                                          (svc => svc.AddUpdateFennecUser(user, User.Identity.Name));

                    if (cs.Status)
                    {
                        status = "OK"; message = "";
                    }
                    else
                    {
                        status = "ERROR"; message = cs.GetCombinedMessage();
                    }

                }
                catch (Exception ex)
                {
                    status = "ERROR";
                    message = ex.Message;

                    FennecLogger.Instance.Error(ex);
                }
            }

            var jsonData = new
            {
                status = status,
                message = message
            };


            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AddNewUser()
        {
            FennecUserModel model = new FennecUserModel();

            CommonStatusPayload<List<Tuple<string, FennecAuthBase>>> cs = _wcfService.InvokeService<IFennecWebUIDataService, CommonStatusPayload<List<Tuple<string, FennecAuthBase>>>>
                                      (svc => svc.GetFennecAuthorizationLookUps());

            if (cs.Status)
            {
                model.UserRoles = new List<FennecElement>();

                foreach (FennecAuthBase role in cs.Payload.Where(a => a.Item1 == "ROLE").Select(a => a.Item2).ToList())
                {
                    model.UserRoles.Add(new FennecElement() { ID = role.ID, Text = role.Name, IsSelected = false });
                }

                model.UserSystems = new List<FennecElement>();

                foreach (FennecAuthBase sys in cs.Payload.Where(a => a.Item1 == "SYSTEM").Select(a => a.Item2).ToList())
                {
                    model.UserSystems.Add(new FennecElement() { ID = sys.ID, Text = sys.Name, IsSelected = false });
                }

                model.UserTypes = new List<SelectListItem>();

                foreach (FennecAuthBase utype in cs.Payload.Where(a => a.Item1 == "USERTYPE").Select(a => a.Item2).ToList())
                {
                    model.UserTypes.Add(new SelectListItem() { Value = utype.ID.ToString(), Text = utype.Name });
                }

            }


            model.DomainName = Util.Helper.GetConfigValue("DefaultDomainNameForUserAdd");

            return PartialView(model);
        }

        private bool CheckMasterData(List<FennecAuthBase> data, long idToCheck)
        {
            bool doesDataExist = false;

            if (data != null && data.Count() > 0)
            {
                var t = data.Where(a => a.ID == idToCheck);

                if (t != null && t.Count() > 0)
                    doesDataExist = true;
            }

            return doesDataExist;
        }

        [HttpGet]
        public ActionResult EditUser(long userID)
        {
            FennecUserModel model = new FennecUserModel();

            CommonStatusPayload<List<Tuple<string, FennecAuthBase>>> cs1 = _wcfService.InvokeService<IFennecWebUIDataService, CommonStatusPayload<List<Tuple<string, FennecAuthBase>>>>
                                      (svc => svc.GetFennecAuthorizationLookUps());


            CommonStatusPayload<FennecUser> cs2 = _wcfService.InvokeService<IFennecWebUIDataService, CommonStatusPayload<FennecUser>>
                                      (svc => svc.GetFennecUserByID(userID));

            if (cs1.Status)
            {
                model.UserRoles = new List<FennecElement>();

                foreach (FennecAuthBase role in cs1.Payload.Where(a => a.Item1 == "ROLE").Select(a => a.Item2).ToList())
                {
                    model.UserRoles.Add(new FennecElement() { ID = role.ID, Text = role.Name, IsSelected = CheckMasterData(cs2.Payload.User_Roles, role.ID) });
                }

                model.UserSystems = new List<FennecElement>();

                foreach (FennecAuthBase sys in cs1.Payload.Where(a => a.Item1 == "SYSTEM").Select(a => a.Item2).ToList())
                {
                    model.UserSystems.Add(new FennecElement() { ID = sys.ID, Text = sys.Name, IsSelected = CheckMasterData(cs2.Payload.User_Systems, sys.ID) });
                }

                model.UserTypes = new List<SelectListItem>();

                foreach (FennecAuthBase utype in cs1.Payload.Where(a => a.Item1 == "USERTYPE").Select(a => a.Item2).ToList())
                {
                    model.UserTypes.Add(new SelectListItem() { Value = utype.ID.ToString(), Text = utype.Name, Selected = (cs2.Payload.User_Type.ID == utype.ID) });
                }
            }

            model.DisplayName = cs2.Payload.Display_Name;
            model.DomainName = cs2.Payload.Domain_Name;
            model.IsActive = cs2.Payload.IsActive;
            model.UserID = cs2.Payload.User_ID ?? Convert.ToInt32(cs2.Payload.User_ID);
            model.UserName = cs2.Payload.User_Name;
            model.OriginalUserName = cs2.Payload.User_Name;//used for edit check
            model.UserTypeID = cs2.Payload.User_Type.ID;
            model.UserPassword = HttpUtility.UrlEncode(cs2.Payload.User_Password);

            return PartialView(model);
        }

        [HttpPost]
        public ActionResult EditUser(FennecUserModel input)
        {
            string status = string.Empty;
            string message = string.Empty;

            if (ModelState.IsValid)
            {
                try
                {

                    FennecUser user = new FennecUser();
                    user.User_ID = input.UserID;
                    user.User_Name = input.UserName;
                    user.Display_Name = input.DisplayName;
                    user.Domain_Name = input.DomainName;
                    user.IsActive = input.IsActive;
                    if (!string.IsNullOrEmpty(input.NewUserPassword))
                        user.User_Password = input.NewUserPassword;
                    else
                        user.User_Password = null;
                    user.User_Type = new FennecAuthBase();
                    user.User_Type.ID = input.UserTypeID;

                    user.User_Roles = new List<FennecAuthBase>();
                    foreach (var s in input.UserRoles.Where(a => a.IsSelected))
                    {
                        user.User_Roles.Add(new FennecAuthBase() { ID = s.ID });
                    }

                    user.User_Systems = new List<FennecAuthBase>();
                    foreach (var s in input.UserSystems.Where(a => a.IsSelected))
                    {
                        user.User_Systems.Add(new FennecAuthBase() { ID = s.ID });
                    }

                    CommonStatus cs = _wcfService.InvokeService<IFennecWebUIDataService, CommonStatus>
                                          (svc => svc.AddUpdateFennecUser(user, User.Identity.Name));

                    if (cs.Status)
                    {
                        status = "OK"; message = "";

                        if (User.Identity.Name.ToLower() == user.User_Name.ToLower())
                        {
                            //clear the cache so credentials can be reloaded on next server get
                            string _authCacheKey = HttpContext.Session.SessionID + "_UserAuth";
                            CacheManager<GenericPrincipal>.Remove(_authCacheKey, "AUTH");
                        }
                    }
                    else
                    {
                        status = "ERROR"; message = cs.GetCombinedMessage();
                    }

                }
                catch (Exception ex)
                {
                    status = "ERROR";
                    message = ex.Message;

                    FennecLogger.Instance.Error(ex);
                }
            }

            var jsonData = new
            {
                status = status,
                message = message
            };


            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }*/
    }
}