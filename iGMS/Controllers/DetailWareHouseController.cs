﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iGMS.Models;

namespace iGMS.Controllers
{
    public class DetailWareHouseController : BaseController
    {
        private iPOSEntities db = new iPOSEntities();
        // GET: DetailWareHouse
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Index2()
        {
            return View();
        }
        public ActionResult List()
        {
            return View();
        }
        [HttpGet]
        public JsonResult TonKho(string seach)
        {
            try
            {
                var c = (from b in db.DetailWareHouses.Where(x => x.Id > 0 &&x.Status==true)
                         select new
                         {
                             id = b.Good.Id,
                             idgood = b.Good.IdGood.Replace(".",""),
                             K = b.IdWareHouse == null ? b.Store.Name : b.WareHouse.Name,
                             name = b.Good.Name,
                             inventory = b.Inventory
                         }).ToList().Where(x => x.id.ToLower().Contains(seach) || x.name.Contains(seach));
                return Json(new { code = 200, c = c, }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Sai !!!" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public JsonResult Receipt()
        {
            try
            {
                var c = (from b in db.Receipts.Where(x => x.Status == false)
                         select new
                         {
                             id = b.Id,
                         }).ToList();
                return Json(new { code = 200, c = c, }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Sai !!!" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult Add(string idwarehouse,string idsupplier,string idgood,string amount,string idReceipt)
        {
            try
            {

                var session = (User)Session["user"];
                var nameAdmin = session.Name;
                var e = db.DetailWareHouses.SingleOrDefault(x => (x.IdWareHouse == idwarehouse || x.IdStore == idwarehouse) && x.IdGoods == idgood);
                var g = db.DetailReceipts.SingleOrDefault(x => x.IdReceipt == idReceipt  && x.idGood == idgood&&x.Status==true);
                var f = db.Receipts.Find(idReceipt);
                g.Status = false;
                f.Status = true;
                db.SaveChanges();
                if (e != null)
                {
                    e.Inventory += float.Parse(amount);
                    db.SaveChanges();
                    return Json(new { code = 200, msg = "Hiển Thị Dữ liệu thành công" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var d = new DetailWareHouse();
                    if (idwarehouse.Substring(0, 1) == "K")
                    {
                        d.IdWareHouse = idwarehouse;
                    }
                    else
                    {
                        d.IdStore = idwarehouse;
                    }
                    d.IdGoods = idgood;
                    d.Inventory = float.Parse(amount);
                    db.DetailWareHouses.Add(d);

                    db.SaveChanges();
                    return Json(new { code = 300, msg = "Hiển Thị Dữ liệu thành công" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Hiểm thị dữ liệu thất bại" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult AddCT(string id, string sumpricetax, string sumpriceli)
        {
            try
            {
                db.Configuration.ProxyCreationEnabled = false;
                var session = (User)Session["user"];
                var nameAdmin = session.Name;
                var d = new License();
                var ids = db.Licenses.Where(x => x.Id == id).ToList();
                if (ids.Count == 0)
                {
                    d.Id = id;
                    d.Seri = id;
                    d.PriceTax = int.Parse(sumpricetax);
                    d.SumPrice = int.Parse(sumpriceli);
                    d.CreateDate = DateTime.Now;
                    db.Licenses.Add(d);
                    db.SaveChanges();
                    return Json(new { code = 200, msg = "Hiển Thị Dữ liệu thành công" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { code = 300, msg = "Trùng Mã Phiếu !!!" }, JsonRequestBehavior.AllowGet);
                }


            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Hiểm thị dữ liệu thất bại" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public JsonResult Show(string idReceipt)
        {
            try
            {
                var a = db.Receipts.Find(idReceipt);
                var idpurchaseorder = a.IdPurchaseOrder;
                var c = (from b in db.Receipts.Where(x => x.Id == idReceipt)
                         select new
                         {
                             id = b.Id,
                             method = b.Method.Name,
                             datepaydh = b.PurchaseOrder.DatePay.Value.Day+"/" + b.PurchaseOrder.DatePay.Value.Month+"/" + b.PurchaseOrder.DatePay.Value.Year,
                             namesupplier = b.PurchaseOrder.Supplier.Name,
                             idsupplier = b.PurchaseOrder.Supplier.Id,
                             warehouse = b.PurchaseOrder.WareHouse.Name == null ? b.PurchaseOrder.Store.Name : b.PurchaseOrder.WareHouse.Name,
                             idwarehouse = b.PurchaseOrder.WareHouse.Id == null ? b.PurchaseOrder.Store.Id : b.PurchaseOrder.WareHouse.Id,
                             sumprice = b.PurchaseOrder.Sumprice
                         }).ToList();
                var d = (from b in db.DetailReceipts.Where(x => x.IdReceipt == idReceipt&&x.Status==true)
                         select new
                         {
                             id = b.Good.Id,
                             name = b.Good.Name,
                             amount = b.Amount,
                         }).ToList();
                return Json(new { code = 200, c = c,d=d }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Sai !!!" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public JsonResult Last()
        {
            try
            {
                var idlast = db.DetailWareHouses.OrderBy(x => x.Id).ToList().LastOrDefault();
                var idpn = idlast.Id;
                return Json(new { code = 200, idpn = idpn,}, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Sai !!!" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public JsonResult Show2(int id)
        {
            try
            {
                var Id_Length = id.ToString().Length;
                var Id = "";
                var Ids = "";
                var ids = 0;
                if (Id_Length < 4)
                {
                    Id = id.ToString();
                }
                else
                {
                    Id = id.ToString().Substring(Id_Length - 4, 4);
                    Ids = id.ToString().Substring(0, Id_Length - 4);
                    ids = int.Parse(Ids);
                }               
                object a = null, c = null;
                if (Id == "0102")
                {
                    a = (from b in db.PurchaseOrders.Where(x => x.Id == ids)
                         select new
                         {
                             K = b.IdWareHouse == null ? b.Store.Name : b.WareHouse.Name,

                         }).ToList();
                    c = (from b in db.DetailTransferOrders.Where(x => x.IdPuchaseOrder ==ids && x.StatusExport == false &&x.Status == true)
                         join bb in db.Goods on b.IdGood equals bb.IdGood
                         select new
                         {
                             id = b.IdGoods,
                             idgood = b.IdGood.Replace(".", ""),
                             name = bb.Name,
                         }).ToList();                    
                }
                else
                {
                     a = (from b in db.SalesOrders.Where(x => x.Id == id)
                             select new
                             {
                                 K = b.IdWareHouse == null ? b.Store.Name : b.WareHouse.Name,
                                 Kid = b.IdWareHouse == null ? b.IdStore : b.IdWareHouse,
                                 customer = b.Customer.Name,
                             }).ToList();
                      c = (from b in db.DetailSaleOrders.Where(x => x.IdSaleOrder == id && x.Status == true)
                             select new
                             {
                                 id = b.IdGoods,
                                 idgood = b.Good.IdGood.Replace(".", ""),
                                 name = b.Good.Name,
                             }).ToList();
                }
                return Json(new { code = 200, a = a, c = c }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Sai !!!" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult Tru(string id,int idsaleorder,string amount,string K,string[] epcshow)
        {
            try
            {
                var aa = db.DetailEPCs.Where(x => x.Status == false).ToList();
                for (int i = 0; i < aa.Count(); i++)
                {
                    var ba = db.DetailEPCs.OrderBy(x => x.Status == false).ToList().LastOrDefault();
                    db.DetailEPCs.Remove(ba);
                    db.SaveChanges();
                }              
                var H = "";
                var E = "";
                if (K.Contains("CH"))
                {
                    H = K;
                    E = null;
                }
                else
                {
                    E = K;
                    H = null;
                }
                for (int i = 0; i < epcshow.Length; i++)
                {
                    var idepc = epcshow[i];
                    var f = db.EPCs.SingleOrDefault(x => x.IdGoods == idepc);
                    if (f != null)
                    {
                        var idf = f.IdGoods;
                        var c = db.DetailSaleOrders.SingleOrDefault(x => x.IdGoods == idf && x.IdSaleOrder == idsaleorder && x.Status == true);
                        var d = db.DetailWareHouses.SingleOrDefault(x => x.IdGoods == idf && x.Status == true);
                        var e = db.EPCs.SingleOrDefault(x => x.IdGoods == idf && x.Status == true);
                        e.Status = false;
                        d.Status = false;
                        c.Status = false;
                        db.SaveChanges();
                    }
                }
                var Status_Detail_SalesOrder = db.DetailSaleOrders.Where(x => x.Status == true && x.IdSaleOrder == idsaleorder).ToList();
                if (Status_Detail_SalesOrder.Count() == 0)
                {
                    db.SalesOrders.Find(idsaleorder).Status = false;
                    db.SaveChanges();
                }
                return Json(new { code = 200, msg = "Chưa Có Hàng Trong kho !!!" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Chưa Có Hàng Trong kho !!!" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}