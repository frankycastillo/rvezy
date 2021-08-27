using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace rvezy.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNullOrWhiteSpace(this string input)
        {
            return string.IsNullOrWhiteSpace(input);
        }

        public static string Copy(this string input)
        {
            return string.IsNullOrWhiteSpace(input) ? string.Empty : string.Copy(input);
        }

        public static Guid ToGuid(this string input)
        {
            return string.IsNullOrWhiteSpace(input) ? Guid.Empty : new Guid(input);
        }

        public static decimal CurrencyToDecimal(this string input)
        {
            return decimal.TryParse(input, NumberStyles.Currency, CultureInfo.CurrentCulture.NumberFormat, out var value) ? value : 0.00m;
        }
        
        public static string ToCurrency(this double? input)
        {
            return input.HasValue ? input.Value.ToString("C", CultureInfo.CreateSpecificCulture("es-es")) : "";
        }

        public static string UppercaseFirst(this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return input;
            }

            return input.Length == 1 ? input.ToUpperInvariant() : char.ToUpper(input[0]) + input.Substring(1);
        }

        public static string GetLast(this string source, int length)
        {
            return string.IsNullOrEmpty(source) || length >= source.Length ? source : source.Substring(source.Length - length);
        }

        public static T SafeConvert<T>(this string source, T defaultValue)
        {
            if (string.IsNullOrEmpty(source))
            {
                return defaultValue;
            }

            return (T) Convert.ChangeType(source, typeof(T));
        }

        public static string SafeSubstring(this string text, int start, int length)
        {
            return string.IsNullOrEmpty(text) || start > text.Length
                ? text
                : text.Substring(start, length > 240 ? 240 : length);
        }

        public static string RemovePunctuation(this string text)
        {
            return !string.IsNullOrEmpty(text)
                ? new string(text.Where(c => !char.IsPunctuation(c)).ToArray())
                : string.Empty;
        }

        public static string GetDomainFromEmail(this string email)
        {
            var domain = string.Empty;

            if (email.Contains("@") && email.IndexOf('@') < email.Length)
            {
                domain = email.Substring(email.IndexOf('@') + 1);
            }

            return domain;
        }

        public static string SanitizeEntry(this string value)
        {
            return string.IsNullOrWhiteSpace(value) ? string.Empty : value.ToLowerInvariant().Trim().Replace("\"", "");
        }

        public static string Sanitize(this string text, IEnumerable<char> remove = null)
        {
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }

            if (remove == null)
            {
                remove = new List<char> {' '};
            }

            text = new string(text.Where(c => !remove.Contains(c)).ToArray());
            return text;
        }

        public static string GetChecksum(this string value)
        {
            string hash;
            using (var md5 = MD5.Create())
            {
                hash = BitConverter.ToString(md5.ComputeHash(Encoding.UTF8.GetBytes(value))).Replace("-", string.Empty);
            }

            return hash;
        }
        
        public static string AddEllipsis(this string text, int len = 15)
        {
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }

            if (text.Length <= len)
            {
                return text;
            }
            text = text.SafeSubstring(0, len - 3);
            text = text + "...";
            return text;
        }
        
        public static string AsDate(this DateTimeOffset? value)
        {
            if (value != null)
            {
                return value.Value.ToString("d", CultureInfo.CreateSpecificCulture("es-es"));
            }

            return "";
        }
        

        // #region Connection String
        //
        // public static string BuildConnectionString(this DbConnectionConfig config)
        // {
        //     if (string.IsNullOrWhiteSpace(config.Port))
        //     {
        //         config.Port = "5432";
        //     }
        //
        //     if (string.IsNullOrWhiteSpace(config.Host) || string.IsNullOrWhiteSpace(config.User) || string.IsNullOrWhiteSpace(config.Password) || string.IsNullOrWhiteSpace(config.Database))
        //     {
        //         throw new InvalidDataException("The database configuration is invalid");
        //     }
        //
        //     return $"Host={config.Host};Port={config.Port};Username={config.User};Password={config.Password};Database={config.Database};ApplicationName={config.ApplicationName}";
        // }
        //
        // #endregion

        #region Base64

        public static string Base64Encode(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return string.Empty;
            }

            var plainTextBytes = Encoding.UTF8.GetBytes(value);
            return Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return string.Empty;
            }

            var base64EncodedBytes = Convert.FromBase64String(value);
            return Encoding.UTF8.GetString(base64EncodedBytes);
        }
        
        public static string FromBase64Bytes(this byte[] value)
        {
            return value == null ? null : Convert.ToBase64String(value);
        }
        
        public static byte[] ToBase64Bytes(this string value)
        {
            return string.IsNullOrWhiteSpace(value) ? null : Convert.FromBase64String(value);
        }

        #endregion

        #region String Compression

        public static string Compress(this string text)
        {
            if (!string.IsNullOrWhiteSpace(text) && text.Length > 255)
            {
                var bytes = Encoding.Unicode.GetBytes(text);
                using (var msi = new MemoryStream(bytes))
                using (var mso = new MemoryStream())
                {
                    using (var gs = new GZipStream(mso, CompressionMode.Compress))
                    {
                        msi.CopyTo(gs);
                    }

                    return Convert.ToBase64String(mso.ToArray());
                }
            }

            return Base64Encode(text);
        }

        public static string Decompress(this string text)
        {
            if (!string.IsNullOrWhiteSpace(text) && text.Length > 255)
            {
                var bytes = Convert.FromBase64String(text);
                using (var msi = new MemoryStream(bytes))
                using (var mso = new MemoryStream())
                {
                    using (var gs = new GZipStream(msi, CompressionMode.Decompress))
                    {
                        gs.CopyTo(mso);
                    }

                    return Encoding.Unicode.GetString(mso.ToArray());
                }
            }

            return Base64Decode(text);
        }

        #endregion

        #region Encoding

        // ReSharper disable once InconsistentNaming
        public static string AsUTF8(this string text, Encoding encoding = null)
        {
            if (encoding == null)
            {
                encoding = Encoding.Default;
            }

            var bytes = encoding.GetBytes(text);
            return Encoding.UTF8.GetString(bytes);
        }

        // ReSharper disable once InconsistentNaming
        public static string AsASCII(this string text, Encoding encoding = null)
        {
            if (encoding == null)
            {
                encoding = Encoding.Default;
            }

            var bytes = encoding.GetBytes(text);
            return Encoding.ASCII.GetString(bytes);
        }

        #endregion

        #region Slash 

        public static string WithTrailingSlash(this string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return string.Empty;
            }

            if (path[path.Length - 1] != '/')
            {
                return path + "/";
            }

            return path;
        }

        public static string WithoutTrailingSlash(this string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return string.Empty;
            }

            if (path[path.Length - 1] == '/')
            {
                return path.SafeSubstring(0, path.Length - 1);
            }

            return path;
        }

        public static string WithLeadingSlash(this string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return "/";
            }

            if (path[0] != '/')
            {
                return "/" + path;
            }

            return path;
        }

        public static string WithoutLeadingSlash(this string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return string.Empty;
            }

            if (path[0] == '/')
            {
                return path.SafeSubstring(1, path.Length - 1);
            }

            return path;
        }

        public static string WithLeadingPeriod(this string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return ".";
            }

            if (path[0] != '.')
            {
                return "." + path;
            }

            return path;
        }

        public static string WithoutLeadingPeriod(this string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return string.Empty;
            }

            if (path[0] == '.')
            {
                return path.SafeSubstring(1, path.Length - 1);
            }

            return path;
        }

        #endregion

        // #region Resources
        //
        // public static string GetFromResource(this string key, string defaultValue)
        // {
        //     const string catchAllValue = "There was an error locating that resource";
        //
        //     if (string.IsNullOrWhiteSpace(key))
        //     {
        //         return catchAllValue;
        //     }
        //
        //     var value = ErrorMessages.ResourceManager.GetString(key, CultureInfo.InvariantCulture);
        //     if (string.IsNullOrWhiteSpace(value))
        //     {
        //         return string.IsNullOrWhiteSpace(defaultValue) ? catchAllValue : defaultValue;
        //     }
        //
        //     return value;
        // }
        //
        // public static T GetFromResource<T>(this string key, T defaultValue)
        // {
        //     var value = ErrorMessages.ResourceManager.GetObject(key, CultureInfo.InvariantCulture);
        //     if (value == null && defaultValue != null)
        //     {
        //         return defaultValue;
        //     }
        //
        //     try
        //     {
        //         return (T) Convert.ChangeType(value, typeof(T));
        //     }
        //     catch (InvalidCastException)
        //     {
        //         return default(T);
        //     }
        // }
        //
        // #endregion

        #region Files

        public static async Task<string> ToBase64(this IFormFile formFile)
        {
            await using var memoryStream = new MemoryStream();
            await formFile.CopyToAsync(memoryStream).ConfigureAwait(false);
            var content = memoryStream.ToArray().ToBase64();

            return content;
        }
        
        public static async Task<byte[]> ToByteArray(this IFormFile formFile)
        {
            await using var memoryStream = new MemoryStream();
            await formFile.CopyToAsync(memoryStream).ConfigureAwait(false);
            return memoryStream.ToArray();
        }
        
       
        #endregion

        #region Streams

        public static byte[] ToBytes(this string text)
        {
            return Encoding.UTF8.GetBytes(text);
        }

        public static byte[] ToBytes(this Stream stream)
        {
            return stream.FromStream().ToBytes();
        }

        public static string ToBase64(this byte[] bytes)
        {
            return Convert.ToBase64String(bytes);
        }

        public static string FromBytes(this byte[] content)
        {
            return Encoding.UTF8.GetString(content, 0, content.Length);
        }

        public static string FromStream(this Stream stream)
        {
            string text;
            using (var reader = new StreamReader(stream))
            {
                text = reader.ReadToEnd();
            }

            return text;
        }

        #endregion

        // #region Query String
        //
        // public static IDictionary<string, string> ToKeyValue(this object metaToken)
        // {
        //     if (metaToken == null)
        //     {
        //         return null;
        //     }
        //
        //     if (!(metaToken is JToken token))
        //     {
        //         return ToKeyValue(JObject.FromObject(metaToken));
        //     }
        //
        //     if (token.HasValues)
        //     {
        //         var contentData = new Dictionary<string, string>();
        //         foreach (var child in token.Children().ToList())
        //         {
        //             var childContent = child.ToKeyValue();
        //             if (childContent != null)
        //             {
        //                 contentData = contentData.Concat(childContent)
        //                     .ToDictionary(k => k.Key, v => v.Value);
        //             }
        //         }
        //
        //         return contentData;
        //     }
        //
        //     var jValue = token as JValue;
        //     if (jValue?.Value == null)
        //     {
        //         return null;
        //     }
        //
        //     var value = jValue?.Type == JTokenType.Date ? jValue?.ToString("o", CultureInfo.InvariantCulture) : jValue?.ToString(CultureInfo.InvariantCulture);
        //
        //     return new Dictionary<string, string> {{token.Path, value}};
        // }
        //
        // public static string ToQueryString(this object source)
        // {
        //     var keyValueContent = source.ToKeyValue();
        //     var formUrlEncodedContent = new FormUrlEncodedContent(keyValueContent);
        //     var urlEncodedString = formUrlEncodedContent.ReadAsStringAsync().Result;
        //     return urlEncodedString.AsUTF8();
        // }
        //
        // #endregion

        #region Tokenize

        public static string[] GetTokens(this string text)
        {
            text = text.ToLower();
            text = text.Replace(",", "");
            text = text.Replace("'", "");
            var tokens = text.Split(new[] {' ', '-'}, StringSplitOptions.RemoveEmptyEntries);

            var result = new List<string>();

            foreach (var token in tokens)
            {
                var strippedToken = token;
                if (token.EndsWith("."))
                {
                    strippedToken = token.Remove(token.Length - 1);
                }

                var stringwithnoAccent = strippedToken.RemoveDiacritics();
                result.Add(stringwithnoAccent);
            }

            return result.ToArray();
        }

        public static string RemoveDiacritics(this string text)
        {
            return string.Concat(text.Normalize(NormalizationForm.FormD)
                    .Where(ch => CharUnicodeInfo.GetUnicodeCategory(ch) != UnicodeCategory.NonSpacingMark))
                .Normalize(NormalizationForm.FormC);
        }

        public static string JoinAndSanitize(this string[] values)
        {
            var result = string.Join("", values);
            result = $"{result}".RemovePunctuation().RemoveDiacritics().Replace(" ", "").Trim();
            result = Regex.Replace(result, @"\s+", "");
            result = result.ToLowerInvariant();
            return result;
        }

        #endregion

        #region Join

        public static string AsCsv(this IEnumerable<string> value, string separator = ",")
        {
            return !value.IsNullOrEmpty() ? string.Join(separator, value) : string.Empty;
        }

        public static string AsCsv(this string[] value, string separator = ",")
        {
            return !value.IsNullOrEmpty() ? string.Join(separator, value) : string.Empty;
        }

        public static string AsCsv<T>(this T[] value, string separator = ",")
        {
            return !value.IsNullOrEmpty() ? string.Join(separator, value) : string.Empty;
        }

        #endregion

        // #region Encryption
        //
        // public static string Encrypt(this string value, string keyString = "")
        // {
        //     if (string.IsNullOrWhiteSpace(keyString))
        //     {
        //         keyString = ConfigurationKeys.EncryptionKey;
        //     }
        //
        //     var key = Encoding.UTF8.GetBytes(keyString);
        //     var vector = Encoding.UTF8.GetBytes(ConfigurationKeys.EncryptionVector);
        //
        //     using (var aesAlg = new AesCryptoServiceProvider())
        //     {
        //         aesAlg.Key = key;
        //         aesAlg.IV = vector;
        //
        //         // Create a decrytor to perform the stream transform.
        //         var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
        //
        //         // Create the streams used for encryption.
        //         using (var msEncrypt = new MemoryStream())
        //         {
        //             using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
        //             {
        //                 using (var swEncrypt = new StreamWriter(csEncrypt))
        //                 {
        //                     //Write all data to the stream.
        //                     swEncrypt.Write(value);
        //                 }
        //
        //                 var encrypted = msEncrypt.ToArray();
        //                 var result = Convert.ToBase64String(encrypted);
        //                 return result;
        //             }
        //         }
        //     }
        // }
        //
        // public static string Decrypt(this string value, string keyString = "")
        // {
        //     if (string.IsNullOrWhiteSpace(keyString))
        //     {
        //         keyString = ConfigurationKeys.EncryptionKey;
        //     }
        //
        //     var key = Encoding.UTF8.GetBytes(keyString);
        //     var vector = Encoding.UTF8.GetBytes(ConfigurationKeys.EncryptionVector);
        //
        //     var cipherText = Convert.FromBase64String(value);
        //
        //     using (var aesAlg = new AesCryptoServiceProvider())
        //     {
        //
        //         // Create the streams used for decryption.
        //         using (var msDecrypt = new MemoryStream(cipherText))
        //         {
        //             aesAlg.Key = key;
        //             aesAlg.IV = vector;
        //             
        //             // Create a decrytor to perform the stream transform.
        //             var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
        //
        //             using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
        //             {
        //                 using (var srDecrypt = new StreamReader(csDecrypt))
        //                 {
        //                     // Read the decrypted bytes from the decrypting stream
        //                     // and place them in a string.
        //                     var decrypted = srDecrypt.ReadToEnd();
        //                     return decrypted;
        //                 }
        //             }
        //         }
        //     }
        // }
        //
        // #endregion
    }
}