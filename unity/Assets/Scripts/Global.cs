using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Global
{
    public static int theme_num;
    public static int content_num;
    public static Vector3 teleportBoothPos;
    public static bool returning;

	public static DataStructures.ThemeContent content;
	public static DataStructures.CourseRun stat;

	public static String courseId = null;
	public static String themeId = null;
	public static String taskId = null;

	public static String courseName = null;
	public static String themeName = null;

	public static String fromSceneName;
}
