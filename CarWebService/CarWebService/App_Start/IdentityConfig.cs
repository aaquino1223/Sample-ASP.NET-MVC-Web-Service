using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using CarWebService.Models;

namespace CarWebService
{
    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.

    public class CarServiceUserManager : UserManager<CarServiceUser>
    {
        public CarServiceUserManager(IUserStore<CarServiceUser> store)
            : base(store)
        {
        }

        public static CarServiceUserManager Create(IdentityFactoryOptions<CarServiceUserManager> options, IOwinContext context)
        {
            var manager = new CarServiceUserManager(new UserStore<CarServiceUser>(context.Get<CarDbContext>()));
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<CarServiceUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };
            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = new DataProtectorTokenProvider<CarServiceUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }
}