using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;
using JKD_Clicker;

public class JKDPoll
{
    private readonly Options options;

    public JKDPoll(Options options)
    {
        this.options = options;
    }

    public void jKDPoll()
    {
        for (int i = 0; i < options.Repeats; i++)
        {
            Log("Uruchamianie silnika przegl¹darki");
            using ChromeDriverService service = ChromeDriverService.CreateDefaultService();
            service.HideCommandPromptWindow = true;

            ChromeOptions chromeOptions = new ChromeOptions();
            chromeOptions.AddArgument("headless");
            using IWebDriver driver = new ChromeDriver(service, chromeOptions);

            IDictionary<string, object> vars = new Dictionary<string, object>();
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;

            try
            {
                Log($"Otwieranie strony '{options.Link}'");
                driver.Navigate().GoToUrl(options.Link);
                Log($"Ustawianie rozmiaru okna (1936, 1066)");
                driver.Manage().Window.Size = new System.Drawing.Size(1936, 1066);
                if (driver.FindElements(By.CssSelector(".rodo-popup-agree")).Count() > 0)
                {
                    Log("Akceptowanie polityki cookies.");
                    driver.FindElement(By.CssSelector(".rodo-popup-agree")).Click();
                }

                Log($"Oczekiwanie {options.LoadWhait}ms na za³adowanie strony");
                Thread.Sleep(options.LoadWhait);

                Log($"Odnajdywanie elementu radiobutton z JKD!");
                var choose = driver.FindElement(By.XPath("//label[contains(.,\'Jan-Krzysztof Duda\')]"));
                Scroll(js, choose);

                Log($"Klik radiobutton z JKD!");
                choose.Click();

                Log($"Odnajdywanie przycisku Submit");
                var submit = driver.FindElement(By.Id("submit"));
                Scroll(js, submit);

                Log("Klick G³osuj.");
                submit.Click();


                Log($"Oczekiwanie {options.AfterWhait}ms na zakoñczenie procedury.");
                Thread.Sleep(options.AfterWhait);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }

            driver.Quit();
            Console.WriteLine("Wykonano prób: " + (i + 1));
        }
    }

    private static void Scroll(IJavaScriptExecutor js, IWebElement element)
    {
        string scrollElementIntoMiddle = "var viewPortHeight = Math.max(document.documentElement.clientHeight, window.innerHeight || 0);"
                                                            + "var elementTop = arguments[0].getBoundingClientRect().top;"
                                                            + "window.scrollBy(0, elementTop-(viewPortHeight/2));";

        js.ExecuteScript(scrollElementIntoMiddle, element);
    }

    private void Log(string message)
    {
        if (options.Verbose)
            Console.WriteLine(message);
    }
}
