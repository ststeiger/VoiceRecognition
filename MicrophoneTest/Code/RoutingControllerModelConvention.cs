
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Authorization;


namespace MicrophoneTest
{

    public class MzApplicationDefaultConvention : IApplicationModelConvention
    {
        public void Apply(ApplicationModel application)
        {
            throw new System.NotImplementedException();
        }
    }


    public class ParameterByDefaultConvention : IParameterModelConvention
    {
        public void Apply(ParameterModel parameter)
        {
            throw new System.NotImplementedException();
        }
    }
    
    
    // https://www.stevejgordon.co.uk/customising-asp-net-mvc-core-behaviour-with-an-iapplicationmodelconvention    
    public class AuthorizeByDefaultConvention : IActionModelConvention
    {
        public void Apply(ActionModel action)
        {
            if (ShouldApplyConvention(action))
            {
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();

                action.Filters.Add(new AuthorizeFilter(policy));
            }
        }

        private bool ShouldApplyConvention(ActionModel action)
        {
            return !action.Attributes.Any(x => x.GetType() == typeof(AuthorizeAttribute)) &&
                   !action.Attributes.Any(x => x.GetType() == typeof(AllowAnonymousAttribute));
        }
    }
    
    
    // https://stackoverflow.com/questions/52874685/asp-net-core-changing-route-on-runtime
    public class RoutingControllerModelConvention : IControllerModelConvention
    {
        
        private readonly IConfiguration _configuration;
        
        
        //public static void HowToRegisterInConfigureServices(IServiceCollection services, IConfiguration configuration)
        //{
        //    services.AddMvc(options =>
        //    {
        //        options.Conventions.Add(new RoutingControllerModelConvention(configuration));
        //        options.Conventions.Add(new AuthorizeByDefaultConvention());
        //    });
        //}


        public RoutingControllerModelConvention(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Apply(ControllerModel controllerModel)
        {
            const string RouteTemplate = "/api/projects/<id>/[action]";

            var routeId = _configuration["RouteIds:" + controllerModel.ControllerName];
            var firstSelector = controllerModel.Selectors[0];

            if (firstSelector.AttributeRouteModel == null)
                firstSelector.AttributeRouteModel = new AttributeRouteModel();

            firstSelector.AttributeRouteModel.Template = RouteTemplate.Replace("<id>", routeId);
        }
    }

}
