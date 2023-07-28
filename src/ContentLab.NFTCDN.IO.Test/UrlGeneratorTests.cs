using ContentLab.NFTCDN.IO;
using Xunit.Abstractions;

namespace ContentLab.IntegrationTest.NFTCDNIO;
public class UrlGeneratorTests
{
    private readonly ITestOutputHelper _outputHelper;

    public UrlGeneratorTests(ITestOutputHelper outputHelper)
    {
        _outputHelper = outputHelper;
    }

    [Fact]
    public void GenerateSimpleImageUrl()
    {
        var urlGenerator = new UrlGenerator("preprod", "7FoxfBgV2k+RSz6UUts3/fG1edG7oIGXxdtIVCdalaI=");

        var expected = "https://asset1cpfcfxay6s73xez8srvhf0pydtd9yqs8hyfawv.preprod.nftcdn.io/image?tk=ZZ388CZwJhhLzm2djfRwaaPb8I_w7luNh5hOHJ2Ev4I&size=128";
        var assetThumbprint = "asset1cpfcfxay6s73xez8srvhf0pydtd9yqs8hyfawv";
        var url = urlGenerator.GenerateImageUrl(assetThumbprint, 128);
        Assert.Equal(expected, url);
    }

    [Fact]
    public void GenerateABunchOfUrls()
    {
        var urlGenerator = new UrlGenerator("preprod", "7FoxfBgV2k+RSz6UUts3/fG1edG7oIGXxdtIVCdalaI=");
        var assetThumbprint = "asset1cpfcfxay6s73xez8srvhf0pydtd9yqs8hyfawv";

        _outputHelper.WriteLine(urlGenerator.GenerateImageUrl(assetThumbprint, null));
        _outputHelper.WriteLine(urlGenerator.GenerateImageUrl(assetThumbprint, 32));
        _outputHelper.WriteLine(urlGenerator.GenerateImageUrl(assetThumbprint, 64));
        _outputHelper.WriteLine(urlGenerator.GenerateImageUrl(assetThumbprint, 128));
        _outputHelper.WriteLine(urlGenerator.GenerateImageUrl(assetThumbprint, 256));
        _outputHelper.WriteLine(urlGenerator.GenerateImageUrl(assetThumbprint, 512));
        _outputHelper.WriteLine(urlGenerator.GenerateImageUrl(assetThumbprint, 1024));
        _outputHelper.WriteLine(urlGenerator.GenerateMetadataUrl(assetThumbprint));
    }
}
