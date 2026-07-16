namespace ScoreZone.Application.FootballCourt.DTOs
{
    public record LocationCoordsRequest
    (
        decimal locationLat, 
        decimal locationLng
    );
}