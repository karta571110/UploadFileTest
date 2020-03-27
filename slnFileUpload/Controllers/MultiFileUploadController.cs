using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace slnFileUpload.Controllers
{
    public class MultiFileUploadController : Controller
    {
        // GET: MultiFileUpload
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(HttpPostedFileBase[] photos)
        {
            string fname = string.Empty;

            for(int i = 0; i < photos.Length; i++)
            {
                HttpPostedFileBase f = (HttpPostedFileBase)photos[i];

                if (f != null)
                {
                    //取得上傳的檔案名稱
                    fname = f.FileName.Substring(f.FileName.LastIndexOf("\\") + 1);

                    //將檔案儲存到網站的files資料夾下
                    f.SaveAs(Server.MapPath("~/Photos") + "\\" + fname);
                }
            }
            return RedirectToAction("ShowPhotos");
        }

        //ShowPhotos方法可顯示Photos資料夾下所有圖檔
        public string ShowPhotos()
        {
            string show = string.Empty;

            //建立可操作Photos資料夾的dir物件
            DirectoryInfo dir = new DirectoryInfo(Server.MapPath("~/Photos"));
            //取得dir物件下的所有檔案(即Photo資料夾下)並放入finfo檔案資訊陣列
            FileInfo[] fInfo = dir.GetFiles();
            int n = 0;
            //逐一將finfo檔案資訊陣列內的所有圖檔指定給show變數
            foreach(FileInfo result in fInfo)
            {
                n++;
                //將目前取得的圖指定給show字串
                show += "<a href='../Photos/" + result.Name + "'target='_blank'><img src='../Photos/" + result.Name + "'width='90' height='60' border='0'></a> ";
                if (n % 4 == 0)
                {
                    show += "<p>";
                }
            }
            //show變數再加上 '返回' Create 動作方法的連結
            show += "<p><a href='Create'>返回</a></p>";
            return show;

        }
    }
}