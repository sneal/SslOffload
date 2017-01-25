#SSL Offload Demo

Sample ASP.NET project to demo Request.Url working correctly behind a load balancer with SSL offload. Request.Url is often used in legacy apps to create links, so when old apps are moved behind a load balancer they often have lots of broken links. There are a few ways to handle this.

1. ASP.NET URL Rewrite module. This is probably the most common solution, however it doesn't work in PCF or other shared hosting providers.
2. Update the code base to generate the proper links. Depending on the app this may be very time consuming and potentially error prone.
3. Update a couple of config settings to force ASP.NET to generate the correct links, as demo'd here.

## Details

There are two changes that we need to make to allow the Request.Url property to have the correct external facing host name. First and most importantly we need to set the `aspnet:UseHostHeaderForRequestUrl` app settings to `true`. This causes the HttpRequest object to use a different code path. This alternate code path uses the HOST header to generate the URL. The most obvious benefit of this is that the URL now has the public facing hostname and port. If you don't use SSL this all you need to do, if you do use SSL offload as most applications these days, continue on.

The HttpRequest and the underlying HttpWorkerRequest determine whether to generate a secure https URL based off the `HTTPS` server variable. This server variable is automatically set by the runtime, but we can override this server variable at the beginning of the request before any pages are processed. By forcing the HTTPS server variable to `on` we thus force the HttpWorkerRequest to generate https URLs.
