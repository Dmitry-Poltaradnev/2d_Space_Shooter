using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetsAndBonus : MonoBehaviour
{
    // создаём массив с предметами для генерации.
    public GameObject[] obj_Planets;

    // также создаём задержку между генерацией планет.
    public float time_Planet_Spawn;

    //Переменная скорости перемещенния планет.
    public float speed_Planets;

    // Создаём список который запрещает дублирование планет подряд
    List<GameObject> planetsList = new List<GameObject>();

    private void Start()
    {
        //Запускаем генерацию планет
        StartCoroutine(PlanetsCreation());
    }

    IEnumerator PlanetsCreation()// добавляем планеты в список используя цикл.
    {
        for (int i = 0; i < obj_Planets.Length; i++)
        {
            planetsList.Add(obj_Planets[i]);
        }
        yield return new WaitForSeconds(7); // После заполнения списка ждём 7 секунд и запускаем выполнение кода.


        // Создаём планеты в бесконечном цикле
        while (true)
        {
            //Выбираем случайную планету из списка
            int RandomIndex = Random.Range(0, planetsList.Count);


            // Создаём её + должна учитываться ширина экрана + высота экрана.+ случайное направление под углом.
            GameObject newPlanet = Instantiate(planetsList[RandomIndex],
                new Vector2(Random.Range(MovePlayer.instanse.borders.minX, MovePlayer.instanse.borders.maxX),
                MovePlayer.instanse.borders.maxY * 1.5f),
                Quaternion.Euler(0, 0, Random.Range(-25, 25)));



            //После создания планеты мы удаляем её из списка что-бы она не дублировалась несколько раз.
            planetsList.RemoveAt(RandomIndex);

            //Добавляем условие если список стал пустым, заполняем его заново.
            if (planetsList.Count == 0)
            {
                for (int i = 0; i < obj_Planets.Length; i++)
                {
                    planetsList.Add(obj_Planets[i]);
                }
            }

            // У созданной планеты мы находим компонент objMoving, и задаём скорость с которой она будет двигаться.
            newPlanet.GetComponent<ObjMoving>().speed = speed_Planets;

            //Добавляем паузу после которой код повторяется и добавляется новая планета.
            yield return new WaitForSeconds(time_Planet_Spawn);

        }

    }
}
