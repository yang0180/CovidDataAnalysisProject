using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Assignment2.Models
{
    /**
     * A real repository, all changes will be made to database
     * @Author: Yang Yang
     */
    public class CovidCaseRepository : ICovidCaseRepository
    {
        private static CovidCaseDbContext _covidCaseDbContext;

        /**
         *  default constructor to using default connection string( location database)
         */
        public CovidCaseRepository() {
            if( _covidCaseDbContext == null)
            {
                _covidCaseDbContext = new CovidCaseDbContext();
                TruncateTable();
                InitData(null);
            }
            
        }

        /**
        *  overloaded constructor which can specify database to connect
        */
        public CovidCaseRepository(string connectionString) {

            if (_covidCaseDbContext == null)
            {
                _covidCaseDbContext = new CovidCaseDbContext();
                _covidCaseDbContext.Database.Connection.ConnectionString = connectionString;
            }

        }

        /**
         *  A method for adding a covid case to database
         */
        public void Add(CovidCase caze)
        {
            //throw new NotImplementedException();
            _covidCaseDbContext.covidCases.Add(caze);
            _covidCaseDbContext.SaveChanges();
        }

        /**
         *  A method for deleting a covid case from database
         */
        public void Delete(int id)
        {
            //throw new NotImplementedException();
            CovidCase cazeToDelete = GetWithId(id);
            if (cazeToDelete != null) {
                _covidCaseDbContext.covidCases.Remove(cazeToDelete);
                _covidCaseDbContext.SaveChanges();
            }
           
        }

        /**
         *  A method for retrieving all covid cases
         */
        public IEnumerable<CovidCase> GetAll()
        {
            //throw new NotImplementedException();

            //retrieve all data from database and
            //save them to simple data structure which is List
            return _covidCaseDbContext.covidCases.ToList();
        }

        /**
         *  A method for retrieving one covid case by id
         */
        public CovidCase GetWithId(int id)
        {
            //throw new NotImplementedException();
            return _covidCaseDbContext.
                            covidCases.
                            FirstOrDefault<CovidCase>(c => c.Id == id);
        }

        /**
        *  same as get all
        */
        public IEnumerable<CovidCase> Reload()
        {
            //throw new NotImplementedException();
            //reload dataset by using database
            return GetAll();
        }

        /**
        *  A method for updating one covid case
        */
        public void Update(CovidCase caze)
        {
            //throw new NotImplementedException();
            CovidCase covidCaseToUpdate = GetWithId(caze.Id);
            if (covidCaseToUpdate != null)
            {

                covidCaseToUpdate.Numconf = caze.Numconf;
                covidCaseToUpdate.Numdeath = caze.Numdeath;
                covidCaseToUpdate.Numprob = caze.Numprob;
                covidCaseToUpdate.Numtoday = caze.Numtoday;
                covidCaseToUpdate.Numtotal = caze.Numtotal;
                covidCaseToUpdate.Prname = caze.Prname;
                covidCaseToUpdate.PrnameFR = caze.PrnameFR;
                covidCaseToUpdate.Pruid = caze.Pruid;
                covidCaseToUpdate.Date = caze.Date;
                _covidCaseDbContext.SaveChanges();
            }
        }

        /**
         *  delete all records in covid case table
         */
        public void TruncateTable() {
            _covidCaseDbContext.Database.ExecuteSqlCommand("TRUNCATE TABLE dbo.CovidCases");
        }

        /**
        *  read all cases from csv file and save to db
        */
        public static void InitData(string dataSourcePath)
        {
            //_cases = new List<CovidCase>();
            string filePath = "";
            //if nothing is passed in as path, using default path which is "App_Data/covid19-download.csv"
            if (String.IsNullOrWhiteSpace(dataSourcePath))
            {
                //filePath = HttpContext.Current.Server.MapPath("~/App_Data/") + "covid19-download.csv";
                filePath = AppDomain.CurrentDomain.GetData("DataDirectory").ToString() + "/covid19-download.csv";
            }
            else
            {
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
                            _covidCaseDbContext.covidCases.Add(caze);
                           
                        }
                    }
                    _covidCaseDbContext.SaveChanges();

                }
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        public IEnumerable<CovidCase> Search(string searchBy, string search)
        {
            if (searchBy.ToLower().Equals("name"))
            {
                return _covidCaseDbContext.covidCases.Where(c => c.Prname.Equals(search)).ToList();
            }

            if (searchBy.ToLower().Equals("namefr"))
            {
                return _covidCaseDbContext.covidCases.Where(c => c.PrnameFR.Equals(search)).ToList();
            }


            if (searchBy.ToLower().Equals("date"))
            {
                return _covidCaseDbContext.covidCases.Where(c => c.Date.Equals(search)).ToList();
            }

            return new List<CovidCase>();
        }
    }
}