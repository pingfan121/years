using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilLib.Util;
using Years.IRepository;
using Years.IServices;
using Years.Model;
using Years.ViewModel;

namespace Years.Services
{
    public class BlogArticleServices : BaseServices<BlogArticle>, IBlogArticleServices
    {
        IBlogArticleRepository dal;

        public BlogArticleServices(IBlogArticleRepository dal)
        {
            this.dal = dal;
            base.baseDal = dal;
        }

        /// <summary>
        /// 获取视图博客详情信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BlogViewModels getBlogDetails(string id)
        {
            BlogArticle blogArticle = dal.QueryWhere(a => a.id == id).FirstOrDefault();
            BlogArticle nextblog = dal.QueryWhere(a => a.id == id).FirstOrDefault();
            BlogArticle prevblog = dal.QueryWhere(a => a.id == id).FirstOrDefault();
            blogArticle.traffic += 1;
            dal.Edit(blogArticle, new string[] { "traffic" });
            dal.SaverChanges();
            //AutoMapper自动映射
            Mapper.Initialize(cfg => cfg.CreateMap<BlogArticle, BlogViewModels>());
            BlogViewModels models = Mapper.Map<BlogArticle, BlogViewModels>(blogArticle);
            if (nextblog != null)
            {
                models.next = nextblog.title;
                models.next_id = nextblog.id;
            }

            if (prevblog != null)
            {
                models.previous = prevblog.title;
                models.previous_id = prevblog.id;
            }
            models.digest = HtmlTool.ReplaceHtmlTag(blogArticle.content).Length > 100 ? HtmlTool.ReplaceHtmlTag(blogArticle.content).Substring(0, 200) : HtmlTool.ReplaceHtmlTag(blogArticle.content);
            return models;

        }


    }
}
