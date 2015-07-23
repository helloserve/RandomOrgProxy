# RandomOrgProxy
A simple C# client for interfacing with the Random.org API

# Why this client?

It is a very user-friendly design, that is to say it's not a verbatim implementation of the Random.Org API, but instead a configurable and simple-use-first implementation that wraps the API.

If you are not concerned about denied requests, don't want to deal with HTTP errors or time-limited throttling, this library has the option to fall back to the limited ```Random``` class in the .NET framework.

Basically, it's Random.Org the easy way, but also supports the hard way with some configuration changes.
