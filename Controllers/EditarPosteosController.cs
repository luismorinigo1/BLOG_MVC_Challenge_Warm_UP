using BLOG.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace BLOG.Controllers
{
    public class EditarPosteosController : Controller
    {
        private BlogMVCImagenEntities dblist = new BlogMVCImagenEntities();
        // GET: EditarPosteos
        public ActionResult Index()
        {
            return View(dblist.posteos.ToList());
        }



        // GET: EditarPosteos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            posteos posteos = dblist.posteos.Find(id);
            if (posteos == null)
            {
                return HttpNotFound();
            }
            return View(posteos);
        }

        // POST: EditarPosteos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Titulo,Contenido,Imagen,Categoria,Fecha")] posteos posteos)
        {
            byte[] imagenActual = null;
            //file
            HttpPostedFileBase FileBase = Request.Files[0];
            if(FileBase == null)
            {
                imagenActual = dblist.posteos.SingleOrDefault(t=> t.Id == posteos.Id).Imagen;
            }
            else
            {
                WebImage webimage = new WebImage(FileBase.InputStream);

                posteos.Imagen = webimage.GetBytes();
            }


            if (ModelState.IsValid)
            {
                dblist.Entry(posteos).State = EntityState.Modified;
                dblist.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(posteos);
        }

        public ActionResult getImage(int id)
        {
            posteos postear = dblist.posteos.Find(id);
            byte[] byteimage = postear.Imagen;

            MemoryStream memoryStream = new MemoryStream(byteimage);
            Image image = Image.FromStream(memoryStream);

            memoryStream = new MemoryStream();
            image.Save(memoryStream, ImageFormat.Jpeg);
            memoryStream.Position = 0;

            return File(memoryStream,"image/jpg");

        }

    }
}