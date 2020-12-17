using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace We_Split
{
    
    class Trips
    {
       
    }
    abstract class TripsDAO
    {
        public abstract BindingList<TRIP> GetAll();
        public abstract void Add(TRIP trip);
        public abstract void Update(TRIP trip);
        public abstract void Delete(TRIP trip);
    }
    class TripsDAOsqlserver : TripsDAO
    {
        public override BindingList<TRIP> GetAll()
        {
            var db = new WP_Project2_WeSplitEntities();
            var result = new BindingList<TRIP>(db.TRIPs.ToList());
            return result;
        }
        public BindingList<TRIP> GetAllByStatusDisplayText(string statusDisplayText)
        {
            var db = new WP_Project2_WeSplitEntities();
            var fullTripsList = db.TRIPs.ToList();
            var fullStatusList = db.STATUS.ToList();

            var query = fullTripsList.Join(
                    fullStatusList,
                    t => t.Status,
                    s => s.StatusID,
                    (t, s) => new { Trips = t, StatusText = s.StatusDisplayText })
                    .Where(r => r.StatusText == statusDisplayText)
                    .Select(r => r.Trips);

            var result = new BindingList<TRIP>(query.ToList());
            return result;
        }
        public TRIP GetTripByTripID(int tripID)
        {
            var db = new WP_Project2_WeSplitEntities();
            var trips = db.TRIPs.ToList();
            var query = trips
                .Where(t => t.TripID == tripID);
            var result = query.ToList()[0];

            return result;
        }
        public override void Add(TRIP trip)
        {
            var db = new WP_Project2_WeSplitEntities();
            db.TRIPs.Add(trip);
            db.SaveChanges();
        }
        public override void Delete(TRIP trip)
        {
            var db = new WP_Project2_WeSplitEntities();
            db.TRIPs.Remove(trip);
            db.SaveChanges();
        }
        public override void Update(TRIP trip)
        {
            var db = new WP_Project2_WeSplitEntities();
            var old_info = db.TRIPs.Find(trip.TripID);
            old_info = trip;
            db.SaveChanges();
        }
    }
}
