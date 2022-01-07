namespace SoccerAPI.Infrastructure.Filters
{
	using System;
	using System.Linq;

	using Microsoft.AspNetCore.Mvc.Filters;

    using SoccerAPI.Common.Constants;
    using SoccerAPI.Common.Exeptions;
    using SoccerAPI.DTOs.User;

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
	public class JwtAuthorizeAttribute : Attribute, IAuthorizationFilter
	{
		public string[] Roles { get; set; }

		public void OnAuthorization(AuthorizationFilterContext context)
		{
			var user = (GetUserForSessionDTO)context.HttpContext.Items["User"];

            if (user == null)
            {
				throw new UnauthorizedAccessCustomException(ExceptionMessages.USER_UNAUTHENTICATED_MESSAGE);
			}

			bool isInRole = false;
			for (int index = 0; index < Roles.Length; index++)
			{
				string role = Roles[index];

				isInRole = user.Roles.Any(x => x.Name == role);

				if (isInRole)
				{
					break;
				}
			}

			if (isInRole == false)
			{
				throw new UnauthorizedAccessCustomException(ExceptionMessages.USER_DOES_NOT_HAVE_PERMISSIONS_ERROR_MESSAGE);
			}
		}
	}
}
