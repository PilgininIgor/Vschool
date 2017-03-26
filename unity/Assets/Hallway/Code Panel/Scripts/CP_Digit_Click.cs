using UnityEngine;

public class CP_Digit_Click : MonoBehaviour
{

    //эти параметры задаются в инспекторе для каждого из объектов, к которым прикреплен скрипт
    string child_to_glow = "";
    int digit = 0;

    //этот параметр определяет длину кода и задается один раз прямо здесь
    //(хотя в инспекторе он, конечно, тоже появляется, но менять отдельно для объекта не стоит)
    int code_length = 5;

    void OnMouseDown()
    {
        //по умолчанию "выключение" скрипта выключает только функцию Update,
        //поэтому надо добавить такое условие
        if (this.enabled)
        {
            //пара телодвижений по иерархии, которую считаем известной
            //скрипт висит на одной из групп кнопок (B<номер> Group)
            //родитель - это общая группа кодовой панели Code Panel
            //среди детей родителя ищем группу Field Group, среди ее детей - текст Field Text

            //через поиск по имени среди всех объектов сцены GameObject.Find(<имя>) было бы короче,
            //но раз уж мы делаем панель универсальной и добавляемой к любой сцене,
            //вдруг в какой-то из этих сцен окажется свой объект с именем Field Text?

            var txt = transform.parent.transform.Find("Field Group").transform.Find("Field Text");
            if (txt.GetComponent<TextMesh>().text.Length >= code_length)
            {
                transform.Find(child_to_glow).GetComponent<Animation>().Play("CP ButtonError");
            }
            else
            {
                transform.Find(child_to_glow).GetComponent<Animation>().Play("CP ButtonLight");
                txt.GetComponent<TextMesh>().text += digit;
            }
        }
    }
}
