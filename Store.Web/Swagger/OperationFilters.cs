using System;
using System.Collections.Generic;
using System.Linq;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Store.Web.Swagger
{
    public class OperationFilters
    {
        public class RemoveVersionParameters : IOperationFilter
        {
            public void Apply(Operation operation, OperationFilterContext context)
            {
                IParameter parameter = operation.Parameters.Single<IParameter>((Func<IParameter, bool>)(p => p.Name == "version"));
                operation.Parameters.Remove(parameter);
            }
        }

        public class SetVersionInPaths : IDocumentFilter
        {
            public void Apply(SwaggerDocument swaggerDoc, DocumentFilterContext context)
            {
                swaggerDoc.Paths = (IDictionary<string, PathItem>)swaggerDoc.Paths.ToDictionary<KeyValuePair<string, PathItem>, string, PathItem>((Func<KeyValuePair<string, PathItem>, string>)(path => path.Key.Replace("v{version}", swaggerDoc.Info.Version)), (Func<KeyValuePair<string, PathItem>, PathItem>)(path => path.Value));
            }
        }
    }
}
