using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace We_Split
{
    class MyUtils
    {
        public static int calcTotalCostMember(int TripID, int MemberID)
        {
            int result = 0;

            var db = new WP_Project2_WeSplitEntities();
            var costList = db.MEMBERCOSTs.ToList();
            var query = from cost in costList
                        where cost.TripID == TripID && cost.MemberID == MemberID
                        select cost;
            
            foreach(var cost in query)
            {
                result += (int)cost.Cost;
            }
            return result;
        }
        public static int calcCostByName(int TripID, string CostName)
        {
            var db = new WP_Project2_WeSplitEntities();
            var membercostslist = db.MEMBERCOSTs.ToList();
            var query = from membercost in membercostslist
                        where membercost.TripID == TripID && membercost.CostName == CostName
                        select membercost;

            int result = 0;
            foreach(var membercost in query)
            {
                result += (int)(membercost.Cost);
            }
            return result;
        }


        //main
        public static int calcTotalCostTrip(int TripID)
        {
            int result = 0;

            var db = new WP_Project2_WeSplitEntities();
            var costList = db.MEMBERCOSTs.ToList();
            var query = from cost in costList
                        where cost.TripID == TripID
                        select cost;

            foreach (var cost in query)
            {
                result += (int)cost.Cost;
            }
            return result;
        }
        public static float calcAverageCostTrip(int TripID)
        {
            int totalCost = calcTotalCostTrip(TripID);
            var db = new WP_Project2_WeSplitEntities();
            var membersList = db.MEMBERSPERTRIPs.ToList();
            var query = from member in membersList
                        where member.TripID == TripID
                        select member;
            int numberOfMembers = query.Count();
            float result = (float)(totalCost / numberOfMembers);
            return result;
        }
        public static object findMemberNameAndCostByTripID(int tripID)
        {
            var db = new WP_Project2_WeSplitEntities();
            var trips = db.TRIPs.ToList();
            var membercosts = db.MEMBERCOSTs.ToList();
            var members = db.MEMBERs.ToList();
            var memberspertrips = db.MEMBERSPERTRIPs.ToList();

            var query = memberspertrips
                .Where(mpt => mpt.TripID == tripID)
                .Join(members,
                        mpt => mpt.MemberID,
                        m => m.MemberID,
                        (mpt, m) => new {
                            MemberName = m.MemberName,
                            TotalCost = MyUtils.calcTotalCostMember((int)mpt.TripID, m.MemberID)});
            var result = query.ToList();
            return result;
        }
        public static BindingList<MEMBERCOST> findCostNameAndCostByTripID(int tripID)
        {
            var db = new WP_Project2_WeSplitEntities();
            var membercosts = db.MEMBERCOSTs.ToList();

            var query = membercosts
                .Where(m => m.TripID == tripID)
                .GroupBy(mc => mc.CostName)
                .Select(gr => new { CostName = gr.Key, Cost = calcCostByName(tripID, gr.Key) });

            var queryList = query.ToList();
            var result = new BindingList<MEMBERCOST>();
            foreach (var cost in queryList)
            {
                result.Add(new MEMBERCOST() { Cost = cost.Cost, CostName = cost.CostName });
            }
            return result;
        }
       
        //public static object findTest(int tripID)
        //{
            
        //}
    }
}
