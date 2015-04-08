using System.Collections.Generic;

namespace ILS.Domain.GameAchievements
{
    public interface IAchievementExecutor
    {
        void Run(Dictionary<string, object> parameters); 
    }
}
