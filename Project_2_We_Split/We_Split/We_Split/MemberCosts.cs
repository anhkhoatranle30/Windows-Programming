using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace We_Split
{
    class MemberCostsBuilder
    {
        private int TripID { get; set; }
        private int MemberID { get; set; }
        private string CostName { get; set; }
        private int Cost { get; set; }
        public MemberCostsBuilder SetTripID(int tripID)
        {
            this.TripID = tripID;
            return this;
        }
        public MemberCostsBuilder SetMemberID(int memberID)
        {
            this.MemberID = memberID;
            return this;
        }
        public MemberCostsBuilder SetCostName(string costName)
        {
            this.CostName = costName;
            return this;
        }
        public MemberCostsBuilder SetCost(int cost)
        {
            this.Cost = cost;
            return this;
        }
        public MEMBERCOST Build()
        {
            var newMC = new MEMBERCOST()
            {
                TripID = this.TripID,
                MemberID = this.MemberID,
                CostName = this.CostName,
                Cost = this.Cost
            };
            return newMC;
        }
    }
    class MemberCosts
    {
    }
    public abstract class MemberCostsDAO
    {
        public abstract BindingList<MEMBERCOST> GetAll();
        public abstract void Add(MEMBERCOST membercost);
        public abstract void Delete(MEMBERCOST membercost);
        public abstract void Update(MEMBERCOST membercost);
    }
    class MemberCostsDAOsqlserver : MemberCostsDAO
    {
        public override BindingList<MEMBERCOST> GetAll()
        {
            var db = new WP_Project2_WeSplitEntities();
            var result = new BindingList<MEMBERCOST>(db.MEMBERCOSTs.ToList());
            return result;
        }
        public BindingList<MEMBERCOST> GetAllByTripID(int tripid)
        {
            var allList = GetAll();
            var query = from membercost in allList
                        where membercost.TripID == tripid
                        select membercost;
            var result = new BindingList<MEMBERCOST>(query.ToList());
            return result;
        }
        public override void Add(MEMBERCOST membercost)
        {
            var db = new WP_Project2_WeSplitEntities();
            db.MEMBERCOSTs.Add(membercost);
            db.SaveChanges();
        }
        public void AddMemberCostToDB(int tripID, int memberID, string costName, int cost)
        {
            var addingMemberCost = new MemberCostsBuilder().SetTripID(tripID).SetMemberID(memberID).SetCostName(costName).SetCost(cost).Build();
            Add(addingMemberCost);
        }
        public override void Delete(MEMBERCOST membercost)
        {
            var db = new WP_Project2_WeSplitEntities();
            db.MEMBERCOSTs.Remove(membercost);
            db.SaveChanges();
        }
        public void DeleteByTripID(int tripID)
        {
            var db = new WP_Project2_WeSplitEntities();
            var membercostsList = db.MEMBERCOSTs.Where(mc => mc.TripID == tripID);
            foreach(var mc in membercostsList)
            {
                db.MEMBERCOSTs.Remove(mc);
                
            }
            db.SaveChanges();
        }
        public override void Update(MEMBERCOST membercost)
        {
            var db = new WP_Project2_WeSplitEntities();
            var old_info = db.MEMBERCOSTs.Find(membercost.CostID);
            old_info = membercost;
            db.SaveChanges();
        }
    }
}
