//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace We_Split
{
    using System;
    using System.Collections.Generic;
    
    public partial class LOCATION
    {
        public int LocationID { get; set; }
        public Nullable<int> TripID { get; set; }
        public string LocationName { get; set; }
    
        public virtual TRIP TRIP { get; set; }
    }
}
