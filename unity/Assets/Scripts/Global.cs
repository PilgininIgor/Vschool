using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Global
{
    public static DataStructures.ThemeContent content;
	public static DataStructures.CourseRun stat;
    public static int theme_num;
    public static int content_num;
    public static Vector3 teleportBoothPos;
    public static string course_json;
	public static string theme_json;
	public static string task_json;
    public static string stats_json;
    public static bool returning;

	public static DataStructures.Course courseData = null;
	public static DataStructures.Theme themeData = null;
	public static DataStructures.ThemeContent taskData = null;

	public static String courseId = null;
	public static String themeId = null;
	public static String taskId = null;

	public static String fromSceneName;
}
