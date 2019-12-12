
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MicrophoneTest
{



    // https://stackoverflow.com/questions/52874685/asp-net-core-changing-route-on-runtime
    public class RoutingControllerModelConvention : IControllerModelConvention
    {
        private readonly IConfiguration _configuration;


        //public static void HowToRegisterInConfigureServices(IServiceCollection services, IConfiguration configuration)
        //{
        //    services.AddMvc(options =>
        //    {
        //        options.Conventions.Add(new RoutingControllerModelConvention(configuration));
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
