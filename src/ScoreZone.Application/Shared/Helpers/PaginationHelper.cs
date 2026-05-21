namespace ScoreZone.Application.Shared.Helpers
{
    public static class PaginationHelper
    {
        
        public static void Normalize(ref int pageNumber, ref int pageSize)
        {
            if(pageNumber < 1) pageNumber = 1;
            if(pageSize < 1) pageSize = 10;
            if(pageSize > 100) pageSize = 100;
        }

        public static int Skip(int pageNumber, int pageSize)
            => (pageNumber - 1) * pageSize;

        // Use it like this e.g.

        // var PageNumber = pageNumber;
        // var PageSize = pageSize;
        // PaginationHelper.Normalize(ref pageNumber,ref pageSize);
        // var skip = PaginationHelper.Skip(pageNumber, pageSize);

        // .Select(.......)
        // .OrderByDescending(x => x.score)
        // .Skip(skip)
        // .Take(pageSize)
        // .ToList();

    }
}