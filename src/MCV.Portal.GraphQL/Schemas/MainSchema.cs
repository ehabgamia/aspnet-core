using Abp.Dependency;
using GraphQL;
using GraphQL.Types;
using MCV.Portal.Queries.Container;

namespace MCV.Portal.Schemas
{
    public class MainSchema : Schema, ITransientDependency
    {
        public MainSchema(IDependencyResolver resolver) :
            base(resolver)
        {
            Query = resolver.Resolve<QueryContainer>();
        }
    }
}