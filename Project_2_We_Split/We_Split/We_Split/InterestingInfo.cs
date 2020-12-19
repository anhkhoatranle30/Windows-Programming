using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace We_Split
{
    class InterestingInfo
    {
    }
    public abstract class InterestingInfoDAO
    {
        public abstract BindingList<INTERESTING_INFO> GetAll();
       
    }
    class InterestingInfoDAOsqlserver : InterestingInfoDAO
    {
        private readonly Random _random = new Random();
        public override BindingList<INTERESTING_INFO> GetAll()
        {
            var db = new WP_Project2_WeSplitEntities();
            var result = new BindingList<INTERESTING_INFO>(db.INTERESTING_INFO.ToList());
            return result;
        }
        public INTERESTING_INFO GetRandom()
        {
            var db = new WP_Project2_WeSplitEntities();
            var list = new BindingList<INTERESTING_INFO>(db.INTERESTING_INFO.ToList());
            int randomNumber = _random.Next(0, list.Count);
            var result = list[randomNumber];
            return result;
        }
    }
}
