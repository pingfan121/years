using Easy4net.Common;
using GameDb.Logic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameLib.Database
{
    public interface IDbConnect
    {
        int init(DbConfig config);
        void release();
        bool Connected { get;}
        List<T> find<T>(string query) where T : TbLogic,new();
        int insert<T>(T t) where T : TbLogic;
        int update<T>(T t) where T : TbLogic;
        int delete<T>(T t) where T : TbLogic;
        int insert<T>(List<T> listT) where T : TbLogic;
        int update<T>(List<T> listT) where T : TbLogic;
        int delete<T>(List<T> listT) where T : TbLogic;
        int executeNonQuery(string sql, Hashtable param);
    }
}
