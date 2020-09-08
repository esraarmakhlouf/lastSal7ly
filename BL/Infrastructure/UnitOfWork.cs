using BL.Repositories;
using Microsoft.EntityFrameworkCore;
using Model;

namespace BL.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private DBContext _ctx;
        public UnitOfWork(DBContext ctx)
        {
            _ctx = ctx;
            _ctx.ChangeTracker.LazyLoadingEnabled = true;
        }
        public ForgetPasswordURLRepository ForgetPasswordURLRepository => new ForgetPasswordURLRepository(_ctx);

        public GroupsRepository GroupsRepository => new GroupsRepository(_ctx);
        public GroupPermissionsRepository GroupPermissionsRepository => new GroupPermissionsRepository(_ctx);
        public PermissionsRepository PermissionsRepository => new PermissionsRepository(_ctx);
        public ScreensRepository ScreensRepository => new ScreensRepository(_ctx);
        public ScreenPermissionsRepository ScreenPermissionsRepository => new ScreenPermissionsRepository(_ctx);
        public ModulesRepository ModulesRepository => new ModulesRepository(_ctx);

        public CountryRepository CountryRepository => new CountryRepository(_ctx);
        public CityRepository CityRepository => new CityRepository(_ctx);
        public DistrictRepository DistrictRepository => new DistrictRepository(_ctx);
        public RegionRepository RegionRepository => new RegionRepository(_ctx);
        public RegionPointsRepository RegionPointsRepository => new RegionPointsRepository(_ctx);

        public JobTitlesRepository JobTitlesRepository => new JobTitlesRepository(_ctx);
        public UsersRepository UsersRepository => new UsersRepository(_ctx);

        public UserGroupsRepository UserGroupsRepository => new UserGroupsRepository(_ctx);
        public TechnicalsRepository TechnicalsRepository => new TechnicalsRepository(_ctx);

        public ServicesRepository ServicesRepository => new ServicesRepository(_ctx);

        public ItemsRepository ItemsRepository => new ItemsRepository(_ctx);
        public ItemImagesRepository ItemImagesRepository => new ItemImagesRepository(_ctx);
        public OffersRepository OffersRepository => new OffersRepository(_ctx);
        public OfferActiveDatesRepository OfferActiveDatesRepository => new OfferActiveDatesRepository(_ctx);
        public OfferItemsRepository OfferItemsRepository => new OfferItemsRepository(_ctx);

        public CustomerRepository CustomerRepository => new CustomerRepository(_ctx);
        public ItemFavouriteRepository ItemFavouriteRepository => new ItemFavouriteRepository(_ctx);
        public ItemsCartRepository ItemsCartRepository => new ItemsCartRepository(_ctx);

        public OrderRepository OrderRepository => new OrderRepository(_ctx);
        public OrderItemsRepository OrderItemsRepository => new OrderItemsRepository(_ctx);
        public OrderServicesRepository OrderServicesRepository => new OrderServicesRepository(_ctx);
        public OrderTrackActionRepository OrderTrackActionRepository => new OrderTrackActionRepository(_ctx);
        public OrderTrackRepository OrderTrackRepository => new OrderTrackRepository(_ctx);
        public ReadyTechnicalsRepository ReadyTechnicalsRepository => new ReadyTechnicalsRepository(_ctx);
        public OrderTechnicalAssignmentRepository OrderTechnicalAssignmentRepository => new OrderTechnicalAssignmentRepository(_ctx);
        public SharedRepository SharedRepository => new SharedRepository(_ctx);
        public ContactUsRepository ContactUsRepository => new ContactUsRepository(_ctx);
        public NotificationRepository NotificationRepository => new NotificationRepository(_ctx);
        public PocketHistoryRepository PocketHistoryRepository => new PocketHistoryRepository(_ctx);
        public ServicesImagesRepository ServicesImagesRepository => new ServicesImagesRepository(_ctx);
        public SystemOptionsRepository SystemOptionsRepository => new SystemOptionsRepository(_ctx);
        public CustomerReviewRepository CustomerReviewRepository => new CustomerReviewRepository(_ctx);


        public void Dispose()
        {
            _ctx.Dispose();
        }

        public void ExecuteSqlCommand(string sqlCommand)
        {
            _ctx.Database.ExecuteSqlCommand(sqlCommand);
        }

        public int Save()
        {
            return _ctx.SaveChanges();
        }
    }
}
