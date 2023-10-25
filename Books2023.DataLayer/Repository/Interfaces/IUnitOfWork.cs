namespace Books2023.DataLayer.Repository.Interfaces
{
    public interface IUnitOfWork
    {
        ICategoryRepository Categories { get; }
        ICoverTypeRepository CoverTypes { get; }
        IProductRepository Products { get; }
        ICompanyRepository Companies { get; }
        IApplicationUserRepository ApplicationUsers { get; }
        IShoppingCartRepository ShoppingCarts { get; }
        void Save();
    }
}
