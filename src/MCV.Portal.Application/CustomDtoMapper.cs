using Abp.Application.Editions;
using Abp.Application.Features;
using Abp.Auditing;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.EntityHistory;
using Abp.Localization;
using Abp.Notifications;
using Abp.Organizations;
using Abp.UI.Inputs;
using AutoMapper;
using MCV.Portal.Auditing.Dto;
using MCV.Portal.Authorization.Accounts.Dto;
using MCV.Portal.Authorization.Permissions.Dto;
using MCV.Portal.Authorization.Roles;
using MCV.Portal.Authorization.Roles.Dto;
using MCV.Portal.Authorization.Users;
using MCV.Portal.Authorization.Users.Dto;
using MCV.Portal.Authorization.Users.Importing.Dto;
using MCV.Portal.Authorization.Users.Profile.Dto;
using MCV.Portal.Chat;
using MCV.Portal.Chat.Dto;
using MCV.Portal.Editions;
using MCV.Portal.Editions.Dto;
using MCV.Portal.Friendships;
using MCV.Portal.Friendships.Cache;
using MCV.Portal.Friendships.Dto;
using MCV.Portal.Localization.Dto;
using MCV.Portal.MultiTenancy;
using MCV.Portal.MultiTenancy.Dto;
using MCV.Portal.MultiTenancy.HostDashboard.Dto;
using MCV.Portal.MultiTenancy.Payments;
using MCV.Portal.MultiTenancy.Payments.Dto;
using MCV.Portal.Notifications.Dto;
using MCV.Portal.Organizations.Dto;
using MCV.Portal.Sessions.Dto;
using MCV.Portal.Source.Announcements;
using MCV.Portal.Source.AnnouncementsTypes;
using MCV.Portal.Source.AnnouncementsTypes.Dto;
using MCV.Portal.Source.Anouncements.Dto;
using MCV.Portal.Source.Birthdays;
using MCV.Portal.Source.PortalNotifications.EmailTemplates;
using MCV.Portal.Source.PortalNotifications.EmailTemplates.Dto;
using MCV.Portal.Source.Restaurant;
using MCV.Portal.Source.Restaurant.Dto;
using MCV.Portal.Source.Restaurants.Dto;
using MCV.Portal.Source.SAPSettings;
using MCV.Portal.Source.SAPSettings.Dto;
using MCV.Portal.Source.Search;
using MCV.Portal.Source.Search.Dto;
using MCV.Portal.Source.Vacations;
using MCV.Portal.Source.Vacations.Dto;
using MCV.Portal.Source.VacationTypes;
using MCV.Portal.Source.VacationTypes.Dto;
using MCV.Portal.Tenants.Dashboard.Dto;

namespace MCV.Portal
{
    internal static class CustomDtoMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {

            //Inputs
            configuration.CreateMap<CheckboxInputType, FeatureInputTypeDto>();
            configuration.CreateMap<SingleLineStringInputType, FeatureInputTypeDto>();
            configuration.CreateMap<ComboboxInputType, FeatureInputTypeDto>();
            configuration.CreateMap<IInputType, FeatureInputTypeDto>()
                .Include<CheckboxInputType, FeatureInputTypeDto>()
                .Include<SingleLineStringInputType, FeatureInputTypeDto>()
                .Include<ComboboxInputType, FeatureInputTypeDto>();
            configuration.CreateMap<StaticLocalizableComboboxItemSource, LocalizableComboboxItemSourceDto>();
            configuration.CreateMap<ILocalizableComboboxItemSource, LocalizableComboboxItemSourceDto>()
                .Include<StaticLocalizableComboboxItemSource, LocalizableComboboxItemSourceDto>();
            configuration.CreateMap<LocalizableComboboxItem, LocalizableComboboxItemDto>();
            configuration.CreateMap<ILocalizableComboboxItem, LocalizableComboboxItemDto>()
                .Include<LocalizableComboboxItem, LocalizableComboboxItemDto>();

            //Chat
            configuration.CreateMap<ChatMessage, ChatMessageDto>();
            configuration.CreateMap<ChatMessage, ChatMessageExportDto>();

            //Feature
            configuration.CreateMap<FlatFeatureSelectDto, Feature>().ReverseMap();
            configuration.CreateMap<Feature, FlatFeatureDto>();

