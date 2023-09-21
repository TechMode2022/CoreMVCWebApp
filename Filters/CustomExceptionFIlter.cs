using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Security.Cryptography.Xml;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace CoreMVCWebApp.Filters
{
    public class CustomExceptionFIlter : IExceptionFilter
    {
        private readonly IModelMetadataProvider _modelMetadataProvider;
        public CustomExceptionFIlter( IModelMetadataProvider modelMetadataProvider)
        {
            _modelMetadataProvider= modelMetadataProvider;
        }

        public void OnException(ExceptionContext context)
        {
            var result = new ViewResult { ViewName = "CustomError" };
            result.ViewData = new ViewDataDictionary(_modelMetadataProvider, context.ModelState);
            result.ViewData.Add("Exception", context.Exception);

            // Here we can pass additional detailed data via viewData
            context.ExceptionHandled= true; // mark exception as handled
            context.Result = result;
        }
    }
}
