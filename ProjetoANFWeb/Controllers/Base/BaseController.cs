using Newtonsoft.Json;
using ProjetoANFCore.Entities;
using System.Configuration;
using System.Text;
using System.Web.Mvc;

namespace ProjetoANFWeb.Controllers.Base
{
    public class BaseController : Controller
    {

        #region Attributes
        #endregion

        #region Properties

        protected string ServiceEndpoint
        {
            get
            {
                return ConfigurationManager.AppSettings["ServiceEndpoint"];
            }
        }

        #endregion

        #region Constants
        #endregion

        #region Constructors
        #endregion

        #region Actions

        #region Get
        #endregion

        #region Post
        #endregion

        #region Put
        #endregion

        #endregion

        #region Methods

        #region Private
        #endregion

        #region Public
        #endregion

        #region Protected
        //TODO: Comentar direito.
        /// <summary>
        /// Overrides the OnException method from the Controller class.
        /// Treats our custom exceptions.
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.Exception == null)
                return;

            var exception = (filterContext.Exception as ViewException);

            if (exception.GetType() == typeof(ViewException))
            {
                filterContext.ExceptionHandled = true;
                filterContext.HttpContext.Response.Clear();
                filterContext.HttpContext.Response.ContentEncoding = Encoding.UTF8;
                filterContext.HttpContext.Response.HeaderEncoding = Encoding.UTF8;
                filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
                filterContext.HttpContext.Response.StatusCode = 500;
                filterContext.HttpContext.Response.StatusDescription = JsonConvert.SerializeObject(new { title = exception.ErrorMessage, content = exception.ReasonPhrase });
            }
            else
            {
                filterContext.ExceptionHandled = true;
                filterContext.HttpContext.Response.Clear();
                filterContext.HttpContext.Response.ContentEncoding = Encoding.UTF8;
                filterContext.HttpContext.Response.HeaderEncoding = Encoding.UTF8;
                filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
                filterContext.HttpContext.Response.StatusCode = 500;
                filterContext.HttpContext.Response.StatusDescription = JsonConvert.SerializeObject(new { title = "Erro", content = filterContext.Exception.Message });
            }

            base.OnException(filterContext);
        }

        #endregion

        #endregion

        #region Events
        #endregion
    }
}