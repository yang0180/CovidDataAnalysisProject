using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Assignment2.Models
{
    [TestClass]
    public class CovidCaseReposTest
    {
        ICovidCaseRepository _repo;
        [TestInitialize]
        public void Init()
        {

            _repo = new CovidCaseRepository();
            _repo.TruncateTable();
        }

        [TestMethod]
        public void TestMain()
        {

            Test_Add();
            Test_GetAll();
        }


        //Does the program read in records, placing data into correct fields of record objects?
        public void Test_GetAll()
        {

            List<CovidCase> cases = (List<CovidCase>)_repo.GetAll();
            Assert.IsNotNull(cases);

            Assert.AreEqual(1, cases.Count);
            //Console.WriteLine("Output : "+AppDomain.CurrentDomain.BaseDirectory.ToString());
        }

        //Does the program add a new record into the sequential data structure?
        public void Test_Add()
        {
            List<CovidCase> cases = (List<CovidCase>)_repo.GetAll();
            Assert.IsNotNull(cases);
            int sizeBeforeAdd = cases.Count;
            _repo.Add(new CovidCase()
            {
                Id = cases.Count + 1,
                Pruid = "TestPruid",
                Prname = "TestName",
                PrnameFR = "TestNameFr"
            });
            int sizeAfterAdd = ((List<CovidCase>)_repo.GetAll()).Count;
            Assert.AreEqual(sizeBeforeAdd + 1, sizeAfterAdd);
        }
    }
}
