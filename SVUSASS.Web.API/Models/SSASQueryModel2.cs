using System.ComponentModel.DataAnnotations;

namespace SVUSASS.Web.API.Models
{
    /// <summary>
    /// The model that holds the sql query
    /// </summary>
    public class SSASQueryModel
    {
        #region Properties
        [Required]
        public string Query { get; set; }
        #endregion
        #region Constructer
        /// <summary>
        /// Default constructer
        /// </summary>
        public SSASQueryModel()
        {

        }
        #endregion
    }
}
