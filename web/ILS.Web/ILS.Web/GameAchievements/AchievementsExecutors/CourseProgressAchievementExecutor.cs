﻿namespace ILS.Web.GameAchievements.AchievementsExecutors
 {
     using System;
     using System.Collections.Generic;
     using System.Linq;
     using System.Web.Script.Serialization;
     using Domain;
     using Domain.GameAchievements;

     public class CourseProgressAchievementExecutor : IAchievementExecutor
     {
         private readonly ILSContext context;

         public CourseProgressAchievementExecutor(ILSContext context)
         {
            this.context = context;
         }

         /// <summary>
         /// Required parameters: gameAchievementId : Guid, courseId : Guid
         /// </summary>
         public GameAchievementRun Run(User user, Dictionary<string, object> parameters)
         {
             var achievementId = (Guid)parameters[AchievementsConstants.GameAchievementIdParamName];
             var courseId = (Guid)parameters[AchievementsConstants.CourseIdParamName];

             var gameAchievement = context.GameAchievements.Find(achievementId);

             var jss = new JavaScriptSerializer();
             var achievementParameters = jss.Deserialize<dynamic>(gameAchievement.AdditionalParameters);
             var achievementParametersCourseId = achievementParameters[0][AchievementsConstants.CourseIdParamName];
             var courseProgressPercents = achievementParameters[0][AchievementsConstants.CourseProgressPercentsParamName];

             if (!courseId.Equals(achievementParametersCourseId))
             {
                 return null;
             }

             if (!context.GameAchievementRuns.Any(x => x.User.Equals(user) && x.GameAchievementId.Equals(achievementId))
                 && context.CourseRun.First(x => x.Course_Id.Equals(courseId)).Progress > courseProgressPercents)
             {
                 var addedGameAchievementRun = context.GameAchievementRuns.Add(
                     new GameAchievementRun { User = user, GameAchievement = gameAchievement, Result = 1 });
                 context.SaveChanges();
                 return addedGameAchievementRun;
             }

             return null;
         }
     }
 }