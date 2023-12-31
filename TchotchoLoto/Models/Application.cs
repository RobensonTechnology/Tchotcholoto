//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TchotchoLoto.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Application
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Application()
        {
            this.AccessTimeApplications = new HashSet<AccessTimeApplication>();
            this.AppNavigationApplications = new HashSet<AppNavigationApplication>();
            this.Roles = new HashSet<Role>();
        }
    
        public int ApplicationId { get; set; }
        public int CompagnieId { get; set; }
        public string ApplicationName { get; set; }
        public string Description { get; set; }
        public string EmailApplication { get; set; }
        public string PasswordEmailApplication { get; set; }
        public byte[] SignatureResponsable { get; set; }
        public byte[] LogoApplication { get; set; }
        public string DevelopperPar { get; set; }
        public string Version { get; set; }
        public string ModifiePar { get; set; }
        public Nullable<System.DateTime> ModifieDate { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AccessTimeApplication> AccessTimeApplications { get; set; }
        public virtual Compagnie Compagnie { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AppNavigationApplication> AppNavigationApplications { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Role> Roles { get; set; }
    }
}
