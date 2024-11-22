namespace onboarding_dotnet.Infrastructures.QueryParam;

using Microsoft.AspNetCore.Mvc.ModelBinding;

public class SnakeCaseQueryBinderProvider : IModelBinderProvider
{
    public IModelBinder? GetBinder(ModelBinderProviderContext context)
    {   
        // Apply only to models explicitly bound to query parameters
        if (context.BindingInfo.BindingSource == BindingSource.Query)
        {
            return new SnakeCaseQueryModelBinder();
        }

        return null;
    }
}
