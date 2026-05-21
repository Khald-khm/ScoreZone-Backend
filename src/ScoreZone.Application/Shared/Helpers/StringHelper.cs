namespace ScoreZone.Application.Shared.Helpers
{
    public static class StringHelper
    {
        public static bool BeValidUrl(string? url)
        {
            if (string.IsNullOrWhiteSpace(url))
                return false;

            if (!Uri.TryCreate(url, UriKind.Absolute, out var result))
                return false;

            if (result.Scheme != Uri.UriSchemeHttp && result.Scheme != Uri.UriSchemeHttps)
                return false;

            if (!result.Host.Contains("."))
                return false;

            return true;
        }
    }
}