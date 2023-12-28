using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using yatracub.Models;

namespace yatracub.Shared.Interface
{
    public interface IcommonFunction
    {
        public string getInsertQuery(object[] parameters, string tableName);
        //public string getInsertObjectDataQuery(object[] parameters, string tableName);
        public string successResult(resultStatus rstatus ,string message = "Success",dynamic data = null);
        public string errorResult(string message = "Success");
        public dynamic IsValueType<T>(T obj);
        public string getSelectQuery(string tableName, object[] wherecondition = null, object[] parameters = null, object[] orderby = null, string isasc = "");
        public string getUpdateQuery(object[] updateColumnName, string tableName, object[] whereCondition = null);
    }
}
