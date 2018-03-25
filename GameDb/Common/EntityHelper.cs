using System;
using System.Collections.Generic;
using System.Text;
using Easy4net.CustomAttributes;
using Easy4net.DBUtility;
using System.Collections;
using System.Reflection;
using System.Data;

namespace Easy4net.Common
{
    public class EntityHelper
    {
        //public static string GetTableName(Type classType, DbOperateType type)
        //{
        //    //string strTableName = string.Empty;
        //    //string strEntityName = string.Empty;

        //    //strEntityName = classType.FullName;

        //    //object[] attr = classType.GetCustomAttributes(false);
        //    //if (attr.Length == 0) return strTableName;

        //    //foreach (object classAttr in attr)
        //    //{
        //    //    if (classAttr is TableAttribute)
        //    //    {
        //    //        TableAttribute tableAttr = classAttr as TableAttribute;
        //    //        strTableName = tableAttr.Name;
        //    //    }
        //    //}

        //    TableAttribute tableAttr = GetTableAttribute(classType, type);

        //    //if (string.IsNullOrEmpty(strTableName) && (type == DbOperateType.INSERT || type == DbOperateType.UPDATE || type == DbOperateType.DELETE))
        //    //{
        //    //    throw new Exception("实体类:" + strEntityName + "的属性配置[Table(name=\"tablename\")]错误或未配置");
        //    //}

        //    return tableAttr.Name;
        //}


        public static TableAttribute GetTableAttribute(Type classType, DbOperateType type)
        {
            TableAttribute tableAttr = null;
            //string strTableName = string.Empty;
            string strEntityName = string.Empty;

            strEntityName = classType.FullName;

            object[] attr = classType.GetCustomAttributes(false);
            if (attr.Length == 0) return null;

            foreach (object classAttr in attr)
            {
                if (classAttr is TableAttribute)
                {
                    tableAttr = classAttr as TableAttribute;
                    //atrTableName = tableAttr.Name;
                }
            }

            if (tableAttr == null && (type == DbOperateType.INSERT || type == DbOperateType.UPDATE || type == DbOperateType.DELETE))
            {
                throw new Exception("实体类:" + strEntityName + "的属性配置[Table(name=\"tablename\")]错误或未配置");
            }

            return tableAttr;
        }

        public static string GetPrimaryKey(object attribute, DbOperateType type)
        {
            string strPrimary = string.Empty;
            IdAttribute attr = attribute as IdAttribute;
            if (type == DbOperateType.INSERT)
            {
                switch (attr.Strategy)
                {
                    case GenerationType.INDENTITY:
                        break;
                    case GenerationType.GUID:
                        strPrimary = System.Guid.NewGuid().ToString();
                        break;
                    case GenerationType.FILL:
                        strPrimary = attr.Name;
                        break;
                }
            }
            else {
                strPrimary = attr.Name;
            }

            return strPrimary;
        }

        public static string GetColumnName(object attribute)
        {
            string columnName = string.Empty;
            if (attribute is ColumnAttribute)
            {
                ColumnAttribute columnAttr = attribute as ColumnAttribute;
                columnName = columnAttr.Name;
            }
            if (attribute is IdAttribute)
            {
                IdAttribute idAttr = attribute as IdAttribute;
                columnName = idAttr.Name;
            }

            return columnName;
        }
        static public Dictionary<Type, PropertyInfo[]> maps = new Dictionary<Type, PropertyInfo[]>();

