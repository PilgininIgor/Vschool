﻿namespace ILS.Web.GameAchievements.AchievementsExecutors
 {
     using System;
     using System.Collections.Generic;
     using System.Linq;
     using Domain;
     using Domain.GameAchievements;

     public class VirtualWorldNumberAchievementExecutor : IAchievementExecutor
     {
         private readonly ILSContext context;

         public VirtualWorldNumberAchievementExecutor(ILSContext context)
         {
             this.context = context;
         }

         /// <summary>
         /// Required parameters: gameAchievementId : Guid
         /// </summary>
         public GameAchievementRun Run(User user, Dictionary<string, object> parameters)
         {
             var achievementId = (Guid)parameters[AchievementsConstants.GameAchievementIdParamName];

             var gameAchievementRuns =
                 context.GameAchievementRuns.Where(x => x.User.Id == user.Id && x.GameAchievementId == achievementId);

             var gameAchievemnt = context.GameAchievements.Find(achievementId);
             var additionalParameters = gameAchievemnt.AdditionalParameters;
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
                 context.SaveChanges();
                 return gameAchievementRuns.First();
             }

             const int result = 1;
             var passed = result >= necessaryNumber;
             var needToShow = result == necessaryNumber;
             var addedGameAchievementRun = context.GameAchievementRuns.Add(
                 new GameAchievementRun { User = context.User.Find(user.Id), GameAchievement = gameAchievemnt, Result = result, Passed = passed, NeedToShow = needToShow });
             context.SaveChanges();
             return addedGameAchievementRun;
         }
     }
 }