using BL.Repositories;
using System;

namespace BL.Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        ForgetPasswordURLRepository ForgetPasswordURLRepository { get; }

        GroupsRepository GroupsRepository { get; }
        GroupPermissionsRepository GroupPermissionsRepository { get; }
        PermissionsRepository PermissionsRepository { get; }
        ScreensRepository ScreensRepository { get; }
        ScreenPermissionsRepository ScreenPermissionsRepository { get; }
        ModulesRepository ModulesRepository { get; }

        CountryRepository CountryRepository { get; }
        CityRepository CityRepository { get; }
        DistrictRepository DistrictRepository { get; }
        RegionRepository RegionRepository { get; }
        RegionPointsRepository RegionPointsRepository { get; }

        JobTitlesRepository JobTitlesRepository { get; }
        UsersRepository UsersRepository { get; }

        UserGroupsRepository UserGroupsRepository { get; }
        TechnicalsRepository TechnicalsRepository { get; }

        ServicesRepository ServicesRepository { get; }

        ItemsRepository ItemsRepository { get; }
        ItemImagesRepository ItemImagesRepository { get; }
        OffersRepository OffersRepository { get; }
        OfferActiveDatesRepository OfferActiveDatesRepository { get; }
        OfferItemsRepository OfferItemsRepository { get; }       

        CustomerRepository CustomerRepository { get; }
        ItemFavouriteRepository ItemFavouriteRepository { get; }
        ItemsCartRepository ItemsCartRepository { get; }

        OrderRepository OrderRepository { get; }
        OrderItemsRepository OrderItemsRepository { get; }
        OrderServicesRepository OrderServicesRepository { get; }
        OrderTrackActionRepository OrderTrackActionRepository { get; }
        OrderTrackRepository OrderTrackRepository { get; }
        ReadyTechnicalsRepository ReadyTechnicalsRepository { get; }
        OrderTechnicalAssignmentRepository OrderTechnicalAssignmentRepository { get; }
        SharedRepository SharedRepository { get; }
        ContactUsRepository ContactUsRepository { get; }
        NotificationRepository NotificationRepository { get; }
        PocketHistoryRepository PocketHistoryRepository { get; }
        ServicesImagesRepository ServicesImagesRepository { get; }
        SystemOptionsRepository SystemOptionsRepository { get; }
        CustomerReviewRepository CustomerReviewRepository { get; }

        void ExecuteSqlCommand(string sqlCommand);
            int Save();

        }
}
