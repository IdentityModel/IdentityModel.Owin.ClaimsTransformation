# Claims Transformation Middleware for OWIN/Katana

Nuget: IdentityModel.Owin.ClaimsTransformation

```csharp
app.UseClaimsTransformation(incoming =>
    {
        // either add claims to incoming, or create new principal
        var appPrincipal = new ClaimsPrincipal(incoming);
        incoming.Identities.First().AddClaim(new Claim("appSpecific", "some_value"));

        return Task.FromResult(appPrincipal);
    });
```
