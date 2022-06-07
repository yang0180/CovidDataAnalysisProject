using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2.Models
{
    public interface ICovidCaseRepository
    {
        IEnumerable<CovidCase> GetAll();

        CovidCase GetWithId(int id);

        void Add(CovidCase caze);

        void Update(CovidCase caze);

        void Delete(int id);

        IEnumerable<CovidCase> Reload();

        void TruncateTable();

        IEnumerable<CovidCase> Search(string searchBy, string search);
    }
}