      /*  public static TableInfo GetTableInfo(object entity, DbOperateType dbOpType, PropertyInfo[] properties)
        {
            bool breakForeach = false;
            string strPrimaryKey = string.Empty;
            TableInfo tableInfo = new TableInfo();
            Type type = entity.GetType();

            TableAttribute tableAttr = GetTableAttribute(type, dbOpType);
            tableInfo.TableName = tableAttr.Name;
            tableInfo.NoAutomaticKey = tableAttr.NoAutomaticKey;

            //tableInfo.TableName = GetTableName(type, dbOpType);

            if (dbOpType == DbOperateType.COUNT)
            {
                return tableInfo;
            }
            
            // 遍历所有字段
            foreach (PropertyInfo property in properties)
            {
                object propvalue = null;
                string columnName = string.Empty;
                string propName = columnName = property.Name;
          
                // 获得字段值
                propvalue = ReflectionHelper.GetPropertyValue(entity, property);

                // 获得字段属性值列表
                object[] propertyAttrs = property.GetCustomAttributes(false);
                // 遍历字段属性列表
                for (int i = 0; i < propertyAttrs.Length; i++)
                {
                    // 当前字段属性
                    object propertyAttr = propertyAttrs[i];
                    // 根据操作类型确定是否应该跳过该字段
                    if (EntityHelper.IsCaseColumn(propertyAttr, dbOpType))
                    {
                        breakForeach = true;
                        break;
                    }

                    // 暂存字段名字
                    string tempVal = GetColumnName(propertyAttr);
                    // 如果字段名为空则使用属性名, 否则使用字段名
                    columnName = tempVal == string.Empty ? propName : tempVal;

                    // 关键字段
                    if (propertyAttr is IdAttribute)
                    {
                        // 插入或删除记录需要根据主键的值来确定是否收集该字段信息
                        if (dbOpType == DbOperateType.INSERT || dbOpType == DbOperateType.DELETE)
                        {
                            // 转换为关键字段类型
                            IdAttribute idAttr = propertyAttr as IdAttribute;
                            // 主键值的填充方式(自增,GUID,设置)
                            tableInfo.Strategy = idAttr.Strategy;
                            // 如果字段值为空
                            if (CommonUtils.IsNullOrEmpty(propvalue))
                            {
                                // 获得主键名字
                                strPrimaryKey = EntityHelper.GetPrimaryKey(propertyAttr, dbOpType);
                                // 主键名字非空
                                if (string.IsNullOrEmpty(strPrimaryKey) == false)
                                    // 主键名作为主键值
                                    propvalue = strPrimaryKey;        
                            }

                            // 记录主键信息
                            tableInfo.Columns.Put(columnName, propvalue);
                            tableInfo.PropToColumn.Put(propName, columnName);
                            tableInfo.ColumnToProp.Put(columnName, propName);
                        }
                        else if (dbOpType != DbOperateType.UPDATE)
                        {// 更新语句在任何情况下都不能更新关键字段
                            tableInfo.Columns.Put(columnName, propvalue);
                            tableInfo.PropToColumn.Put(propName, columnName);
                            tableInfo.ColumnToProp.Put(columnName, propName);
                        }
                        tableInfo.Id.Put(columnName, propvalue);
                        
                        breakForeach = true;
                    }
                }

                // 删除记录只需要收集关键字段就行了
                if (breakForeach && dbOpType == DbOperateType.DELETE)
                    break;
                // 
                if (breakForeach) 
                {
                    breakForeach = false; 
                    continue; 
                }

                // 非关键字段
                tableInfo.Columns.Put(columnName, propvalue);
                tableInfo.PropToColumn.Put(propName, columnName);
                tableInfo.ColumnToProp.Put(columnName, propName);
            }
            return tableInfo;
        }*/
        static Dictionary<PropertyInfo, object[]> CustomAttributes = new Dictionary<PropertyInfo, object[]>();
        public static TableInfo GetTableInfo(object entity, DbOperateType dbOpType)
        {
            
            //
            Type type = entity.GetType();
            bool breakForeach = false;
            string strPrimaryKey = string.Empty;
            TableInfo tableInfo = new TableInfo();
            PropertyInfo[] properties;
            if (!maps.ContainsKey(type)) {
                properties=type.GetProperties();
                maps[type] = properties;
                for (int i = 0; i < properties.Length;i++ )
                {
                    CustomAttributes[properties[i]] = properties[i].GetCustomAttributes(false);
                }
            }
            TableAttribute tableAttr = GetTableAttribute(type, dbOpType);
            tableInfo.TableName = tableAttr.Name;
            tableInfo.NoAutomaticKey = tableAttr.NoAutomaticKey;

            //tableInfo.TableName = GetTableName(type, dbOpType);

            if (dbOpType == DbOperateType.COUNT)
            {
                return tableInfo;
            }
            properties = maps[type];
            // 遍历所有字段
            foreach (PropertyInfo property in properties)
            {
                object propvalue = null;
                string columnName = string.Empty;
                string propName = columnName = property.Name;

                // 获得字段值
                propvalue = ReflectionHelper.GetPropertyValue(entity, property);

                // 获得字段属性值列表
                object[] propertyAttrs = CustomAttributes[property];// property.GetCustomAttributes(false);
                // 遍历字段属性列表
                for (int i = 0; i < propertyAttrs.Length; i++)
                {
                    // 当前字段属性
                    object propertyAttr = propertyAttrs[i];
                    // 根据操作类型确定是否应该跳过该字段
                    if (EntityHelper.IsCaseColumn(propertyAttr, dbOpType))
                    {
                        breakForeach = true;
                        break;
                    }

                    // 暂存字段名字
                    string tempVal = GetColumnName(propertyAttr);
                    // 如果字段名为空则使用属性名, 否则使用字段名
                    columnName = tempVal == string.Empty ? propName : tempVal;

                    // 关键字段
                    if (propertyAttr is IdAttribute)
                    {
                        // 插入或删除记录需要根据主键的值来确定是否收集该字段信息
                        if (dbOpType == DbOperateType.INSERT || dbOpType == DbOperateType.DELETE)
                        {
                            // 转换为关键字段类型
                            IdAttribute idAttr = propertyAttr as IdAttribute;
                            // 主键值的填充方式(自增,GUID,设置)
                            tableInfo.Strategy = idAttr.Strategy;
                            // 如果字段值为空
                            if (CommonUtils.IsNullOrEmpty(propvalue))
                            {
                                // 获得主键名字
                                strPrimaryKey = EntityHelper.GetPrimaryKey(propertyAttr, dbOpType);
                                // 主键名字非空
                                if (string.IsNullOrEmpty(strPrimaryKey) == false)
                                    // 主键名作为主键值
                                    propvalue = strPrimaryKey;
                            }

                            // 记录主键信息
                            tableInfo.Columns.Put(columnName, propvalue);
                            tableInfo.PropToColumn.Put(propName, columnName);
                            tableInfo.ColumnToProp.Put(columnName, propName);
                        }
                        else if (dbOpType != DbOperateType.UPDATE)
                        {// 更新语句在任何情况下都不能更新关键字段
                            tableInfo.Columns.Put(columnName, propvalue);
                            tableInfo.PropToColumn.Put(propName, columnName);
                            tableInfo.ColumnToProp.Put(columnName, propName);
                        }
                        tableInfo.Id.Put(columnName, propvalue);

                        breakForeach = true;
                    }
                }

                // 删除记录只需要收集关键字段就行了
                if (breakForeach && dbOpType == DbOperateType.DELETE)
                    break;
                // 
                if (breakForeach)
                {
                    breakForeach = false;
                    continue;
                }

                // 非关键字段
                tableInfo.Columns.Put(columnName, propvalue);
                tableInfo.PropToColumn.Put(propName, columnName);
                tableInfo.ColumnToProp.Put(columnName, propName);
            }

            /*if (dbOpType == DbOperateType.UPDATE)
            {
                tableInfo.Columns.Put(tableInfo.Id.Key, tableInfo.Id.Value);
            }*/

            return tableInfo;
        }
        public static PropertyInfo GetPrimaryKeyPropertyInfo(object entity, PropertyInfo[] properties)
        {
            bool breakForeach = false;
            Type type = entity.GetType();
            PropertyInfo properyInfo = null;

            foreach (PropertyInfo property in properties)
            {
                string columnName = string.Empty;
                string propName = columnName = property.Name;

                object[] propertyAttrs = property.GetCustomAttributes(false);
                for (int i = 0; i < propertyAttrs.Length; i++)
                {
                    object propertyAttr = propertyAttrs[i];

                    if (propertyAttr is IdAttribute)
                    {
                        properyInfo = property;
                        breakForeach = true;
                        break;
                    }
                }
                if (breakForeach) break;
            }

            return properyInfo;
        }

