using System;

namespace SVUSASS.Logging.IServices
{
    /// <summary>
    /// The base logging service functions
    /// </summary>
    public interface ILoggingService
    {
        void LogException(Exception ex);
    }
}
