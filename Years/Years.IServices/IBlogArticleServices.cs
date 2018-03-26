using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Years.Model;
using Years.ViewModel;

namespace Years.IServices
{
    public interface IBlogArticleServices : IBaseServices<BlogArticle>
    {
        /// <summary>
        /// 获取视图博客详情信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        BlogViewModel getBlogDetails(string id);
    }
}
