using System.Text.Json.Serialization;

namespace PraticalCook.Domain.Entities.Enumerations
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum FoodType
    {
        Vegetable,
        Fruit,
        Grain,
        Protein,
        Dairy,
        Oil,
        Spice,
        Condiment,
        Sweetener,
        Baking,
        Beverage,
        Canned,
        Nut,
        Snack,
        Other
    }
}