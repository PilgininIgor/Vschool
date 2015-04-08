using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ILS.Domain;
using System.Data.Entity;

namespace ILS.Web.DataExtensions
{
	public static class DataExtensions
	{
		/*
        public static Test MapTest(this HttpRequestBase rq, ILSContext context)
		{
			var form = rq.Form;
			//we have form
			if (form.AllKeys.Contains("id"))
			{
				var testId = Guid.Parse(form["id"]);
				var test = context.Test.FindOrCreate(testId);
				test.Name = form["name"];
				test.RandomizeQuestions = form.AllKeys.Contains("trnd") && bool.Parse(form["trnd"]);

				//we have questions
				var qNum = 0;
				while (form.AllKeys.Contains("type" + (++qNum)))
				{
					if (form["type" + qNum] == "text")
					{
						var qid = Guid.NewGuid();
						if (!String.IsNullOrEmpty(form["qid" + qNum]))
							Guid.TryParse(form["qid" + qNum], out qid);
						var q = context.ClosedQuestion.FindOrCreate(qid);
						q.Id = qid;
						q.Prize = 1;
						q.Punishment = 1;
						q.Text = form["question" + qNum];
						q.RandomizeAnswers = form.AllKeys.Contains("qrnd"+qNum) && bool.Parse(form["qrnd"+qNum]);
						for (int i = 1; i <= 5; i++)
						{
							var aid = Guid.NewGuid();
							if (!String.IsNullOrEmpty(form["aid" + qNum + i]))
								Guid.TryParse(form["aid" + qNum + i], out aid);
							var a = context.Answer.FindOrCreate(aid);
							a.Id = aid;
							a.Text = form["answer" + qNum + i];
							a.Weight = form.AllKeys.Contains("correct" + qNum+i) && bool.Parse(form["correct" + qNum+i]) ? 1 : 0;
							q.Answers.CheckAndAdd(a);
						}

						test.Questions.CheckAndAdd(q);
					}
				}
				return test;
			}
			return null;
		}
         * */

		public static T FindOrCreate<T>(this DbSet<T> dbSet, object primaryKey)
			where T : class, new()
		{
			var entry = dbSet.Find(primaryKey);
			if (entry == null)
				entry = dbSet.Add(new T());

			return entry;
		}

		public static void CheckAndAdd<T>(this ICollection<T> target, T value)
			where T : ILS.Domain.EntityBase
		{
			if (!target.Any(x => x.Id == value.Id))
				target.Add(value);
		}
	}
}