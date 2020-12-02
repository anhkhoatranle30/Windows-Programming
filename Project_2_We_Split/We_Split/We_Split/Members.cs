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
