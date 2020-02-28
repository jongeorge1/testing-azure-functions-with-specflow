namespace SpecFlowTests.Hooks
{
    using System.Threading.Tasks;
    using Corvus.SpecFlow.Extensions;
    using TechTalk.SpecFlow;

    [Binding]
    public static class DemoFunctionPerScenario
    {
        [BeforeScenario("usingDemoFunctionPerScenario")]
        public static Task StartFunctionsAsync(FeatureContext featureContext, ScenarioContext scenarioContext)
        {
            var functionsController = new FunctionsController();
            scenarioContext.Set(functionsController);

            return functionsController.StartFunctionsInstance(
                featureContext,
                scenarioContext,
                "DemoFunction",
                7075,
                "netcoreapp3.0");
        }

        [AfterScenario("usingDemoFunctionPerScenario")]
        public static void StopFunction(ScenarioContext scenarioContext)
        {
            FunctionsController functionsController = scenarioContext.Get<FunctionsController>();
            functionsController.TeardownFunctions();
        }
    }
}