        public static List<T> toList<T>(IDataReader sdr, TableInfo tableInfo, PropertyInfo[] properties) where T : new()
        {
            List<T> list = new List<T>();

            while (sdr.Read())
            {
                T entity = new T();
                foreach (PropertyInfo property in properties)
                {
                    String name = tableInfo.PropToColumn[property.Name].ToString();
                    if (tableInfo.TableName == string.Empty)
                    {
                        if (EntityHelper.IsCaseColumn(property, DbOperateType.SELECT)) continue;
                        ReflectionHelper.SetPropertyValue(entity, property, sdr[name]);
                        continue;
                    }

                    ReflectionHelper.SetPropertyValue(entity, property, sdr[name]);
                }
                list.Add(entity);
            }

            return list;
        }

        public static List<T> toList<T>(IDataReader sdr) where T : new()
        {
            List<T> list = new List<T>();
            PropertyInfo[] properties = ReflectionHelper.GetProperties(new T().GetType());

            while (sdr.Read())
            {
                T entity = new T();
                foreach (PropertyInfo property in properties)
                {
                    String name = property.Name;
                    ReflectionHelper.SetPropertyValue(entity, property, sdr[name]);
                }
                list.Add(entity);
            }

            return list;
        }



        public static string GetFindSql(TableInfo tableInfo, DbCondition condition)
        {
            StringBuilder sbColumns = new StringBuilder();

            // tableInfo.Columns.Put(tableInfo.Id.Key, tableInfo.Id.Value);
            foreach(String key in tableInfo.Id.Keys)
            {
                tableInfo.Columns.Put(key, tableInfo.Id[key]);
            }

            foreach (String key in tableInfo.Columns.Keys)
            {
                string nKey = DbKeywords.FormatColumnName(key.Trim());
                sbColumns.Append(nKey).Append(",");
            }

            if (sbColumns.Length > 0) sbColumns.Remove(sbColumns.ToString().Length - 1, 1);

            string strSql = String.Empty;
            if (String.IsNullOrEmpty(condition.queryString)) {
                strSql = "SELECT {0} FROM {1}";
                strSql = string.Format(strSql, sbColumns.ToString(), tableInfo.TableName);
                strSql += condition.ToString();
            }
            else {
                strSql = condition.ToString();
            }

            strSql = strSql.ToUpper();

            return strSql;
        }

