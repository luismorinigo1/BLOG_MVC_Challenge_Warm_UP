using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BLOG.Models;

namespace BLOG.Controllers
{
    public class posteosController : Controller
    {
        private BlogMVCImagenEntities db = new BlogMVCImagenEntities();

        // GET: posteos
        public ActionResult Index()
        {
            return View(db.posteos.ToList());
        }

        // GET: posteos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            posteos posteos = db.posteos.Find(id);
            if (posteos == null)
            {
                return HttpNotFound();
            }
            return View(posteos);
        }

        // GET: posteos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: posteos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Titulo,Contenido,Imagen,Categoria,Fecha")] posteos posteos)
        {
            if (ModelState.IsValid)
            {
                db.posteos.Add(posteos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(posteos);
        }

        // GET: posteos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            posteos posteos = db.posteos.Find(id);
            if (posteos == null)
            {
                return HttpNotFound();
            }
            return View(posteos);
        }

        // POST: posteos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Titulo,Contenido,Imagen,Categoria,Fecha")] posteos posteos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(posteos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(posteos);
        }

        // GET: posteos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            posteos posteos = db.posteos.Find(id);
            if (posteos == null)
            {
                return HttpNotFound();
            }
            return View(posteos);
        }

        // POST: posteos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            posteos posteos = db.posteos.Find(id);
            db.posteos.Remove(posteos);
            db.SaveChanges();
            return RedirectToAction("Index");
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
