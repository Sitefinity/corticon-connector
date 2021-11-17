using System;
using Telerik.Microsoft.Practices.Unity;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Forms;
using Telerik.Sitefinity.Web.Services.Contracts.Operations;

namespace Telerik.Sitefinity.CorticonConnector
{
    public sealed class Installer
    {
        private Installer()
        {
        }

        /// <summary>
        /// Subscribe to Sitefinity events
        /// </summary>
        public static void PreApplicationStart()
        {
            Bootstrapper.Initialized += Bootstrapper_Initialized;
            Bootstrapper.Bootstrapped += Installer.Bootstrapper_Bootstrapped;
            ObjectFactory.RegisteringIoCTypes += RegisteringIoCTypes;
        }

        private static void Bootstrapper_Bootstrapped(object sender, EventArgs e)
        {
            // Register the Resource class
            Res.RegisterResource<CorticonResources>();
        }

        private static void Bootstrapper_Initialized(object sender, Telerik.Sitefinity.Data.ExecutedEventArgs e)
        {
            // Register form definitions extender
            ObjectFactory.Container.RegisterType<FormsConnectorDefinitionsExtender, CorticonFormsDefinitionsExtender>("Corticon");
        }

        private static void RegisteringIoCTypes(object s, EventArgs args)
        {
            // Register OData service endpoint provider
            ObjectFactory.Container.RegisterType(typeof(IOperationProvider), typeof(CorticonOperationProvider), typeof(CorticonOperationProvider).Name);
        }
    }
}
