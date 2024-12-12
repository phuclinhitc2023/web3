using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Web_3.Models;

namespace Web_3.Controllers
{
    public class HoaDonBanController : Controller
    {
        private QLSachContext db = new QLSachContext();

        // GET: HoaDonBan
        public ActionResult Index(string tenKH)
        {
            var hoaDonBans = db.HoaDonBans.Include(h => h.KhachHang).Include(h => h.Sach);
            if (!string.IsNullOrEmpty(tenKH)) hoaDonBans = hoaDonBans.Where(hd => hd.KhachHang.HoTen.ToLower().Contains(tenKH.ToLower()));
            return View(hoaDonBans.ToList());
        }

        // GET: HoaDonBan/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoaDonBan hoaDonBan = db.HoaDonBans.Find(id);
            if (hoaDonBan == null)
            {
                return HttpNotFound();
            }
            return View(hoaDonBan);
        }

        // GET: HoaDonBan/Create
        public ActionResult Create()
        {
            ViewBag.MaKH = new SelectList(db.KhachHangs, "MaKH", "HoTen");
            ViewBag.MaSach = new SelectList(db.Saches, "MaSach", "TenSach");
            return View();
        }

        // POST: HoaDonBan/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaHD,MaSach,MaKH,NgayBan,SoLuong")] HoaDonBan hoaDonBan)
        {
            if (ModelState.IsValid)
            {
                db.HoaDonBans.Add(hoaDonBan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaKH = new SelectList(db.KhachHangs, "MaKH", "HoTen", hoaDonBan.MaKH);
            ViewBag.MaSach = new SelectList(db.Saches, "MaSach", "TenSach", hoaDonBan.MaSach);
            return View(hoaDonBan);
        }

        // GET: HoaDonBan/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoaDonBan hoaDonBan = db.HoaDonBans.Find(id);
            if (hoaDonBan == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaKH = new SelectList(db.KhachHangs, "MaKH", "HoTen", hoaDonBan.MaKH);
            ViewBag.MaSach = new SelectList(db.Saches, "MaSach", "TenSach", hoaDonBan.MaSach);
            return View(hoaDonBan);
        }

        // POST: HoaDonBan/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaHD,MaSach,MaKH,NgayBan,SoLuong")] HoaDonBan hoaDonBan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hoaDonBan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaKH = new SelectList(db.KhachHangs, "MaKH", "HoTen", hoaDonBan.MaKH);
            ViewBag.MaSach = new SelectList(db.Saches, "MaSach", "TenSach", hoaDonBan.MaSach);
            return View(hoaDonBan);
        }

        // GET: HoaDonBan/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoaDonBan hoaDonBan = db.HoaDonBans.Find(id);
            if (hoaDonBan == null)
            {
                return HttpNotFound();
            }
            return View(hoaDonBan);
        }

        // POST: HoaDonBan/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HoaDonBan hoaDonBan = db.HoaDonBans.Find(id);
            db.HoaDonBans.Remove(hoaDonBan);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult InvoicesMaxNumber()
        {
            var hoadons = db.HoaDonBans.Where(hd => hd.SoLuong == db.HoaDonBans.Max(x => x.SoLuong));
            return View(hoadons);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
