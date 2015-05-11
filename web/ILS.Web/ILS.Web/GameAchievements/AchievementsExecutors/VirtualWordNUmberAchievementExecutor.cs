﻿namespace ILS.Web.GameAchievements.AchievementsExecutors
 {
     using System;
     using System.Collections.Generic;
     using System.Linq;
     using System.Web;
     using Domain;
     using Domain.GameAchievements;

     public class VirtualWordNumberAchievementExecutor : IAchievementExecutor
     {
         /// <summary>
         /// Required parameters: gameAchievementId
         /// </summary>
         public GameAchievementRun Run(Dictionary<string, object> parameters)
         {
             Guid achievementId = new Guid(parameters[AchievementsConstants.GameAchievementIdParamName] as string);

             ILSContext context = new ILSContext();
             User user = context.User.First(x => x.Name == HttpContext.Current.User.Identity.Name);

             var gameAchievementRuns =
                 context.GameAchievementRuns.Where(x => x.User.Equals(user) && x.GameAchievementId.Equals(achievementId));

             if (gameAchievementRuns.Any())
             {
                 gameAchievementRuns.First().Result++;
                 return gameAchievementRuns.First();
             }

             return context.GameAchievementRuns.Add(
                 new GameAchievementRun { User = user, GameAchievementId = achievementId, Result = 1 });
         }
     }
 }