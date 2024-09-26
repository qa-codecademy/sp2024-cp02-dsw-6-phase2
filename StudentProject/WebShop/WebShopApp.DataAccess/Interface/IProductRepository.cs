﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopApp.Domain.Models;

namespace WebShopApp.DataAccess.Interface
{
    public interface IProductRepository :IRepository<Product>
    {

        List<Product> FilterProducts(int? category, string? brand);

    }
}
