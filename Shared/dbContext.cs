using System.Text;
using yatracub.Models;

namespace yatracub.Shared
{
    public interface IdbContext
    {
        public string getConnectionString();
    }
    public class dbContext : IdbContext
    {
        public readonly AppSettings _appSettings;
        //internal readonly object Certificates;
        private IConfiguration _configuration;
        public dbContext(AppSettings appSettings, IConfiguration configuration)
        {
            _appSettings = appSettings;
            _configuration = configuration;
        }

        public string getConnectionString()
        {
            //var Database = _configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;
            return new StringBuilder().Append("Server=").Append(_appSettings.DataSourcce)
                                        .Append(";Database=").Append(_appSettings.Database)
                                            .Append(";User ID=").Append(_appSettings.userId)
                                          .Append(";Password=").Append(_appSettings.Password)
                                       .Append("Trusted_Connection=True;MultipleActiveResultSets=true;Integrated Security=true; Encrypt=false")
                                    .ToString();
        }

    }
}