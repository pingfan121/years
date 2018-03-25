using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using Easy4net.DBUtility;

namespace Easy4net.Common
{
    public class TableInfo
    {
        private string tableName;
        private int strategy;        
        private IdInfo id = new IdInfo();
        private ColumnInfo columns = new ColumnInfo();
        private Map propToColumn = new Map();
        private Map columnToProp = new Map();


        public bool NoAutomaticKey
        {
            get;
            set;
        }

        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

        public int Strategy
        {
            get { return strategy; }
            set { strategy = value; }
        }

        public IdInfo Id
        {
            get { return id; }
            set { id = value; }
        }        
                
        public ColumnInfo Columns
        {
            get { return columns; }
            set { columns = value; }
        }

        public Map PropToColumn
        {
            get { return propToColumn; }
            set { propToColumn = value; }
        }

        public Map ColumnToProp
        {
            get { return columnToProp; }
            set { columnToProp = value; }
        }

        public List<IDbDataParameter> GetParameterList()
        {
            if (this.Columns == null || this.Columns.Count == 0) return new List<IDbDataParameter>();

            List<IDbDataParameter> paramList = new List<IDbDataParameter>();
            foreach (string key in this.Columns.Keys)
            {
                if (!string.IsNullOrEmpty(key.Trim()))
                {
                    object value = this.Columns[key];
                    if (value != null)
                    {
                        IDbDataParameter param = DbFactory.CreateDbParameter();
                        param.ParameterName = key;
                        param.Value = value;

                        paramList.Add(param);
                    }
                }
            }

            return paramList;
        }

        public IDbDataParameter[] GetParameters(List<IDbDataParameter> paramList)
        {
            int i = 0;
            IDbDataParameter[] parameters = DbFactory.CreateDbParameters(paramList.Count);
            foreach (IDbDataParameter dbParameter in paramList)
            {
                parameters[i] = dbParameter;
                i++;
            }

            return parameters;
        }  

        public IDbDataParameter[] GetParameters()
        {
            if (this.Columns == null || this.Columns.Count == 0) return DbFactory.CreateDbParameters(1);
            
            List<IDbDataParameter> paramList = new List<IDbDataParameter>();
            foreach (string key in this.Columns.Keys)
            {
                if (!string.IsNullOrEmpty(key.Trim()))
                {
                    object value = this.Columns[key];
                    if (value != null)
                    {
                        IDbDataParameter param = DbFactory.CreateDbParameter();
                        param.ParameterName = key;
                        param.Value = value;

                        paramList.Add(param);
                    }
                }
            }

            int i = 0;
            IDbDataParameter[] parameters = DbFactory.CreateDbParameters(paramList.Count);
            foreach (IDbDataParameter dbParameter in paramList)
            {
                parameters[i] = dbParameter;
                i++;
            }
         
            return parameters;
        }       
    }
}
