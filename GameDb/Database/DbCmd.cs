using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameLib.Util;
using System.Collections;
using GameDb.Logic;

namespace GameLib.Database
{
    public class DbSelect<T> : IDbCmd where T : TbLogic,new()
    {
        static private LogImplement log = LogFactory.getLogger("DbSelect<T>");

        private EnumDbCommandType _dbCmdType = EnumDbCommandType.select;
        private IDbConnect _dbCnt;
        private string _sql;

        private int _error;
        private DefOnResult _cbOnResult;
        private List<T> _listRecord;

        private string _context;
        
        private long[] _costTime = new long[2];

        public long[] CostTime
        {
            get { return _costTime; }
            set { _costTime = value; }
        }

        public EnumDbCommandType DbCmdType
        {
            get { return _dbCmdType; }
        }

        public IDbConnect DbConnect
        {
            get { return _dbCnt; }
            set { _dbCnt = value; }
        }

        public int Error
        {
            get { return _error; }
            set { _error = value; }
        }

        public DefOnResult CbOnResult
        {
            get { return _cbOnResult; }
            set { _cbOnResult = value; }
        }

        public List<T> ListRecord
        {
            get { return _listRecord; }
        }

        public string Context
        {
            get { return _context; }
            set { _context = value; }
        }

        public DbSelect(IDbConnect cnt, string sql, DefOnResult cbOnResult, string context = "")
        {
            _dbCnt = cnt;
            _sql = sql;
            _error = 0;
            _cbOnResult = cbOnResult;
            _listRecord = null;
            Context = context;
        }

        public DbSelect(List<T> listT, string context = "")
        {
            _dbCnt = null;
            _sql = null;
            _error = 0;
            _cbOnResult = null;
            _listRecord = listT;
            Context = context;
        }

        public void processRequest()
        {
            if (_listRecord != null)
                return;

            try
            {
                _listRecord = _dbCnt.find<T>(_sql);
            }
            catch (Exception ex)
            {
                log.error(string.Format("{0}", typeof(T)), ex);
            }
        }
        public void processResponse() { }

        public override string ToString()
        {
            return base.ToString();
        }
    }

    public class DbInsert<T> : IDbCmd where T : TbLogic
    {
        static private LogImplement log = LogFactory.getLogger("DbInsert<T>");

        private EnumDbCommandType _dbCmdType = EnumDbCommandType.insert;
        private IDbConnect _dbCnt;
        private T record;

        private string _context;

        private int _error;
        private DefOnResult _cbOnResult;

        private long[] _costTime = new long[2];

        public long[] CostTime
        {
            get { return _costTime; }
            set { _costTime = value; }
        }

        public EnumDbCommandType DbCmdType
        {
            get { return _dbCmdType; }
        }

        public IDbConnect DbConnect
        {
            get { return _dbCnt; }
            set { _dbCnt = value; }
        }

        public T Record
        {
            get { return record; }
        }

        public string Context
        {
            get { return _context; }
            set { _context = value; }
        }

        public int EffectRows
        {
            get { return _error; }
            set { _error = value; }
        }

        public DefOnResult CbOnResult
        {
            get { return _cbOnResult; }
            set { _cbOnResult = value; }
        }

        public DbInsert(IDbConnect cnt, T insert, DefOnResult cbOnResult, string context = "")
        {
            _dbCnt = cnt;
            record = insert;
            _error = 0;
            _cbOnResult = cbOnResult;
            Context = context;
        }
        public List<T> ListRecords;
        public DbInsert(IDbConnect cnt, List<T> inserts, DefOnResult cbOnResult, string context = "")
        {
            _dbCnt = cnt;
            ListRecords = inserts;
            _error = 0;
            _cbOnResult = cbOnResult;
            Context = context;
        }
        public void processRequest()
        {
            try
            {
                if (ListRecords != null)
                {
                    _error = _dbCnt.insert<T>(ListRecords);
                }
                else
                {
                    _error = _dbCnt.insert<T>(record);
                }
            }
            catch (Exception ex)
            {
                log.error(string.Format("{0}", typeof(T)), ex);
            }
        }
        public void processResponse() { }

        public override string ToString()
        {
            return base.ToString();
        }
    }

    public class DbUpdate<T> : IDbCmd where T : TbLogic
    {
        static private LogImplement log = LogFactory.getLogger("DbUpdate<T>");

        private EnumDbCommandType _dbCmdType = EnumDbCommandType.update;
        private IDbConnect _dbCnt;
        private List<T> _listRecord;

        private string _context;

        private int _error;
        private DefOnResult _cbOnResult;

        private long[] _costTime = new long[2];

        public long[] CostTime
        {
            get { return _costTime; }
            set { _costTime = value; }
        }

        public EnumDbCommandType DbCmdType
        {
            get { return _dbCmdType; }
        }

        public IDbConnect DbConnect
        {
            get { return _dbCnt; }
            set { _dbCnt = value; }
        }

        public List<T> ListRecord
        {
            get { return _listRecord; }
        }

        public string Context
        {
            get { return _context; }
            set { _context = value; }
        }

        public int Error
        {
            get { return _error; }
            set { _error = value; }
        }

