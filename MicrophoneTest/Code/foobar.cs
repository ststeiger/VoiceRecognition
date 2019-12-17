using System.Globalization;
using System;
using System.Net.Http;
using System.Reflection.Emit;
using MicrophoneTest.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;


namespace MicrophoneTest.Code
{

    using System.Net.Http.Headers;


    class TestMethodAttribute : System.Attribute
    {
    }
    
    class faa
    {


        [TestMethod]
        public void TestDI()
        {
            var sc = new ServiceCollection();
            sc.AddSingleton<string>("oo");
            // services.AddSingleton<IControllerFactory, MyCustomControllerFactory>();
            
            IServiceProvider provider = sc.BuildServiceProvider();
            
            System.Type t = System.Type.GetType("");
            
            
            // Microsoft.Extensions.DependencyInjection.Abstractions
            Microsoft.Extensions.DependencyInjection.ActivatorUtilities.CreateInstance(provider, t); 
            // is there a way to use ActivatorUtilities.CreateInstance(type, serviceProvider) with hybrid parameters, some from the container some provided yourself? 
            // Yes, it has extra arguments: 
            // CreateInstance(IServiceProvider provider, Type instanceType,params object[] parameters)
        }

        [TestMethod]
        public void TestCreateController()
        {
            IControllerActivator controllerActivator = null;
            System.Collections.Generic.IEnumerable<Microsoft.AspNetCore.Mvc.Internal.IControllerPropertyActivator> propertyActivators = null;            
            DefaultControllerFactory fac = new DefaultControllerFactory(controllerActivator, propertyActivators);
            
            ControllerContext cc = new ControllerContext();
            
            fac.CreateController(cc);
        }


        [TestMethod]
        public void TestValuesController()
        {
            
            
            
            // ValuesController controller = new ValuesController();
            HomeController controller = new HomeController();
            
            
            controller.ControllerContext = new ControllerContext();
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            
            controller.ControllerContext.HttpContext.Request.Headers["device-id"] = "20317";

            IActionResult result = controller.Index();
            
            //var viewResult = Assert.IsType<ViewResult>(result);
            System.Diagnostics.Debug.Assert(result is ViewResult);

            //the controller correctly receives the http header key value pair device-id:20317

            // var contentResult = Assert.IsType<ContentResult>(result);
            // System.Diagnostics.Debug.Assert(result is ContentResult);
            
            ContentResult cr = (ContentResult) result;
            System.Diagnostics.Debug.Assert("Session not found.".Equals( cr.Content));
        }
        
        
        public static void ParseValue( string v, string [] a)
        {
            // Microsoft.Net.Http.Headers.StringWithQualityHeaderValue.Parse(x);
            
            // Microsoft.Net.Http.Headers.StringWithQualityHeaderValue.MultipleValueStringWithQualityParser
            Microsoft.Net.Http.Headers.StringWithQualityHeaderValue.ParseList(v.Split(';'));
            
            System.Net.Http.Headers.StringWithQualityHeaderValue.Parse(v);
            Microsoft.Net.Http.Headers.StringWithQualityHeaderValue.Parse(new StringSegment(v));
        }
    }
}
