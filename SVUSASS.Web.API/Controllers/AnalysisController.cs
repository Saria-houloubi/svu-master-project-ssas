using Microsoft.AnalysisServices.AdomdClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SVUSASS.Logging.IServices;
using SVUSASS.Shared.Messages;
using SVUSASS.Web.API.Controllers.Base;
using SVUSASS.Web.API.Models;
using System.Collections.Generic;

namespace SVUSASS.Web.API.Controllers
{
    /// <summary>
    /// The controller to recive the queries to execute on the analysis server
    /// </summary>
    public class AnalysisController : BaseConroller
    {
        #region Properties

        public IConfiguration Configuration { get; private set; }
        #endregion

        #region Constructer
        /// <summary>
        /// Default constructer
        /// </summary>
        public AnalysisController(ILoggingService loggingService, IConfiguration configuration)
            : base(loggingService)
        {
            Configuration = configuration;
        }
        #endregion

        #region POST Requests
        /// <summary>
        /// Executes a query on the SASS db
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult ExecuteQuery([FromBody] SSASQueryModel model)
        {
            //Check if the data was supplied right
            if (ModelState.IsValid)
            {
                //Get the connection from the config
                var connectionStr = Configuration.GetConnectionString("AnalysisServer");
                //Check that we have the connection
                if (!string.IsNullOrEmpty(connectionStr))
                {
                    //Create the connection
                    using (var connection = new AdomdConnection(connectionStr))
                    {
                        try
                        {
                            //Open the connection to the db
                            connection.Open();
                            //Create the command
                            using (var command = connection.CreateCommand())
                            {
                                //Set the command query
                                command.CommandText = model.Query;
                                //Get the reader
                                using (var reader = command.ExecuteReader())
                                {
                                    //holds the results from the reader as row count and the filedName, value pair
                                    var dbReadResult = new Dictionary<int, KeyValuePair<string, string>>();
                                    //Start reading the data
                                    while (reader.Read())
                                    {
                                        //loop through the filds
                                        for (int i = 0; i < reader.FieldCount; i++)
                                        {
                                            dbReadResult.Add(i, new KeyValuePair<string, string>(reader.GetName(i), reader.GetValue(i).ToString()));
                                        }

                                    }

                                    return Ok(dbReadResult);
                                }
                            }


                        }
                        catch (System.Exception ex)
                        {
                            //Log the error
                            LoggingService.LogException(ex);

                            return InternalServerError(new
                            {
                                message = ErrorMessages.AnalysisServer_UnableToOpenConnnection,
                                Exception = ex.GetBaseException()
                            });
                        }
                        finally
                        {
                            connection.Clone();
                        }

                    }
                }
                return InternalServerError(new
                {
                    message = ErrorMessages.AnalysisServer_NotFound
                });
            }
            return BadRequest();
        }
        #endregion

        #region GET Requests
        #endregion
    }
}
