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
    
    public partial class RapportDeVente
    {
        public int RepportDeVenteId { get; set; }
        public int TirageId { get; set; }
        public int UserPointDeVenteId { get; set; }
        public int NbreTicketVendu { get; set; }
        public int Montant { get; set; }
        public string ModifiePar { get; set; }
        public Nullable<System.DateTime> ModifieDate { get; set; }
    
        public virtual Tirage Tirage { get; set; }
        public virtual UserPointDeVente UserPointDeVente { get; set; }
    }
}
