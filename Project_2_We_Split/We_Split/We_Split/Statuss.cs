using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace We_Split
{
    class Statuss
    {
    }
    public abstract class StatusDAO
    {
        public abstract BindingList<STATUS> GetAll();
        public abstract void Add(STATUS info);
        public abstract void Delete(STATUS info);
        public abstract void Update(STATUS info);
    }
    class StatusDAOsqlserver : StatusDAO
    {
        public override BindingList<STATUS> GetAll()
        {
            var db = new WP_Project2_WeSplitEntities();
            var result = new BindingList<STATUS>(db.STATUS.ToList());
            return result;
        }
        public string GetStatusByTripID(int tripID)
        {
            var db = new WP_Project2_WeSplitEntities();
            var trips = db.TRIPs.ToList();
            var statuses = db.STATUS.ToList();
            var query = trips
                .Join(statuses,
                    t => t.Status,
                    s => s.StatusID,
                    (t, s) => new { Status = s.StatusDisplayText, TripID = t.TripID })
                .Where(r => r.TripID == tripID)
                .Select(r => r.Status);
            var result = query.ToList()[0];

            return result;
        }
        public override void Add(STATUS info)
        {
            var db = new WP_Project2_WeSplitEntities();
            db.STATUS.Add(info);
            db.SaveChanges();
        }
        public override void Delete(STATUS info)
        {
            var db = new WP_Project2_WeSplitEntities();
            db.STATUS.Remove(info);
            db.SaveChanges();
        }
        public override void Update(STATUS info)
        {
            var db = new WP_Project2_WeSplitEntities();
            var old_info = db.STATUS.Find(info.StatusID);
            old_info = info;
            db.SaveChanges();
        }
    }
}
