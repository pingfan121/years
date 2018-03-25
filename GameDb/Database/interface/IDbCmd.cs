using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameLib.Database
{
//     public delegate void DefOnRequest(IDbCmd cmd);
    public delegate void DefOnResult(IDbCmd cmd);

    public enum EnumDbCommandType
    {
        unknown,
        select,
        insert,
        update,
        delete,
        list,
        sql,
    }

    public interface IDbCmd
    {
        EnumDbCommandType DbCmdType { get; }
        IDbConnect DbConnect { get; set; }
        DefOnResult CbOnResult { get; set; }
        string Context { get; set; }
        long[] CostTime { get; set; }

        void processRequest();
        void processResponse();
    }
}
