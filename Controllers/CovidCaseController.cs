using Assignment2.Models;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment2.Controllers
{
    /**
     * Controller for covid case
     * @Author: Yang Yang
     */
    public class CovidCaseController : Controller
    {
        //private readonly ICovidCaseRepository _repo = new MockCovidCaseRepository();
        private readonly ICovidCaseRepository _repo = new CovidCaseRepository();
        // GET: CovidCase
        public ActionResult Index(string searchBy, string search)
        {
            if (searchBy == null || search == null)
            {
                var data = _repo.GetAll();
                return View(data);
            }
            else {
                var data = _repo.Search(searchBy,search);
                return View(data);
            }
            
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CovidCase caze)
        {
            _repo.Add(caze);
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            CovidCase caze = _repo.GetWithId(id);
            return View(caze);
        }

        public ActionResult Edit(int Id)
        {

            CovidCase updateCase = _repo.GetAll().Where(c => c.Id == Id).FirstOrDefault();

            return View(updateCase);
        }

        [HttpPost]
        public ActionResult Edit(CovidCase caze)
        {

            _repo.Update(caze);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {

            _repo.Delete(id);

            return RedirectToAction("Index");
        }

        public ActionResult SaveToFile()
        {

            var data = _repo.GetAll();
            string filePath = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/") + "covid19-test.csv";
            using (var writer = new StreamWriter(filePath))
            using (var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                /*
                csvWriter.Configuration.Delimiter = ",";
                csvWriter.Configuration.HasHeaderRecord = true;
                csvWriter.Configuration.AutoMap<CovidCase>();
                */

                csvWriter.WriteHeader<CovidCase>();
                csvWriter.NextRecord();
                csvWriter.WriteRecords(data);

                writer.Flush();
            }

            return RedirectToAction("Index");

        }

        public ActionResult Reload()
        {
            _repo.Reload();
            return RedirectToAction("Index");
        }
    }
}