        public static string GetFindAllSql(TableInfo tableInfo)
        {
            StringBuilder sbColumns = new StringBuilder();

            // tableInfo.Columns.Put(tableInfo.Id.Key, tableInfo.Id.Value);
            foreach (String key in tableInfo.Id.Keys)
            {
                tableInfo.Columns.Put(key, tableInfo.Id[key]);
            }
            foreach (String key in tableInfo.Columns.Keys)
            {
                string nKey = DbKeywords.FormatColumnName(key.Trim());
                sbColumns.Append(nKey).Append(",");
            }

            if (sbColumns.Length > 0) sbColumns.Remove(sbColumns.ToString().Length - 1, 1);

            string strSql = "SELECT {0} FROM {1}";
            strSql = string.Format(strSql, sbColumns.ToString(), tableInfo.TableName);

            return strSql;
        }

        public static string GetFindByIdSql(TableInfo tableInfo)
        {
            StringBuilder sbColumns = new StringBuilder();

//             if (tableInfo.Columns.ContainsKey(tableInfo.Id.Key))
//                 tableInfo.Columns[tableInfo.Id.Key] = tableInfo.Id.Value;
//             else
//                 tableInfo.Columns.Put(tableInfo.Id.Key, tableInfo.Id.Value);

            int idx = 0;
            string where = "";
            foreach (String key in tableInfo.Id.Keys)
            {
                if (tableInfo.Columns.ContainsKey(key))
                    tableInfo.Columns[key] = tableInfo.Id[key];
                else
                    tableInfo.Columns.Put(key, tableInfo.Id[key]);

                where += string.Format("{0}={1}{2}", key, AdoHelper.DbParmChar, key);
                if (idx++ < (tableInfo.Id.Count - 1))
                    where += " and ";
            }

            foreach (String key in tableInfo.Columns.Keys)
            {
                string nKey = DbKeywords.FormatColumnName(key.Trim());
                sbColumns.Append(nKey).Append(",");
            }

            if (sbColumns.Length > 0) sbColumns.Remove(sbColumns.ToString().Length - 1, 1);

//             string strSql = "SELECT {0} FROM {1} WHERE {2} = " + AdoHelper.DbParmChar + "{2}";
//             strSql = string.Format(strSql, sbColumns.ToString(), tableInfo.TableName, tableInfo.Id.Key);
            string strSql = "SELECT {0} FROM {1} WHERE {2}";
            strSql = string.Format(strSql, sbColumns.ToString(), tableInfo.TableName, where);


            return strSql;
        }

