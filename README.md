# RandomOrgProxy
A simple C# client for interfacing with the Random.org API

# Why this client?

It is a very user-friendly design, that is to say it's not a verbatim implementation of the Random.Org API, but instead a configurable and simple-use-first implementation that wraps the API.

If you are not concerned about denied requests, don't want to deal with HTTP errors or time-limited throttling, this library has the option to fall back to local ```Random``` class implementations.

Basically, it's Random.Org the easy way, but also supports the hard way through some configuration changes.

# Configuration

So what is configurable?

- You can set to not use the fallback mode in cases where: no requests are left for your API key, time-limited throttling is done by Random.Org or HTTP errors occur. If you do, you will get exceptions in those cases.
- You can set whether values should generate with or without replacement.
- You can set a standard set of characters for random strings, or specify it per call.

# Usage

Check out the unit tests for full usage on every available request.

Example usage:

```
    RandomOrgClient proxy = new RandomOrgClient("your api key here");
    int[] result = proxy.GetIntegers(100, 10, 50);
```