using System.Text.Json.Serialization;

namespace PraticalCook.Domain.Entities.Enumerations
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum FoodType
    {
        vegetable,
        fruit,
        grain,
        protein,
        dairy,
        oil,
        spice,
        condiment,
        sweetener,
        baking,
        beverage,
        canned,
        nut,
        snack,
        misc
    }
}