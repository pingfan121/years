using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Years.ViewModel
{
    public class AdvertisementViewModel
    {

        /// <summary>
        /// 分类ID
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime create_time { get; set; }

        /// <summary>
        /// 广告图片
        /// </summary>
        public string img_url { get; set; }

        /// <summary>
        /// 广告标题
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// 广告链接
        /// </summary>
        public string url { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }
    }
}
