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
    
    public partial class Good
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Good()
        {
            this.DetailBills = new HashSet<DetailBill>();
            this.DetailGoodOrders = new HashSet<DetailGoodOrder>();
            this.DetailReceipts = new HashSet<DetailReceipt>();
            this.DetailSaleOrders = new HashSet<DetailSaleOrder>();
            this.DetailSupplierGoods = new HashSet<DetailSupplierGood>();
            this.DetailTransferOrders = new HashSet<DetailTransferOrder>();
            this.DetailWareHouses = new HashSet<DetailWareHouse>();
            this.EPCs = new HashSet<EPC>();
            this.Promotions = new HashSet<Promotion>();
        }
    
        public string Id { get; set; }
        public string IdGood { get; set; }
        public string IdWareHouse { get; set; }
        public string IdCate { get; set; }
        public string IdUnit { get; set; }
        public string Material { get; set; }
        public string IdSeason { get; set; }
        public string IdColor { get; set; }
        public string IdSize { get; set; }
        public string IdStyle { get; set; }
        public string IdGender { get; set; }
        public string IdGroupGood { get; set; }
        public string StyleColorSize { get; set; }
        public string SKU { get; set; }
        public string LI_PE { get; set; }
        public string Div { get; set; }
        public string OutSize { get; set; }
        public string ProductLine { get; set; }
        public string Company { get; set; }
        public string Name { get; set; }
        public string IdCoo { get; set; }
        public Nullable<double> Price { get; set; }
        public Nullable<double> PriceNew { get; set; }
        public Nullable<double> PriceTax { get; set; }
        public Nullable<double> InternalPrice { get; set; }
        public Nullable<double> GTGTInternalTax { get; set; }
        public Nullable<double> Discount { get; set; }
        public Nullable<double> InternalDiscount { get; set; }
        public Nullable<System.DateTime> Expiry { get; set; }
        public Nullable<System.DateTime> WarrantyPeriod { get; set; }
        public Nullable<double> Inventory { get; set; }
        public Nullable<double> MinimumInventory { get; set; }
        public Nullable<double> MaximumInventory { get; set; }
        public string Description { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public string CreateBy { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
        public string ModifyBy { get; set; }
        public Nullable<bool> Status { get; set; }
    
        public virtual CateGood CateGood { get; set; }
        public virtual Color Color { get; set; }
        public virtual Coo Coo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DetailBill> DetailBills { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DetailGoodOrder> DetailGoodOrders { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DetailReceipt> DetailReceipts { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DetailSaleOrder> DetailSaleOrders { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DetailSupplierGood> DetailSupplierGoods { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DetailTransferOrder> DetailTransferOrders { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DetailWareHouse> DetailWareHouses { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EPC> EPCs { get; set; }
        public virtual Gender Gender { get; set; }
        public virtual GroupGood GroupGood { get; set; }
        public virtual Season Season { get; set; }
        public virtual Size Size { get; set; }
        public virtual Style Style { get; set; }
        public virtual Unit Unit { get; set; }
        public virtual WareHouse WareHouse { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Promotion> Promotions { get; set; }
    }
}
