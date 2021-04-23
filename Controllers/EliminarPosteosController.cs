using BLOG.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace BLOG.Controllers
{
    public class EliminarPosteosController : Controller
    {
        private BlogMVCImagenEntities dblist = new BlogMVCImagenEntities();

        // GET: EliminarPosteos
        public ActionResult Index()
        {
            return View(dblist.posteos.ToList());
        }

        // GET: posteos/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: posteos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            posteos posteos = dblist.posteos.Find(id);
            dblist.posteos.Remove(posteos);
            dblist.SaveChanges();
            return RedirectToAction("Index");
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

            return File(memoryStream, "image/jpg");

        }

    }

}