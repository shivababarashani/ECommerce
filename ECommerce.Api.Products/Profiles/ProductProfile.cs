using ECommerce.Api.Products.Db;
using ECommerce.Api.Products.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Products.Profiles
{
    public class ProductProfile:AutoMapper.Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductModel>();
        }
    }
}
