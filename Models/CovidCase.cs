using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Assignment2.Models
{
    /**
     * Model class for covid case
     * @Author: Yang Yang
     */
    public class CovidCase
    {
        /**
         *  create variables for a covid case object
         * */
        public int Id { get; set; }
        public string Pruid { set; get; }
        public string Prname { set; get; }
        public string PrnameFR { set; get; }
        public string Date { set; get; }
        public string Numconf { set; get; }
        public string Numprob { set; get; }
        public string Numdeath { set; get; }
        public string Numtotal { set; get; }
        public string Numtoday { set; get; }
        public string Ratetotal { set; get; }


    }
}