using Microsoft.AspNetCore.Http;
using SVUSASS.Logging.IServices;
using System;
using System.Threading.Tasks;

namespace SVUSASS.Logging.Services
{
    /// <summary>
    /// The default loggin service class
    /// </summary>
    public class DefaultLoggingSservice : ILoggingService
    {
        #region Constructer
        /// <summary>
        /// Default constructer
        /// </summary>
        public DefaultLoggingSservice()
        {

        }
        #endregion
        public Task LogException(Exception ex)
        {
            Console.WriteLine(ex.GetBaseException().Message);

            return Task.CompletedTask;
        }

        public Task LogRequest(HttpRequest data)
        {
            Console.WriteLine(data.ToString());

            return Task.CompletedTask;
        }
    }
}
