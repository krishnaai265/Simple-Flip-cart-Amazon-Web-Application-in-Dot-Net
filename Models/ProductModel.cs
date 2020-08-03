using MVCDatabaseFirstApproach.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCShoppingCard12.Models
{
    public class ProductModel
    {
        

        
        private List<Product> products;

        public ProductModel()
        {
            this.products = new List<Product>(){
                new Product{
                    ProductId=3,
                    ProductName="Name 1",
                    ProductPrice=5,
                    ProductImage="thumb1.gif"
                },
                new Product{
                    ProductId=2,
                    ProductName="Name 2",
                    ProductPrice=2,
                    ProductImage="thumb2.gif"
                },
                new Product{
                    ProductId=1,
                    ProductName="Name 3",
                    ProductPrice=3,
                    ProductImage="thumb3.gif"
                },
            };

     //       foreach(var i in )
        }
        public List<Product> findAll() {
            return this.products;
        }
        public Product find(int id) {
            return this.products.Single(p => p.ProductId.Equals(id));
        }
    }
}