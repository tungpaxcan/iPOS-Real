//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace iGMS.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class SalesOrder
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SalesOrder()
        {
            this.Deliveries = new HashSet<Delivery>();
            this.DetailSaleOrders = new HashSet<DetailSaleOrder>();
        }
    
        public int Id { get; set; }
        public Nullable<int> IdTypeStatus { get; set; }
        public Nullable<int> IdPayMethod { get; set; }
        public string IdWareHouse { get; set; }
        public string IdStore { get; set; }
        public string IdCustomer { get; set; }
        public string IdUser { get; set; }
        public Nullable<int> IdTypeSales { get; set; }
        public string Name { get; set; }
        public Nullable<System.DateTime> DatePay { get; set; }
        public Nullable<System.DateTime> DeliveryDate { get; set; }
        public Nullable<bool> PayStatus { get; set; }
        public Nullable<double> Receivable { get; set; }
        public Nullable<double> Liabilities { get; set; }
        public Nullable<double> PartialPay { get; set; }
        public string Description { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public string CreateBy { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
        public string ModifyBy { get; set; }
        public Nullable<bool> Status { get; set; }
    
        public virtual Customer Customer { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Delivery> Deliveries { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DetailSaleOrder> DetailSaleOrders { get; set; }
        public virtual PaymentMethod PaymentMethod { get; set; }
        public virtual Store Store { get; set; }
        public virtual User User { get; set; }
        public virtual WareHouse WareHouse { get; set; }
    }
}
