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
using Microsoft.AspNetCore.Hosting;


namespace Anerme.Controllers
{
    public class BusinessController : Controller
    {
        private MyContext dbContext;
        private readonly IWebHostEnvironment myWebHostEnvirontment;
        public BusinessController(MyContext context, IWebHostEnvironment webHostEnvironment)
        {
            dbContext = context;
            myWebHostEnvirontment = webHostEnvironment;
        }

        private List<int> StringIntConverter(String[] StringArr)
        {
            List<int> IntArr = new List<int>();
            for(int i = 0; i < StringArr.Count(); i++)
            {
                if(StringArr[i] != "")
                {
                    int Num = 0;
                    for(int x = 0; x < StringArr[i].Count(); x++)
                    {
                        Num *= 10;
                        Num += StringArr[i][x] - '0';
                    }
                    IntArr.Add(Num);
                }
            }
            return IntArr;
        }

        [HttpGet("BusinessLoggedIn")]
        public IActionResult BusinessLoggedIn()
        {
            int? CompanyID = HttpContext.Session.GetInt32("CompanyID");
            int? UserID = HttpContext.Session.GetInt32("UserID");

            Administrator admin = dbContext.Administrators.Where(c => c.CompanyID == CompanyID && c.UserID == UserID)
                        .Include(c => c.User)
                        .Include(c => c.company)
                        .ThenInclude(c => c.Banner)
                        .ThenInclude(c => c.MenuItems)
                        .ThenInclude(c => c.DropDownItems)
                        .FirstOrDefault();

            if(CompanyID == null || UserID == null || admin == null)
            {
                return RedirectToAction("BusinessLogin", "Home");
            }

            return View(admin);
        }

