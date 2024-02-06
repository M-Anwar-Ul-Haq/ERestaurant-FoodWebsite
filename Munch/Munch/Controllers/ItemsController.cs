namespace Munch.Controllers
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Threading;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Munch.Models;
    using Microsoft.AspNetCore.Mvc;
    using ViewModels.Categories;
    using ViewModels.Items;
    using Munch.Data;
    using Microsoft.EntityFrameworkCore;

    public class ItemsController : Controller
    {
        private readonly MunchContext context;
        private readonly IMapper mapper;

        public ItemsController(MunchContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public IActionResult All()
        {
            var items = this.context
                .Items
                .ProjectTo<ItemsAllViewModels>
                (this.mapper.ConfigurationProvider)
                .ToList();

            return this.View(items);
        }
        public IActionResult A_All()
        {
            var items = this.context
                .Items
                .ProjectTo<ItemsAllViewModels>
                (this.mapper.ConfigurationProvider)
                .ToList();

            return this.View(items);
        }
    }
}
