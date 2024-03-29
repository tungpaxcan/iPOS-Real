﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iGMS.Models;

namespace iGMS.Controllers
{
    public class PromotionsController : BaseController
    {
        private iPOSEntities db = new iPOSEntities(); 
        // GET: Promotions
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Adds()
        {
            return View();
        }
        [HttpPost]
        public JsonResult AddGood(string idgood,float amount)
        {
            try
            {
                var user = (User)Session["user"];
                var idUser = user.Id;
                var idPromotion = db.Promotions.OrderBy(x => x.Status == true).ToList().LastOrDefault().Id;
                var promotion = db.Promotions.Find(idPromotion);
                promotion.WithGood = true;
                var detailPromotion = new DetailPromotion();
                detailPromotion.IdGood = idgood;
                detailPromotion.Amount = amount;
                detailPromotion.IdPromotion = idPromotion;
                detailPromotion.CreateDate = DateTime.Now;
                detailPromotion.CreateBy = idUser;
                detailPromotion.ModifyDate = DateTime.Now;
                detailPromotion.ModifyBy = idUser;
                detailPromotion.Status = true;
                db.DetailPromotions.Add(detailPromotion);
                db.SaveChanges();
                return Json(new { code = 200, }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception e)
            {
                return Json(new { code = 500, msg = "Sai !!!" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult Add(DateTime since, DateTime todate,string name)
        {
            try
            {
                var user = (User)Session["user"];
                var idUser = user.Id;
                var promotion = new Promotion();
                promotion.Name = name;
                promotion.Since = since;
                promotion.ToDate = todate;
                promotion.CreateDate = DateTime.Now;
                promotion.CreateBy = idUser;
                promotion.ModifyDate = DateTime.Now;
                promotion.ModifyBy = idUser;
                promotion.Status = true;
                db.Promotions.Add(promotion);
                db.SaveChanges();
                return Json(new { code = 200, }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Sai !!!" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult AddDiscount(float discount)
        {
            try
            {
                var user = (User)Session["user"];
                var idUser = user.Id;
                var idPromotion = db.Promotions.OrderBy(x => x.Status == true).ToList().LastOrDefault().Id;
                var promotion = db.Promotions.Find(idPromotion);
                promotion.Discount = discount;
                db.SaveChanges();
                return Json(new { code = 200, }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Sai !!!" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult AddPrice(float price,float conditionpricecb,int day,DateTime date)
        {
            try
            {
                var user = (User)Session["user"];
                var idUser = user.Id;
                var idPromotion = db.Promotions.OrderBy(x => x.Status == true).ToList().LastOrDefault().Id;
                var promotion = db.Promotions.Find(idPromotion);
                promotion.Price = price;
                promotion.ConditionPrice = conditionpricecb;
                promotion.Day = day;
                promotion.Date = date;
                db.SaveChanges();
                return Json(new { code = 200, }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Sai !!!" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult AddMore(int addmore)
        {
            try
            {
                var user = (User)Session["user"];
                var idUser = user.Id;
                var idPromotion = db.Promotions.OrderBy(x => x.Status == true).ToList().LastOrDefault().Id;
                var promotion = db.Promotions.Find(idPromotion);
                promotion.AmountDonate = addmore;
                db.SaveChanges();
                return Json(new { code = 200, }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Sai !!!" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public JsonResult Promotion(string seach)
        {
            try
            {
                var user = (User)Session["user"];
                var idUser = user.Id;
                var promotion = (from a in db.Promotions.Where(x => x.Id > 0)
                                 select new { 
                                    id=a.Id,
                                    name=a.Name
                                 }).ToList().Where(x=>x.name.Contains(seach)||x.name.ToLower().Contains(seach));
                return Json(new { code = 200,promotion=promotion }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Sai !!!" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult DeletePromotion(int id)
        {
            try
            {
                var detailpromotioncount = db.DetailPromotions.Where(x => x.IdPromotion == id).Count();
                for(int i = 0; i < detailpromotioncount; i++)
                {
                    var detailpromotion = db.DetailPromotions.OrderBy(x => x.IdPromotion == id).ToList().LastOrDefault();
                    db.DetailPromotions.Remove(detailpromotion);
                    db.SaveChanges();
                }
                var goodcount = db.Goods.Where(x => x.IdPromotion == id).Count();
                for(int  i = 0; i < goodcount; i++)
                {
                    var good = db.Goods.OrderBy(x => x.IdPromotion == id).ToList().LastOrDefault();
                    good.IdPromotion = null;
                    db.SaveChanges();
                }
                var promotion = db.Promotions.Find(id);
                db.Promotions.Remove(promotion);
                db.SaveChanges();
                return Json(new { code = 200 }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Sai !!!" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public JsonResult InfoPromotion(int idpromotion)
        {
            try
            {
                var user = (User)Session["user"];
                var idUser = user.Id;
                var promotion = db.Promotions.SingleOrDefault(x => x.Id == idpromotion);
                string info = "";
                if (promotion.WithGood != null)
                {
                    var goodgift = (from a in db.DetailPromotions.Where(x => x.IdPromotion == idpromotion)
                            join b in db.Goods on a.IdGood.Replace(".", "") equals b.IdGood.Replace(".", "")
                            select new
                            {
                                id = b.IdGood,
                                name = b.Name,
                                amount = a.Amount
                            }).ToList().Take(1); 
                    return Json(new { code = 300, goodgift = goodgift }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    if (promotion.Discount != null)
                    {
                        info += "Giảm Giá : " + promotion.Discount + "%";
                    }
                    else if (promotion.Id == idpromotion &&promotion.ConditionPrice != null && promotion.ConditionPrice != 0)
                    {
                        info += "Giảm " + Encode.Money(promotion.Price.ToString()) + " Khi Có Đơn Hàng "+ Encode.Money(promotion.ConditionPrice.ToString());
                    }else if(promotion.Day != 0 && promotion.Day!=null)
                    {
                        info += "Giảm " + Encode.Money(promotion.Price.ToString()) + " Khi Mua Hàng Vào "+Encode.DayOfWeek((int)promotion.Day);
                    }
                    else if ( promotion.Date.Value.Year != 1753&&promotion.Date!=null)
                    {
                        info += "Giảm " + Encode.Money(promotion.Price.ToString()) + " Khi Mua Hàng Vào Ngày " + promotion.Date.Value.Day +"/"+ promotion.Date.Value.Month;
                    }   
                    else if ( promotion.AmountDonate>0)
                    {
                        info += "Mua 1 Tặng " + promotion.AmountDonate;
                    }
                    return Json(new { code = 200, info = info }, JsonRequestBehavior.AllowGet);
                }              
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Sai !!!" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}