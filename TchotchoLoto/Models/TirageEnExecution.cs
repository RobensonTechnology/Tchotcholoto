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
    
    public partial class TirageEnExecution
    {
        public int TirageEnExecutionId { get; set; }
        public int TirageId { get; set; }
        public bool Statut { get; set; }
        public System.DateTime DateExecution { get; set; }
        public Nullable<System.DateTime> DateLancement { get; set; }
        public string ModifiePar { get; set; }
        public Nullable<System.DateTime> ModifieDate { get; set; }
    
        public virtual Tirage Tirage { get; set; }
    }
}
