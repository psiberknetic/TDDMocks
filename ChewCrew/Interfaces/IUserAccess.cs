using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChewCrew.Interfaces
{
    public interface IUserAccess
    {
        bool CanAddRestaurant();
        bool CanRateRestaurant();
        bool CanEliminateRestaurantSuggestions();
        bool CanSuggestRestaurants();
        bool CanOverrideGroupDecision();
    }
}
