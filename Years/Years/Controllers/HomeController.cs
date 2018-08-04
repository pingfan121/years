using System.Linq;
using System.Web.Mvc;
using Years.IServices;
using Years.WebCore;
using UtilLib.Util;
using Years.Services;
using Years.ViewModel;
using Webdiyer.WebControls.Mvc;

namespace Years.WebUI.Controllers
{
    public class HomeController : BaseController
    {
       

        public ActionResult Index(int pageindex = 1)
        {
//            //获取控制器名称
//            ViewBag.controllername = RouteData.Values["controller"].ToString().ToLower();

//            int pagesize = 6;
//            //获取发布博文信息
//            var blogArticleList = BlogArticleService.QueryWhere(a => true).OrderByDescending(a => a.create_time).ToPagedList(pageindex, pagesize);

//            foreach (var item in blogArticleList)
//            {
//                if (!string.IsNullOrEmpty(item.content))
//                {
//                    item.content = HtmlTool.ReplaceHtmlTag(item.content);
//                    if (item.content.Length > 200)
//                    {
//                        item.content = item.content.Substring(0, 200);
//                    }
//                }

//            }
//            //获取轮播广告新
//            ViewBag.adList = AdvertisementServices.QueryOrderBy(a => true, a => a.create_time, false).ToPagedList(1, 3);
//            //发布时间排序
//            ViewBag.blogtimelist = BlogArticleService.QueryOrderBy(c => true, c => c.create_time, false);
//            //评论排序
//            ViewBag.blogtrafficlist = BlogArticleService.QueryOrderBy(c => true, c => c.traffic, false);
        
//            //留言排序
//            string sql = @"select a.*,b.title from (select blog_id,count(1) as counts  from guest_book group by blog_id) as a
//inner join blog_article as b
//on
//b.id=a.blog_id order by counts desc";

//            ViewBag.blogguestbooklist = GuestbookService.RunProc<TopgbViewModel>(sql);


            return View();
        }
    }
}