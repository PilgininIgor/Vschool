﻿namespace ILS.Web.GameAchievements.AchievementsExecutors
 {
     using System;
     using System.Collections.Generic;
     using System.Linq;
     using System.Web;
     using System.Web.Script.Serialization;
     using Domain;
     using Domain.GameAchievements;

     public class CourseProgressAchievementExecutor : IAchievementExecutor
     {
         /// <summary>
         /// Required parameters: gameAchievementId, courseId
         /// </summary>
         public GameAchievementRun Run(Dictionary<string, object> parameters)
         {
             Guid achievementId = new Guid(parameters[AchievementsConstants.GameAchievementIdParamName] as string);
             Guid courseId = new Guid(parameters[AchievementsConstants.CourseIdParamName] as string);

             ILSContext context = new ILSContext();
             GameAchievement gameAchievement = context.GameAchievements.Find(achievementId);

             JavaScriptSerializer jss = new JavaScriptSerializer();
             var achievementParameters = jss.Deserialize<dynamic>(gameAchievement.AdditionalParameters);
             var achievementParametersCourseId = achievementParameters[0][AchievementsConstants.CourseIdParamName];
             var courseProgressPercents = achievementParameters[0][AchievementsConstants.CourseProgressPercentsParamName];

             if (!courseId.Equals(achievementParametersCourseId))
             {
                 return null;
             }

             User user = context.User.First(x => x.Name == HttpContext.Current.User.Identity.Name);
             if (!context.GameAchievementRuns.Any(x => x.User.Equals(user) && x.GameAchievementId.Equals(achievementId))
                 && context.CourseRun.First(x => x.Course_Id.Equals(courseId)).Progress > courseProgressPercents)
             {
                 return context.GameAchievementRuns.Add(
                     new GameAchievementRun { User = user, GameAchievementId = achievementId, Result = 1 });
             }

             return null;
         }
     }
 }