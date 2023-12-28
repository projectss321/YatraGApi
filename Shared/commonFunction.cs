using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Primitives;
using Microsoft.OpenApi.Any;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Specialized;
using System.Data.Common;
using System.Security.Principal;
using System.Text;
using System.Web.Helpers;
using yatracub.Models;
using yatracub.Shared.Interface;
using yatracub.Util;

namespace yatracub.Shared
{
    public class commonFunction : IcommonFunction
    {
        //public httpStatus _http;
        private readonly IdbContext _dbConnection;
        public commonFunction(IdbContext dbConnection)
        {
            _dbConnection = dbConnection;
            //_http = http;
        }
        public string getInsertQuery(object[] parameters, string tableName)
        {
            try
            {
                StringBuilder query = new StringBuilder();
                parameters.Cast<DbParameter>();
                int paramLength = parameters.Length;
                int i = 1;

                if (parameters != null && paramLength > 0)
                {
                    if (tableName != null && tableName != "")
                    {
                        query.Append("insert into ").Append(tableName).Append("(");

                        foreach (DbParameter param in parameters)
                        {
                            var ParameterName = param.ParameterName.Replace("@", string.Empty);
                            if (i == paramLength)
                            {
                                query.Append(ParameterName);
                            }
                            else
                            {
                                query.Append(ParameterName).Append(", ");
                            }

                            i++;
                        }
                        query = query.Append(")").Append(" values(");
                        i = 1;
                    }

                
                    foreach (DbParameter param in parameters)
                    {
                        var ParameterName = param.ParameterName.Replace("@", string.Empty);
                        var paramValue = param.Value;
                        if (i == paramLength)
                        {
                            if(int.TryParse(paramValue.ToString(), out _))
                            {
                                query.Append(paramValue);
                            }
                            else
                            {
                                query.Append("'" + paramValue + "'");
                            }
                            
                        }
                        else
                        {
                            if (int.TryParse(paramValue.ToString(), out _))
                            {
                                query.Append(paramValue).Append(", ");
                            }
                            else
                            {
                                query.Append("'" + paramValue + "'").Append(", ");
                            }
                                
                        }

                        i++;
                    }
                    query.Append(")");
                }
                return query.ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        //public string getInsertObjectDataQuery(object[] parameters, string tableName)
        //{
        //    try
        //    {
        //        StringBuilder query = new StringBuilder();

        //        if (parameters != null && parameters.Length > 0)
        //        {
        //            if (tableName != null && tableName != "")
        //            {
        //                query.Append("insert into ").Append(tableName).Append("(");
        //                for (int i = 0; i < parameters.Length; i++)
        //                {
        //                    if (i == parameters.Length - 1)
        //                    {
        //                        query.Append(parameters[i].ParameterName);
        //                    }
        //                    else
        //                    {
        //                        query.Append(parameters[i].ParameterName).Append(", ");
        //                    }
        //                }
        //                query = query.Append(")").Append(" values(").Replace("@", "");
        //            }

        //            for (int i = 0; i < parameters.Length; i++)
        //            {
        //                if (i == parameters.Length - 1)
        //                {
        //                    if (IsValueType(parameters[i].Value).Name == "Object")
        //                    {
        //                        if (Double.TryParse(parameters[i].Value.ToString(), out _))
        //                        {
        //                            query.Append(Convert.ToDouble(parameters[i].Value));
        //                        }
        //                        else if (int.TryParse(parameters[i].Value.ToString(), out _) == true)
        //                        {
        //                            query.Append(Convert.ToInt64(parameters[i].Value));
        //                        }
        //                        else
        //                        {
        //                            query.Append("'" + parameters[i].Value + "'").ToString();
        //                        }
        //                        //query.Append(parameters[i].Value);
        //                    }
        //                    else
        //                    {
        //                        query.Append(parameters[i].ParameterName);
        //                    }
        //                }
        //                else
        //                {
        //                    if (IsValueType(parameters[i].Value).Name == "Object")
        //                    {
        //                        if (Double.TryParse(parameters[i].Value.ToString(), out _))
        //                        {
        //                            query.Append(Convert.ToDouble(parameters[i].Value)).Append(", ");
        //                        }
        //                        else if (int.TryParse(parameters[i].Value.ToString(), out _) == true)
        //                        {
        //                            query.Append(Convert.ToInt64(parameters[i].Value)).Append(", ");
        //                        }
        //                        else
        //                        {
        //                            query.Append("'" + parameters[i].Value + "'").Append(", ").ToString();
        //                        }
        //                        //else
        //                        //{
        //                        //    query.Append(parameters[i].Value).Append(", ");
        //                        //}
        //                    }
        //                    else
        //                    {
        //                        query.Append(parameters[i].ParameterName).Append(", ");
        //                    }
        //                }
        //            }
        //            query.Append(")");
        //        }
        //        return query.ToString();
        //    }
        //    catch (Exception ex)
        //    {
        //        return ex.Message;
        //    }
        //}

        public string getSelectQuery(string tableName, object[] wherecondition = null, object[] selectedcolumnName = null, object[] orderby = null, string isasc = "")
        {
            StringBuilder query = new StringBuilder();
            //int selectedcolumnLength = selectedcolumnName.Length ;
            //int whereconditionLength = wherecondition.Length;
            int i = 1;

            query.Append("select ");

            if (selectedcolumnName != null && selectedcolumnName.Length > 0)
            {
                foreach (DbParameter selectcolumn in selectedcolumnName.Cast<DbParameter>())
                {
                    if (i == selectedcolumnName.Length)
                    {
                        query.Append(selectcolumn.ToString().Replace("@", string.Empty));
                    }
                    else
                    {
                        query.Append(selectcolumn.ToString().Replace("@", string.Empty) + " , ");
                    }
                    i++;
                }
                i = 1;
            }
            else
            {
                query.Append("*");
            }
            query.Append(" from " + tableName);

            if (wherecondition.Length > 0)
            {
                query.Append(" with(nolock) where ");

                foreach (DbParameter where in wherecondition.Cast<DbParameter>())
                {
                    var paramName = where.ParameterName.Replace("@", string.Empty);
                    var paramValue = where.Value;
                    if (int.TryParse(paramValue.ToString(), out _) == true)
                    {
                        if (i == wherecondition.Length)
                        {
                            query.Append(paramName + " = " + paramValue);
                           
                        }
                        else
                        {
                            query.Append(paramName + " = " + paramValue + " and ");
                           
                        }
                    }
                    else
                    {
                        if (i == wherecondition.Length)
                        {
                            query.Append(paramName + " = " + "'" + paramValue + "'");
                           
                        }
                        else
                        {
                            query.Append(paramName + " = " + "'" + paramValue + "'" + " and ");
                           
                        }
                    }
                    i++;
                }
                i = 1;
            }

            if (orderby != null && orderby.Length > 0 && isasc != null && isasc != "")
            {
                query.Append(" Order By " + orderby + isasc);
            }

            return query.ToString();
        }

        public string getUpdateQuery(object[] updateColumnName, string tableName, object[] whereCondition = null)
        {
            StringBuilder query = new StringBuilder();
            int updateLength = updateColumnName.Length;
            int whereConditionLength = whereCondition.Length;

            query.Append("update " + tableName + " set ");

            if (updateColumnName != null && updateLength > 0)
            {
                int i = 1;
                foreach(DbParameter columnName in updateColumnName.Cast<DbParameter>())
                {
                    var paramName = columnName.ParameterName.Replace("@", string.Empty);
                    var columnValue = columnName.Value;

                    if(int.TryParse(columnValue.ToString(), out _))
                    {
                        if (i == updateLength)
                        {
                            query.Append(paramName + " = " + columnValue);
                            
                        }
                        else
                        {
                            query.Append(paramName + " = " + columnValue + " , ");
                            
                        }
                    }
                    else
                    {
                        if (i == updateLength)
                        {
                            query.Append(paramName + " = " + "'" + columnValue + "'");
                        }
                        else
                        {
                            query.Append(paramName + " = " + "'" + columnValue + "'" + ",");
                        }
                    }
                    i++;
                }
            }

            if(whereCondition != null && whereConditionLength > 0)
            {
                int i = 1;
                query.Append(" where ");
                foreach (DbParameter columnName in whereCondition.Cast<DbParameter>())
                {
                    var paramName = columnName.ParameterName.Replace("@", string.Empty);
                    var columnValue = columnName.Value;
                    if (int.TryParse(columnValue.ToString(), out _))
                    {
                        if (i == whereConditionLength)
                        {
                            query.Append(paramName + " = " + columnValue);

                        }
                        else
                        {
                            query.Append(paramName + " = " + columnValue + " , ");

                        }
                    }
                    else
                    {
                        if (i == whereConditionLength)
                        {
                            query.Append(paramName + " = " + "'" + columnValue + "'");
                        }
                        else
                        {
                            query.Append(paramName + " = " + "'" + columnValue + "'" + ",");
                        }
                    }
                    i++;
                }
            }

            return query.ToString();
        }
        public string successResult(resultStatus rstatus = null, string message = "Success" , object Data = null)
        {
            try
            {
                //resultStatus rstatus = new resultStatus();
                rstatus.Data = Data != null ? Data : rstatus.Data;
                rstatus.status = 1;
                rstatus.httpStatusCode = 200;
                rstatus.message = message;
                JsonConvert.SerializeObject(rstatus.Data);

                return JsonConvert.SerializeObject(rstatus);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string errorResult(string message = "Success")
        {
            try
            {
                resultStatus rstatus = new resultStatus();
                rstatus.Data = null;
                rstatus.status = 0;
                rstatus.httpStatusCode = 200;
                rstatus.message = message;

                return JsonConvert.SerializeObject(rstatus);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public dynamic IsValueType<T>(T obj)
        {
            return typeof(T);
        }

    }
}
