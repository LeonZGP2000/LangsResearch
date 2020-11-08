using SDating.Models;

namespace SDating.Controllers
{
    public interface IMatchAnalyser
    {
        SessionResult GetMatchingResult(DatingSession input);
    }
}
