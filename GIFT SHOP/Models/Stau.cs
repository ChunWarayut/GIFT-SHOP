﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GIFT_SHOP.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    public partial class Stau
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Stau()
        {
            this.Sales = new HashSet<Sale>();
        }

        [DisplayName("รหัสสถานะการชำระเงิน")]
        public int Staus_ID { get; set; }
        [DisplayName("สถานะการชำระเงิน")]
        public string Staus_name { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sale> Sales { get; set; }
    }
}