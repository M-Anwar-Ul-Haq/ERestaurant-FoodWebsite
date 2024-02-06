using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Munch.Models;
using Microsoft.AspNetCore.Mvc;
using Munch.ViewModels.Categories;
using Munch.ViewModels.Items;
using Munch.Data;
using Microsoft.EntityFrameworkCore;

namespace Munch.Controllers
{
    public class ItemsManageController : Controller
    {
        private readonly MunchContext context;
        private readonly IMapper mapper;

        public ItemsManageController(MunchContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public IActionResult Create()
        {
            var categories = this.context.Categories
                .ProjectTo<CreateItemViewModel>(mapper.ConfigurationProvider)
                .ToList();

            return this.View(categories);
        }

        [HttpPost]
        public IActionResult Create(CreateItemInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Error", "Home");
            }
            Item item = this.mapper.Map<Item>(model);

            this.context.Items.Add(item);
            this.context.SaveChanges();

            return RedirectToAction("A_All","ItemsManage");
        }

        public IActionResult Remove(int ItemId)
        {
            try
            {
                var item = context.Items.Find(ItemId);

                if (item == null)
                {
                    return NotFound("CartItem not found.");
                }

                context.Items.Remove(item);
                context.SaveChanges();

                // Redirect to the "All" action to refresh the view
                return RedirectToAction("A_All");
            }
            catch (Exception ex)
            {
                // Log the exception, or return a more detailed error message
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet]
        public IActionResult Update(int itemId)
        {
            var item = context.Items.Find(itemId);

            if (item == null)
            {
                return NotFound("Item not found.");
            }

            var model = mapper.Map<UpdateItemViewModel>(item);

            // Load categories for the dropdown
            model.Categories = context.Categories
                .ProjectTo<CreateItemViewModel>(mapper.ConfigurationProvider)
                .ToList();

            return View(model);
        }

        [HttpPost]
        public IActionResult Update(UpdateItemViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = context.Categories
                    .ProjectTo<CreateItemViewModel>(mapper.ConfigurationProvider)
                    .ToList();
                return View(model);
            }

            var item = context.Items.Find(model.ItemId);

            if (item == null)
            {
                return NotFound("Item not found.");
            }

            item.Name = model.Name;
            item.Price = model.Price;
            item.CategoryId = model.CategoryId;

            context.SaveChanges();

            return RedirectToAction("A_All");
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
