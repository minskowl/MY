using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;
using PGMRX120Lib;

namespace PDSchema
{
    /// <summary>
    /// Summary description for OleDbSchemaBuilder.
    /// </summary>
    public class PdSchemaBuilder
    {
        private CodeParser pgr;

        public PdSchemaBuilder()
        {
            pgr = new CodeParser("D:\\Projects\\Net\\CodeRocket\\2.0\\res\\TSQL.GMR");

        }
        //public TableSchema CreateTableSchema(PdPDM.BaseTable table)
        //{
            
        //}
        //public TableSchema CreateTableSchema(PdPDM.View view)
        //{
        //    TableSchema ti = new TableSchema();
        //    ti.Alias = view.Name;
        //    ti.Name = view.DisplayName;
        //    ti.TableType = TableType.VIEW;
        //    foreach (PdPDM.ViewColumn  col in view.Columns)
        //    {
        //       if(!(col.Name.Trim().Length==0 || col.DisplayName.Trim().Length ==0 ||  col.DataType.Trim().Length==0))
        //        ti.AddColumn(GetColumnSchema(col));
        //    }


        //    return ti;
        //}
        public TableSchema CreateTableSchema(PdPDM.BaseTable tab)
        {
            TableSchema ti = new TableSchema();
            ti.Alias = tab.Name;
            ti.Name = tab.DisplayName;
            ti.TableType = TableType.TABLE;
            foreach (PdPDM.BaseColumn col in tab.Columns)
            {
                ColumnSchema ci = new ColumnSchema();
                ci.Alias = col.Name;
                ci.Name = col.DisplayName;



                string netType = col.GetExtendedAttributeText("NetType");
                if (netType != null && netType.Length > 0)
                    ci.NetType = netType;
                else
                    ci.NetType = DbTypeToNeType(col.DataType);

                ci.DataType = col.DataType;
                ci.Length = col.Length;
                
                if(col.ClassName=="Column")
                {
                    PdPDM.Column column = (PdPDM.Column)col;
                    ci.AllowNulls = !column.Mandatory;
                    ci.IsAutoIncrement = column.Identity;
                    ci.IsPrimaryKey = column.Primary;
                    ci.IsForeignKey = column.ForeignKey;
                }
  

                ci.HighValue = col.HighValue;
                ci.LowValue = col.LowValue;

                ci.IsActive = Convert.ToBoolean(col.GetExtendedAttributeText("IsActive"));

                ci.DefaultValue = getDefaultValue(ci.AllowNulls, col.DefaultValue, ci.NetType);
                ci.IsReadOnly = Convert.ToBoolean(col.GetExtendedAttributeText("IsReadOnly"));


                //ci.DataTypeId = (int)dr["ProviderType"];
                //ci.DefaultTestValue = ProviderInfoManager.GetInstance().GetTestDefaultById(this.dbProviderType, ci.DataTypeId);

                //ci.IsUnique = (bool)dr["IsUnique"];

                //ci.Ordinal = (int)dr["ColumnOrdinal"];


                ti.AddColumn(ci);
            }

            //foreach (PdPDM.Procedure pro in tab.Procedures)
            //    ti.AddProcedure(CreateProcedureSchema(pro));

            return ti;
        }
        //private ColumnSchema GetColumnSchema(ViewColumn col)
        //{
        //    ColumnSchema ci = new ColumnSchema();
        //    ci.Alias = col.Name;
        //    ci.Name = col.DisplayName;

        //    ci.AllowNulls = false;

        //    ci.NetType = DbTypeToNeType(col.DataType);

        //    ci.DataType = col.DataType;
        //    ci.Length = col.Length;
        //    ci.IsAutoIncrement = false;

        //    ci.IsPrimaryKey = false;
        //    ci.IsForeignKey = false;

        //    ci.HighValue = col.HighValue;
        //    ci.LowValue = col.LowValue;

        //    ci.IsActive = true;

        //    ci.DefaultValue = getDefaultValue(ci.AllowNulls, col.DefaultValue, ci.NetType);
        //    ci.IsReadOnly = false;
        //    return ci;


