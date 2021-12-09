using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Anerme.Models
{
    public class Product
    {
        [Key]
        public int ProductID {get;set;}
        public string ProductTitle {get;set;}
        public string HighLevelDesc {get;set;}
        public string DetailedDesc {get;set;}
        public int Price {get;set;}
        public int Inventory {get;set;}
        public int CompanyID {get;set;}
        public DateTime? CreatedAt {get;set;}
        public Company company {get;set;}
        public List<ProductImage> ProductImages {get;set;}
        public List<ProductCategory> ProductCategories {get;set;}
        [NotMapped]
        public string ImageIDString {get;set;}
        [NotMapped]
        public string CategoriesString {get;set;}
        [NotMapped]
        public double StorePrice {get;set;}
        [NotMapped]
        public List<string> Paragraphs {get;set;}

        public void SetParagraphs()
        {
            Paragraphs = new List<string>();
            DetailedDesc += "\r\n";
            int position = 0;
            int start = 0;
            do
            {
                position = DetailedDesc.IndexOf("\r\n", start);
                if (position >= 0)
                {
                    if (DetailedDesc.Substring(start, position - start + 1).Trim() != "")
                    {
                        String myParagraph = DetailedDesc.Substring(start, position - start + 1).Trim();

                        Paragraphs.Add(myParagraph);
                    }
                    start = position + 1;
                }
            } while (position > 0);
        }
        public void SetDBPrice()
        {
            Price = (int)(StorePrice * 100);
        }
        public void SetStorePrice()
        {
            StorePrice = ((double)Price) / 100;
        }

        public void MatchMemberVariables(Product product)
        {
            ProductTitle = product.ProductTitle;
            HighLevelDesc = product.HighLevelDesc;
            DetailedDesc = product.DetailedDesc;
            Price = product.Price;
            Inventory = product.Inventory;
        }

        public void CreateProductCategoriesList(List<Category> Categories, int _ProductID)
        {
            ProductCategories = new List<ProductCategory>();
            for(int i = 0; i < Categories.Count(); i++)
            {
                ProductCategory PC = new ProductCategory()
                {
                    ProductID = _ProductID,
                    CategoryID = Categories[i].CategoryID
                };
                ProductCategories.Add(PC);
            }
        }

        public void CreateCategoriesProductCategoriesList(List<string> CategoriesArr, List<Category> Categories, int _ProductID)
        {
            ProductCategories = new List<ProductCategory>();
            for(int i = 0; i < CategoriesArr.Count(); i++)
            {
                if(CategoriesArr[i].Trim() != "")
                {
                    bool found = false;
                    string CatName = CategoriesArr[i].TrimStart().TrimEnd();
                    for(int x = 0; x < Categories.Count(); x++)
                    {
                        if(Categories[x].CategoryName == CatName)
                        {
                            ProductCategory PC = new ProductCategory(Categories[x].CategoryID, _ProductID);
                            ProductCategories.Add(PC);
                            found = true;
                            break;
                        }
                    }
                    if(!found)
                    {
                        ProductCategory PC = new ProductCategory(_ProductID, CatName);
                        ProductCategories.Add(PC);
                    }
                }
            }
        }

        public void SetProductImages(List<int> ImageIDs)
        {
            ProductImages = new List<ProductImage>();
            for(int i = 0; i < ImageIDs.Count(); i++)
            {
                ProductImage NewProductImage = new ProductImage()
                {
                    ImageID = ImageIDs[i],
                    OrderNumber = i+1
                };
                ProductImages.Add(NewProductImage);
            }
        }
    }    
}