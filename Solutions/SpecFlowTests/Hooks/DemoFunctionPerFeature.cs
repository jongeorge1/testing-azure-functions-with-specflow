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

        [BeforeScenario("usingDemoFunctionPerFeatureWithAdditionalConfiguration")]
        public static Task StartFunctionWithAdditionalConfigurationAsync(FeatureContext featureContext)
        {
            var functionConfiguration = new FunctionConfiguration();
            functionConfiguration.EnvironmentVariables.Add("ResponseMessage", "Welcome, {name}");
            featureContext.Set(functionConfiguration);

            return StartFunctionsAsync(featureContext);
        }

        [AfterFeature("usingDemoFunctionPerFeature", "usingDemoFunctionPerFeatureWithAdditionalConfiguration")]
        public static void StopFunction(FeatureContext featureContext)
        {
            FunctionsController functionsController = featureContext.Get<FunctionsController>();
            functionsController.TeardownFunctions();
        }
    }
}
