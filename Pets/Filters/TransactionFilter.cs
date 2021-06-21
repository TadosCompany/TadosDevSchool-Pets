namespace Pets.Filters
{
    using System;
    using System.Data.Common;
    using System.Threading.Tasks;
    using Database.Abstractions;
    using Microsoft.AspNetCore.Mvc.Filters;


    public class TransactionFilter : IAsyncActionFilter
    {
        private readonly IDbTransactionProvider _dbTransactionProvider;


        public TransactionFilter(IDbTransactionProvider dbTransactionProvider)
        {
            _dbTransactionProvider =
                dbTransactionProvider ?? throw new ArgumentNullException(nameof(dbTransactionProvider));
        }


        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            ActionExecutedContext actionExecutedContext = await next();

            if (_dbTransactionProvider.IsInitialized)
            {
                DbTransaction dbTransaction = await _dbTransactionProvider.GetCurrentTransactionAsync();
                
                if (actionExecutedContext.Exception != null)
                {
                    await dbTransaction.RollbackAsync();
                }
                else
                {
                    await dbTransaction.CommitAsync();
                }
            }
        }
    }
}