        [HttpGet("WebPageManager/{ID}")]
        public IActionResult WebPageManager(int ID)
        {
            int? CompanyID = HttpContext.Session.GetInt32("CompanyID");
            int? UserID = HttpContext.Session.GetInt32("UserID");

            Administrator admin = dbContext.Administrators
                            .Where(c => c.CompanyID == CompanyID && c.UserID == UserID)
                            .Include(c => c.User)
                            .Include(c => c.company)
                            .ThenInclude(c => c.Images)
                            .Include(c => c.company)
                            .ThenInclude(c => c.Banner)
                            .ThenInclude(c => c.MenuItems)
                            .ThenInclude(c => c.ContactUsPage)
                            .ThenInclude(c => c.Address)
                            .ThenInclude(c => c.StreetAddress)
                            .Include(c => c.company)
                            .ThenInclude(c => c.Banner)
                            .ThenInclude(c => c.MenuItems)
                            .ThenInclude(c => c.ContactUsPage)
                            .ThenInclude(c => c.Address)
                            .ThenInclude(c => c.City)
                            .Include(c => c.company)
                            .ThenInclude(c => c.Banner)
                            .ThenInclude(c => c.MenuItems)
                            .ThenInclude(c => c.ContactUsPage)
                            .ThenInclude(c => c.Address)
                            .ThenInclude(c => c.State)
                            .Include(c => c.company)
                            .ThenInclude(c => c.Banner)
                            .ThenInclude(c => c.MenuItems)
                            .ThenInclude(c => c.InformationPage)
                            .Include(c => c.company)
                            .ThenInclude(c => c.Banner)
                            .ThenInclude(c => c.MenuItems)
                            .ThenInclude(c => c.PageCategories)
                            .ThenInclude(c => c.Category)
                            .Include(c => c.company)
                            .ThenInclude(c => c.Banner)
                            .ThenInclude(c => c.MenuItems)
                            .ThenInclude(c => c.ContactUsPage)
                            .ThenInclude(c => c.StoreDeliveryMethods)
                            .Include(c => c.company)
                            .ThenInclude(c => c.Banner)
                            .ThenInclude(c => c.MenuItems)
                            .ThenInclude(c => c.DropDownItems)
                            .ThenInclude(c => c.InformationPage)
                            .Include(c => c.company)
                            .ThenInclude(c => c.Banner)
                            .ThenInclude(c => c.MenuItems)
                            .ThenInclude(c => c.DropDownItems)
                            .ThenInclude(c => c.PageCategories)
                            .ThenInclude(c => c.Category)
                            .Include(c => c.company)
                            .ThenInclude(c => c.Banner)
                            .ThenInclude(c => c.MenuItems)
                            .ThenInclude(c => c.DropDownItems)
                            .ThenInclude(c => c.ContactUsPage)
                            .ThenInclude(c => c.StoreDeliveryMethods)
                            .Include(c => c.company)
                            .ThenInclude(c => c.Banner)
                            .ThenInclude(c => c.MenuItems)
                            .ThenInclude(c => c.DropDownItems)
                            .ThenInclude(c => c.ContactUsPage)
                            .ThenInclude(c => c.Address)
                            .ThenInclude(c => c.StreetAddress)
                            .Include(c => c.company)
                            .ThenInclude(c => c.Banner)
                            .ThenInclude(c => c.MenuItems)
                            .ThenInclude(c => c.DropDownItems)
                            .ThenInclude(c => c.ContactUsPage)
                            .ThenInclude(c => c.Address)
                            .ThenInclude(c => c.City)
                            .Include(c => c.company)
                            .ThenInclude(c => c.Banner)
                            .ThenInclude(c => c.MenuItems)
                            .ThenInclude(c => c.DropDownItems)
                            .ThenInclude(c => c.ContactUsPage)
                            .ThenInclude(c => c.Address)
                            .ThenInclude(c => c.State)
                            .FirstOrDefault();

            admin.company.DeliveryMethods = dbContext.DeliveryMethods.ToList();

            if(CompanyID == null || UserID == null || admin == null)
            {
                return RedirectToAction("BusinessLogin", "Home");
            }

            for(int i = 0; i < admin.company.Banner.MenuItems.Count(); i++)
            {
                if(admin.company.Banner.MenuItems[i].DropDownItems != null)
                {
                    for(int x = 0; x < admin.company.Banner.MenuItems[i].DropDownItems.Count(); x++)
                    {
                        if(admin.company.Banner.MenuItems[i].DropDownItems[x].MenuItemID == ID)
                        {
                            admin.company.MenuItem = admin.company.Banner.MenuItems[i].DropDownItems[x];
                            if(admin.company.Banner.MenuItems[i].DropDownItems[x].LTPActionID == 1)
                            {
                                return View("ProductsPageManager", admin);
                            }
                            if(admin.company.Banner.MenuItems[i].DropDownItems[x].LTPActionID == 2)
                            {
                                return View("InformationPageManager", admin);
                            }
                            if(admin.company.Banner.MenuItems[i].DropDownItems[x].LTPActionID == 3)
                            {
                                return View("ContactUsPageManager", admin);
                            }
                        }
                    }
                }
                if(admin.company.Banner.MenuItems[i].MenuItemID == ID)
                {
                    admin.company.MenuItem = admin.company.Banner.MenuItems[i];
                    if(admin.company.Banner.MenuItems[i].LTPActionID == 1)
                    {
                        return View("ProductsPageManager", admin);
                    }
                    if(admin.company.Banner.MenuItems[i].LTPActionID == 2)
                    {
                        return View("InformationPageManager", admin);
                    }
                    if(admin.company.Banner.MenuItems[i].LTPActionID == 3)
                    {
                        return View("ContactUsPageManager", admin);
                    }
                }
            }
            return View("Dashboard");
        }

        [HttpGet("BusinessDashboard")]
        public IActionResult BusinessDashboard()
        {
            int? CompanyID = HttpContext.Session.GetInt32("CompanyID");
            int? UserID = HttpContext.Session.GetInt32("UserID");

            if(CompanyID == null || UserID == null)
            {
                return RedirectToAction("BusinessLogin", "Home");
            }

            Administrator admin = dbContext.Administrators.Where(c => c.CompanyID == CompanyID && c.UserID == UserID)
                                    .Include(c => c.User)
                                    .Include(c => c.company)
                                    .ThenInclude(c => c.Banner)
                                    .ThenInclude(c => c.MenuItems)
                                    .ThenInclude(c => c.DropDownItems)
                                    .FirstOrDefault();

            return View(admin);
        }

