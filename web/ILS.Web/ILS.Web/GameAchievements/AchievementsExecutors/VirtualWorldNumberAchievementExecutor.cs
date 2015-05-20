﻿namespace ILS.Web.GameAchievements.AchievementsExecutors
 {
     using System;
     using System.Collections.Generic;
     using System.Linq;
     using System.Web;
     using Domain;
     using Domain.GameAchievements;

     public class VirtualWorldNumberAchievementExecutor : IAchievementExecutor
     {
         /// <summary>
         /// Required parameters: gameAchievementId
         /// </summary>
         public GameAchievementRun Run(User user, Dictionary<string, object> parameters)
         {
             var achievementId = new Guid(parameters[AchievementsConstants.GameAchievementIdParamName] as string);
             var context = new ILSContext();

             var gameAchievementRuns =
                 context.GameAchievementRuns.Where(x => x.User.Id == user.Id && x.GameAchievementId == achievementId);

             var additionalParameters = context.GameAchievements.Find(achievementId).AdditionalParameters;
             var necessaryNumber = 1;
             if (!String.IsNullOrEmpty(additionalParameters))
             {
                 foreach (var group in additionalParameters.Split(';'))
                 {
                     var paramNameValue = group.Split('=');
                     switch (paramNameValue[0])
                     {
                         case AchievementsConstants.NumberParamName:
                             necessaryNumber = int.Parse(paramNameValue[1]);
                             break;
                     }
                 }
             }
             if (gameAchievementRuns.Any())
             {
                 var gameAchievementRun = gameAchievementRuns.First();
                 gameAchievementRun.Result++;
                 if (gameAchievementRun.Result >= necessaryNumber)
                 {
                     gameAchievementRun.Passed = true;
                     gameAchievementRun.NeedToShow = gameAchievementRun.Result == necessaryNumber;
                 }
                 return gameAchievementRuns.First();
             }

             const int result = 1;
             var passed = result >= necessaryNumber;
             var needToShow = result == necessaryNumber;
             return context.GameAchievementRuns.Add(
                 new GameAchievementRun { UserId = user.Id, GameAchievementId = achievementId, Result = result, Passed = passed, NeedToShow = needToShow });
         }
     }
 }