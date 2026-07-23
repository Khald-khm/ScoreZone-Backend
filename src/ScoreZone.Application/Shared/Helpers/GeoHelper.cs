namespace ScoreZone.Application.Shared.Helpers;

public static class GeoHelper
{
    private const double EarthRadiusKm = 6371;

    public static double CalculateDistance(
        double lat1,
        double lng1,
        double lat2,
        double lng2)
    {
        double dLat = DegreesToRadians(lat2 - lat1);
        double dlng = DegreesToRadians(lng2 - lng1);

        lat1 = DegreesToRadians(lat1);
        lat2 = DegreesToRadians(lat2);

        double a =
            Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
            Math.Cos(lat1) * Math.Cos(lat2) *
            Math.Sin(dlng / 2) * Math.Sin(dlng / 2);

        double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

        return EarthRadiusKm * c;
    }

    private static double DegreesToRadians(double degrees)
    {
        return degrees * Math.PI / 180;
    }
}