        public static string GetFindCountSql(TableInfo tableInfo)
        {
            StringBuilder sbColumns = new StringBuilder();

            string strSql = "SELECT COUNT(0) FROM {1} ";
            strSql = string.Format(strSql, sbColumns.ToString(), tableInfo.TableName);

            foreach (String key in tableInfo.Columns.Keys)
            {
                string nKey = DbKeywords.FormatColumnName(key.Trim());
                sbColumns.Append(nKey).Append("=").Append(AdoHelper.DbParmChar).Append(key);
            }

            if (sbColumns.Length > 0)
            {
                strSql += " WHERE " + sbColumns.ToString();
            }

            return strSql;
        }

        public static string GetFindCountSql(TableInfo tableInfo, DbCondition condition)
        {
            string strSql = "SELECT COUNT(0) FROM {0}";
            strSql = string.Format(strSql, tableInfo.TableName);
            strSql += condition.ToString();

            return strSql;
        }

        public static string GetFindByPropertySql(TableInfo tableInfo)
        {
            StringBuilder sbColumns = new StringBuilder();

            // tableInfo.Columns.Put(tableInfo.Id.Key, tableInfo.Id.Value);
            int idx = 0;
            string where = "";
            foreach (String key in tableInfo.Id.Keys)
            {
                tableInfo.Columns.Put(key, tableInfo.Id[key]);
                where += string.Format("{0}={1}{2}", key, AdoHelper.DbParmChar, key);
                if (idx++ < (tableInfo.Id.Count - 1))
                    where += " and ";
            }

            foreach (String key in tableInfo.Columns.Keys)
            {
                string nKey = DbKeywords.FormatColumnName(key.Trim());
                sbColumns.Append(nKey).Append(",");
            }

            if (sbColumns.Length > 0) sbColumns.Remove(sbColumns.ToString().Length - 1, 1);

//             string strSql = "SELECT {0} FROM {1} WHERE {2} = " + AdoHelper.DbParmChar + "{2}";
//             strSql = string.Format(strSql, sbColumns.ToString(), tableInfo.TableName, tableInfo.Id.Key);
            string strSql = "SELECT {0} FROM {1} WHERE {2}";
            strSql = string.Format(strSql, sbColumns.ToString(), tableInfo.TableName, where);

            return strSql;
        }

        public static string GetAutoSql()
        {
            string autoSQL = "";
            if (AdoHelper.DbType == DatabaseType.SQLSERVER)
            {
                autoSQL = " select scope_identity() as AutoId ";
            }

            if (AdoHelper.DbType == DatabaseType.ACCESS)
            {
                autoSQL = " select @@IDENTITY as AutoId ";
            }

            if (AdoHelper.DbType == DatabaseType.MYSQL)
            {
                autoSQL = " ;select @@identity ";
            }

            return autoSQL;
        }

        public static string GetInsertSql(TableInfo tableInfo)
        {
            StringBuilder sbColumns = new StringBuilder();
            StringBuilder sbValues = new StringBuilder();

            if(tableInfo.Strategy != GenerationType.INDENTITY)
            {
                foreach (String key in tableInfo.Id.Keys)
                {
                    if (tableInfo.Strategy == GenerationType.GUID && tableInfo.Id[key] == null)
                    {
                        tableInfo.Id[key] = Guid.NewGuid().ToString();
                    }

                    if (tableInfo.Id[key] != null)
                    {
                        tableInfo.Columns.Put(key, tableInfo.Id[key]);
                    }
                }
            }
           
            foreach (String key in tableInfo.Columns.Keys)
            {
                Object value = tableInfo.Columns[key];
                if (!string.IsNullOrEmpty(key.Trim()) && value != null)
                {
                    string nKey = DbKeywords.FormatColumnName(key.Trim());
                    sbColumns.Append(nKey).Append(",");
                    sbValues.Append(AdoHelper.DbParmChar).Append(key).Append(",");
                }
            }

            if (sbColumns.Length > 0 && sbValues.Length > 0)
            {
                sbColumns.Remove(sbColumns.ToString().Length - 1, 1);
                sbValues.Remove(sbValues.ToString().Length - 1, 1);
            }

            string strSql = "INSERT INTO {0}({1}) VALUES({2})";
            strSql = string.Format(strSql, tableInfo.TableName, sbColumns.ToString(), sbValues.ToString());


            if (!tableInfo.NoAutomaticKey)
            {
                if (AdoHelper.DbType == DatabaseType.SQLSERVER || AdoHelper.DbType == DatabaseType.MYSQL)
                {
                    string autoSql = EntityHelper.GetAutoSql();
                    strSql = strSql + autoSql;
                }
            }

            return strSql;
        }