            //Role
            configuration.CreateMap<RoleEditDto, Role>().ReverseMap();
            configuration.CreateMap<Role, RoleListDto>();
            configuration.CreateMap<UserRole, UserListRoleDto>();

            //Edition
            configuration.CreateMap<EditionEditDto, SubscribableEdition>().ReverseMap();
            configuration.CreateMap<EditionCreateDto, SubscribableEdition>();
            configuration.CreateMap<EditionSelectDto, SubscribableEdition>().ReverseMap();
            configuration.CreateMap<SubscribableEdition, EditionInfoDto>();

            configuration.CreateMap<Edition, EditionInfoDto>().Include<SubscribableEdition, EditionInfoDto>();

            configuration.CreateMap<SubscribableEdition, EditionListDto>();
            configuration.CreateMap<Edition, EditionEditDto>();
            configuration.CreateMap<Edition, SubscribableEdition>();
            configuration.CreateMap<Edition, EditionSelectDto>();


            //Payment
            configuration.CreateMap<SubscriptionPaymentDto, SubscriptionPayment>().ReverseMap();
            configuration.CreateMap<SubscriptionPaymentListDto, SubscriptionPayment>().ReverseMap();
            configuration.CreateMap<SubscriptionPayment, SubscriptionPaymentInfoDto>();

            //Permission
            configuration.CreateMap<Permission, FlatPermissionDto>();
            configuration.CreateMap<Permission, FlatPermissionWithLevelDto>();

            //Language
            configuration.CreateMap<ApplicationLanguage, ApplicationLanguageEditDto>();
            configuration.CreateMap<ApplicationLanguage, ApplicationLanguageListDto>();
            configuration.CreateMap<NotificationDefinition, NotificationSubscriptionWithDisplayNameDto>();
            configuration.CreateMap<ApplicationLanguage, ApplicationLanguageEditDto>()
                .ForMember(ldto => ldto.IsEnabled, options => options.MapFrom(l => !l.IsDisabled));

            //Tenant
            configuration.CreateMap<Tenant, RecentTenant>();
            configuration.CreateMap<Tenant, TenantLoginInfoDto>();
            configuration.CreateMap<Tenant, TenantListDto>();
            configuration.CreateMap<TenantEditDto, Tenant>().ReverseMap();
            configuration.CreateMap<CurrentTenantInfoDto, Tenant>().ReverseMap();

            //User
            configuration.CreateMap<User, UserEditDto>()
                .ForMember(dto => dto.Password, options => options.Ignore())
                .ReverseMap()
                .ForMember(user => user.Password, options => options.Ignore());
            configuration.CreateMap<User, UserLoginInfoDto>();
            configuration.CreateMap<User, UserListDto>();
            configuration.CreateMap<User, ChatUserDto>();
            configuration.CreateMap<User, OrganizationUnitUserListDto>();
            configuration.CreateMap<Role, OrganizationUnitRoleListDto>();
            configuration.CreateMap<CurrentUserProfileEditDto, User>().ReverseMap();
            configuration.CreateMap<UserLoginAttemptDto, UserLoginAttempt>().ReverseMap();
            configuration.CreateMap<ImportUserDto, User>();

            //AuditLog
            configuration.CreateMap<AuditLog, AuditLogListDto>();
            configuration.CreateMap<EntityChange, EntityChangeListDto>();
            configuration.CreateMap<EntityPropertyChange, EntityPropertyChangeDto>();

            //Friendship
            configuration.CreateMap<Friendship, FriendDto>();
            configuration.CreateMap<FriendCacheItem, FriendDto>();

            //OrganizationUnit
            configuration.CreateMap<OrganizationUnit, OrganizationUnitDto>();

            /* ADD YOUR OWN CUSTOM AUTOMAPPER MAPPINGS HERE */

            //Birthday
            configuration.CreateMap<Birthday, MyBirthday>();
            
            //AnnouncementType
            configuration.CreateMap<AnnouncementType, AnnouncementTypeListDto>();
            configuration.CreateMap<CreateAnnouncementTypeInput, AnnouncementType>();
            configuration.CreateMap<AnnouncementType, GetAnnouncementTypeForEditOutput>();

