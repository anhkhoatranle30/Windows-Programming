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
    
    public partial class TRIP
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TRIP()
        {
            this.LOCATIONs = new HashSet<LOCATION>();
            this.MEMBERCOSTs = new HashSet<MEMBERCOST>();
            this.MEMBERSPERTRIPs = new HashSet<MEMBERSPERTRIP>();
            this.TRIPIMAGES = new HashSet<TRIPIMAGE>();
        }
    
        public int TripID { get; set; }
        public string TripName { get; set; }
        public Nullable<int> Status { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LOCATION> LOCATIONs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MEMBERCOST> MEMBERCOSTs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MEMBERSPERTRIP> MEMBERSPERTRIPs { get; set; }
        public virtual STATUS STATUS1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TRIPIMAGE> TRIPIMAGES { get; set; }
    }
}
