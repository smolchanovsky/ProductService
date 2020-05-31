using ProductService.Core.DataLayer;
﻿using Microsoft.Extensions.Configuration;

 namespace ProductService.Web.Api.Configs
{
    public class ProductDbContextConfig : IProductDbContextConfig
    {
        private readonly IConfiguration configuration;

        public string ConnectionString => configuration["SqlDb:ConnectionString"];

        public ProductDbContextConfig(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
    }
}
