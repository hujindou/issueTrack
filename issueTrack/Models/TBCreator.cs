//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace issueTrack.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class TBCreator
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TBCreator()
        {
            this.TBIssues = new HashSet<TBIssue>();
        }
    
        public int FDRepositoryID { get; set; }
        public string FDCreator { get; set; }
        public string FDToken { get; set; }
        public int FDLoginMethod { get; set; }
        public System.DateTime FDCreationTImestamp { get; set; }
    
        public virtual TBRepository TBRepository { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBIssue> TBIssues { get; set; }
    }
}
