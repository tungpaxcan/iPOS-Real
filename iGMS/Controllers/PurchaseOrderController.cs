﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows;
using iGMS.Models;
using OfficeOpenXml;

namespace iGMS.Controllers
{
    public class PurchaseOrderController : BaseController
    {
        private VietTienEntities db = new VietTienEntities();
        // GET: PurchaseOrder
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Add(int paymethod,string name,DateTime datepay,DateTime deliverydate,float sumprice,float liabilities,float partialpay,string des,string H,string supplier)
        {
            try
            {
                var session = (User)Session["user"];
                var nameAdmin = session.Name;
                var d = new PurchaseOrder();
                if (H.Contains("CH"))
                {
                    d.IdStore = H;
                }
                else
                {
                    d.IdWareHouse = H;
                }
                d.IdSupplier = supplier ;
                d.Status = false;
                d.IdPayMethod = paymethod;
                d.Name = name;
                d.DatePay = datepay;
                d.DeliveryDate = deliverydate;
                d.Sumprice = sumprice;
                d.Liabilities = liabilities;
                d.PartialPay = partialpay;
                d.Description = des;
                d.CreateDate = DateTime.Now;
                d.CreateBy = nameAdmin;
                d.ModifyDate = DateTime.Now;
                d.ModifyBy = nameAdmin;
                db.PurchaseOrders.Add(d);
                db.SaveChanges();
                return Json(new { code = 200, msg = "Hiển Thị Dữ liệu thành công" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Hiểm thị dữ liệu thất bại" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult AddDetail(float price,float sumpricegoods,string epc,string goods,string H)
        {
            try
            {
                var session = (User)Session["user"];
                var nameAdmin = session.Name;
                var idpurchaseorder = db.PurchaseOrders.OrderBy(x => x.Id);
                var idpu = idpurchaseorder.ToList().LastOrDefault();
                var good = db.Goods.OrderBy(x => x.Id.Contains(goods)).ToList().LastOrDefault();
                var id = idpu.Id;
                var e = new Good();
                var ea = db.Goods.Find(epc);
                if(ea == null)
                {
                    e.Id = epc;
                    e.IdGood = good.IdGood;
                    e.IdWareHouse = good.IdWareHouse;
                    e.Material = good.Material;
                    e.IdSeason = good.IdSeason;
                    e.IdColor = good.IdColor;
                    e.IdSize = good.IdSize;
                    e.IdStyle = good.IdStyle;
                    e.IdCate = good.IdCate;
                    e.IdGender = good.IdGender;
                    e.IdGroupGood = good.IdGroupGood;
                    e.SKU = good.SKU;
                    e.IdGender = good.IdGender;
                    e.Company = good.Company;
                    e.Name = good.Name;
                    e.IdCoo = good.IdCoo;
                    e.Price = price;
                    e.PriceNew = price;
                    db.Goods.Add(e);
                    db.SaveChanges();
                }
            
                var g = new EPC();
                var ga = db.EPCs.SingleOrDefault(x => x.IdGoods == epc);
                if (ga == null)
                {
                    g.IdGoods = epc;
                    g.IdEPC = epc;
                    g.Status = false;
                    db.EPCs.Add(g);
                    db.SaveChanges();
                }              
                var f = new DetailWareHouse();
                var fa = db.DetailWareHouses.SingleOrDefault(x => x.IdGoods == epc);
                if (fa == null)
                {
                    if (H.Contains("CH"))
                    {
                        f.IdStore = H;
                    }
                    else
                    {
                        f.IdWareHouse = H;
                    }
                    f.IdGoods = epc;
                    f.Inventory = 1;
                    f.Status = false;
                    db.DetailWareHouses.Add(f);
                    db.SaveChanges();
                }
            
                var d = new DetailGoodOrder();
                var da = db.DetailGoodOrders.SingleOrDefault(x => x.IdGoods == epc);
                if (da == null)
                {
                    d.EPC = epc;
                    d.IdGoods = epc;
                    d.Price = price;
                    d.IdPurchaseOrder = id;
                    d.Sumprice = sumpricegoods;
                    d.Description = "";
                    d.CreateDate = DateTime.Now;
                    d.CreateBy = nameAdmin;
                    d.ModifyDate = DateTime.Now;
                    d.ModifyBy = nameAdmin;
                    d.Status = true;
                    db.DetailGoodOrders.Add(d);
                    db.SaveChanges();
                }
            
                return Json(new { code = 200, msg = "Hiển Thị Dữ liệu thành công" }, JsonRequestBehavior.AllowGet);        
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Hiểm thị dữ liệu thất bại" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public JsonResult Bill(string supplier)
        {
            try
            {
                var c = (from b in db.Suppliers.Where(x => x.Id == supplier)
                         select new
                         {
                             id = b.Id,
                             name = b.Name,
                             address = b.AddRess,
                             phone = b.Phone,
                             fax = b.Fax
                         }).ToList();
                return Json(new { code = 200, c = c }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Sai !!!" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult Bill1()
        {
            try
            {
                var a = db.PurchaseOrders.OrderBy(x => x.Id).ToList().LastOrDefault();
                var id = a.Id;
                var c = (from b in db.PurchaseOrders.Where(x => x.Id == id)
                                       select new
                                       {
                                           id = b.Id,
                                           datedh = b.CreateDate.Value.Day+"/"+b.CreateDate.Value.Month+"/"+b.CreateDate.Value.Year,
                                           paydh = b.PaymentMethod.Name,
                                           datepaydh = b.DatePay.Value.Day + "/" + b.DatePay.Value.Month + "/" + b.DatePay.Value.Year,
                                           sumpricedh = b.Sumprice
                                       }).ToList();
                return Json(new { code = 200, c = c }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Sai !!!" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult Bill2()
        {
            try
            {
                var a = db.PurchaseOrders.OrderBy(x => x.Id).ToList().LastOrDefault();
                var id = a.Id;
                var c = (from b in db.DetailGoodOrders.Where(x => x.IdPurchaseOrder == id)
                         select new
                         {
                             idgoods = b.IdGoods,
                             epc = b.EPC,
                             name = b.Good.Name,
                             size = b.Good.Size.Name,
                             price = b.Price,
                             sumprice = b.Sumprice
                         }).ToList();
                return Json(new { code = 200, c = c }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Sai !!!" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public JsonResult Supplier()
        {
            try
            {
                var c = (from b in db.Suppliers.Where(x => x.Id.Length > 0)
                         select new
                         {
                             id = b.Id,
                             name = b.Name
                         }).ToList();
                return Json(new { code = 200, c = c, }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Sai !!!" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public JsonResult ListGoods(string supplier,string seach)
        {
            try
            {
                var c = (from b in db.DetailSupplierGoods.Where(x => x.IdSupplier == supplier)
                         select new
                         {
                             id = (b.Good.Id).Substring(0, b.Good.Id.Length-8),
                             name = b.Good.Name,
                             size = b.Good.Size.Name,
                             purchaseprice = b.PurchasePrice==null?0:b.PurchasePrice,
                             purchasediscount= b.PurchaseDiscount==null?0:b.PurchaseDiscount,
                             purchasetax = b.PurchaseTax == null ? 0 : b.PurchaseTax
                         }).ToList().Where(x=>x.id.Contains(seach));
                return Json(new { code = 200, c = c, }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Sai !!!" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public JsonResult PayMethod()
        {
            try
            {
                var c = (from b in db.PaymentMethods.Where(x => x.Id > 0)
                         select new
                         {
                             id = b.Id,
                             name = b.Name,
                         }).ToList();
                return Json(new { code = 200, c = c, }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Sai !!!" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public JsonResult WareHouse()
        {
            try
            {
                var c = (from b in db.WareHouses.Where(x => x.Id.Length > 0)
                         select new
                         {
                             id = b.Id,
                             name = b.Name,
                         }).ToList();
                return Json(new { code = 200, c = c, }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Sai !!!" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public JsonResult Store()
        {
            try
            {
                var c = (from b in db.Stores.Where(x => x.Id.Length > 0)
                         select new
                         {
                             id = b.Id,
                             name = b.Name,
                         }).ToList();
                return Json(new { code = 200, c = c, }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Sai !!!" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public JsonResult Active()
        {
            try
            {
                var c = (from b in db.Stores.Where(x => x.Id.Length > 0)
                         select new
                         {
                             id = b.Id,
                             name = b.Name,
                         }).ToList();
                return Json(new { code = 200, c = c, }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Sai !!!" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public JsonResult EPCDaCo()
        {
            try
            {
                var c = (from b in db.EPCs.Where(x => x.Status == true)
                         select new
                         {
                             epc = b.IdEPC
                         }).ToList();
                var d = (from b in db.DetailGoodOrders.Where(x => x.Status==true)
                         select new
                         {
                             epc = b.EPC
                         }).ToList();
                return Json(new { code = 200, c = c,d=d }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Sai !!!" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}