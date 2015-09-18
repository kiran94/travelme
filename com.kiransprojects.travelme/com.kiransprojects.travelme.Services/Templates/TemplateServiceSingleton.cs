namespace com.kiransprojects.travelme.Services.Templates
{
    using RazorEngine.Configuration;
    using RazorEngine.Templating;

    /// <summary>
    /// RazorEngine's Template Service will cache an email generation so once done the subsequent operations will be very quick, 
    /// this only works on the same instance of Template Service and therefore the class should be generated as a singleton.
    /// </summary>
    public class TemplateServiceSingleton
    {
        /// <summary>
        /// Razor Engine Service
        /// </summary>
        public static IRazorEngineService service { get; set; }

        /// <summary>
        /// Gets an instance of the Razor Engine Service
        /// </summary>
        /// <returns>RazorEngine Service</returns>
        public static IRazorEngineService getInstance()
        {
            if (service == null)
            {
                TemplateServiceConfiguration config = new TemplateServiceConfiguration();
                config.Language = RazorEngine.Language.CSharp;

                service = RazorEngineService.Create(config); 
            }
            return service;
        }
    }
}