        [HttpGet("AccountWebsite")]
        public IActionResult AccountWebsite()
        {
            int? CompanyID = HttpContext.Session.GetInt32("CompanyID");
            int? UserID = HttpContext.Session.GetInt32("UserID");

            if(CompanyID == null || UserID == null)
            {
                return RedirectToAction("BusinessLogin", "Home");
            }

            Administrator admin = dbContext.Administrators.Where(c => c.CompanyID == CompanyID && c.UserID == UserID)
                                    .Include(c => c.User)
                                    .Include(c => c.company)
                                    .ThenInclude(c => c.Images)
                                    .Include(c => c.company)
                                    .ThenInclude(c => c.Banner)
                                    .ThenInclude(c => c.MenuItems)
                                    .ThenInclude(c => c.DropDownItems)
                                    .FirstOrDefault();

            admin.company.LTPActions = dbContext.LTPActions.ToList();
            admin.company.MenuActions = dbContext.MenuActions.ToList();
            admin.company.BannerPartials = dbContext.BannerPartials.ToList();

            if(admin.company.Banner != null)
            {
                if(admin.company.Banner.MenuItems != null)
                {
                    for(int i = 0; i < admin.company.Banner.MenuItems.Count(); i++)
                    {
                        admin.company.Banner.MenuItems[i].RemoveParent();
                    }
                }
            }

            return View(admin);
        }
        
        [HttpGet("Orders")]
        public IActionResult Orders()
        {
            return View();
        }
        
        [HttpGet("Comments")]
        public IActionResult Comments()
        {
            return View();
        }
        
        [HttpGet("Reviews")]
        public IActionResult Reviews()
        {
            return View();
        }
        
        [HttpGet("Users")]
        public IActionResult Users()
        {
            int? CompanyID = HttpContext.Session.GetInt32("CompanyID");
            int? UserID = HttpContext.Session.GetInt32("UserID");

            if(CompanyID == null || UserID == null)
            {
                return RedirectToAction("BusinessLogin", "Home");
            }

            Administrator admin = dbContext.Administrators.Where(c => c.CompanyID == CompanyID && c.UserID == UserID)
                                    .Include(c => c.User)
                                    .Include(c => c.company)
                                    .ThenInclude(c => c.Administrators)
                                    .ThenInclude(c => c.User)
                                    .Include(c => c.company)
                                    .ThenInclude(c => c.Banner)
                                    .ThenInclude(c => c.MenuItems)
                                    .ThenInclude(c => c.DropDownItems)
                                    .FirstOrDefault();

            admin.company.AdminLevels = dbContext.AdminLevels.ToList();

            return View(admin);
        }
        
        [HttpGet("MyProfile")]
        public IActionResult MyProfile()
        {
            int? CompanyID = HttpContext.Session.GetInt32("CompanyID");
            int? UserID = HttpContext.Session.GetInt32("UserID");

            if(CompanyID == null || UserID == null)
            {
                return RedirectToAction("BusinessLogin", "Home");
            }

            Administrator admin = dbContext.Administrators.Where(c => c.CompanyID == CompanyID && c.UserID == UserID)
                                    .Include(c => c.User)
                                    .Include(c => c.company)
                                    .ThenInclude(c => c.Images)
                                    .Include(c => c.company)
                                    .ThenInclude(c => c.Banner)
                                    .ThenInclude(c => c.MenuItems)
                                    .ThenInclude(c => c.DropDownItems)                                    
                                    .FirstOrDefault();

            return View(admin);
        }

