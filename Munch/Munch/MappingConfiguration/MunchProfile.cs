namespace Munch.MappingConfiguration
{
    using AutoMapper;
    using Munch.ViewModels.Categories;
    using Munch.ViewModels.Items;
    using Munch.Models;
    using Munch.ViewModels.CartItems;

    public class MunchProfile : Profile
    {
        public MunchProfile()
        {
            //Categories
            this.CreateMap<CreateCategoryInputModel, Category>()
                .ForMember(x => x.Name, y => y.MapFrom(x => x.CategoryName));

            this.CreateMap<Category, CategoryAllViewModel>();

            //Items
            this.CreateMap<Category, CreateItemViewModel>()
                .ForMember(x => x.CategoryId, y => y.MapFrom(x => x.Id))
                .ForMember(x => x.CategoryName, y => y.MapFrom(x => x.Name));

            this.CreateMap<CreateItemInputModel, Item>();

            this.CreateMap<Item, ItemsAllViewModels>()
                .ForMember(x => x.Category, y => y.MapFrom(x => x.Category.Name));

            this.CreateMap<Item, UpdateItemViewModel>();

            //CartItems
            this.CreateMap<CartItem, OrderAllViewModel>()
                .ForMember(dest => dest.CartItemId, opt => opt.MapFrom(src => src.CartItemId))
                .ForMember(dest => dest.ItemId, opt => opt.MapFrom(src => src.ItemId))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.Item, opt => opt.MapFrom(src => src.Item))
                .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.Quantity * (src.Item != null ? src.Item.Price : 0)));

        }
    }
}
