using Abp.IdentityServer4;
using Abp.Zero.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MCV.Portal.Authorization.Roles;
using MCV.Portal.Authorization.Users;
using MCV.Portal.Chat;
using MCV.Portal.Editions;
using MCV.Portal.Friendships;
using MCV.Portal.MultiTenancy;
using MCV.Portal.MultiTenancy.Accounting;
using MCV.Portal.MultiTenancy.Payments;
using MCV.Portal.Storage;
using MCV.Portal.Source.Restaurant;
using MCV.Portal.Source.AnnouncementsTypes;
using MCV.Portal.Source.Birthdays;
using MCV.Portal.Source.Announcements;
using MCV.Portal.Source.VacationTypes;
using MCV.Portal.Source.Vacations;
using MCV.Portal.Source.EmpHierarchy;
using MCV.Portal.Source.Search;
using MCV.Portal.Source.SAPSettings;

namespace MCV.Portal.EntityFrameworkCore
{
    public class PortalDbContext : AbpZeroDbContext<Tenant, Role, User, PortalDbContext>, IAbpPersistedGrantDbContext
    {

        /* Define an IDbSet for each entity of the application */
        //VacationTypes
        public virtual DbSet<VacationType> VacationTypes { get; set; }

        //Vacations
        public virtual DbSet<EmployeeData> EmployeeData { get; set; }
        public virtual DbSet<EmployeeVacationQuota> EmployeeVacationQuota { get; set; }
        public virtual DbSet<EmployeeVacation> EmployeeVacations { get; set; }
        public virtual DbSet<EmployeeVacationsView> EmployeeVacationsView { get; set; }
        public virtual DbSet<ManagerVacations> ManagerVacations { get; set; }
         

        //Sap Settings
        public virtual DbSet<SAPSetting> SAPSettings { get; set; }


        //Restaurants
        public virtual DbSet<RestItem> RestItems { get; set; }

        public virtual DbSet<RestInfo> RestInfos { get; set; }
        public virtual DbSet<RestInfoAdmins> RestInfoAdmins { get; set; }

        public virtual DbSet<RestSchedule> RestSchedules { get; set; }

        public virtual DbSet<RestScheduleItem> RestScheduleItems { get; set; }

        public virtual DbSet<RestRequest> RestRequests { get; set; }

        public virtual DbSet<RestRequestItem> RestRequestItems { get; set; }

        public virtual DbSet<RestResponse> RestResponses { get; set; }

        public virtual DbSet<EmployeesView> EmployeesView { get; set; }

        public virtual DbSet<ItemsCategory> ItemsCategories { get; set; }

        public virtual DbSet<RestCategory> RestCategories { get; set; }

        public virtual DbSet<RestNonSchItem> RestNonSchItems { get; set; }

        public virtual DbSet<RequestStatus> RequestStatus { get; set; }

        public virtual DbSet<PaymentType> PaymentTypes { get; set; }

        public virtual DbSet<RequestLog> RequestLogs { get; set; }


        //Announcements
        public virtual DbSet<AnnouncementType> AnnouncementTypes { get; set; }
        public virtual DbSet<Announcement> Announcements { get; set; }


        //Birthdays
        public virtual DbSet<Birthday> Birthdays { get; set; }

        //SAPEmpHierarchy
        public virtual DbSet<EmpHierarchy> EmpHierarchies { get; set; }

        //MainServiceSearch
        public virtual DbSet<Search> Searches { get; set; }

        public virtual DbSet<BinaryObject> BinaryObjects { get; set; }

        public virtual DbSet<Friendship> Friendships { get; set; }

        public virtual DbSet<ChatMessage> ChatMessages { get; set; }

        public virtual DbSet<SubscribableEdition> SubscribableEditions { get; set; }

        public virtual DbSet<SubscriptionPayment> SubscriptionPayments { get; set; }

        public virtual DbSet<Invoice> Invoices { get; set; }

        public virtual DbSet<PersistedGrantEntity> PersistedGrants { get; set; }

        public PortalDbContext(DbContextOptions<PortalDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Birthday>().HasKey(x => new { x.BirthName });
            modelBuilder.Entity<EmployeeData>().HasKey(x => new { x.BirthName });
            modelBuilder.Entity<EmployeeVacationQuota>().HasKey(x => new { x.emp_username });
            modelBuilder.Entity<EmployeeVacationsView>().HasKey(x => new { x.Id });
            modelBuilder.Entity<ManagerVacations>().HasKey(x => new { x.EmployeeVacationId });
            //modelBuilder.Entity<EmployeeData>().Ignore(x => new { x.Name });

            modelBuilder.Entity<BinaryObject>(b =>
            {
                b.HasIndex(e => new { e.TenantId });
            });

            modelBuilder.Entity<ChatMessage>(b =>
            {
                b.HasIndex(e => new { e.TenantId, e.UserId, e.ReadState });
                b.HasIndex(e => new { e.TenantId, e.TargetUserId, e.ReadState });
                b.HasIndex(e => new { e.TargetTenantId, e.TargetUserId, e.ReadState });
                b.HasIndex(e => new { e.TargetTenantId, e.UserId, e.ReadState });
            });

            modelBuilder.Entity<Friendship>(b =>
            {
                b.HasIndex(e => new { e.TenantId, e.UserId });
                b.HasIndex(e => new { e.TenantId, e.FriendUserId });
                b.HasIndex(e => new { e.FriendTenantId, e.UserId });
                b.HasIndex(e => new { e.FriendTenantId, e.FriendUserId });
            });

            modelBuilder.Entity<Tenant>(b =>
            {
                b.HasIndex(e => new { e.SubscriptionEndDateUtc });
                b.HasIndex(e => new { e.CreationTime });
            });

            modelBuilder.Entity<SubscriptionPayment>(b =>
            {
                b.HasIndex(e => new { e.Status, e.CreationTime });
                b.HasIndex(e => new { PaymentId = e.ExternalPaymentId, e.Gateway });
            });

            modelBuilder.Entity<EmployeesView>().HasKey(x => new { x.Id });

            modelBuilder.ConfigurePersistedGrantEntity();

            modelBuilder.Entity<RestRequest>(b =>
            {
                b.HasIndex(e => new { e.RestSchedulesId });
            });

           // modelBuilder.Entity(typeof(RestRequest))
           //.HasOne(typeof(RestSchedule), "RestSchedulesId")
           //.WithMany()
           //.HasForeignKey("RestSchedulesId")
           //.OnDelete(DeleteBehavior.NoAction);
        }
    }
}
