using System.Text.Json.Serialization;

namespace PraticalCook.Domain.Entities.Enumerations
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum FoodType
    {
        Vegetable,
        Fruit,
        Protein,
        Carbohydrate,
        Dairy,
        Fat,
        Drinks,
        Other,
    }
}