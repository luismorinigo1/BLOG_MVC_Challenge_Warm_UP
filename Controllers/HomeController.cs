using BLOG.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace BLOG.Controllers
{
    public class HomeController : Controller
    {
        private BlogMVCImagenEntities dbL = new BlogMVCImagenEntities();
        public ActionResult Index()
        {
            return View(dbL.posteos.ToList());
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
            //file
            HttpPostedFileBase FileBase = Request.Files[0];

            WebImage webimage = new WebImage(FileBase.InputStream);

            posteos.Imagen = webimage.GetBytes();

            if (ModelState.IsValid)
            {
                dbL.posteos.Add(posteos);
                dbL.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(posteos);
        }


        //public ActionResult getImage()
        //{
        //    posteos postear = dbL.posteos.Find();
        //    byte[] byteimage = postear.Imagen;

        //    MemoryStream memoryStream = new MemoryStream(byteimage);
        //    Image image = Image.FromStream(memoryStream);

        //    memoryStream = new MemoryStream();
        //    image.Save(memoryStream, ImageFormat.Jpeg);
        //    memoryStream.Position = 0;

        //    return File(memoryStream, "image/jpg");

        //}







    }
}