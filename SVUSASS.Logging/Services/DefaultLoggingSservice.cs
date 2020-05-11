using SVUSASS.Logging.IServices;
using System;

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
        public void LogException(Exception ex)
        {
            Console.WriteLine(ex.GetBaseException().Message);
        }
    }
}
