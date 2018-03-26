using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Years.ViewModel
{
    /// <summary>
    /// 博客信息展示类
    /// </summary>
    public class BlogViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        public string id { get; set; }
        /// <summary>创建人
        /// 
        /// </summary>
        public string submitter { get; set; }

        /// <summary>博客标题
        /// 
        /// </summary>
        public string title { get; set; }

        /// <summary>摘要
        /// 
        /// </summary>
        public string digest { get; set; }

        /// <summary>
        /// 上一篇
        /// </summary>
        public string previous { get; set; }

        /// <summary>
        /// 上一篇id
        /// </summary>
        public string previous_id { get; set; }

        /// <summary>
        /// 下一篇
        /// </summary>
        public string next { get; set; }

        /// <summary>
        /// 下一篇id
        /// </summary>
        public string next_id { get; set; }

        /// <summary>类别
        /// 
        /// </summary>
        public string category { get; set; }

        /// <summary>内容
        /// 
        /// </summary>
        public string content { get; set; }

        /// <summary>
        /// 访问量
        /// </summary>
        public int btraffic { get; set; }

        /// <summary>
        /// 评论数量
        /// </summary>
        public int comment_num { get; set; }

        /// <summary> 修改时间
        /// 
        /// </summary>
        public DateTime update_time { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public System.DateTime create_time { get; set; }
        /// <summary>备注
        /// 
        /// </summary>
        public string remark { get; set; }
    }
}
