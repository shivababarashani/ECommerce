using AutoMapper;
using ECommerce.Api.Products.Db;
using ECommerce.Api.Products.Profiles;
using ECommerce.Api.Products.Providers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Xunit;

namespace ECommerce.Api.Products.Tests
{
    public class ProductsServiceTest
    {
        [Fact]
        public async void GetProductsReturnAllProducts()
        {
            var options = new DbContextOptionsBuilder<ProductsDbContext>().UseInMemoryDatabase(nameof(GetProductsReturnAllProducts))
                .Options;
            var dbContext = new ProductsDbContext(options);

            CreateProducts(dbContext);

            var productProfile = new ProductProfile();
            var configuratuion = new MapperConfiguration(conf => conf.AddProfile(productProfile));
            var mapper = new Mapper(configuratuion);

            var productsProvider = new ProductsProvider(dbContext,null, mapper);

            var product = await productsProvider.GetProductsAsync();
            Assert.True(product.IsSuccess);
            Assert.True(product.Products.Any());
            Assert.Null(product.ErrorMessage);
        }

         
        [Fact]
        public async void GetProductsReturnProductUsingValidId()
        {
            var options = new DbContextOptionsBuilder<ProductsDbContext>().UseInMemoryDatabase(nameof(GetProductsReturnProductUsingValidId))
                .Options;
            var dbContext = new ProductsDbContext(options);

            CreateProducts(dbContext);

            var productProfile = new ProductProfile();
            var configuratuion = new MapperConfiguration(conf => conf.AddProfile(productProfile));
            var mapper = new Mapper(configuratuion);

            var productsProvider = new ProductsProvider(dbContext, null, mapper);

            var product = await productsProvider.GetProductAsync(1);
            Assert.True(product.IsSuccess);
            Assert.NotNull(product.Product);
            Assert.True(product.Product.Id == 1);
            Assert.Null(product.ErrorMessage);
        }

        [Fact]
        public async void GetProductsReturnProductUsingInValidId()
        {
            var options = new DbContextOptionsBuilder<ProductsDbContext>().UseInMemoryDatabase(nameof(GetProductsReturnProductUsingValidId))
                .Options;
            var dbContext = new ProductsDbContext(options);

            CreateProducts(dbContext);

            var productProfile = new ProductProfile();
            var configuratuion = new MapperConfiguration(conf => conf.AddProfile(productProfile));
            var mapper = new Mapper(configuratuion);

            var productsProvider = new ProductsProvider(dbContext, null, mapper);

            var product = await productsProvider.GetProductAsync(-1);
            Assert.False(product.IsSuccess);
            Assert.Null(product.Product);
            Assert.NotNull(product.ErrorMessage);
        }


        private void CreateProducts(ProductsDbContext dbContext)
        {
            for (int i = 1; i < 10; i++)
            {
                dbContext.Products.Add(new Product()
                {
                    Id = i,
                    Name = Guid.NewGuid().ToString(),
                    Inventory=i+10,
                    Price=(decimal)(i*3.14)
                });
                dbContext.SaveChanges();
            }      
        }
    }
}