        [HttpGet("Addresses")]
        public IActionResult Addresses()
        {
            int? CompanyID = HttpContext.Session.GetInt32("CompanyID");
            int? UserID = HttpContext.Session.GetInt32("UserID");

            Administrator admin = dbContext.Administrators
                                        .Where(c => c.CompanyID == CompanyID && c.UserID == UserID)
                                        .Include(c => c.User)
                                        .Include(c => c.company)
                                        .ThenInclude(c => c.CompanyAddresses)
                                        .ThenInclude(c => c.Address)
                                        .ThenInclude(c => c.StreetAddress)
                                        .Include(c => c.company)
                                        .ThenInclude(c => c.CompanyAddresses)
                                        .ThenInclude(c => c.Address)
                                        .ThenInclude(c => c.City)
                                        .Include(c => c.company)
                                        .ThenInclude(c => c.CompanyAddresses)
                                        .ThenInclude(c => c.Address)
                                        .ThenInclude(c => c.State)   
                                        .Include(c => c.company)
                                        .ThenInclude(c => c.Banner)
                                        .ThenInclude(c => c.MenuItems)
                                        .ThenInclude(c => c.DropDownItems)                                                                        
                                        .FirstOrDefault();

            if(CompanyID == null || UserID == null || admin == null)
            {
                return RedirectToAction("BusinessLogin", "Home");
            }

            if(admin.company.CompanyAddresses.Count() == 0)
            {
                admin.company.CompanyAddresses.Add(new CompanyAddress());
                admin.company.CompanyAddresses[0].Address = new Address();
                admin.company.CompanyAddresses[0].Address.StreetAddress = new StreetAddress();
                admin.company.CompanyAddresses[0].Address.City = new City();
                admin.company.CompanyAddresses[0].Address.State = new State();
            }

            return View(admin);
        }
        
        [HttpGet("Products")]
        public IActionResult Products()
        {
            int? CompanyID = HttpContext.Session.GetInt32("CompanyID");
            int? UserID = HttpContext.Session.GetInt32("UserID");

            CompanyID = 10;
            UserID = 10;

            if(CompanyID == null || UserID == null)
            {
                return RedirectToAction("BusinessLogin", "Home");
            }

            Administrator admin = dbContext.Administrators
                                        .Where(c => c.CompanyID == CompanyID && c.UserID == UserID)
                                        .Include(c => c.User)
                                        .Include(c => c.company)
                                        .ThenInclude(c => c.Images)
                                        .Include(c => c.company)
                                        .ThenInclude(c => c.Banner)
                                        .ThenInclude(c => c.MenuItems)
                                        .ThenInclude(c => c.DropDownItems)                                                                             
                                        .FirstOrDefault();

            admin.company.Products = dbContext.Products
                                        .Where(c => c.CompanyID == CompanyID)
                                        .Include(c => c.ProductImages)
                                        .ThenInclude(c => c.Image)
                                        .Include(c => c.ProductCategories)
                                        .ThenInclude(c => c.Category)
                                        .ToList();

            admin.company.Administrators = null;
            admin.User.Administrators = null;

            if(admin != null)
            {
                try
                {
                    for(int i = 0; i < admin.company.Products.Count(); i++)
                    {
                        admin.company.Products[i].ProductImages = admin.company.Products[i].ProductImages.OrderBy(c => c.OrderNumber).ToList();
                        admin.company.Products[i].SetStorePrice();
                        admin.company.Products[i].company = null;
                    }
                } catch{}
                return View(admin);
            }
        
            return RedirectToAction("BusinessLogin", "Home");
        }

        [HttpGet("EditProduct/{productID}")]
        public IActionResult EditProduct(int productID)
        {
            int? CompanyID = HttpContext.Session.GetInt32("CompanyID");
            int? UserID = HttpContext.Session.GetInt32("UserID");

            CompanyID = 10;
            UserID = 10;

            if(CompanyID == null || UserID == null)
            {
                return RedirectToAction("BusinessLogin", "Home");
            }

            Administrator admin = dbContext.Administrators
                                        .Where(c => c.CompanyID == CompanyID && c.UserID == UserID)
                                        .Include(c => c.User)
                                        .Include(c => c.company)
                                        .ThenInclude(c => c.Images)
                                        .Include(c => c.company)
                                        .ThenInclude(c => c.Banner)
                                        .ThenInclude(c => c.MenuItems)
                                        .ThenInclude(c => c.DropDownItems)
                                        .FirstOrDefault();


            if(admin != null && admin.company != null)
            {
                admin.company.Products = new List<Product>();
                Product dbProduct = dbContext.Products.Where(c => c.ProductID == productID && c.CompanyID == CompanyID)
                                                .Include(c => c.ProductImages)
                                                .Include(c => c.ProductCategories)
                                                .ThenInclude(c => c.Category)
                                                .FirstOrDefault();
                dbProduct.SetStorePrice();
                admin.User.Administrators = null;
                admin.company.Administrators = null;
                admin.company.Products[0].company = null;
                admin.company.Products[0].ProductImages = admin.company.Products[0].ProductImages.OrderBy(c => c.OrderNumber).ToList();
            }
            return View(admin);
        }

