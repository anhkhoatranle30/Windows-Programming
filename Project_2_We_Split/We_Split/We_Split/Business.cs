using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace We_Split
{
    class Business
    {
        public static BindingList<dynamic> ShowTripsList(string sttString = "")
        {
            var result = new BindingList<dynamic>();
            var tripsList = new BindingList<TRIP>();

            if(sttString == "")
            {
                tripsList = new TripsDAOsqlserver().GetAll();
            }
            else
            {
                tripsList = new TripsDAOsqlserver().GetAllByStatusDisplayText(sttString);
            }

            foreach(var trip in tripsList)
            {
                var membersList = new MembersDAOsqlserver().GetAllByTripID(trip.TripID);
                StringBuilder memberDisplayString = new StringBuilder();
                foreach(var member in membersList)
                {
                    memberDisplayString.Append(member.MemberName).Append(", ");
                }
                memberDisplayString.Remove(memberDisplayString.Length - 2, 2);
                result.Add(new
                {
                    TripID = trip.TripID,
                    TripName = trip.TripName,
                    MembersName = memberDisplayString.ToString()
                });
            }

            return result;
        }
    }
}
