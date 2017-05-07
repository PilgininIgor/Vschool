using UnityEngine;

public class ReturnToCourse : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (Global.returning)
        {
            BootstrapParser bootstrapParser = GameObject.Find("Bootstrap").GetComponent<BootstrapParser>();
            bootstrapParser.CourseConstructor(Global.course_json);

            StatisticParser statisticParser = GameObject.Find("Bootstrap").GetComponent<StatisticParser>();
            //statisticParser.StatisticDisplay(Global.stats_json);

            var TeleportBoothArray = GameObject.FindGameObjectsWithTag("TeleportBooth");
            for (var i = 0; i < TeleportBoothArray.Length; i++)
            {
                if (TeleportBoothArray[i].transform.position == Global.teleportBoothPos)
                {
                    TeleportBoothArray[i].GetComponent<TeleportToSceneScript>().be_ready_to_receive = true;
                    GameObject Player = GameObject.Find("MainCamera").GetComponent<CameraFinC>().player;
                    Player.transform.position = TeleportBoothArray[i].transform.position;

                    //запоминаем угол, на который повернута будка-получатель
                    var rt = Mathf.Round(TeleportBoothArray[i].transform.localRotation.eulerAngles.y);
                    //ручками поворачиваем камеру так, чтобы она смотрела на будку
                    //GameObject.Find("MainCamera").GetComponent<OrbitCam>().x = rt + 180;
                    //поворачиваем персонажа так, чтобы он стоял лицом к выходу из будки
                    Player.transform.rotation = Quaternion.AngleAxis(rt, Player.transform.up);

                    break;
                }
            }
            Global.returning = false;
        }
    }
}
