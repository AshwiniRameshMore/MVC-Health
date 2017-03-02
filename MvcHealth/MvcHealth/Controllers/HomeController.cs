using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcHealth.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {         
            return View();
        }
        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file)
        {

            if (file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/App_Data/uploads"), fileName);
                file.SaveAs(path);
               
            }

            return RedirectToAction("Index");
        }
        public ActionResult Data()
        {
            ViewBag.Hospital = viewHealthData();
            return View();
        }
        public ActionResult Add()
        {
            return View();
        }
        public ActionResult InsertDetails(FormCollection collection)
        {
            //DO LOGIC TO INSERT DETAILS
            ViewBag.result = "Uploaded successfully";
            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Hospital Charge Data";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public List<health> viewHealthData()
        {
            string connectString = System.Configuration.ConfigurationManager.ConnectionStrings["HealthDBConnectionString"].ToString();

            Linq2SQLDataContext db = new Linq2SQLDataContext(connectString);

            var Hospital = (from m in db.healths
                              orderby m.ID ascending
                              select m).ToList();
            return Hospital;
        }

        //[HttpGet]


        [HttpPost]
        public ActionResult getHealthData(String id)
        {
            string connectString = System.Configuration.ConfigurationManager.ConnectionStrings["HealthDBConnectionString"].ToString();
            Linq2SQLDataContext db = new Linq2SQLDataContext(connectString);      
            var Hospital = (from m in db.healths
                              where m.ID == Int32.Parse(id)
                              select m);
            return Json(Hospital.Single());
        }
        [HttpPost]
        public ActionResult redirectAdd()
        {
            return RedirectToAction("Add", "Home");
        }
        public ActionResult deleteHealthData(FormCollection collection)
        {
            string connectString = System.Configuration.ConfigurationManager.ConnectionStrings["HealthDBConnectionString"].ToString();
            Linq2SQLDataContext db = new Linq2SQLDataContext(connectString);
            health Hospital = db.healths.FirstOrDefault(e => e.ID == Int32.Parse(collection.Get("del_subj_id")));
            db.healths.DeleteOnSubmit(Hospital);
            db.SubmitChanges();
            return RedirectToAction("Data", "Home");
        }

        public ActionResult updateHealthData(FormCollection collection)
        {
            string connectString = System.Configuration.ConfigurationManager.ConnectionStrings["HealthDBConnectionString"].ToString();
            Linq2SQLDataContext db = new Linq2SQLDataContext(connectString);
            int id;
            bool isUpdate = int.TryParse(collection.Get("subj_id"), out id);
            health healthPopulate;
            if (isUpdate)
                healthPopulate = db.healths.FirstOrDefault(e => e.ID == id);
            else
                healthPopulate = new health();
            try
            {



                healthPopulate.DRG_Definition = collection.Get("DRG_Definition");
                healthPopulate.Provider_Id = (Double.Parse(collection.Get("Provider_Id")));
                healthPopulate.Provider_Name = collection.Get("Provider_Name");
                healthPopulate.Provider_Street_Address = collection.Get("Provider_Street_Address");
                healthPopulate.Provider_City = collection.Get("Provider_City");
                healthPopulate.Provider_State = collection.Get("Provider_State");
                healthPopulate.Provider_Zip_Code = (Double.Parse(collection.Get("Provider_Zip_Code")));
                healthPopulate.Hospital_Referral_Region_Description = collection.Get("Hospital_Referral_Region_Description");
                healthPopulate._Total_Discharges_ = (Double.Parse(collection.Get("_Total_Discharges_")));
                healthPopulate._Average_Covered_Charges_ = (Decimal.Parse(collection.Get("_Average_Covered_Charges_")));
                healthPopulate._Average_Total_Payments_ = (Decimal.Parse(collection.Get("_Average_Total_Payments_")));

                if (!isUpdate)
                    db.healths.InsertOnSubmit(healthPopulate);
                db.SubmitChanges();
                return RedirectToAction("Data", "Home");
            }
            catch(Exception e)
            {
                return RedirectToAction("Data", "Home");
            }
           
        }

        

    }
}