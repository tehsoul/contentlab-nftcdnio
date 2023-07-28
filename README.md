# ContentLab.NFTCDN.IO

This is a quick c# implementation of url generation for [NFTCDN.IO](https://nftcdn.io/)

All code is based on documentation available at [https://github.com/nftcdn/support.nftcdn.io](https://github.com/nftcdn/support.nftcdn.io)

## Quick get started

The core of the code and all you should need is at [UrlGenerator.cs](./src/ContentLab.NFTCDN.IO/UrlGenerator.cs)

The only dependency package for this code is the nuget package [Microsoft.IdentityModel.Tokens](https://www.nuget.org/packages/Microsoft.IdentityModel.Tokens) because we need the Base64UrlEncoder.

Sample:
```csharp
    [Fact]
    public void GenerateSimpleImageUrl()
    {
        var urlGenerator = new UrlGenerator("preprod", "7FoxfBgV2k+RSz6UUts3/fG1edG7oIGXxdtIVCdalaI=");

        var expected = "https://asset1cpfcfxay6s73xez8srvhf0pydtd9yqs8hyfawv.preprod.nftcdn.io/image?tk=ZZ388CZwJhhLzm2djfRwaaPb8I_w7luNh5hOHJ2Ev4I&size=128";
        var assetThumbprint = "asset1cpfcfxay6s73xez8srvhf0pydtd9yqs8hyfawv";
        var url = urlGenerator.GenerateImageUrl(assetThumbprint, 128);
        Assert.Equal(expected, url);
    }

```

Ideally you would instantiate an UrlGenerator once during your app's bootstrapping procedures and register it as a singleton in your DI, then just inject wherever needed.

There is some other helper code in the sourcecode that relies on other packages like [SauceControl.Blake2Fast](https://www.nuget.org/packages/SauceControl.Blake2Fast) in the context of constructing a CIP14 compliant assset thumbprint from a given policyid+assetname, but that's not needed if you already have the asset thumbprints in your code.

*Disclaimer: use at your own risk but feel free to leave feedback - at this point this is only verified to be correct based on the information available in the documentation for PREPROD. I have not yet tested this on mainnet due to lack of a key for now.*