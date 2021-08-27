using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text.Json;

namespace rvezy.Extensions
{
    public static class ConversionExtensions
    {
        #region Null Checks

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> value)
        {
            return value == null || !value.Any();
        }

        public static bool IsNullOrEmpty(this byte[] value)
        {
            return value == null || value.Length == 0;
        }

        #endregion

        #region Numbers & Money

        public static int AsPercent(this double value)
        {
            return (int) Math.Round(value * 100, MidpointRounding.AwayFromZero);
        }

        public static string AsMoney(this long input, bool cents = true, string locale = "en-US")
        {
            return ((double) input).AsMoney(cents, locale);
        }

        public static string AsMoney(this double input, bool cents = false, string locale = "en-US")
        {
            if (cents)
            {
                input = input / 100;
            }

            var cultureInfo = new CultureInfo(locale);
            var result = !string.IsNullOrWhiteSpace(locale)
                ? input.ToString("C", cultureInfo.NumberFormat)
                : input.ToString("C");

            return result;
        }

        #endregion

        #region Generic Conversion

        public static T? ToNullable<T>(this string value) where T : struct
        {
            var result = new T?();
            try
            {
                if (!string.IsNullOrEmpty(value) && value.Trim().Length > 0)
                {
                    var conv = TypeDescriptor.GetConverter(typeof(T));
                    result = (T) conv.ConvertFrom(value);
                }
            }
            catch
            {
            }

            return result;
        }

        public static T? ConvertTo<T>(this string value) where T : struct
        {
            var result = new T?();
            try
            {
                if (!string.IsNullOrEmpty(value) && value.Trim().Length > 0)
                {
                    var conv = TypeDescriptor.GetConverter(typeof(T));
                    result = (T) conv.ConvertFrom(value);
                }
            }
            catch
            {
            }

            return result;
        }

        #endregion

        #region DateTime

        public static DateTimeOffset ToDateTimeOffset(this string value, DateTimeOffset defaultValue)
        {
            var dtValue = DateTimeOffset.TryParse(value, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal,
                out var dateResult)
                ? dateResult
                : defaultValue;

            return dtValue;
        }

        public static DateTime ToUtcDateTime(this string value, DateTime defaultValue)
        {
            var dtValue = DateTime.TryParse(value, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal,
                out var dateResult)
                ? dateResult
                : defaultValue;

            return DateTime.SpecifyKind(dtValue, DateTimeKind.Utc);
        }

        public static string ToIso8601(this DateTimeOffset? value)
        {
            return value.HasValue ? value.GetValueOrDefault().ToIso8601() : string.Empty;
        }

        public static string ToIso8601(this DateTimeOffset value)
        {
            return value.DateTime.ToIso8601();
        }

        public static string ToIso8601(this DateTime? value)
        {
            return value.HasValue ? value.GetValueOrDefault().ToIso8601() : string.Empty;
        }

        public static string ToIso8601(this DateTime value)
        {
            return value.ToString("yyyy-MM-ddTHH:mm:ssZ");
        }

        public static double AsUnixTimestamp(this DateTime value)
        {
            return value.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
        }

        #endregion

        #region Time Periods

        // public static ITimePeriod GetOrDefault(this ITimePeriodRequest timePeriod)
        // {
        //     if (!timePeriod.IsValid())
        //     {
        //         return new TimePeriod
        //         {
        //             StartTime = DateTime.UtcNow,
        //             EndTime = DateTime.UtcNow + TimeSpan.FromHours(48)
        //         };
        //     }
        //
        //     return new TimePeriod
        //     {
        //         StartTime = timePeriod.StartsOn,
        //         EndTime = timePeriod.EndsOn
        //     };
        // }
        //
        // public static ITimePeriod GetOrDefault(this ITimePeriod timePeriod)
        // {
        //     return timePeriod ?? new TimePeriod
        //     {
        //         StartTime = DateTime.UtcNow,
        //         EndTime = DateTime.UtcNow + TimeSpan.FromHours(48)
        //     };
        // }
        //
        // public static ITimePeriod GetOrDefault(this ITimePeriod timePeriod, TimeSpan timeSpan)
        // {
        //     return timePeriod ?? new TimePeriod
        //     {
        //         StartTime = DateTime.UtcNow,
        //         EndTime = DateTime.UtcNow + timeSpan
        //     };
        // }
        //
        // public static ITimePeriod AsDatePeriod(this ITimePeriod timePeriod)
        // {
        //     if (!timePeriod.IsValid())
        //     {
        //         timePeriod = timePeriod.GetOrDefault();
        //     }
        //
        //     timePeriod.StartTime = timePeriod.StartTime.Date;
        //     timePeriod.EndTime = timePeriod.EndTime.Date;
        //
        //     return timePeriod;
        // }
        //
        // public static IDateTimePeriod AsDateTimePeriod(this ITimePeriod timePeriod)
        // {
        //     if (!timePeriod.IsValid())
        //     {
        //         timePeriod = timePeriod.GetOrDefault();
        //     }
        //
        //     var result = new DateTimePeriod
        //     {
        //         StartTime = timePeriod.StartTime.Date,
        //         EndTime = timePeriod.EndTime.Date
        //     };
        //
        //     return result;
        // }

        #endregion

        #region JSON

        public static string ToJson(this object value, JsonSerializerOptions settings = null)
        {
            try
            {
                var result = settings == null
                    ? JsonSerializer.Serialize(value)
                    : JsonSerializer.Serialize(value, settings);
        
                return result;
            }
            catch (Exception ex)
            {
                // Log.Error($"ConversionExtensions.ToJson: Error converting {value}", ex);
            }
        
            return null;
        }
        
        public static T FromJson<T>(this string value, JsonSerializerOptions settings = null)
        {
            try
            {
                var result = settings == null
                    ? JsonSerializer.Deserialize<T>(value)
                    : JsonSerializer.Deserialize<T>(value, settings);
        
                return result;
            }
            catch (Exception ex)
            {
                // Log.Error($"ConversionExtensions.FromJson: Error converting {value}", ex);
            }
        
            return default(T);
        }

        #endregion
    }
}