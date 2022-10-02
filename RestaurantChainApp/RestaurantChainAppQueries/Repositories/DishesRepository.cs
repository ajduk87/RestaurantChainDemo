using RestaurantChainAppQueries.Dtoes;
using System.Collections.Generic;
using System.Data;
using Dapper;

namespace RestaurantChainAppQueries.Repositories
{
    public class DishesRepository
    {
        public List<Dish> SelectDishes(IDbConnection connection)
        {

            return connection.Query<Dish>(Sql.Queries["SelectDishes"]).AsList();
        }

        public List<Dish> SelectSingleDishes(IDbConnection connection)
        {

            return connection.Query<Dish>(Sql.Queries["SelectSingleDishes"]).AsList();
        }

        public List<Meal> SelectMeals(IDbConnection connection)
        {

            return connection.Query<Meal>(Sql.Queries["SelectMeals"]).AsList();
        }

        public List<Dish> SelectDishesByMealId(IDbConnection connection, int mealid)
        {

            return connection.Query<Dish>(Sql.Queries["SelectDishesByMealId"], new { mealid = mealid }).AsList();
        }
    }
}
