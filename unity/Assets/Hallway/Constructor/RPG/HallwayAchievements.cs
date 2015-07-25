using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HallwayAchievements : MonoBehaviour
{
    private const int facultyStandsSize = 10, laddersSize = 8;
    public GameObject Bootstrap;
    public bool[] facultyStands = new bool[facultyStandsSize], ladders = new bool[laddersSize];
    public bool light1 = false, light2 = false, light3 = false;
    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < facultyStandsSize; i++) facultyStands[i] = false;
        for (int i = 0; i < laddersSize; i++) ladders[i] = false;
    }

    public void Check(string objname, string addInfo)
    {
        RPGParser rpgParser = Bootstrap.GetComponent<RPGParser>();
        int i, index;

        if (objname.Substring(0, objname.Length - 2) == "WallScreen")
        {
            rpgParser.SaveAchievemnt(DataStructures.AchievementTrigger.Stend, null);
        }

        if (objname.Substring(0, objname.Length - 1) == "Ladder")
        {
            if (!rpgParser.RPG.ladderJump_First)
            {
//                rpgParser.Achievement("Прыжок на лесенку!\n+10 очков!", 10);
//                rpgParser.RPG.ladderJump_First = true;
            }
            if (!rpgParser.RPG.ladderJump_All)
            {
                index = System.Int32.Parse(objname.Substring(objname.Length - 1, 1)) - 1;
                ladders[index] = true;
                i = 0;
                while ((i < 8) && (ladders[i])) i++;
                if (i == 8)
                {
//                    rpgParser.Achievement("Посещены все лесенки!\n+30 очков!", 30);
//                    rpgParser.RPG.ladderJump_All = true;
                }
            }
        }

        if ((!rpgParser.RPG.letThereBeLight) && ((objname == "Light1") || (objname == "Light2") || (objname == "Light3")))
        {
            if (objname == "Light1") light1 = true;
            else if (objname == "Light2") light2 = true;
            else if (objname == "Light3") light3 = true;
            if (light1 && light2 && light3)
            {
//                rpgParser.Achievement("Изучена система освещения!\n+50 очков!", 50);
//                rpgParser.RPG.letThereBeLight = true;
            }
        }

        if (objname == "Stand about History")
        {
            rpgParser.SaveAchievemnt(DataStructures.AchievementTrigger.Stend, null);
        }
        if (objname == "Stand about Science")
        {
            rpgParser.SaveAchievemnt(DataStructures.AchievementTrigger.Stend, null);
        }
        if (objname == "Stand about Staff")
        {
            rpgParser.SaveAchievemnt(DataStructures.AchievementTrigger.Stend, null);
        }
    }
}
