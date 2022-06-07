using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Assignment2.Models
{
    public class MockCovidCaseRepository : ICovidCaseRepository
    {
        private static List<CovidCase> _cases;

        public MockCovidCaseRepository() {
            if (_cases == null) {
                InitData(null);
            }
        }

        public MockCovidCaseRepository(string dataSourcePath) {
            if (_cases == null)
            {
                InitData(dataSourcePath);
            }
        }

        public void Add(CovidCase caze)
        {
            int newId = _cases.Count + 1;
            caze.Id = newId;
            _cases.Add(caze);
        }

        public void Delete(int id)
        {
            CovidCase deleteCase = _cases.
                        FirstOrDefault<CovidCase>(cc => cc.Id == id);
            if(deleteCase != null)
            {
                _cases.Remove(deleteCase);
            }
            
        }

        public IEnumerable<CovidCase> GetAll()
        {
            return _cases;
        }

        public CovidCase GetWithId(int id)
        {
            return _cases.FirstOrDefault<CovidCase>(cc => cc.Id == id);
        }

        public IEnumerable<CovidCase> Reload()
        {
            InitData(null);
            return _cases;
        }

        public void Update(CovidCase caze)
        {
            CovidCase updateCase = _cases.
                        FirstOrDefault<CovidCase>(cc => cc.Id == caze.Id);
            if (updateCase != null) {
                updateCase.Pruid = caze.Pruid;
                updateCase.Prname = caze.Prname;
                updateCase.PrnameFR = caze.PrnameFR;
                updateCase.Numprob = caze.Numprob;
                updateCase.Numtoday = caze.Numtoday;
                updateCase.Numconf = caze.Numconf;
                updateCase.Numdeath = caze.Numdeath;
                updateCase.Numtotal = caze.Numtotal;
            }
        }

        public List<CovidCase> InitData(string dataSourcePath) {
            _cases = new List<CovidCase>();
            string filePath = "";
            //if nothing is passed in as path, using default path which is "App_Data/covid19-download.csv"
            if (String.IsNullOrWhiteSpace(dataSourcePath))
            {
                //filePath = HttpContext.Current.Server.MapPath("~/App_Data/") + "covid19-download.csv";
                filePath = AppDomain.CurrentDomain.GetData("DataDirectory").ToString() + "/covid19-download.csv";
            }
            else {
                // Otherwise, using path passed
                filePath = dataSourcePath;
            }

            //string filePath = AppDomain.CurrentDomain.GetData("DataDirectory").ToString() + "/covid19-download.csv";
            try
            {
                using (var reader = new StreamReader(filePath))
                {
                    //init a list for storing covid cases read from .csv
                    List<CovidCase> theCases = new List<CovidCase>();
                    int cursor = 1;
                    int primarykeyValue = 1;
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(',');
                        if (cursor <= 1)
                        {
                            cursor++;

                        }
                        else
                        {
                            CovidCase caze = new CovidCase()
                            {
                                Id = primarykeyValue,
                                Pruid = values[0],
                                Prname = values[1],
                                PrnameFR = values[2],
                                Date = values[3],
                                Numconf = values[5],
                                Numprob = values[6],
                                Numdeath = values[7],
                                Numtotal = values[8],
                                Numtoday = values[13],
                                Ratetotal = values[15],
                            };
                            // Add case to above list
                            theCases.Add(caze);
                            primarykeyValue++;
                        }
                    }

                    var casesArr = theCases.ToArray();
                    for (var i = 0; i < casesArr.Length; i++)
                    {
                        _cases.Add(casesArr[i]);
                    }

                }
                return _cases;
            }
            catch (Exception e) {
               Console.WriteLine( e.Message);
            }

            return null;
        }

        public void TruncateTable()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CovidCase> Search(string searchBy, string search)
        {
            throw new NotImplementedException();
        }
    }
}