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
            if (!rpgParser.RPG.facultyStands_Seen)
            {
                rpgParser.Achievement("Обнаружены стенды о факультетах!\n+10 очков!", 10);
                rpgParser.RPG.facultyStands_Seen = true;
            }
            if (!rpgParser.RPG.facultyStands_Finish)
            {
                index = System.Int32.Parse(objname.Substring(objname.Length - 2, 2)) - 1;
                facultyStands[index] = true;
                i = 0;
                while ((i < 10) && (facultyStands[i])) i++;
                if (i == 10)
                {
                    rpgParser.Achievement("Изучены все стенды о факультетах!\n+20 очков!", 20);
                    rpgParser.RPG.facultyStands_Finish = true;
                }
            }
        }

        if (objname.Substring(0, objname.Length - 1) == "Ladder")
        {
            if (!rpgParser.RPG.ladderJump_First)
            {
                rpgParser.Achievement("Прыжок на лесенку!\n+10 очков!", 10);
                rpgParser.RPG.ladderJump_First = true;
            }
            if (!rpgParser.RPG.ladderJump_All)
            {
                index = System.Int32.Parse(objname.Substring(objname.Length - 1, 1)) - 1;
                ladders[index] = true;
                i = 0;
                while ((i < 8) && (ladders[i])) i++;
                if (i == 8)
                {
                    rpgParser.Achievement("Посещены все лесенки!\n+30 очков!", 30);
                    rpgParser.RPG.ladderJump_All = true;
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
                rpgParser.Achievement("Изучена система освещения!\n+50 очков!", 50);
                rpgParser.RPG.letThereBeLight = true;
            }
        }

        if (objname == "Stand about History")
        {
            if (!rpgParser.RPG.historyStand_Seen)
            {
                rpgParser.Achievement("Обнаружен стенд об истории кафедры!\n+10 очков!", 10);
                rpgParser.RPG.historyStand_Seen = true;
            }
            if ((addInfo == "completed") && (!rpgParser.RPG.historyStand_Finish))
            {
                rpgParser.Achievement("Изучена история кафедры!\n+20 очков!", 20);
                rpgParser.RPG.historyStand_Finish = true;
            }
        }
        if (objname == "Stand about Science")
        {
            if (!rpgParser.RPG.scienceStand_Seen)
            {
                rpgParser.Achievement("Обнаружен стенд о научной работе!\n+10 очков!", 10);
                rpgParser.RPG.scienceStand_Seen = true;
            }
            if ((addInfo == "completed") && (!rpgParser.RPG.scienceStand_Finish))
            {
                rpgParser.Achievement("Изучен стенд о научной работе!\n+20 очков!", 20);
                rpgParser.RPG.scienceStand_Finish = true;
            }
        }
        if (objname == "Stand about Staff")
        {
            if (!rpgParser.RPG.staffStand_Seen)
            {
                rpgParser.Achievement("Обнаружен стенд о преподавателях!\n+10 очков!", 10);
                rpgParser.RPG.staffStand_Seen = true;
            }
            if ((addInfo == "completed") && (!rpgParser.RPG.staffStand_Finish))
            {
                rpgParser.Achievement("Изучен стенд о преподавателях!\n+20 очков!", 20);
                rpgParser.RPG.staffStand_Finish = true;
            }
        }
    }
}
