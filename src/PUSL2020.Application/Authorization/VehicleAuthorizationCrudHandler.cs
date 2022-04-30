using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using PUSL2020.Domain.Entities.Vehicles;

namespace PUSL2020.Application.Authorization;

public class VehicleAuthorizationCrudHandler :
    AuthorizationHandler<OperationAuthorizationRequirement, Vehicle>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        OperationAuthorizationRequirement requirement,
        Vehicle vehicle)
    {
        context.Succeed(requirement);

        return Task.CompletedTask;
    }
}