        public static string GetUpdateSql(TableInfo tableInfo)
        {
            StringBuilder sbBody = new StringBuilder();

            
            foreach (String key in tableInfo.Columns.Keys)
            {
                Object value = tableInfo.Columns[key];
                if (!string.IsNullOrEmpty(key.Trim()) && value != null)
                {
                    string nKey = DbKeywords.FormatColumnName(key.Trim());
                    sbBody.Append(nKey).Append("=").Append(AdoHelper.DbParmChar + key).Append(",");
                }
            }

            if (sbBody.Length > 0) sbBody.Remove(sbBody.ToString().Length - 1, 1);

            //tableInfo.Columns.Put(tableInfo.Id.Key, tableInfo.Id.Value);

            int idx = 0;
            string where = "";
            foreach (String key in tableInfo.Id.Keys)
            {
                tableInfo.Columns.Put(key, tableInfo.Id[key]);

                where += string.Format("{0}={1}{2}", key, AdoHelper.DbParmChar, key);
                if (idx++ < (tableInfo.Id.Count - 1))
                    where += " and ";
            }

//             string strSql = "update {0} set {1} where {2} =" + AdoHelper.DbParmChar + tableInfo.Id.Key;
//             strSql = string.Format(strSql, tableInfo.TableName, sbBody.ToString(), tableInfo.Id.Key);

            string strSql = "update {0} set {1} where {2}";
            strSql = string.Format(strSql, tableInfo.TableName, sbBody.ToString(), where);


            return strSql;
        }

        public static string GetDeleteByIdSql(TableInfo tableInfo)
        {
            int idx = 0;
            string where = "";
            foreach (String key in tableInfo.Id.Keys)
            {
                tableInfo.Columns.Put(key, tableInfo.Id[key]);

                where += string.Format("{0}={1}{2}", key, AdoHelper.DbParmChar, key);
                if (idx++ < (tableInfo.Id.Count - 1))
                    where += " and ";
            }

//             string strSql = "delete from {0} where {1} =" + AdoHelper.DbParmChar + tableInfo.Id.Key;
//             strSql = string.Format(strSql, tableInfo.TableName, tableInfo.Id.Key);

            string strSql = "delete from {0} where {1}";
            strSql = string.Format(strSql, tableInfo.TableName, where);
 
            return strSql;
        }

        public static void SetParameters(ColumnInfo columns, params IDbDataParameter[] parms)
        {
            int i = 0;
            foreach (string key in columns.Keys)
            {
                if (!string.IsNullOrEmpty(key.Trim()))
                {
                    object value = columns[key];
                    if (value != null)
                    {
                        parms[i].ParameterName = key;
                        parms[i].Value = value;
                        i++;
                    }
                }
            }
        }

        public static bool IsCaseColumn(object attribute, DbOperateType dbOperateType)
        {
            if (attribute is ColumnAttribute)
            {
                ColumnAttribute columnAttr = attribute as ColumnAttribute;
                if (columnAttr.Ignore)
                {
                    return true;
                }
                if (!columnAttr.IsInsert && dbOperateType == DbOperateType.INSERT)
                {
                    return true;
                }
                if (!columnAttr.IsUpdate && dbOperateType == DbOperateType.UPDATE)
                {
                    return true;
                } 
            }

            return false;
        }

        public static bool IsCaseColumn(PropertyInfo property, DbOperateType dbOperateType)
        {
            bool isBreak = false;
            object[] propertyAttrs = property.GetCustomAttributes(false);
            foreach (object propertyAttr in propertyAttrs)
            {
                if (EntityHelper.IsCaseColumn(propertyAttr, DbOperateType.SELECT))
                {
                    isBreak = true; break;
                }
            }

            return isBreak;
        }
    }
}
