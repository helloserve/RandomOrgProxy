# RandomOrgProxy
A simple .NET Standard 1.4 client for interfacing with the Random.org API

# Why this client?
A configurable and simple-use-first implementation that wraps the Random.org API.

If you are not concerned about denied requests and don't want to deal with HTTP errors or time-limited throttling, this library has the option to fall back to local ```System.Random``` class implementations.

# Dependencies
This package has no dependencies apart from the standard Microsoft abstraction packages.

# License
Apache License 2.0 - https://www.apache.org/licenses/LICENSE-2.0

# Configuration

So what is configurable?

- You can set to not use the fallback mode in cases where: no requests are left for your API key, time-limited throttling is done by Random.Org or HTTP errors occur. If you do, you will get exceptions in those cases.
- You can set whether values should generate with or without replacement (optional parameter on Random.org).
- You can set a standard set of characters for random strings, or specify it per call.

# Usage

Essential service configuration:
```
services.AddRandomOrg("<your API key>");
```

Extended service configuration:
```
services.AddRandomOrg(options =>
{
    options.ApiKey = "<your API key>";
    options.WithReplacement = false;
    options.ShouldFallback = false;
});
```


Example usage:

```
private IRandomOrgClient _randomOrgClient;

public Foo(IRandomOrgClient randomOrgClient)
{
    _randomOrgClient = randomOrgClient;
}
    
public async Task Process()
{
    int[] integers = await _randomOrgClient.GetIntegersAsync(100, 10, 50);
    strings[] strings = await _randomOrgClient.GetStringsAsync(100, 10);
}
```
