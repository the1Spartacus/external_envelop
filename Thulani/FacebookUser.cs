// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using Thulani;
//
//    var facebookUser = FacebookUser.FromJson(jsonString);

namespace Thulani
{
    using System;
    using System.Collections.Generic;
    using System.Net;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class FacebookUser
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("picture")]
        public Picture Picture { get; set; }

        [JsonProperty("locale")]
        public string Locale { get; set; }

        [JsonProperty("link")]
        public string Link { get; set; }

        [JsonProperty("cover")]
        public Cover Cover { get; set; }

        [JsonProperty("age_range")]
        public AgeRange AgeRange { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }

        [JsonProperty("is_verified")]
        public bool IsVerified { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
    }

    public partial class AgeRange
    {
        [JsonProperty("min")]
        public long Min { get; set; }
    }

    public partial class Cover
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("offset_x")]
        public long OffsetX { get; set; }

        [JsonProperty("offset_y")]
        public long OffsetY { get; set; }

        [JsonProperty("source")]
        public string Source { get; set; }
    }

    public partial class Picture
    {
        [JsonProperty("data")]
        public Data Data { get; set; }
    }

    public partial class Data
    {
        [JsonProperty("height")]
        public long Height { get; set; }

        [JsonProperty("is_silhouette")]
        public bool IsSilhouette { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("width")]
        public long Width { get; set; }
    }

    public partial class FacebookUser
    {
        public static FacebookUser FromJson(string json) => JsonConvert.DeserializeObject<FacebookUser>(json, Thulani.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this FacebookUser self) => JsonConvert.SerializeObject(self, Thulani.Converter.Settings);
    }

    internal class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters = {
                new IsoDateTimeConverter()
                {
                    DateTimeStyles = DateTimeStyles.AssumeUniversal,
                },
            },
        };
    }
}