            //Announcements
            configuration.CreateMap<Announcement, AnnouncementsListDto>();
            configuration.CreateMap<CreateAnnouncementInput, Announcement>();
            configuration.CreateMap<Announcement, GetAnnouncementForEditOutput>();

            //* Restaurants*//
            //RestItems
            configuration.CreateMap<RestItem, RestItemsListDto>();
            configuration.CreateMap<CreateRestItemInput, RestItem>();
            configuration.CreateMap<RestItem, GetRestItemForEditOutput>();

            //RestInfo
            configuration.CreateMap<RestInfo, RestInfosListDto>();
            configuration.CreateMap<RestInfosListDto, RestInfo>();
            configuration.CreateMap<CreateRestInfoInput, RestInfo>();
            configuration.CreateMap<RestInfo, GetRestInfoForEditOutput>();
            configuration.CreateMap<RestInfoAdmins, RestInfoAdminsDto>();

            //RestSchedule
            configuration.CreateMap<RestSchedule, RestSchesListDto>();
            configuration.CreateMap<CreateRestScheInput, RestSchedule>();
            configuration.CreateMap<RestSchedule, GetRestScheForEditOutput>();

            //RestScheduleItem
            configuration.CreateMap<AttachRestScheItemInput, RestScheduleItem>();
            configuration.CreateMap<RestScheduleItem, GetRestScheItemForAttachOutput>();
            configuration.CreateMap<RestScheduleItem, RestSchesItemListDto>();
            configuration.CreateMap<RestSchesItemListDto, RestScheduleItem>();


            //RestRequest
            configuration.CreateMap<RestRequest, RestReqListDto>();
            configuration.CreateMap<CreateRestReqInput, RestRequest>();
            configuration.CreateMap<RestRequest, GetRestReqForEditOutput>();
            configuration.CreateMap<EmployeesView, EmployeesViewList>();
            configuration.CreateMap<RestRequestItem, SelectRestReqSchItem>();
            configuration.CreateMap<RestRequestItem, RestReqItemListDto>();
            configuration.CreateMap<RestReqItemListDto, RestRequestItem>();

            configuration.CreateMap<RestResponse, RestRespListDto>();

            configuration.CreateMap<RestCategory, RestCategoryListDto>();
            configuration.CreateMap<ItemsCategory, ItemsCategoryListDto>();

            configuration.CreateMap<CreateNonSchRestInput, RestNonSchItem>();

            configuration.CreateMap<RestNonSchItem, RestNonSchesListDto>();

            configuration.CreateMap<CreateRestScheInput, RestNonSchItem>();

            configuration.CreateMap<PaymentType, PaymentTypesListDto>();
            configuration.CreateMap<PaymentTypesListDto, PaymentType>();
            configuration.CreateMap<RequestLogListDto, RequestLog>();
            configuration.CreateMap<RequestLog, RequestLogListDto>();

            

            //VacationTypes
            configuration.CreateMap<VacationType, VacationTypesListDto>();
            configuration.CreateMap<EmployeeData, EmployeeDto>();
            configuration.CreateMap<EmployeeVacationQuota, EmployeeVacationQuotaDto>();

            //Vacations
            configuration.CreateMap<CreateEmployeeVacationInput, EmployeeVacation>();
            configuration.CreateMap<EmployeeVacationsView, EmployeeVacationsListDto>();
            configuration.CreateMap<EmployeeVacation, EmployeeVacationsListDto>();
            configuration.CreateMap<ManagerVacations, ManagerVacationsOutputListDto>();


            //SAP Settings
            configuration.CreateMap<SAPSetting, SAPSettingListDto>();
            configuration.CreateMap<CreateSAPSettingInput, SAPSetting>();
            configuration.CreateMap<SAPSetting, GetSAPSettingForEditOutput>();

            //Search Main
            configuration.CreateMap<Search, SearchMainListDto>();

            //////PortalNotifications
            //EmailTemplates
            configuration.CreateMap<EmailTemplate, EmailTemplatesListDto>();
            configuration.CreateMap<CreateEmailTemplateInput, EmailTemplate>();
            configuration.CreateMap<EmailTemplate, GetUpdateEmailTemplate>();
            configuration.CreateMap<UpdateEmailTemplateInput, EmailTemplate>();
            

        }
    }
}
