using Microsoft.AspNetCore.Mvc.Filters;

namespace GameBackend.Library.Filters
{
    public class JsonResultFilter : IResultFilter
    {
        public void OnResultExecuted(ResultExecutedContext context)
        {
            throw new NotImplementedException();
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            throw new NotImplementedException();
        }
    }
}
