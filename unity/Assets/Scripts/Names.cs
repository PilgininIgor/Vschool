using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Names : MonoBehaviour {

	public class Scenes : MonoBehaviour {
		
		public const string Quiz = "QuizRoom";
		public const string Lecture = "LectureRoom";
		public const string Task1 = "task1Room";
		public const string Task2 = "task2Room";
		public const string Task3 = "task3Room";
		public const string Island = "Islands";
		public const string ThemeRoom = "ThemeScene";
		public const string CourseRoom = "CourseRoom";
		public const string World = "world";

	}

	public class SpawnPlaces : MonoBehaviour {

		public const string Course = "SpawnPlaceCourse";
		public const string Theme = "SpawnPlaceTheme";
		public const string Start = "SpawnPlaceStart";
		public const string Hall = "SpawnPlaceHall";
		public const string Task = "SpawnPlaceTask";
		public const string Simple = "SpawnPlace";

	}

	public class Teleports : MonoBehaviour {

		public const string ToCourse = "TeleportBooth_ToCourse";
		public const string ToTheme = "TeleportBooth_ToTheme";
		public const string ToTask = "TeleportBooth_ToTask";
		public const string ToHall = "TeleportBooth_ToHall";
		public const string Simple = "TeleportBooth";

	}
}
