using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace We_Split
{
    class TripBuilder 
    { 
        private string TripName { get; set; }
        private int Status { get; set; }
        private string TripDes { get; set; }
        public TripBuilder SetTripName(string tripName)
        {
            TripName = tripName;
            return this;
        }
        public TripBuilder SetStatus(int status)
        {
            Status = status;
            return this;
        }
        public TripBuilder SetStatus(string statusDisplayText)
        {
            Status = new StatusDAOsqlserver().GetStatusIDByText(statusDisplayText);
            return this;
        }
        public TripBuilder SetDes(string tripDes)
        {
            TripDes = tripDes;
            return this;
        }
        public TRIP Build()
        {
            if (TripDes == null)
            {
                TripDes = " ";
            }
            var result = new TRIP()
            {
                Status = Status,
                TripName = TripName,
                TripDes = TripDes
            };
            return result;
        }
    }
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
        public TRIP Find(int tripID)
        {
            var db = new WP_Project2_WeSplitEntities();
            var result = db.TRIPs.Find(tripID);
            return result;
        }
        public override void Add(TRIP trip)
        {
            var db = new WP_Project2_WeSplitEntities();
            db.TRIPs.Add(trip);
            db.SaveChanges();
        }

        /// <summary>
        /// Hàm thêm chuyến đi vào sql server
        /// </summary>
        /// <param name="TripName"></param>
        /// <param name="statusDisplayText"></param>
        /// <param name="TripDes"></param>
        /// <returns>Trả về tripID của chuyến đi vừa mới thêm vào</returns>
        public int AddTripToDB(string TripName, string statusDisplayText, string TripDes = "")
        {
            var addingTrip = new TripBuilder()
                                    .SetTripName(TripName)
                                    .SetStatus(statusDisplayText)
                                    .SetDes(TripDes)
                                    .Build();
            Add(addingTrip);
            int addedTripID = new TripsDAOsqlserver().GetAll().Last().TripID;
            return addedTripID;
        }
        public override void Delete(TRIP trip)
        {
            var db = new WP_Project2_WeSplitEntities();
            db.TRIPs.Remove(trip);
            db.SaveChanges();
        }
        public void DeleteByTripID(int tripID)
        {
            //deletetrip
            var db = new WP_Project2_WeSplitEntities();
            var deletingTrip = db.TRIPs.Find(tripID);
            db.TRIPs.Remove(deletingTrip);
            db.SaveChanges();
        }
        public void DeleteWholeTripByTripID(int tripID)
        {
            //trip images
            new TripImagesDAOsqlserver().DeleteByTripID(tripID);
            //location
            new LocationDAOsqlserver().DeleteByTripID(tripID);
            //membercost
            new MemberCostsDAOsqlserver().DeleteByTripID(tripID);
            //memberspertrip
            new MembersPerTripDAOsqlserver().DeleteByTripID(tripID);
            //delete trip
            new TripsDAOsqlserver().DeleteByTripID(tripID);
        }
        public override void Update(TRIP trip)
        {
            var db = new WP_Project2_WeSplitEntities();
            var old_info = db.TRIPs.Find(trip.TripID);
            old_info = trip;
            db.SaveChanges();
        }
        public BindingList<TRIP> SearchTripByTripName(string tripName)
        {
            var db = new WP_Project2_WeSplitEntities();
            var result = new BindingList<TRIP>(db.TRIPs.Where(t => t.TripName.Contains(tripName)).ToList());
            return result;
        }
        public BindingList<TRIP> SearchTripByMemberName(string membername)
        {
            var db = new WP_Project2_WeSplitEntities();
            var membersList = db.MEMBERs.ToList();
            var mptList = db.MEMBERSPERTRIPs.ToList();

            var tripIDList = membersList.Where(m => m.MemberName.Contains(membername))
                    .Select(m => m.MemberID)
                    .Join(mptList,
                            memberid => memberid,
                            mpt => mpt.MemberID,
                            (memberid, mpt) => mpt.TripID)
                    .Distinct()
                    .ToList();

            var result = new BindingList<TRIP>();
            foreach(var tripid in tripIDList)
            {
                result.Add(GetTripByTripID((int)tripid));
            }
            return result;
        }
    }
}
