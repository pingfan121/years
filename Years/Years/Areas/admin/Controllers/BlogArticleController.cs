using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Years.IServices;
using Years.Model;
using Years.WebCore;

namespace Years.WebUI.Areas.admin.Controllers
{
    public class BlogArticleController : BaseController
    {
        IBlogArticleServices BlogArticleServive;
        public BlogArticleController(IBlogArticleServices BlogArticleServive)
        {
            this.BlogArticleServive = BlogArticleServive;
        }
        // GET: admin/BlogArticle
        public ActionResult Index()
        {
            var BlogArticles = BlogArticleServive.QueryWhere(a => true).OrderByDescending(a => a.create_time);
            return View(BlogArticles);
        }

        public ActionResult Add()
        {
            return View();
        }

        //新增博文
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(BlogArticle blogArticle)
        {
            blogArticle.create_time = DateTime.Now;
            blogArticle.submitter = "admin";
            blogArticle.update_time = DateTime.Now;
            blogArticle.remark = string.Empty;
            BlogArticleServive.Add(blogArticle);
            BlogArticleServive.SaverChanges();
            return Content("ok:添加成功！");
        }

        //图片上传
        public ActionResult upload()
        {
            //文件保存目录路径
            String savePath = "/upload/";

            //定义允许上传的文件扩展名
            Hashtable extTable = new Hashtable();
            extTable.Add("image", "gif,jpg,jpeg,png,bmp");
            extTable.Add("flash", "swf,flv");
            extTable.Add("media", "swf,flv,mp3,wav,wma,wmv,mid,avi,mpg,asf,rm,rmvb");
            extTable.Add("file", "doc,docx,xls,xlsx,ppt,htm,html,txt,zip,rar,gz,bz2");

            //最大文件大小
            int maxSize = 1000000;

            HttpPostedFileBase imgFile = Request.Files["imgFile"];
            if (imgFile == null)
            {
                return Content("error|请选择文件。");
            }

            String dirPath = Server.MapPath(savePath);
            if (!Directory.Exists(dirPath))
            {
                return Content("error|上传目录不存在。");
            }

            String dirName = Request.QueryString["dir"];
            if (String.IsNullOrEmpty(dirName))
            {
                dirName = "image";
            }
            if (!extTable.ContainsKey(dirName))
            {
                return Content("error|目录名不正确。");
            }

            String fileName = imgFile.FileName;
            String fileExt = Path.GetExtension(fileName).ToLower();

            if (imgFile.InputStream == null || imgFile.InputStream.Length > maxSize)
            {
                return Content("error|上传文件大小超过限制。");
            }

            if (String.IsNullOrEmpty(fileExt) || Array.IndexOf(((String)extTable[dirName]).Split(','), fileExt.Substring(1).ToLower()) == -1)
            {
                return Content("error|上传文件扩展名是不允许的扩展名。\n只允许" + ((String)extTable[dirName]) + "格式。");
            }

            //创建文件夹
            dirPath += dirName + "/";
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }
            String ymd = DateTime.Now.ToString("yyyyMMdd", DateTimeFormatInfo.InvariantInfo);
            dirPath += ymd + "/";
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }

            String newFileName = DateTime.Now.ToString("yyyyMMddHHmmss_ffff", DateTimeFormatInfo.InvariantInfo) + fileExt;
            String filePath = dirPath + newFileName;

            imgFile.SaveAs(filePath);

            String fileUrl = savePath + "image/" + ymd + "/" + newFileName;
            return Content(fileUrl);
        }
    }
}