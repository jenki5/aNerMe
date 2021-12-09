using System;
using System.Runtime;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Anerme.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace Anerme.Controllers
{
    public class HomeController : Controller
    {
        private MyContext dbContext;
        public HomeController(MyContext context)
        {
            dbContext = context;
        }

        public int GetPrice(string Price)
        {
            int price = 0;

            for (int i = 0; i < Price.Length; i++)
            {
                if (Price[i] >= '0' && Price[i] <= '9')
                {
                    price *= 10;
                    price += Price[i] - '0';
                }
            }

            return price;
        }

        public void LoadBristleconeProducts()
        {
            List<Product> Products = new List<Product>();
            HtmlAgilityPack.HtmlWeb web = new HtmlAgilityPack.HtmlWeb();
            HtmlAgilityPack.HtmlDocument doc = web.Load("https://www.bristleconebikes.com/product-list/bikes-1000/mountain-1006/");
            var SubPages = doc.DocumentNode.SelectNodes("//a[@target='_self']");
            for (int i = 0; i < SubPages.Count; i++)
            {
                string webpage = SubPages[i].GetAttributeValue("href", "href");
                HtmlAgilityPack.HtmlDocument doc1 = web.Load(webpage);
                var Brand = doc1.DocumentNode.SelectNodes("//span[@class='seBrandName']");
                var ProductPageLink = doc1.DocumentNode.SelectNodes("//a[@class='seProductAnchor']");
                var titles = doc1.DocumentNode.SelectNodes("//span[@class='seItemName']");
                var price = doc1.DocumentNode.SelectNodes("//span[@class='seRegularPrice']");
                var images = doc1.DocumentNode.SelectNodes("//img[@class='img-responsive seResultImage']");
                if(Brand != null)
                {
                    for (int x = 0; x < Brand.Count; x++)
                    {
                        try
                        {
                            Console.WriteLine($"Brand: {Brand[x].InnerText}; Title: {titles[x].InnerText}; Price: {price[x].InnerText}; Image: {images[x].GetAttributeValue("src","src")}; Link: {ProductPageLink[x].GetAttributeValue("href", "href")}.");
                            HtmlAgilityPack.HtmlDocument ProductPage = web.Load("https://www.bristleconebikes.com" + ProductPageLink[x].GetAttributeValue("href", "href"));
                            var ProductImages = ProductPage.DocumentNode.SelectNodes("//li[@class='seitemimagecarousel-item touchcarousel-item']");
                            //var help = ProductPage.DocumentNode.SelectNodes("//h1[@class='header seProductInformationTitle']//span")[2].InnerText;
                            Product NewProduct = new Product()
                            {
                                ProductImages = new List<ProductImage>(),
                                CreatedAt = DateTime.Now,
                                CompanyID = 10,
                                Inventory = 10,
                                ProductTitle = ProductPage.DocumentNode.SelectNodes("//span[@class='seProductBrandName']")[0].InnerText,
                                HighLevelDesc = ProductPage.DocumentNode.SelectNodes("//h1[@class='header seProductInformationTitle']//span")[2].InnerText,
                                DetailedDesc = ProductPage.DocumentNode.SelectNodes("//p[@class='seProductPrimaryDescription']")[0].InnerText
                            };

                            string StringPrice = ProductPage.DocumentNode.SelectNodes("//div[@class='seRegularPrice']")[0].InnerText;
                            NewProduct.Price = GetPrice(StringPrice);
                            if(ProductImages != null)
                            {
                                for (int j = 0; j < ProductImages.Count; j++)
                                {
                                    try
                                    {
                                        string path = ProductImages[j].FirstChild.NextSibling.GetAttributeValue("src", "src");
                                        path = path.Replace("micro", "large");
                                        Console.WriteLine(path);

                                        Image NewImage = new Image()
                                        {
                                            ImagePath = ProductImages[j].FirstChild.NextSibling.GetAttributeValue("src", "src"),
                                            ImageName = ProductImages[j].Id,
                                            CompanyID = 10
                                        };

                                        ProductImage PI = new ProductImage()
                                        {
                                            Image = NewImage
                                        };

                                        NewProduct.ProductImages.Add(PI);
                                    }
                                    catch { }
                                }
                            }

                            Products.Add(NewProduct);
                        }
                        catch 
                        {
                            Console.WriteLine($"Could not print for site page: {webpage}");
                        }
                    }
                }
            }

            Products = Products.GroupBy(c => c.DetailedDesc).Select(c => c.First()).ToList();
            dbContext.Products.AddRange(Products);
            dbContext.SaveChanges();
        }

        // public void ToLarge()
        // {
        //     List<Image> Images = dbContext.Images.Where(c => c.CompanyID == 10).ToList();
        //     for(int i = 0; i < Images.Count(); i++)
        //     {
        //         Images[i].ImagePath = Images[i].ImagePath.Replace("micro", "large");
        //     }

        //     dbContext.SaveChanges();
        // }

        [HttpGet("")]
        public IActionResult Index()
        {
            IndexModel IM = new IndexModel();

            IM.FeaturedCompanies = dbContext.Companies.Where(c => c.CompanyID > 6)
                                        .Include(c => c.Banner)
                                        .ThenInclude(c => c.Image)
                                        .Take(6).ToList();

            IM.FeaturedCompanies.Remove(IM.FeaturedCompanies[1]);

            return View(IM);
        }

        [HttpGet("ContactUs")]
        public IActionResult ContactUs()
        {
            return View();
        }

        [HttpGet("Login")]
        public IActionResult Login()
        {
            ViewBag.LoginError = HttpContext.Session.GetString("LoginError");
            HttpContext.Session.SetString("LoginError", "");
            return View();
        }

        [HttpGet("Local/{CompanyName}/{CompanyID}")]
        public IActionResult Local(string CompanyName, int CompanyID)
        {
            Administrator admin = new Administrator();
            admin.company = dbContext.Companies.Where(c => c.CompanyID == CompanyID)
                                        .Include(c => c.Banner)
                                        .ThenInclude(c => c.Image)
                                        .Include(c => c.Banner)
                                        .ThenInclude(c => c.BannerPartial)                                     
                                        .Include(c => c.Banner)
                                        .ThenInclude(c => c.MenuItems)
                                        .ThenInclude(c => c.DropDownItems)
                                        .FirstOrDefault();

            admin.company.Banner.RemoveParentMenuItems();

            if(admin.company.Banner.MenuItems[0].MenuActionID == 1)
            {
                admin.company.MenuItem = dbContext.MenuItems
                                                .Where(c => c.MenuItemID == admin.company.Banner.MenuItems[0].MenuItemID)
                                                .Include(c => c.ContactUsPage)
                                                .ThenInclude(c => c.Image)
                                                .Include(c => c.ContactUsPage)
                                                .ThenInclude(c => c.Address)
                                                .ThenInclude(c => c.StreetAddress)
                                                .Include(c => c.ContactUsPage)
                                                .ThenInclude(c => c.Address)
                                                .ThenInclude(c => c.City)
                                                .Include(c => c.ContactUsPage)
                                                .ThenInclude(c => c.Address)
                                                .ThenInclude(c => c.State)
                                                .Include(c => c.InformationPage)
                                                .Include(c => c.PageCategories)
                                                .ThenInclude(c => c.Category)
                                                .FirstOrDefault();
            }
            else if(admin.company.Banner.MenuItems[0].MenuActionID == 2)
            {
                admin.company.MenuItem = dbContext.MenuItems
                                                .Where(c => c.MenuItemID == admin.company.Banner.MenuItems[0].DropDownItems[0].MenuItemID)
                                                .Include(c => c.ContactUsPage)
                                                .ThenInclude(c => c.Image)
                                                .Include(c => c.ContactUsPage)
                                                .ThenInclude(c => c.Address)
                                                .ThenInclude(c => c.StreetAddress)
                                                .Include(c => c.ContactUsPage)
                                                .ThenInclude(c => c.Address)
                                                .ThenInclude(c => c.City)
                                                .Include(c => c.ContactUsPage)
                                                .ThenInclude(c => c.Address)
                                                .ThenInclude(c => c.State)
                                                .Include(c => c.InformationPage)
                                                .Include(c => c.PageCategories)
                                                .ThenInclude(c => c.Category)
                                                .FirstOrDefault();
            }

            if(admin.company.MenuItem.LTPActionID == 1)
            {
                admin.company.Products = dbContext.Products
                    .Where(c => c.CompanyID == CompanyID)
                    .Include(c => c.ProductImages)
                    .ThenInclude(c => c.Image)
                    .Include(c => c.ProductCategories)
                    .ToList();

                for(int i = 0; i < admin.company.Products.Count(); i++)
                {
                    bool found = false;
                    for(int j = 0; j < admin.company.Products[i].ProductCategories.Count(); j++)
                    {
                        for(int x = 0; x < admin.company.MenuItem.PageCategories.Count(); x++)
                        {
                            if(admin.company.Products[i].ProductCategories[j].CategoryID == admin.company.MenuItem.PageCategories[x].CategoryID)
                            {
                                found = true;
                                break;
                            }
                        }
                    }
                    if(!found)
                    {
                        admin.company.Products.Remove(admin.company.Products[i]);
                        i--;
                    }
                    else
                    {
                        admin.company.Products[i].company = null;
                        admin.company.Products[i].ProductImages = admin.company.Products[i].ProductImages.OrderBy(c => c.OrderNumber).ToList();
                        admin.company.Products[i].SetStorePrice();
                    }
                }
            }
            
            return View(admin);
        }

        [HttpGet("Local/{CompanyName}/{CompanyID}/{PageID}")]
        public IActionResult Local(string CompanyName, int CompanyID, int PageID)
        {
            Administrator admin = new Administrator();
            admin.company = dbContext.Companies.Where(c => c.CompanyID == CompanyID)
                                        .Include(c => c.Banner)
                                        .ThenInclude(c => c.Image)
                                        .Include(c => c.Banner)
                                        .ThenInclude(c => c.BannerPartial)
                                        .Include(c => c.Banner)
                                        .ThenInclude(c => c.MenuItems)
                                        .ThenInclude(c => c.DropDownItems)
                                        .FirstOrDefault();

            admin.company.Banner.RemoveParentMenuItems();

            admin.company.MenuItem = dbContext.MenuItems
                                .Where(c => c.MenuItemID == PageID)
                                .Include(c => c.ContactUsPage)
                                .ThenInclude(c => c.StoreDeliveryMethods)
                                .ThenInclude(c => c.DeliveryMethod)
                                .Include(c => c.ContactUsPage)
                                .ThenInclude(c => c.Image)
                                .Include(c => c.ContactUsPage)
                                .ThenInclude(c => c.Address)
                                .ThenInclude(c => c.StreetAddress)
                                .Include(c => c.ContactUsPage)
                                .ThenInclude(c => c.Address)
                                .ThenInclude(c => c.City)
                                .Include(c => c.ContactUsPage)
                                .ThenInclude(c => c.Address)
                                .ThenInclude(c => c.State)
                                .Include(c => c.InformationPage)
                                .Include(c => c.PageCategories)
                                .ThenInclude(c => c.Category)
                                .FirstOrDefault();

            if(admin.company.MenuItem.LTPActionID == 1)
            {
                admin.company.Products = dbContext.Products
                    .Where(c => c.CompanyID == CompanyID)
                    .Include(c => c.ProductImages)
                    .ThenInclude(c => c.Image)
                    .Include(c => c.ProductCategories)
                    .ToList();

                for(int i = 0; i < admin.company.Products.Count(); i++)
                {
                    bool found = false;
                    for(int j = 0; j < admin.company.Products[i].ProductCategories.Count(); j++)
                    {
                        for(int x = 0; x < admin.company.MenuItem.PageCategories.Count(); x++)
                        {
                            if(admin.company.Products[i].ProductCategories[j].CategoryID == admin.company.MenuItem.PageCategories[x].CategoryID)
                            {
                                found = true;
                                break;
                            }
                        }
                    }
                    if(!found)
                    {
                        admin.company.Products.Remove(admin.company.Products[i]);
                        i--;
                    }
                    else
                    {
                        admin.company.Products[i].company = null;
                        admin.company.Products[i].ProductImages = admin.company.Products[i].ProductImages.OrderBy(c => c.OrderNumber).ToList();
                        admin.company.Products[i].SetStorePrice();
                    }
                }
            }
            else if(admin.company.MenuItem.LTPActionID == 3)
            {
                admin.company.MenuItem.ContactUsPage.SetStringOpenClose();
            }

            

            return View(admin);
        }

        [HttpGet("Product/{ProductID}")]
        public IActionResult Product(int ProductID)
        {
            Administrator admin = new Administrator()
            {
                company = new Company(),
                User = new User()
            };

            Product dbProduct = dbContext.Products.Where(c => c.ProductID == ProductID)
                                            .Include(c => c.ProductImages)
                                            .ThenInclude(c => c.Image)
                                            .FirstOrDefault();

            dbProduct.SetStorePrice();
            dbProduct.ProductImages = dbProduct.ProductImages.OrderBy(c => c.OrderNumber).ToList();

            admin.company = dbContext.Companies.Where(c => c.CompanyID == dbProduct.CompanyID)
                                    .Include(c => c.Banner)
                                    .ThenInclude(c => c.Image)
                                    .Include(c => c.Banner)
                                    .ThenInclude(c => c.BannerPartial)
                                    .Include(c => c.Banner)
                                    .ThenInclude(c => c.MenuItems)
                                    .ThenInclude(c => c.DropDownItems)
                                    .FirstOrDefault();

            admin.company.Products = new List<Product>();
            admin.company.Products.Add(dbProduct);
            admin.company.Products[0].company = null;
            admin.company.Products[0].SetParagraphs();

            return View(admin);
        }

        [HttpGet("Logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        [HttpGet("Register")]
        public IActionResult Register()
        {
            ViewBag.RegisterError = HttpContext.Session.GetString("RegisterError");
            HttpContext.Session.SetString("RegisterError", "");
            return View();
        }

        [HttpGet("UserDashboard")]
        public IActionResult UserDashboard()
        {
            int? UserID = HttpContext.Session.GetInt32("UserID");
            User user = dbContext.Users.Where(c => c.UserID == UserID).FirstOrDefault();
            if(user != null)
            {
                return View(user);
            }
            else
            {
                HttpContext.Session.SetString("LoginError", "Please login before proceeding.");
                return RedirectToAction("Login");
            }
        }

        [HttpPost("LoginUser")]
        public IActionResult LoginUser(User user)
        {
            User ExistingUser = dbContext.Users.Where(c => c.Email == user.Email).FirstOrDefault();
            PasswordHasher<User> Hasher = new PasswordHasher<User>();
            if(ExistingUser != null)
            {
                if(Hasher.VerifyHashedPassword(user, ExistingUser.Password, user.Password) != 0)
                {
                    HttpContext.Session.SetInt32("UserID", ExistingUser.UserID);
                    if(HttpContext.Session.GetInt32("LoginRegisterBusiness") == 1)
                    {
                        HttpContext.Session.SetInt32("LoginRegisterBusiness", 0);
                        return RedirectToAction("RegisterNewBusiness");
                    }
                    else if(HttpContext.Session.GetInt32("LoginRegisterBusiness") == 2)
                    {
                        HttpContext.Session.SetInt32("LoginRegisterBusiness", 0);
                        return RedirectToAction("BusinessLogin");
                    }
                    return RedirectToAction("UserDashboard");
                }
                else
                {
                    HttpContext.Session.SetString("LoginError", "Problem with email or password.");                    
                }
            }
            else
            {
                HttpContext.Session.SetString("LoginError", "Problem with email or password.");                
            }
            return RedirectToAction("Login");
        }

        [HttpPost("RegisterUser")]
        public IActionResult RegisterUser(User user)
        {
            User ExistingUser = dbContext.Users.Where(c => c.Email == user.Email).FirstOrDefault();
            if(ExistingUser == null){
                PasswordHasher<string> Hasher = new PasswordHasher<string>();
                user.Password = Hasher.HashPassword(user.Password, user.Password);
                user.CreatedAt = DateTime.Now;
                dbContext.Add(user);
                dbContext.SaveChanges();
                HttpContext.Session.SetInt32("UserID", user.UserID);
                return RedirectToAction("UserDashboard");
            }
            else{
                HttpContext.Session.SetString("RegisterError", "This email already has an account.");
                return RedirectToAction("Register");
            }
        }

        [HttpGet("RegisterNewBusiness")]
        public IActionResult RegisterNewBusiness()
        {
            int? UserID = HttpContext.Session.GetInt32("UserID");
            if(UserID == null)
            {
                HttpContext.Session.SetInt32("LoginRegisterBusiness", 1);
                HttpContext.Session.SetString("LoginError", "Login before registering your business.");
                return RedirectToAction("Login");
            }
            else 
            {
                ViewBag.RegisterError = HttpContext.Session.GetString("RegisterError");
                HttpContext.Session.SetString("RegisterError", "");
                return View();
            }
        }

        [HttpGet("BusinessLogin")]
        public IActionResult BusinessLogin()
        {
            int? UserID = HttpContext.Session.GetInt32("UserID");
            if(UserID == null)
            {
                HttpContext.Session.SetInt32("LoginRegisterBusiness", 2);
                HttpContext.Session.SetString("LoginError", "Please login to your accout first.");
                return RedirectToAction("Login");
            }
            else 
            {
                ViewBag.RegisterError = HttpContext.Session.GetString("LoginError");
                HttpContext.Session.SetString("LoginError", "");
                Administrator admin = new Administrator()
                {
                    UserID = (int)UserID
                };
                return View(admin);
            }
        }

        [HttpPost("LoginBusiness")]
        public IActionResult LoginBusiness(Administrator admin)
        {
            Administrator dbAdmin = dbContext.Administrators.Where(c => c.UserID == admin.UserID && c.AdministratorName == admin.AdministratorName).FirstOrDefault();
            PasswordHasher<User> Hasher = new PasswordHasher<User>();
            if(dbAdmin != null){
                if(Hasher.VerifyHashedPassword(admin.User, dbAdmin.Password, admin.Password) != 0)
                {
                    HttpContext.Session.SetInt32("UserID", dbAdmin.UserID);
                    HttpContext.Session.SetInt32("CompanyID", dbAdmin.CompanyID);
                    return RedirectToAction("BusinessLoggedIn", "Business");
                }
                else
                {
                    HttpContext.Session.SetString("LoginError", "Incorrect with email or password.");                    
                }
            }
            else
            {
                HttpContext.Session.SetString("LoginError", "Incorrect with email or password.");                
            }
            return RedirectToAction("BusinessLogin");
        }

        [HttpPost("RegisterBusiness")]
        public IActionResult RegisterBusiness(Company company)
        {
            if(company.Administrators[0].Password  == company.Administrators[0].ConfirmPassword)
            {
                int? UserID = HttpContext.Session.GetInt32("UserID");
                if(UserID == null)
                {
                    HttpContext.Session.SetInt32("LoginRegisterBusiness", 1);
                    HttpContext.Session.SetString("LoginError", "Login before registering your business.");
                    return RedirectToAction("Login");
                }
                else 
                {
                    company.CreatedAt = DateTime.Now;
                    company.Administrators[0].CreatedAt = DateTime.Now;
                    company.Administrators[0].UserID = (int)UserID;
                    company.Administrators[0].AdminLevelID = 4;
                    PasswordHasher<string> Hasher = new PasswordHasher<string>();
                    company.Administrators[0].Password = Hasher.HashPassword(company.Administrators[0].Password, company.Administrators[0].Password);
                    dbContext.Add(company);
                    dbContext.SaveChanges();
                    HttpContext.Session.SetInt32("CompanyID", company.CompanyID);
                    return RedirectToAction("BusinessLoggedIn", "Business");
                }
            }
            else
            {
                HttpContext.Session.SetString("RegisterError", "Passwords did not match.");
                return RedirectToAction("RegisterNewBusiness");
            }
        }
    }
}
