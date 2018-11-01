using ProjetoANFCore.Entities;
using ProjetoANFCore.Extensions;
using ProjetoANFService.Models.Error;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;

namespace ProjetoANFService.Infrastructure.Handlers
{
    public class GlobalExceptionHandler : ExceptionHandler
    {
        public override void Handle(ExceptionHandlerContext context)
        {
            if(context.Exception.GetType() == typeof(ApiException))
            {
                context.Result = new ErrorMessageResult(context.Request,
                    ((ApiException)context.Exception).Description, ((ApiException)context.Exception).Title);
            }
            else if(context.Exception.GetType() == typeof(SqlException))
            {
                context.Result = new ErrorMessageResult(context.Request,
                    $"[{((SqlException)context.Exception).Number}] : {((SqlException)context.Exception).Message}", "Sql Error");
            }
            else
            {
                context.Result = new ErrorMessageResult(context.Request,
                    context.Exception.GetAllMessages(), "Error");
            }

            base.Handle(context);
        }

        public override Task HandleAsync(ExceptionHandlerContext context, CancellationToken cancellationToken)
        {
            if (context.Exception.GetType() == typeof(ApiException))
            {
                context.Result = new ErrorMessageResult(context.Request,
                    ((ApiException)context.Exception).Description, ((ApiException)context.Exception).Title);
            }
            else if (context.Exception.GetType() == typeof(SqlException))
            {
                context.Result = new ErrorMessageResult(context.Request,
                    $"[{((SqlException)context.Exception).Number}] : {((SqlException)context.Exception).Message}", "Sql Error");
            }
            else
            {
                context.Result = new ErrorMessageResult(context.Request,
                    context.Exception.GetAllMessages(), "Error");
            }
            return base.HandleAsync(context, cancellationToken);
        }
    }
}