        public DefOnResult CbOnResult
        {
            get { return _cbOnResult; }
            set { _cbOnResult = value; }
        }

        public DbUpdate(IDbConnect cnt, List<T> update, DefOnResult cbOnResult, string context = "")
        {
            _dbCnt = cnt;
            _listRecord = update;
            _error = 0;
            _cbOnResult = cbOnResult;
            Context = context;
        }
        T Record;
        public DbUpdate(IDbConnect cnt, T update, DefOnResult cbOnResult, string context = "")
        {
            _dbCnt = cnt;
            Record = update;
            _error = 0;
            _cbOnResult = cbOnResult;
            Context = context;
        }
        public void processRequest()
        {
            try
            {
                if (_listRecord != null)
                {
                    _error = _dbCnt.update<T>(_listRecord);
                    
                }
                else if (Record != null) {
                    _error = _dbCnt.update<T>(Record);
                    
                    //Record.changedKeys.Clear();//存盘完毕
                }
            }
            catch (Exception ex)
            {
                log.error(string.Format("{0}", typeof(T)), ex);
            }
        }
        public void processResponse() { }

        public override string ToString()
        {
            return base.ToString();
        }
    }

    public class DbCmdList : IDbCmd
    {
        private EnumDbCommandType _dbCmdType = EnumDbCommandType.list;
        private DefOnResult _cbOnResult;
        private List<IDbCmd> _cmdList;

        private int _error;
        private string _context;

        private long[] _costTime = new long[2];

        public long[] CostTime
        {
            get { return _costTime; }
            set { _costTime = value; }
        }

        public EnumDbCommandType DbCmdType
        {
            get { return _dbCmdType; }
        }

        public IDbConnect DbConnect { get; set; }

        public DefOnResult CbOnResult 
        { 
            get { return _cbOnResult; }
            set { _cbOnResult = value; }
        }

        public List<IDbCmd> CmdList
        {
            get { return _cmdList; }
        }

        public string Context
        {
            get { return _context; }
            set { _context = value; }
        }

        public int Error
        {
            get { return _error; }
            set { _error = value; }
        }

        public DbCmdList(List<IDbCmd> cmdList, DefOnResult cbOnResult, string context = "")
        {
            _cmdList = cmdList;
            _cbOnResult = cbOnResult;
            Context = context;
        }

        public void processRequest()
        {
            foreach (IDbCmd cmd in _cmdList)
                cmd.processRequest();

        }
        public void processResponse() 
        {
            if (_cbOnResult != null)
                _cbOnResult(this);
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }

    public class DbCmdSqlNonQuery : IDbCmd
    {
        static private LogImplement log = LogFactory.getLogger("DbCmdSqlNonQuery");

        private EnumDbCommandType _dbCmdType = EnumDbCommandType.select;
        private IDbConnect _dbCnt;
        private string _sql;
        private Hashtable _param;

        private int _error;
        private DefOnResult _cbOnResult;

        private string _context;
        
        private long[] _costTime = new long[2];

        public long[] CostTime
        {
            get { return _costTime; }
            set { _costTime = value; }
        }

        public EnumDbCommandType DbCmdType
        {
            get { return _dbCmdType; }
        }

        public IDbConnect DbConnect
        {
            get { return _dbCnt; }
            set { _dbCnt = value; }
        }

        public int Error
        {
            get { return _error; }
            set { _error = value; }
        }

        public DefOnResult CbOnResult
        {
            get { return _cbOnResult; }
            set { _cbOnResult = value; }
        }

        public string Context
        {
            get { return _context; }
            set { _context = value; }
        }

        public DbCmdSqlNonQuery(IDbConnect cnt, string sql, Hashtable param, DefOnResult cbOnResult, string context = "")
        {
            _dbCnt = cnt;
            _sql = sql;
            _param = param;
            _error = 0;
            _cbOnResult = cbOnResult;
            Context = context;
        }

        public void processRequest()
        {
            if (_sql == null || _sql == "")
                return;

            try
            {
                _error = _dbCnt.executeNonQuery(_sql, _param);
            }
            catch (Exception ex)
            {
                log.error(string.Format("{0}-{1}-{2}", typeof(DbCmdSqlNonQuery), _sql, _param, ex));
            }
        }
        public void processResponse() { }
    }

    public class DbCmdCustom : IDbCmd
    {
        private EnumDbCommandType _dbCmdType = EnumDbCommandType.sql;
        private IDbConnect _dbCnt;
        private long[] _costTime = new long[2];

        public long[] CostTime
        {
            get { return _costTime; }
            set { _costTime = value; }
        }

        public EnumDbCommandType DbCmdType
        {
            get { return _dbCmdType; }
        }

        public IDbConnect DbConnect
        {
            get { return _dbCnt; }
            set { _dbCnt = value; }
        }

        public string Context { get; set; }

        public int Error { get; set; }


        public DefOnResult CbOnResult { get; set; }

        public Object TbProvider { get; set; }

        public DbCmdCustom()
        {
            TbProvider = null;
        }
        public virtual void processRequest() { }
        public virtual void processResponse() { }
    }
}