        [HttpGet("AddProduct")]
        public IActionResult AddProduct()
        {
            int? CompanyID = HttpContext.Session.GetInt32("CompanyID");
            int? UserID = HttpContext.Session.GetInt32("UserID");

            Administrator admin = dbContext.Administrators
                                        .Where(c => c.CompanyID == CompanyID && c.UserID == UserID)
                                        .Include(c => c.User)
                                        .Include(c => c.company)
                                        .ThenInclude(c => c.Images)
                                        .Include(c => c.company)
                                        .ThenInclude(c => c.Banner)
                                        .ThenInclude(c => c.MenuItems)
                                        .ThenInclude(c => c.DropDownItems)                                        
                                        .FirstOrDefault();

            if(CompanyID == null || UserID == null)
            {
                return RedirectToAction("BusinessLogin", "Home");
            }

            return View(admin);
        }

        [HttpPost]
        public IActionResult SubmitBanner(Banner banner)
        {
            int? CompanyID = HttpContext.Session.GetInt32("CompanyID");
            int? UserID = HttpContext.Session.GetInt32("UserID");

            if(CompanyID == null || UserID == null)
            {
                return RedirectToAction("BusinessLogin", "Home");
            }
            
            Banner dbBanner = dbContext.Banners.Where(c => c.CompanyID == CompanyID)
                                        .Include(c => c.MenuItems)
                                        .ThenInclude(c => c.DropDownItems)
                                        .FirstOrDefault();

            if(dbBanner == null)
            {
                banner.CompanyID = (int)CompanyID;
                dbContext.Add(banner);
                dbContext.SaveChanges();
            }
            else
            {
                dbBanner.MatchBanner(banner);
                dbContext.SaveChanges();
            }

            string message = "SUCCESS";
            return Json(new { Message = message });
        }

        [HttpPost("SubmitNewAddress")]
        public IActionResult SubmitNewAddress(Administrator administrator)
        {
            CompanyAddress companyAddress = administrator.company.CompanyAddresses[0];
            companyAddress.CompanyID = 6;

            companyAddress.Address.StreetAddress.StreetAddressName = companyAddress.Address.StreetAddress.StreetAddressName.TrimStart().TrimEnd();
            StreetAddress dbStreetAddress = dbContext.StreetAddresses.Where(c => c.StreetAddressName == companyAddress.Address.StreetAddress.StreetAddressName).FirstOrDefault();
            if(dbStreetAddress != null)
            {
                companyAddress.Address.StreetAddressID = dbStreetAddress.StreetAddressID;
                companyAddress.Address.StreetAddress = null;
            }

            companyAddress.Address.City.CityName = companyAddress.Address.City.CityName.TrimStart().TrimEnd();
            City dbCity = dbContext.Cities.Where(c => c.CityName == companyAddress.Address.City.CityName).FirstOrDefault();
            if(dbCity != null)
            {
                companyAddress.Address.CityID = dbCity.CityID;
                companyAddress.Address.City = null;
            }

            companyAddress.Address.State.StateName = companyAddress.Address.State.StateName.TrimStart().TrimEnd();
            State dbState = dbContext.States.Where(c => c.StateName == companyAddress.Address.State.StateName).FirstOrDefault();
            if(dbState != null)
            {
                companyAddress.Address.StateID = dbState.StateID;
                companyAddress.Address.State = null;
            }

            try
            {
                dbContext.Add(companyAddress);
                dbContext.SaveChanges();
            }catch{}

            return RedirectToAction("Addresses");
        }

