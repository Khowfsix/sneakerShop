//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication1.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            this.Shipments = new HashSet<Shipment>();
            this.OrderDetails = new HashSet<OrderDetail>();
        }
    
        public int orderID { get; set; }
        public string userID { get; set; }
        public int cartID { get; set; }
        public Nullable<System.DateTime> orderDate { get; set; }
        public Nullable<byte> status { get; set; }
        public Nullable<byte> shipping { get; set; }
        public Nullable<long> totalPay { get; set; }
        public Nullable<int> paymentType { get; set; }
        public string address { get; set; }
        public string customerName { get; set; }
        public string numberPhone { get; set; }
        public string Email { get; set; }
    
        public virtual AspNetUser AspNetUser { get; set; }
        public virtual Cart Cart { get; set; }
        public virtual paymentType paymentType1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Shipment> Shipments { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
