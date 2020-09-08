namespace BL.SharedCS
{
    public static class Enumrations
    {


        public enum EN_OfferUsageLimitTypes
        {
            None = 1, OneUsePerCustomer, NumberOfTotalTimes
        }
        public enum EN_OfferConditions
        {
            None = 1, MinPurchaseAmount, MaxPurchaseAmount, MinItemQty, MaxItemQty
        }
        public enum EN_OfferTypes { BuyXGetY, buyxqtygetyqty }

        public enum EN_DiscountTypes { Percentage = 1, FixedAmount }
        public enum EN_DeliveryTypes { Percentage = 1, FixedAmount }
        public enum EN_ApplyToCustomersTypes
        {
            None = 1, Everyone, SpecificCustomers, SpecificCustomerGroups
        }
        public enum EN_ApplyToTypes { EntireOrder = 1, Categories, Items }

        public enum EN_RewardTypes { PointsOnTotalReceipt = 1, PointsVSCurrencyAmount }

        public enum EN_Modules { }

        public enum EN_Screens
        {
            Groups, JobTitle, Country, Users, Items, ServiceType, SubCategories, Offers, Categories,
            Services,
            City,
            Customer,
            ItemFavourite,
            ItemsCart,
            Regions,
            OrderTrackAction,
            Order,
            ServicesReport,
            TechnicalsReport, SystemOptions, CustomerReview
        }
        public enum EN_OrderActions
        {
            canceled = 1,
            rejected,//by admin
            ordered,
            accpted_by_user,
            in_progress,
            shipped,
            delivered,
            completed,
            reviewed
        }
        public enum EN_OrderTechnicalAssignmentStatus
        {
            waitToAnswer = 1,
            accepted,
            rejected,
            timedout,
            notavailable
        }
        public enum EN_Transactions
        {
            Charge = 1,
            ServiceCost,
            ServiceCompanyProfit
        }
        public enum EN_SystemOptions { startWorkDay = 1, endWorkDay, timeOfBreak }

        public enum RequestStatus { Pending = 1, Approved, Reject }
        public enum En_JobTitle { Admin = 1, Technical, Supplier, Secretary, egent }
        public enum EN_TypeUser { Customer = 1, Technical, Supplier, Admin, User }

        public enum EN_Permissions { View = 1, Create, Edit, Delete, UpdateItemsStock, Approve, Reject, BulkUploadItemsStock, OrdersDistributions, RejectOrder, CompleteOrder }
    }
}
