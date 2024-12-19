﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Products
{
    public interface IProductRepository : IGenericRepository<Product,int>
    {
        public Task<List<Product>> GetTopPriceProductAsync(int count);

    }
}
