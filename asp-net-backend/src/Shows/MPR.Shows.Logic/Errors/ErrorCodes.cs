using MPR.Shared.Logic.Errors;

namespace MPR.Shows.Logic.Errors
{
    public class ErrorCodes : SharedErrorCodes
    {
        public const string MOVIE_NOTEXISTS = "CINEMA_NOTEXISTS";
        public const string ROOM_NOTEXISTS = "ROOM_NOTEXISTS";
        public const string SHOW_NOTEXISTS = "SHOW_NOTEXISTS";
        public const string SHOW_DATETIME_OVERLAPS = "SHOW_DATETIME_OVERLAPS";
    }
}
