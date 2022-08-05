using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace TestProject1
{
    [TestFixture]
    public class Tests
    {
        IWebDriver webDriver;

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void HomepageTest()
        {
            StartChromeDriver();
            FindTitle();
            FindURL();
            FindLogo();
            ClickLogo();
            NavigateNavbar("MEN");
            NavigateBack();
            NavigateForward();
            Refresh();
            Assert.That(webDriver.Url, Is.EqualTo("http://qa1magento.dev.evozon.com/men.html"));

        }
        [Test]
        public void AccountTest()
        {
            StartChromeDriver();
            ClickAccount();
            Assert.AreEqual(MyAccount(), "My Account");
        }
        [Test]
        public void LanguagesTest()
        {
            StartChromeDriver();
            ListLanquages(1);
            ClickLanguages();
            Assert.AreEqual(French(),"French");
        }


         [Test]
         public void SearchTest()
         {
             StartChromeDriver();
             ClearSearch();
             AddInSearch("woman");
             Assert.That("http://qa1magento.dev.evozon.com/catalogsearch/result/?q=woman", Is.EqualTo(webDriver.Url));
        }
        
         [Test]
         public void NewProductList()
         {
             StartChromeDriver();
             ListNewProducts();
            Assert.AreEqual(5, ListNewProducts());
            //PT TETSTUL ASTA DECOMENTAM LINIA CU ENV QA2
         }
         [Test]

         public void NavbarTest()
         {
             StartChromeDriver();
             NavigateNavbar("SALE");
             Assert.That("http://qa1magento.dev.evozon.com/sale.html", Is.EqualTo(webDriver.Url));
         }

        [TearDown]
        public void After()
        {
            Quit();
        }

        public void StartChromeDriver()
        {
            webDriver = new ChromeDriver("C://Users//denis//OneDrive//Desktop//internshipEvozon//TestProject1//TestProject1");
            webDriver.Manage().Window.Maximize();
            webDriver.Navigate().GoToUrl("http://qa1magento.dev.evozon.com");
            //webDriver.Navigate().GoToUrl("http://qa2magento.dev.evozon.com");
        }
        public void FindTitle()
        {
            Console.WriteLine("Title is: " + webDriver.Title);
        }

        public void FindURL()
        {
            string url = webDriver.Url;
            Console.WriteLine("URL este: " + url);
        }
        public void FindLogo()
        {
            IWebElement getLogo = webDriver.FindElement(By.CssSelector(".logo"));
            Console.WriteLine("Logo is: " + getLogo);
        }
        public void ClickLogo()
        {
            IWebElement clickLogo = webDriver.FindElement(By.CssSelector(".logo"));
            clickLogo.Click();
        }
        public void NavigateNavbar(string name)
        {
            IList<IWebElement> productList = webDriver.FindElements(By.CssSelector("#nav li.level0"));

            foreach (var i in productList)
                Console.WriteLine(i.Text);

            Console.WriteLine("Nr de Categorii este " + productList.Count());

            foreach (var i in productList)
            {
                if (i.Text.Equals(name))
                {
                    Console.WriteLine(i);
                    i.Click();
                    break;
                }
            }
            //sau un switch case pt categories fiecare cu selectorul.
            //sau metode pt fiecare categories 
        }
        public void NavigateBack()
        {
            webDriver.Navigate().Back();
        }
        public void NavigateForward()
        {
            webDriver.Navigate().Forward();
        }
        public void Refresh()
        {
            webDriver.Navigate().Refresh();
        }
        public void Quit()
        {
            webDriver.Quit();
        }
        public void ClickAccount()
        {
            IWebElement clickLogo = webDriver.FindElement(By.CssSelector(".skip-link.skip-account"));
            clickLogo.Click();
        }
        public string MyAccount()
        {
            IWebElement myAccount = webDriver.FindElement(By.CssSelector("#header-account .links .first"));
            return myAccount.Text;
        }
        public void ListLanquages(int number)
        {
            IWebElement language = webDriver.FindElement(By.Id("select-language"));

            SelectElement l = new SelectElement(language);

            Console.WriteLine("Number Lanquages List " + l.Options.Count);

            l.SelectByIndex(number);

        }
        public void ClickLanguages()
        {
            IWebElement clickLanguages = webDriver.FindElement(By.Id("select-language"));
            clickLanguages.Click();
        }

        public string French()
        {
            IWebElement list = webDriver.FindElement(By.CssSelector("#select-language"));
            SelectElement french = new SelectElement(list);
            return french.SelectedOption.Text;
        }
        public void ClearSearch()
        {
            IWebElement searchBar = webDriver.FindElement(By.CssSelector("#search"));
            searchBar.Clear();
        }
        public void AddInSearch(string word)
        {
            IWebElement searchBar = webDriver.FindElement(By.CssSelector("#search"));
            searchBar.SendKeys(word);
            searchBar.Submit();
        }

        public int ListNewProducts()
        {
            IList<IWebElement> newProducts = webDriver.FindElements(By.CssSelector(".products-grid.products-grid .item"));

            Console.WriteLine("Number NewProducts List " + newProducts.Count());

            foreach (var i in newProducts)
            {
                Console.WriteLine(i);
            }
            return newProducts.Count();
        }

    }
}
