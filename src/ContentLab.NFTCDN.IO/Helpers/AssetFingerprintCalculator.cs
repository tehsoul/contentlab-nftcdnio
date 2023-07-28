using Blake2Fast;
using System.Globalization;


namespace ContentLab.NFTCDN.IO.Helpers;
public class AssetFingerprintCalculator
{
    private const string HRP = "asset";

    public static string FromAssetId(string assetId)
    {
        var bytes = ConvertHexStringToByteArray(assetId);

        // create Blake2b-160 (20 bytes) hash of the hex policy+assetname
        var hash = Blake2b.ComputeHash(20, bytes);

        // encode said hash in bech32, prefixed with the hrp ("asset")
        var encoded = Bech32Engine.Encode(HRP, hash);

        return encoded;
    }

    public static string FromParts(string policyId, string assetNameHex)
    {
        var fullAssetId = $"{policyId}{assetNameHex}";

        return FromAssetId(fullAssetId);
    }


    private static byte[] ConvertHexStringToByteArray(string hexString)
    {
        if (hexString.Length % 2 != 0)
        {
            throw new ArgumentException(String.Format(CultureInfo.InvariantCulture, "The binary key cannot have an odd number of digits: {0}", hexString));
        }

        byte[] data = new byte[hexString.Length / 2];
        for (int index = 0; index < data.Length; index++)
        {
            string byteValue = hexString.Substring(index * 2, 2);
            data[index] = byte.Parse(byteValue, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
        }

        return data;
    }
}
