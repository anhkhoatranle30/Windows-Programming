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
        public int MemberID { get; set;}
        public string CostName { get; set; }
        public int Cost { get; set; }
    }
    class Trips
    {
        public int TripID { get; set; }
        public string TripName { get; set; }
        public int Status { get; set; }
        public BindingList<string> Locations { get; set; }
        public BindingList<int> MembersPerTrip { get; set; }
        public BindingList<MemberCosts> MemberCosts { get; set; }
        public BindingList<string> TripImages { get; set; }
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
