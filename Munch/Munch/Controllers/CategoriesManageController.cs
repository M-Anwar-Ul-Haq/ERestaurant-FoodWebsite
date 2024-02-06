namespace Munch.Controllers
{
    using System;
    using System.Linq;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Data;
    using Munch.Models;
    using Microsoft.AspNetCore.Mvc;
    using ViewModels.Categories;

    public class CategoriesManageController : Controller
    {
        private readonly MunchContext context;
        private readonly IMapper mapper;

        public CategoriesManageController(MunchContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Create(CreateCategoryInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Category category = this.mapper.Map<Category>(model);

            this.context.Categories.Add(category);
            context.SaveChanges();

            return this.RedirectToAction("A_All", "CategoriesManage");
        }

        public IActionResult Remove(int ItemId)
        {
            try
            {
                var item = context.Categories.Find(ItemId);

                if (item == null)
                {
                    return NotFound("CartItem not found.");
                }

                context.Categories.Remove(item);
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
        public IActionResult A_All()
        {
            var categories = this.context
                .Categories
                .ProjectTo<CategoryAllViewModel>
                (this.mapper.ConfigurationProvider)
                .ToList();

            return this.View(categories);
        }
    }
}