        [HttpGet("DeleteAddress/{AddressID}")]
        public IActionResult DeleteAddress(int AddressID)
        {
            int? CompanyID = HttpContext.Session.GetInt32("CompanyID");
            
            CompanyAddress companyAddress = dbContext.CompanyAddresses.Where(c => c.AddressID == AddressID && c.CompanyID == CompanyID).FirstOrDefault();

            if(companyAddress != null)
            {
                dbContext.CompanyAddresses.Remove(companyAddress);
                dbContext.SaveChanges();
            }

            return RedirectToAction("Addresses");
        }

        [HttpPost("SubmitNewProduct")]
        public IActionResult SubmitNewProduct(Administrator administrator)
        {
            Product NewProduct = administrator.company.Products[0];
            NewProduct.SetDBPrice();
            NewProduct.CreatedAt = DateTime.Now;
            if(NewProduct.ImageIDString == null)
            {
                NewProduct.ImageIDString = "";
            }
            if(NewProduct.CategoriesString == null)
            {
                NewProduct.CategoriesString = "";
            }
            NewProduct.ProductImages = new List<ProductImage>();
            char[] CharArr = {','};
            String[] StringImageIDs = NewProduct.ImageIDString.Split(CharArr);
            List<string> CategoriesArr = NewProduct.CategoriesString.ToLower().Split(CharArr).OrderBy(c => c).ToList();
            List<Category> DbCategories = dbContext.Categories.Where(c => CategoriesArr.Contains(c.CategoryName)).OrderBy(c => c.CategoryName).ToList();
            
            if(CategoriesArr.Count() == DbCategories.Count())
            {
                NewProduct.CreateProductCategoriesList(DbCategories, 0);
            }
            else{
                NewProduct.CreateCategoriesProductCategoriesList(CategoriesArr, DbCategories, 0);
            }

            List<int> ImageIDs = StringIntConverter(StringImageIDs);
            NewProduct.SetProductImages(ImageIDs);            

            dbContext.Add(NewProduct);
            dbContext.SaveChanges();

            return RedirectToAction("Products");
        }

        [HttpPost("SubmitPageCategories")]
        public IActionResult SubmitPageCategories(Administrator admin)
        {
            int? CompanyID = HttpContext.Session.GetInt32("CompanyID");
            int? UserID = HttpContext.Session.GetInt32("UserID");

            if(CompanyID == null || UserID == null)
            {
                return RedirectToAction("BusinessLogin", "Home");
            }

            char[] CharArr = {','};
            MenuItem MI = admin.company.MenuItem;
            MenuItem DBMI = dbContext.MenuItems.Where(c => c.MenuItemID == MI.MenuItemID)
                                .Include(c => c.PageCategories)
                                .FirstOrDefault();
            List<string> CategoriesArr = MI.CategoriesString.ToLower().Split(CharArr).OrderBy(c => c).ToList();
            List<Category> DbCategories = dbContext.Categories.Where(c => CategoriesArr.Contains(c.CategoryName)).OrderBy(c => c.CategoryName).ToList();

            if(DBMI.CategoriesString == null)
            {
                DBMI.CategoriesString = "";
            }

            if(CategoriesArr.Count() == DbCategories.Count())
            {
                MI.CreatePageCategoriesList(DbCategories, 0);
            }
            else{
                MI.CreateCategoriesPageCategoriesList(CategoriesArr, DbCategories, 0);
            }

            DBMI.PageCategories = MI.PageCategories;
            dbContext.SaveChanges();
            return RedirectToAction($"WebPageManager", new { ID = admin.company.MenuItem.MenuItemID });
        }

