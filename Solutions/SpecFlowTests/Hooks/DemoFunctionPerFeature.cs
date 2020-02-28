namespace SpecFlowTests.Hooks
{
    using System.Threading.Tasks;
    using Corvus.SpecFlow.Extensions;
    using TechTalk.SpecFlow;

    [Binding]
    public static class DemoFunctionPerFeature
    {
        [BeforeFeature("usingDemoFunctionPerFeature")]
        public static Task StartFunctionsAsync(FeatureContext featureContext)
        {
            var functionsController = new FunctionsController();
            featureContext.Set(functionsController);

            return functionsController.StartFunctionsInstance(
                featureContext,
                null,
                "DemoFunction",
                7075,
                "netcoreapp3.0");
        }

        [AfterFeature("usingDemoFunctionPerFeature")]
        public static void StopFunction(FeatureContext featureContext)
        {
            FunctionsController functionsController = featureContext.Get<FunctionsController>();
            functionsController.TeardownFunctions();
        }
    }
}
