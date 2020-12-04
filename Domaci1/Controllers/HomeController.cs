using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Domaci1.Controllers
{
    public class HomeController : Controller
    {
        Fakultet1Entities db2 = new Fakultet1Entities();
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
       
        public ActionResult About()
        {
            return View(db2.Students.ToList());
        }

        public ActionResult Predmet()
        {
            return View(db2.Predmets.ToList());
        }

        public ActionResult Ispit()
        {
            List<Student> ispitLis = db2.Students.ToList();
            ViewBag.ispitListaStd = new SelectList(ispitLis, "BrIndeksa", "BrIndeksa");

            List<Predmet> ispitPLis = db2.Predmets.ToList();
            ViewBag.ispitListaPredmet = new SelectList(ispitPLis, "IdP", "Naziv");

            return View(db2.Ispits.ToList());
        }

        public ActionResult Save()
        {
          //  db2.Students.ToList();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult DodajStudenta(Student student)
        {
            string message = "Uspesno sacuvano!";
            bool status = true;
            if (db2.Students.ToList().Count == 0)
            {
                student.IdS = 1;
            }
            else
            { student.IdS = db2.Students.Max(x => x.IdS) + 1; }
            
            db2.Students.Add(student);
            db2.SaveChanges();
            
            return Json(new { status=status, message=message}, JsonRequestBehavior.AllowGet);
            db2.Configuration.LazyLoadingEnabled = false;

        }
        [HttpPost]
        public ActionResult DodajPredmet(Predmet predmet)
        {
            string message = "Uspesno sacuvano!";
            bool status = true;
            if (db2.Predmets.ToList().Count == 0)
            {
                predmet.IdP = 1;
            }
            else
            { predmet.IdP = db2.Predmets.Max(x => x.IdP) + 1; }
            
            db2.Predmets.Add(predmet);
            db2.SaveChanges();

            return Json(new { status = status, message = message}, JsonRequestBehavior.AllowGet);
            db2.Configuration.LazyLoadingEnabled = false;

        }
        [HttpPost]
        public ActionResult DodajIspit(Ispit ispit)
        {
            string message = "Uspesno sacuvano!";
            bool status = true;
            if(db2.Ispits.ToList().Count == 0)
            {
                ispit.IdI = 1;
            } else
            { ispit.IdI = db2.Ispits.Max(x => x.IdI) + 1; }
            
            db2.Ispits.Add(ispit);
            db2.SaveChanges();

            return Json(new { status = status, message = message }, JsonRequestBehavior.AllowGet);
            db2.Configuration.LazyLoadingEnabled = false;

        }

        public ActionResult loaddata()
        {
            using (Fakultet1Entities db = new Fakultet1Entities())
            {
                var studList = db.Students.ToList();
                db.Configuration.LazyLoadingEnabled = false;
                return Json(new { data = studList }, JsonRequestBehavior.AllowGet);
            }


        }
        public ActionResult loaddata1()
        {
            using (Fakultet1Entities db = new Fakultet1Entities())
            {
                var studList = db.Predmets.ToList();
                db.Configuration.LazyLoadingEnabled = false;
                return Json(new { data = studList }, JsonRequestBehavior.AllowGet);
            }


        }
        public ActionResult loaddata2()
        {
            using (Fakultet1Entities db = new Fakultet1Entities())
            {
                var studList = db.Ispits.ToList();
                db.Configuration.LazyLoadingEnabled = false;
                return Json(new { data = studList }, JsonRequestBehavior.AllowGet);
            }


        }
        public ActionResult saveStudent(FakultetViewModel model)
        {
            try
            {
                Fakultet1Entities db = new Fakultet1Entities();

                Student s = new Student();
                s.BrIndeksa = model.BrIndeksa;
                s.IdS = model.IdS;
                s.Ime = model.Ime;
                s.Prezime = model.Prezime;
                s.Grad = model.Grad;

                db.Students.Add(s);
                db.SaveChanges();
                db.Configuration.LazyLoadingEnabled = false;

            }
            catch (Exception ex)
            {
                throw ex;
            }

            
            

            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult deleteStudent(int BrIndeksa)
        {
            var student = db2.Students.Where(x => x.BrIndeksa == BrIndeksa).FirstOrDefault();
            var ispit = db2.Ispits.Where(x => x.BrIndeksa == BrIndeksa).FirstOrDefault();
            var ok = false;
            while (ispit != null) {
                db2.Ispits.Remove(ispit);
                db2.SaveChanges();
                ispit = db2.Ispits.Where(x => x.BrIndeksa == BrIndeksa).FirstOrDefault();
                ok = true;
            }

            db2.Students.Remove(student);
            db2.SaveChanges();
          


            string message = "Uspesno obrisano!";
            bool status = true;
            
            return Json(new { status = status, message = message }, JsonRequestBehavior.AllowGet);
            db2.Configuration.LazyLoadingEnabled = false;
        }

        [HttpPost]
        public ActionResult deletePredmet(int IdP)
        {
            var predmet = db2.Predmets.Where(x => x.IdP == IdP).FirstOrDefault();
            var ispit = db2.Ispits.Where(x => x.IdP == IdP).FirstOrDefault();
            var ok = false;
            while (ispit != null)
            {
                db2.Ispits.Remove(ispit);
                db2.SaveChanges();
                ispit = db2.Ispits.Where(x => x.IdP == IdP).FirstOrDefault();
                ok = true;
            }
           
            db2.Predmets.Remove(predmet);
            db2.SaveChanges();
           
            string message = "Uspesno obrisano!";
            bool status = true;

            return Json(new { status = status, message = message }, JsonRequestBehavior.AllowGet);
            db2.Configuration.LazyLoadingEnabled = false;
        }

        [HttpPost]
        public ActionResult deleteIspit(int IdI)
        {
            var ispit = db2.Ispits.Where(x => x.IdI == IdI).FirstOrDefault();
            db2.Ispits.Remove(ispit);
            db2.SaveChanges();
           
            string message = "Uspesno obrisano!";
            bool status = true;

            return Json(new { status = status, message = message }, JsonRequestBehavior.AllowGet);
            db2.Configuration.LazyLoadingEnabled = false;
        }
        [HttpGet]
        public ActionResult GetStudent(int BrIndeksa)
        {
            Student stud = new Student();
            var student = db2.Students.Where(x => x.BrIndeksa == BrIndeksa).FirstOrDefault();
            stud.BrIndeksa = student.BrIndeksa;
            stud.IdS = student.IdS;
            stud.Ime = student.Ime;
            stud.Prezime = student.Prezime;
            stud.Grad = student.Grad;
            
            return Json(new { success = true, data = stud }, JsonRequestBehavior.AllowGet);
            db2.Configuration.LazyLoadingEnabled = false;
        }
        [HttpGet]
        public ActionResult GetStudentBrIndeksa()
        {
            var data = from s in db2.Students select new { s.BrIndeksa };

            return Json(data.ToList(), JsonRequestBehavior.AllowGet);
            db2.Configuration.LazyLoadingEnabled = false;
        }
        [HttpGet]
        public ActionResult GetPredmet(int IdP)
        {
            Predmet predm = new Predmet();
            var predmet = db2.Predmets.Where(x => x.IdP == IdP).FirstOrDefault();
            predm.IdP = predmet.IdP;
            predm.Naziv = predmet.Naziv;
            

            return Json(new { success = true, data = predm }, JsonRequestBehavior.AllowGet);
            db2.Configuration.LazyLoadingEnabled = false;
        }

        [HttpGet]
        public ActionResult GetIspit(int IdI)
        {
            Ispit ispit1 = new Ispit();
            var ispit = db2.Ispits.Where(x => x.IdI == IdI).FirstOrDefault();
            ispit1.IdI = ispit.IdI;
            ispit1.BrIndeksa = ispit.BrIndeksa;
            ispit1.IdP = ispit.IdP;
            ispit1.Ocena = ispit.Ocena;
            ispit1.Datum = ispit.Datum;
           
            return Json(new { success = true, data = ispit1 }, JsonRequestBehavior.AllowGet);
            db2.Configuration.LazyLoadingEnabled = false;
        }


        [HttpPost]
        public ActionResult UpdateStudenta(Student student)
        {
            string message = "Uspesno promenjeno!";
            bool status = true;
          //  var student1 = db2.Students.Where(x => x.BrIndeksa == student.BrIndeksa).FirstOrDefault();
          //  db2.Students.Remove(student1);

             db2.Entry(student).State = System.Data.Entity.EntityState.Modified;
            db2.SaveChanges();
            // db2.Students.Add(student);
            //  db2.SaveChanges();
            db2.Students.ToList();
            return Json(new { status = status, message = message }, JsonRequestBehavior.AllowGet);
            db2.Configuration.LazyLoadingEnabled = false;
        }
        [HttpPost]
        public ActionResult UpdatePredmeta(Predmet predmet)
        {
            string message = "Uspesno promenjeno!";
            bool status = true;
            //  var student1 = db2.Students.Where(x => x.BrIndeksa == student.BrIndeksa).FirstOrDefault();
            //  db2.Students.Remove(student1);

            db2.Entry(predmet).State = System.Data.Entity.EntityState.Modified;
            db2.SaveChanges();
            // db2.Students.Add(student);
            //  db2.SaveChanges();
            db2.Predmets.ToList();
            return Json(new { status = status, message = message }, JsonRequestBehavior.AllowGet);
            db2.Configuration.LazyLoadingEnabled = false;
        }

        [HttpPost]
        public ActionResult UpdateIspit(Ispit ispit)
        {
            string message = "Uspesno promenjeno!";
            bool status = true;
            //  var student1 = db2.Students.Where(x => x.BrIndeksa == student.BrIndeksa).FirstOrDefault();
            //  db2.Students.Remove(student1);

            db2.Entry(ispit).State = System.Data.Entity.EntityState.Modified;
            db2.SaveChanges();
            // db2.Students.Add(student);
            //  db2.SaveChanges();
            db2.Ispits.ToList();
            return Json(new { status = status, message = message }, JsonRequestBehavior.AllowGet);
            db2.Configuration.LazyLoadingEnabled = false;
        }
    }
}