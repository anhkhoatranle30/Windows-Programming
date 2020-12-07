using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace We_Split
{
    class MemberCosts
    {
    }
    public abstract class MemberCostsDAO
    {
        public abstract BindingList<MEMBERCOST> GetAll();
        public abstract void Add(MEMBERCOST membercost);
        public abstract void Delete(MEMBERCOST membercost);
        public abstract void Update(MEMBERCOST membercost);
    }
    class MemberCostsDAOsqlserver : MemberCostsDAO
    {
        public override BindingList<MEMBERCOST> GetAll()
        {
            var db = new WP_Project2_WeSplitEntities();
            var result = new BindingList<MEMBERCOST>(db.MEMBERCOSTs.ToList());
            return result;
        }
        public BindingList<MEMBERCOST> GetAllByTripID(int tripid)
        {
            var allList = GetAll();
            var query = from membercost in allList
                        where membercost.TripID == tripid
                        select membercost;
            var result = new BindingList<MEMBERCOST>(query.ToList());
            return result;
        }
        public override void Add(MEMBERCOST membercost)
        {
            var db = new WP_Project2_WeSplitEntities();
            db.MEMBERCOSTs.Add(membercost);
            db.SaveChanges();
        }
        public override void Delete(MEMBERCOST membercost)
        {
            var db = new WP_Project2_WeSplitEntities();
            db.MEMBERCOSTs.Remove(membercost);
            db.SaveChanges();
        }
        public override void Update(MEMBERCOST membercost)
        {
            var db = new WP_Project2_WeSplitEntities();
            var old_info = db.MEMBERCOSTs.Find(membercost.CostID);
            old_info = membercost;
            db.SaveChanges();
        }
    }
}
