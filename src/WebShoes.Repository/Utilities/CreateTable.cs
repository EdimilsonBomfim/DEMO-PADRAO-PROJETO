using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using WebShoes.Domain.Entities;
using WebShoes.Domain.Entities.Abstract;

namespace WebShoes.Repository.Utilities
{
    /// <summary>
    /// https://stackoverflow.com/questions/15746050/ado-net-distinct-data-bases/15788258#15788258
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class HelperSQL<T> 
    {
        protected string _primaryKeyField;
        protected List<BuildClass> _class = new List<BuildClass>();

        public HelperSQL()
        {
            PropertyInfo pkProp = typeof(T).GetProperties().Where(p => p.GetCustomAttributes(typeof(KeyAttribute), false).Length > 0).FirstOrDefault();
            if (pkProp != null)
            {
                _primaryKeyField = pkProp.Name;
            }

            foreach (PropertyInfo prop in typeof(T).GetProperties())
            {
                if (prop.GetCustomAttributes<NotMappedAttribute>().Count() > 0) continue;
                MaxLengthAttribute maxLength = prop.GetCustomAttribute<MaxLengthAttribute>();
                MinLengthAttribute minLength = prop.GetCustomAttribute<MinLengthAttribute>();
                StringLengthAttribute stringLength = prop.GetCustomAttribute<StringLengthAttribute>();
                RequiredAttribute required = prop.GetCustomAttribute<RequiredAttribute>();
                RangeAttribute range = prop.GetCustomAttribute<RangeAttribute>();
                DataTypeAttribute dataType = prop.GetCustomAttribute<DataTypeAttribute>();
                KeyAttribute key = prop.GetCustomAttribute<KeyAttribute>();
                
                var cl = new BuildClass
                {
                    Name = prop.Name,
                    MaxLength = maxLength != null
                    ? (int?)maxLength.Length
                    : stringLength != null
                        ? (int?)stringLength.MaximumLength : null,
                    MinLength = minLength != null
                    ? (int?)minLength.Length
                    : stringLength != null
                        ? (int?)stringLength.MinimumLength : null,
                    PrimaryKey = key != null ? true : false,
                    Type = prop.PropertyType.Name.ToString()
                };
                _class.Add(cl);
            }
        }

        public  string TableName { get { return typeof(T).Name; } }
      
        public  string CreateStatement()
        {
            
                return "CREATE TABLE " + TableName + " (" + GetDelimetedCreateParamList(",")
                    + ", CONSTRAINT PK_"
                    + _class.Where(c => c.PrimaryKey).FirstOrDefault().Name
                    + " PRIMARY KEY("
                    + string.Join(",", _class.Where(c => c.PrimaryKey).Select(c => c.Name)) + ") )";
                    }

        protected string GetDelimetedCreateParamList(string delimeter)
        {
            return string.Join(delimeter, _class.Select(k => string.Format(" {0} {1} ({2}) {3}" + Environment.NewLine,
                k.Name,
                GetSqlType(k.Type),
                k.MaxLength,
                k.NotNull == true || k.PrimaryKey == true ? "NOT NULL " : ""
                //k.PrimaryKey == true ? "PRIMARY KEY" : ""

                ).Replace("()", ""))
                );
        }

        protected string GetSqlType(string type)
        {
            switch (type.ToUpper())
            {
                case "INT16":
                    return "smallint";
                case "INT16?":
                    return "smallint";
                case "INT32":
                    return "int";
                case "INT32?":
                    return "int";
                case "INT64":
                    return "bigint";
                case "INT64?":
                    return "bigint";
                case "STRING":
                    return "NVARCHAR";
                case "XML":
                    return "Xml";
                case "BYTE":
                    return "binary";
                case "BYTE?":
                    return "binary";
                case "BYTE[]":
                    return "varbinary";
                case "GUID":
                    return "uniqueidentifier";
                case "GUID?":
                    return "uniqueidentifier";
                case "TIMESPAN":
                    return "time";
                case "TIMESPAN?":
                    return "time";
                case "DECIMAL":
                    return "money";
                case "DECIMAL?":
                    return "money";
                case "BOOL":
                    return "bit";
                case "BOOL?":
                    return "but";
                case "DATETIME":
                    return "datetime";
                case "DATETIME?":
                    return "datetime";
                case "DOUBLE":
                    return "float";
                case "DOUBLE?":
                    return "float";
                case "CHAR[]":
                    return "nchar";

            }
            return "UNKNOWN";
        }

        public void CreateFile()
        {
            var fileName = Directory.GetParent(Environment.CurrentDirectory).Parent + @"src\WebShoes.Repository\Utilities\Scripts\" + typeof(T).Name + ".txt";

            // Check if file already exists. If yes, delete it.     
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }

            // Create a new file     
            using (StreamWriter sw = File.CreateText(fileName))
            {
                CreateStatement();
            }

        }
    }
}