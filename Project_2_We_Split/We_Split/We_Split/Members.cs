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
        public abstract BindingList<Members> GetAll();
        public abstract void Add(Members member);
        public abstract void Update(Members member);
        public abstract void Delete(Members member);
    }

    class MembersDAOsqlserver : MembersDAO
    {
        public override BindingList<Members> GetAll()
        {
            var db = new WP_Project2_WeSplitEntities();
            var db_list = new BindingList<MEMBER>(db.MEMBERs.ToList());
            var result = new BindingList<Members>();
            foreach(var db_element in db_list)
            {
                result.Add(new Members()
                {
                    MemberID = db_element.MemberID
                                            ,
                    MemberName = db_element.MemberName
                                            ,
                    Avatar = AppDomain.CurrentDomain.BaseDirectory + "MemberAvatars\\" + db_element.MemberID + ".jpg"
                });
            }
            return result;
        }
        public override void Add(Members member)
        {
            var db = new WP_Project2_WeSplitEntities();
            var info = new MEMBER() { MemberID = member.MemberID, MemberName = member.MemberName };
            db.MEMBERs.Add(info);
            db.SaveChanges();
        }
        public override void Delete(Members member)
        {
            var db = new WP_Project2_WeSplitEntities();
            var info = new MEMBER() { MemberID = member.MemberID, MemberName = member.MemberName };
            db.MEMBERs.Remove(info);
            db.SaveChanges();
        }
        public override void Update(Members member)
        {
            var db = new WP_Project2_WeSplitEntities();

            var old_member = db.MEMBERs.Find(member.MemberID);
            var info = new MEMBER() { MemberID = member.MemberID, MemberName = member.MemberName };
            old_member = info;
            db.SaveChanges();
        }
    }
}