        //    //ci.DataTypeId = (int)dr["ProviderType"];
        //    //ci.DefaultTestValue = ProviderInfoManager.GetInstance().GetTestDefaultById(this.dbProviderType, ci.DataTypeId);

        //    //ci.IsUnique = (bool)dr["IsUnique"];

        //    //ci.Ordinal = (int)dr["ColumnOrdinal"];
        //}



        private ProcedureSchema CreateProcedureSchema(PdPDM.Procedure pro)
        {
            ProcedureSchema prc = new ProcedureSchema();
            prc.Name = pro.Name;
            //foreach( PdPDM.Parameter par in pro.Par

            //string tmpFile = System.IO.Path.GetTempFileName();
            //System.IO.File.WriteAllText(tmpFile, pro.TextPreview);
            

            //if (!pgr.ParseFile(tmpFile)) throw new Exception("Not Parse");
            if (!pgr.ParseString(pro.TextPreview)) throw new Exception("Not Parse");
            CodeObject list = pgr.Find("/.*procedure_param_list", pgr.GetRoot());
            if (list != null) ParseProcedure_param_list(prc,list);

            return prc;
        }
        private void ParseProcedure_param_list( ProcedureSchema prc,CodeObject decl)
        {
            //           procedure_param_list ::=
            //	{procedure_param, ","} ;
            CodeObject obj = decl.FirstChild();
            while (obj!=null)
            {
                switch(obj.Type)
                {
                    case "procedure_param":
                        prc.AddParameter(ParseProcedure_param(obj));
                        break;
                    default:
                        throw new NotImplementedException("Not Implemented Type: " + obj.Type);
                }
                obj = obj.NextSibling();
            }  
            
            

        }
        private ParameterSchema ParseProcedure_param(CodeObject decl)
        {
            //procedure_param ::=
            //	parameter ["AS"]
            //	data_type [varying_tag] 
            //	["=" param_init_value ] 
            //	[output_tag] ;
            ParameterSchema param = new ParameterSchema();
            
            CodeObject obj = decl.FirstChild();
            while (obj != null)
            {
                switch (obj.Type)
                {
                    case "parameter":
                        param.Name = obj.Text;
                        break;
                    case "data_type":
                    case "param_init_value":
                    case "output_tag":
                        break;
                    default:
                        throw new NotImplementedException("Not Implemented Type: " + obj.Type);
                }
                obj = obj.NextSibling();
            }
            return param;
        }
        private string getDefaultValue(bool AllowNulls, string dbDefValue, string netType)
        {
            if (AllowNulls)
            {
                if (netType == "String" || netType == "System.String")
                    return String.Empty;
                else
                    return "= null";

            }
            // Поле обязательное 
            if (dbDefValue != null && dbDefValue.Length > 0) return dbDefValue;

            switch (netType)
            {
                //case "int":                    
                //case "Int32":
                //case "Int16":
                //case "Int64":
                //case "Integer":
                //case "Byte":
                //case "Bit":
                //case "Short":
                //case "Single":
                //case "Double":
                //case "Float":
                //case "Decimal":
                //case "System.Int32":
                //case "System.Int16":
                //case "System.Int64":                    
                //    return " = 0";

                case "String":
                case "System.String":
                    return "= String.Empty";

                case "DateTime":
                case "System.DateTime":
                    return " = DateTime.MinValue";

                case "System.Boolean":
                case "bool":
                    return " = false";
                default:
                    return string.Empty;
            }

        }
        private string DbTypeToNeType(string dbType)
        {
            string[] ar = dbType.Split('(');
            dbType = ar[0];
            switch (dbType)
            {
                case "bit":
                    return "bool";
                case "int":
                    return "Int32";

                case "nvarchar":
                case "varchar":
                case "char":
                case "nchar":
                case "ntext":
                    return "String";
                case "datetime":
                    return "DateTime";
                case "image":
                    return "byte[]";
                     
                default:
                    throw new NotImplementedException("NotImplementedException DbTypeToNeType: " + dbType);

            }
        }







    }
}
