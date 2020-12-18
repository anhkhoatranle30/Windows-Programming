using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace We_Split
{
    class Locations
    {
    }
    public abstract class LocationDAO
    {
        public abstract BindingList<LOCATION> GetAll();
        public abstract void Add(LOCATION location);
        public abstract void Delete(LOCATION location);
        public abstract void Update(LOCATION location);

    }
    class LocationDAOsqlserver : LocationDAO
    {
        public override BindingList<LOCATION> GetAll()
        {
            var db = new WP_Project2_WeSplitEntities();
            var result = new BindingList<LOCATION>(db.LOCATIONs.ToList());
            return result;
        }
        public BindingList<LOCATION> GetLocationNameByTripID(int tripID)
        {
            var db = new WP_Project2_WeSplitEntities();
            var trips = db.TRIPs.ToList();
            var locations = db.LOCATIONs.ToList();
            var query = trips.Join(
                    locations,
                    t => t.TripID,
                    l => l.TripID,
                    (t, l) => new { TripID = t.TripID, Locations = l })
                .Where(x => x.TripID == tripID)
                .Select(r => r.Locations);
            var result = new BindingList<LOCATION>(query.ToList());
            return result;
        }
        public override void Add(LOCATION location)
        {
            var db = new WP_Project2_WeSplitEntities();
            db.LOCATIONs.Add(location);
            db.SaveChanges();
        }
        public override void Delete(LOCATION location)
        {
            var db = new WP_Project2_WeSplitEntities();
            db.LOCATIONs.Remove(location);
            db.SaveChanges();
        }
        public override void Update(LOCATION location)
        {
            var db = new WP_Project2_WeSplitEntities();
            var old_info = db.LOCATIONs.Find(location.LocationID);
            old_info = location;
            db.SaveChanges();
        }
    }
}
