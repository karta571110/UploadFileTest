using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace slnFileUpload.Controllers
{
    public class FileUploadController : Controller
    {
        // GET: FileUpload
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(HttpPostedFileBase photo)
        {
            string fileName = string.Empty;//上傳圖檔

            if(photo != null)
            {
                if (photo.ContentLength > 0)
                {
                    fileName = Path.GetFileName(photo.FileName);
                    var path = Path.Combine(Server.MapPath("~/Photos"), fileName);
                    photo.SaveAs(path);
                }
            }
            return RedirectToAction("ShowPhotos");
        }

        /// <summary>
        /// ShowPhotos方法可顯示Photos資料夾下的所有圖檔
        /// </summary>
        /// <returns></returns>
        public string ShowPhotos()
        {
            string show = string.Empty;

            DirectoryInfo dir = new DirectoryInfo(Server.MapPath("~/Photos"));

            FileInfo[] finfo = dir.GetFiles();//取得dir物件下的所有檔案(即Photos資料夾下)並放入finfo檔案資訊陣列
            int n = 0;

            foreach(FileInfo result in finfo)
            {
                n++;

                show += "<a href='../Photos/" + result.Name + "'target='_blank'><img src='../Photos/" + result.Name + "' width='90' heigth='60' border='0'></a>";
                if (n % 4 == 0)//顯示四個圖之後即跳一段落
                {
                    show += "<P>";

                }

            }

            show += "<p><a href='Create'>返回</a></p>";
            return show;

        }
    }
}