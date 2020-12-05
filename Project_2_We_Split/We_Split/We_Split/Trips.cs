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
        public int TripID { get; set; }
        public string TripName { get; set; }
        public int Status { get; set; }
        public BindingList<string> Locations { get; set; }
        public BindingList<Members> MembersPerTrip { get; set; }
        public BindingList<string> TripImages { get; set; }
    }
    abstract class TripsDAO
    {
        public abstract BindingList<Trips> GetAll();
        public abstract void Add(Trips trip);
        public abstract void Update(Trips trip);
        public abstract void Delete(Trips trip);
    }
    //class TripsDAOsqlserver : TripsDAO
    //{
    //    public override BindingList<Trips> GetAll()
    //    {
    //        var db = new WP_Project2_WeSplitEntities();
    //        var result = new BindingList<TRIP>(db.TRIPs.ToList());
    //        return result;
    //    }
    //    public override void Add(Trips trip)
    //    {
    //        var db = new WP_Project2_WeSplitEntities();
    //        db.TRIPs.Add(trip);
    //        db.SaveChanges();
    //    }
    //    public override void Delete(Trips trip)
    //    {
    //        var db = new WP_Project2_WeSplitEntities();
    //        db.TRIPs.Remove(trip);
    //        db.SaveChanges();
    //    }
    //    public override void Update(Trips trip)
    //    {
    //        var db = new WP_Project2_WeSplitEntities();
    //        var old_info = db.TRIPs.Find(trip.TripID);
    //        old_info = trip;
    //        db.SaveChanges();
    //    }
    //}
}
