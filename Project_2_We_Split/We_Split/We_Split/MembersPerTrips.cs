using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace We_Split
{
    class MembersPerTrips
    {
    }
    public abstract class MembersPerTripDAO
    {
        public abstract BindingList<MEMBERSPERTRIP> GetAll();
        public abstract void Add(MEMBERSPERTRIP info);
        public abstract void Delete(MEMBERSPERTRIP info);
        public abstract void Update(MEMBERSPERTRIP info);
    }
    class MembersPerTripDAOsqlserver : MembersPerTripDAO
    {
        public override BindingList<MEMBERSPERTRIP> GetAll()
        {
            var db = new WP_Project2_WeSplitEntities();
            var result = new BindingList<MEMBERSPERTRIP>(db.MEMBERSPERTRIPs.ToList());
            return result;
        }
        public override void Add(MEMBERSPERTRIP info)
        {
            var db = new WP_Project2_WeSplitEntities();
            db.MEMBERSPERTRIPs.Add(info);
            db.SaveChanges();
        }
        public void AddMembersPerTripToDB(int tripID, int memberID)
        {
            var addingMPT = new MEMBERSPERTRIP()
            {
                TripID = tripID,
                MemberID = memberID
            };
            Add(addingMPT);
        }
        public override void Delete(MEMBERSPERTRIP info)
        {
            var db = new WP_Project2_WeSplitEntities();
            db.MEMBERSPERTRIPs.Remove(info);
            db.SaveChanges();
        }
        public void DeleteByTripID(int tripID)
        {
            var db = new WP_Project2_WeSplitEntities();
            var mptList = db.MEMBERSPERTRIPs.Where(mpt => mpt.TripID == tripID);
            foreach(var mpt in mptList)
            {
                db.MEMBERSPERTRIPs.Remove(mpt);
                
            }
            db.SaveChanges();
        }
        public override void Update(MEMBERSPERTRIP info)
        {
            var db = new WP_Project2_WeSplitEntities();
            var old_info = db.MEMBERSPERTRIPs.Find(info.RecordID);
            old_info = info;
            db.SaveChanges();
        }
    }
}