        [HttpPost("SubmitEditProduct")]
        public IActionResult SubmitEditProduct(Administrator admin)
        {
            Product EditProduct = admin.company.Products[0];
            Product DBProduct = dbContext.Products.Where(c => c.CompanyID == EditProduct.CompanyID && c.ProductID == EditProduct.ProductID)
                                            .Include(c => c.ProductImages)
                                            .Include(c => c.ProductCategories)
                                            .FirstOrDefault();
            EditProduct.SetDBPrice();
            DBProduct.MatchMemberVariables(EditProduct);

            char[] CharArr = {','};
            String[] StringImageIDs = EditProduct.ImageIDString.Split(CharArr);
            List<string> CategoriesArr = EditProduct.CategoriesString.ToLower().Split(CharArr).OrderBy(c => c).ToList();
            List<Category> DbCategories = dbContext.Categories.Where(c => CategoriesArr.Contains(c.CategoryName)).OrderBy(c => c.CategoryName).ToList();
            
            if(CategoriesArr.Count() == DbCategories.Count())
            {
                EditProduct.CreateProductCategoriesList(DbCategories, 0);
            }
            else{
                EditProduct.CreateCategoriesProductCategoriesList(CategoriesArr, DbCategories, 0);
            }

            DBProduct.ProductCategories = EditProduct.ProductCategories;
            List<int> ImageIDs = StringIntConverter(StringImageIDs);
            DBProduct.SetProductImages(ImageIDs);
            dbContext.SaveChanges();

            return RedirectToAction("Products");
        }

        [HttpPost("SubmitInformationPage")]
        public IActionResult SubmitInformationPage(Administrator admin)
        {
            int? CompanyID = HttpContext.Session.GetInt32("CompanyID");
            int? UserID = HttpContext.Session.GetInt32("UserID");

            if(CompanyID == null || UserID == null)
            {
                return RedirectToAction("BusinessLogin", "Home");
            }

            MenuItem dbMenuItem = dbContext.MenuItems.Where(c => c.MenuItemID == admin.company.MenuItem.MenuItemID)
                                            .Include(c => c.InformationPage)
                                            .FirstOrDefault();

            admin.company.MenuItem.InformationPage.CompanyID = CompanyID;

            if(dbMenuItem.InformationPage == null)
            {
                dbMenuItem.InformationPage = admin.company.MenuItem.InformationPage;
            }
            else
            {
                dbMenuItem.InformationPage.MatchInformationPage(admin.company.MenuItem.InformationPage);
            }

            dbContext.SaveChanges();
            return RedirectToAction($"WebPageManager", new { ID = admin.company.MenuItem.MenuItemID });
        }

        [HttpPost("SubmitNewAdministrator")]
        public IActionResult SubmitNewAdministrator(Administrator administrator)
        {
            int? CompanyID = HttpContext.Session.GetInt32("CompanyID");

            administrator.CompanyID = (int)CompanyID;

            User dbUser = dbContext.Users.Where(c => c.Email == administrator.User.Email).FirstOrDefault();
            List<string> Passwords = administrator.User.PasswordGenerator();
            if(dbUser != null)
            {
                Administrator newAdmin = new Administrator(Passwords[0], Passwords[1], (int)CompanyID, administrator.AdminLevelID);
                if(dbUser.Administrators == null)
                {
                    dbUser.Administrators = new List<Administrator>();
                }
                dbUser.Administrators.Add(newAdmin);
                dbUser.SendNewPasswordEmail(Passwords[0]);
            }
            else
            {
                administrator.AdministratorName = Passwords[0];
                administrator.Password = Passwords[1];
                administrator.User.Password = Passwords[1];
                administrator.CreatedAt = DateTime.Now;
                administrator.User.CreatedAt = DateTime.Now;
                dbContext.Add(administrator);
                administrator.User.SendNewPasswordEmail(Passwords[0]);
            }
            dbContext.SaveChanges();
            
            return RedirectToAction("Users");
        }

