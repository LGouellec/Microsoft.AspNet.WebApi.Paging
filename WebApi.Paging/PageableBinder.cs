using System;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;
using WebApi.Paging.Pageable;

namespace WebApi.Paging
{
    public class PageableBinder : IModelBinder
    {
        private readonly PageableBinderConfig _binderConfig = new PageableBinderConfig();

        private static int ParseIntOrDefault(string parameter, int defaultValue, int upper = int.MaxValue)
        {
            if (!int.TryParse(parameter, out int value)) value = defaultValue;

            value = value < 0 ? 0 : value;
            value = value > upper ? upper : value;

            return value;
        }

        public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
        {
            if (bindingContext.ModelType != typeof(IPageable))
                return false;

            var hasPrefix = bindingContext.ValueProvider.ContainsPrefix(bindingContext.ModelName);
            var searchPrefix = (hasPrefix) ? bindingContext.ModelName + "." : "";

            string spageNumber = GetValue(bindingContext, searchPrefix, _binderConfig.PageParameterName);
            string spageSize = GetValue(bindingContext, searchPrefix, _binderConfig.SizeParameterName);

            bool r1 = Int32.TryParse(spageNumber, out int pageNumber);
            bool r2 = Int32.TryParse(spageSize, out int pageSize);

            if (r1 && r2)
                bindingContext.Model = Pageable.Pageable.Of(pageNumber, pageSize);
            else
                bindingContext.Model = PageableConstants.UnPaged;

            return true;
        }

        private string GetValue(ModelBindingContext context, string prefix, string key)
        {
            var result = context.ValueProvider.GetValue(prefix + key);
            return result?.AttemptedValue;
        }
    }
}
