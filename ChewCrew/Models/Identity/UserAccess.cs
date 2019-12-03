using ChewCrew.Helpers;
using ChewCrew.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace ChewCrew.Models.Identity
{
    public class UserAccess : IUserAccess
    {
        private readonly ClaimsPrincipal _principal;

        public UserAccess(ClaimsPrincipal principal)
        {
            _principal = principal;
        }

        public bool CanAddRestaurant()
        {
            return _principal.IsInRole(ChewCrewRoles.GroupAdmin) || _principal.IsInRole(ChewCrewRoles.SuperAdmin);
        }

        public bool CanEliminateRestaurantSuggestions()
        {
            throw new NotImplementedException();
        }

        public bool CanOverrideGroupDecision()
        {
            throw new NotImplementedException();
        }

        public bool CanRateRestaurant()
        {
            return true;
        }

        public bool CanSuggestRestaurants()
        {
            throw new NotImplementedException();
        }
    }
}