        [HttpPost("SubmitEditAdministrator")]
        public IActionResult SubmitEditAdministrator(Administrator administrator)
        {
            int? CompanyID = HttpContext.Session.GetInt32("CompanyID");
            int? UserID = HttpContext.Session.GetInt32("UserID");
            
            if(CompanyID == null || UserID == null)
            {
                return RedirectToAction("BusinessLogin", "Home");
            }

            List<int> userIDs = new List<int>();
            userIDs.Add((int)UserID);
            userIDs.Add(administrator.UserID);

            List<Administrator> dbAdmin = dbContext.Administrators.Where(c => userIDs.Contains(c.UserID) && c.CompanyID == CompanyID).ToList();

            bool? ConfirmRights = null;
            for(int i = 0; i < dbAdmin.Count(); i++)
            {
                if(dbAdmin[i].UserID == UserID && ConfirmRights == null)
                {
                    if(dbAdmin[i].AdminLevelID == 4)
                    {
                        ConfirmRights = true;
                        i = 0; 
                    }
                    else
                    {
                        ConfirmRights = false;
                    }
                }
                else if(dbAdmin[i].UserID == administrator.UserID && ConfirmRights == true)
                {
                    if(administrator.AdminLevelID == 999)
                    {
                        dbContext.Administrators.Remove(dbAdmin[i]);
                    }
                    else
                    {
                        dbAdmin[i].AdminLevelID = administrator.AdminLevelID;
                    }
                    dbContext.SaveChanges();
                    break;
                }
            }

            return RedirectToAction("Users");
        }

        [HttpPost("SubmitEditProfile")]
        public IActionResult SubmitEditProfile(Administrator administrator)
        {
            int? CompanyID = HttpContext.Session.GetInt32("CompanyID");
            int? UserID = HttpContext.Session.GetInt32("UserID");
            
            User UpdateUser = administrator.User;
            User dbUser = dbContext.Users.Where(c => c.UserID == UserID).FirstOrDefault();
            
            
            if(CompanyID == null || UserID == null || dbUser == null)
            {
                return RedirectToAction("BusinessLogin", "Home");
            }

            dbUser.UpdateUser(UpdateUser);
            dbContext.SaveChanges();
            return RedirectToAction("MyProfile");
        }

        public IActionResult SubmitContactUsPage(ContactUsPage CUP, int MenuItemID)
        {
            int? CompanyID = HttpContext.Session.GetInt32("CompanyID");
            int? UserID = HttpContext.Session.GetInt32("UserID");

            MenuItem dbMenuItem = dbContext.MenuItems.Where(c => c.MenuItemID == MenuItemID)
                                        .Include(c => c.ContactUsPage)
                                        .ThenInclude(c => c.StoreDeliveryMethods)
                                        .FirstOrDefault();
            
            if(CompanyID == null || UserID == null || dbMenuItem == null)
            {
                return RedirectToAction("BusinessLogin", "Home");
            }

            CUP.CompanyID = (int)CompanyID;
            if(dbMenuItem.ContactUsPage == null)
            {
                dbMenuItem.ContactUsPage = CUP;
            }
            else
            {
                dbMenuItem.ContactUsPage.MatchContactUsPage(CUP);
            }

            dbContext.SaveChanges();

            string message = "SUCCESS";
            return Json(new { Message = message });
        }

        [HttpPost]
        public async Task<IActionResult> SingleImageUpload(Image image)
        {
            int? CompanyID = HttpContext.Session.GetInt32("CompanyID");
            int? UserID = HttpContext.Session.GetInt32("UserID");

            if(CompanyID == null || UserID == null)
            {
                return RedirectToAction("BusinessLogin", "Home");
            }
            image.CompanyID = (int)CompanyID;
            DateTime timeNow = DateTime.Now;
            string timeNowString = timeNow.Year.ToString() + "-"
                + timeNow.Month.ToString() + "-"
                + timeNow.Day.ToString() + "-"
                + timeNow.Hour.ToString() + "-"
                + timeNow.Minute.ToString() + "-"
                + timeNow.Second.ToString() + "-"
                + timeNow.Millisecond.ToString();
            string folder = "images/";

            folder += timeNowString + "_" + image.ImageFile.FileName;
            folder = Regex.Replace(folder, " ", string.Empty);
            string serverFolder = Path.Combine(myWebHostEnvirontment.WebRootPath, folder);
            await image.ImageFile.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
            image.ImagePath = "/" + folder;
            
            dbContext.Add(image);
            dbContext.SaveChanges();

            image.ImageFile = null;
            Image message = image;

            return Json(new { Message = message });
        }
    }
}
