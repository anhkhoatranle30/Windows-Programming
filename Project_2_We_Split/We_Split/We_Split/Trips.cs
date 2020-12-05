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
    //        var db_trip_list = new BindingList<TRIP>(db.TRIPs.ToList());
    //        var result = new BindingList<Trips>();
    //        foreach (var db_trip in db_trip_list)
    //        {
    //            var temp_location = new BindingList<string>();
    //            foreach (var db_trip_location in db_trip.LOCATIONs.ToList())
    //            {
    //                temp_location.Add(db_trip_location.LocationName);
    //            }

    //            var temp_tripimages = new BindingList<string>();
    //            foreach (var db_trip_images in db_trip.TRIPIMAGES.ToList())
    //            {
    //                temp_tripimages.Add(db_trip_images.Path);
    //            }

    //            var temp_memberspertrip = new BindingList<int>();
    //            var temp_membercost = new BindingList<MemberCosts>();
    //            foreach (var db_trip_memberspertrip in db_trip.MEMBERSPERTRIPs.ToList())
    //            {
    //                temp_memberspertrip.Add(db_trip_memberspertrip.MemberID);
    //                foreach(var db_trip_membercost in db_trip_memberspertrip.MEMBERCOSTs.ToList())
    //                {
    //                    temp_membercost.Add(new MemberCosts()
    //                    {
    //                        MemberID = db_trip_membercost.MemberID,
    //                        CostName = db_trip_membercost.CostName,
    //                        Cost = (int)db_trip_membercost.Cost
    //                    });

    //                }
    //            }

                
                

    //            result.Add(new Trips()
    //            {
    //                TripID = db_trip.TripID,
    //                TripName = db_trip.TripName,
    //                Status = (int)db_trip.Status,
    //                Locations = temp_location,
    //                TripImages = temp_tripimages,
    //                MembersPerTrip = temp_memberspertrip,
    //                MemberCosts = temp_membercost
    //            });
    //        }
    //        return result;
    //    }
    //    public override void Add(Trips trip)
    //    {
    //        var db = new WP_Project2_WeSplitEntities();
    //        var newTrip = new TRIP()
    //        {
    //            TripID = trip.TripID,
    //            TripName = trip.TripName,
    //            Status = trip.Status
    //        };
    //        db.TRIPs.Add(newTrip);

    //        foreach(var newLocation in trip.Locations)
    //        {
    //            db.LOCATIONs.Add(new LOCATION()
    //            {
    //                TripID = trip.TripID,
    //                LocationName = newLocation
    //            });
    //        }

    //        foreach(var member in trip.MembersPerTrip)
    //        {
    //            db.MEMBERSPERTRIPs.Add(new MEMBERSPERTRIP()
    //            {
    //                MemberID = member,
    //                TripID = trip.TripID
    //            });
    //        }

    //        foreach(var membercost in trip.MemberCosts)
    //        {
    //            db.MEMBERCOSTs.Add(new MEMBERCOST()
    //            {
    //                MemberID = membercost.MemberID,
    //                TripID = trip.TripID,
    //                Cost = membercost.Cost,
    //                CostName = membercost.CostName
    //            });
    //        }

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
