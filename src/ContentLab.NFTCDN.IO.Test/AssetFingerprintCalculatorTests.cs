using ContentLab.NFTCDN.IO.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentLab.IntegrationTest.NFTCDNIO;
public class AssetFingerprintCalculatorTests
{
    [Theory]
    [InlineData("40fa2aa67258b4ce7b5782f74831d46a84c59a0ff0c28262fab21728", "436c61794e6174696f6e37353639", "asset1m2wdg58w5fc7srq08ndkxl9usamvqefuu55yrd")]
    [InlineData("901ba6e9831b078e131a1cc403d6139af21bda255cea6c9f770f4834", "4d616c6c6172644f7264657230303435", "asset1ud76uam22c2ve272rklxm926a0smfcyl779af0")]
    [InlineData("4cbd11e94241808aa2f4b93bf22a8d8a93fcd4326985c87e0c73d651", "000de140536872696e6531323232", "asset1na7497dtw4gfujjt923vr3cykda3f8xuu9tqjf")]
    [InlineData("43528549ec02054e09bcdadb34d27fcf18f699f1e925a86afd483066", "000de1404f544b50697261746534303233", "asset1ck2aym6tfpxy5rzkfxjk7rlgxhcc3v8hz6q78j")]
    public async Task calculate_assetfingerprint_from_policyid_and_hex_assetname(string policyId, string assetNameHex, string assetFingerprint)
    {
        var calculatedFingerprint = AssetFingerprintCalculator.FromParts(policyId, assetNameHex);

        Assert.Equal(assetFingerprint, calculatedFingerprint);
    }
}
