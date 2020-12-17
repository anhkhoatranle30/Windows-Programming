using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace We_Split
{
    class Members
    {
        public int MemberID { get; set; }
        public string MemberName { get; set; }
        public string Avatar { get; set; }
    }

    abstract class MembersDAO
    {
        public abstract BindingList<MEMBER> GetAll();
        public abstract void Add(MEMBER member);
        public abstract void Update(MEMBER member);
        public abstract void Delete(MEMBER member);
    }

    class MembersDAOsqlserver : MembersDAO
    {
        public override BindingList<MEMBER> GetAll()
        {
            var db = new WP_Project2_WeSplitEntities();
            var result = new BindingList<MEMBER>(db.MEMBERs.ToList());
            return result;
        }
        public BindingList<MEMBER> GetAllByTripID(int tripID)
        {
            var db = new WP_Project2_WeSplitEntities();
            var members = db.MEMBERs.ToList();
            var memberspertrips = db.MEMBERSPERTRIPs.ToList();
            var query = members
                .Join(memberspertrips,
                    m => m.MemberID,
                    mpt => mpt.MemberID,
                    (m, mpt) => new { Member = m, TripID = mpt.TripID })
                .Where(r => r.TripID == tripID)
                .Select(r => r.Member);
            var result = new BindingList<MEMBER>(query.ToList());
            return result;
        }
        public override void Add(MEMBER member)
        {
            var db = new WP_Project2_WeSplitEntities();
            db.MEMBERs.Add(member);
            db.SaveChanges();
        }
        public override void Delete(MEMBER member)
        {
            var db = new WP_Project2_WeSplitEntities();
            db.MEMBERs.Remove(member);
            db.SaveChanges();
        }
        public override void Update(MEMBER member)
        {
            var db = new WP_Project2_WeSplitEntities();

            var old_member = db.MEMBERs.Find(member.MemberID);
            old_member = member;
            db.SaveChanges();
        }
    }
}
