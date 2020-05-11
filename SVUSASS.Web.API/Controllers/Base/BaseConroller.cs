using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SVUSASS.Logging.IServices;

namespace SVUSASS.Web.API.Controllers.Base
{
    /// <summary>
    /// The base controller for cross functions and data
    /// </summary>
    [Route("api/[controller]/[action]")]
    public class BaseConroller : Controller
    {
        #region Properties
        /// <summary>
        /// The service to store any logs
        /// </summary>
        public ILoggingService LoggingService { get; private set; }
        #endregion

        #region Constructer
        /// <summary>
        /// Default constructer
        /// </summary>
        public BaseConroller(ILoggingService loggingService)
        {
            LoggingService = loggingService;
        }
        #endregion

        #region Helpers
        /// <summary>
        /// Returns a 500 internal server error with the sent message
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        protected IActionResult InternalServerError(object message = null) => StatusCode(StatusCodes.Status500InternalServerError, message);
        #endregion
    }
}
