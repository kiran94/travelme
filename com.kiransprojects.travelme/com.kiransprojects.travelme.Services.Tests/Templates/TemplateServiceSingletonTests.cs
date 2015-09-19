namespace com.kiransprojects.travelme.Services.Tests.Templates
{
    using com.kiransprojects.travelme.Services.Templates;
    using NUnit.Framework;
    using RazorEngine.Templating;

    [TestFixture]
    public class TemplateServiceSingletonTests
    {
        /// <summary>
        /// Ensures a new instance can be created when not existing.
        /// </summary>
        [Test]
        public void TemplateService_NonExisting_NewGenerated()
        {
            IRazorEngineService service = TemplateServiceSingleton.getInstance();
            Assert.NotNull(service);
        }

        /// <summary>
        /// Ensures the same is retrieved when existing. 
        /// </summary>
        [Test]
        public void TemplateService_Exisiting_SameRetrieved()
        {
            IRazorEngineService first = TemplateServiceSingleton.getInstance();
            IRazorEngineService second = TemplateServiceSingleton.getInstance();

            Assert.AreEqual(first, second);
        }



    }
}