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

    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            this.Sales = new HashSet<Sale>();
            this.SaleDetails = new HashSet<SaleDetail>();
        }

        [DisplayName("รหัสผู้ใช้งาน")]
        public int U_ID { get; set; }
        [DisplayName("Username")]
        public string U_username { get; set; }
        [DisplayName("Password")]
        public string U_password { get; set; }
        [DisplayName("ชื่อ")]
        public string U_name { get; set; }
        [DisplayName("นามสกุล")]
        public string U_lastname { get; set; }
        [DisplayName("เบอร์โทรศัพท์")]
        public string U_tel { get; set; }
        [DisplayName("อีเมลล์")]
        public string U_mail { get; set; }
        [DisplayName("ที่อยู่")]
        public string U_add { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sale> Sales { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SaleDetail> SaleDetails { get; set; }
    }
}