using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PraticalCook.Domain.Entities.Enumerations
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Measurement
    {
        [EnumMember(Value = "Unit(s)")]
        Unit,

        [EnumMember(Value = "Cup")]
        Cup,

        [EnumMember(Value = "Tbsp")]
        Tablespoon,

        [EnumMember(Value = "Tsp")]
        Teaspoon,

        [EnumMember(Value = "Gr")]
        Grams,

        [EnumMember(Value = "Ml")]
        Milliliters,

        [EnumMember(Value = "Piece(s)")]
        Piece
    }
}