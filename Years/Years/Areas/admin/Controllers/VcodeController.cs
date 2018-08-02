using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wchl.WMBlog.WebCore.Attrs;
using Years.Common;

namespace Wchl.WMBlog.WebUI.Areas.admin.Controllers
{
    [SkipCheckLogin]
    public class VcodeController : Controller
    {
        // GET: admin/Vcode
        public ActionResult Vcode()
        {
            string vcode = GetVcode(1);

            Session[Keys.vcode] = vcode;

            byte[] imgbuffer;
            using (Image img = new Bitmap(65, 25))
            {
                using (Graphics g = Graphics.FromImage(img))
                {
                    g.Clear(Color.White);
                    g.DrawString(vcode, new Font("黑体", 18, FontStyle.Bold | FontStyle.Strikeout | FontStyle.Italic), new SolidBrush(Color.Red), 4, 4);
                }

                //定义一个空的内存流对象
                using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                {
                    //将图片对象中的流写入ms中
                    img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    //将ms中的数据转换成byte[]
                    imgbuffer = ms.ToArray();
                }
            }
            return File(imgbuffer, "image/jpeg");
        }
        //随机数
        Random r = new Random();

        private string GetVcode(int p)
        {
            string str = "23456789abcdefghjkmnpqrstuvwxyzABCDEFGHJKMNPQRSTUVWXYZ";
            string res = string.Empty;
            int length = str.Length;
            for (int i = 0; i < p; i++)
            {
                res += str[r.Next(length)];
            }
            return res;
        }
    }
}