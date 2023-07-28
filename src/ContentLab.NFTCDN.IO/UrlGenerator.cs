using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Text;

namespace ContentLab.NFTCDN.IO;

/// <summary>
/// A generator that will provide you with valid links as specified at <see cref="https://github.com/nftcdn/support.nftcdn.io">NFTDCNIO support documentation</see>
/// </summary>
public class UrlGenerator
{
    public UrlGenerator(string subdomain, string secretKey)
    {
        Subdomain = subdomain;
        SecretKeyString = secretKey;
        SecretKey = Convert.FromBase64String(secretKey);
        var hasher = HMACSHA256.Create("HMACSHA256");
        hasher.Key = SecretKey;
        Hasher = hasher;
    }

    public string Subdomain { get; private set; }
    public byte[] SecretKey { get; private set; }
    public string SecretKeyString { get; private set; }

    public HMAC Hasher { get; set; }

    private const string PATH_IMAGE = "/image";
    private const string PATH_METADATA = "/metadata";

    public string GenerateImageUrl(string assetThumbprint, int? size)
    {
        return GenerateUrl(assetThumbprint, PATH_IMAGE, size);
    }

    public string GenerateMetadataUrl(string assetThumbprint)
    {
        return GenerateUrl(assetThumbprint, PATH_METADATA, null);
    }

    private string GenerateUrl(string assetThumbprint, string path, int? size)
    {
        var urlToHash = BuildUrl(assetThumbprint, path, null, size);
        var computed = Hasher.ComputeHash(Encoding.ASCII.GetBytes(urlToHash));
        var tkForUrl = Base64UrlEncoder.Encode(computed);

        return BuildUrl(assetThumbprint, path, tkForUrl, size);
    }

    private string BuildUrl(string assetThumbprint, string path, string tk, int? size)
    {
        var url = $"https://{assetThumbprint}.{Subdomain}.nftcdn.io{path}";

        url = $"{url}?tk={tk}";

        if (size.HasValue)
        {
            url = $"{url}&size={size.Value}";
        }

        return url;
    }
}


