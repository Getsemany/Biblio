using Biblio.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Biblio.Authorization;

namespace Biblio.Authorization
{
    public class ContactIsUserAuthorizationHandler
                : AuthorizationHandler<OperationAuthorizationRequirement, Contact>
    {

        

        protected override Task
            HandleRequirementAsync(AuthorizationHandlerContext context,
                                   OperationAuthorizationRequirement requirement,
                                   Contact resource)
        {
            if (context.User == null || resource == null)
            {
                return Task.CompletedTask;
            }

            // If not asking for CRUD permission, return.

            if (requirement.Name != Constants.CreateOperationName &&
                requirement.Name != Constants.UpdateOperationName &&
                requirement.Name != Constants.DeleteOperationName )
            {
                return Task.CompletedTask;
            }

            if (context.User.IsInRole(Constants.ContactUsersRole))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}