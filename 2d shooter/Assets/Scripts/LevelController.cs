using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]/* Добавим новый класс через который будем взаимодействовать с волной. Делаем его Serializable 
                      для того чтобы его поля отображались в инспекторе.*/
public class EnemyWaves
{
    //Добавим время через которое мы будем создавать волну
    public float TimeToStart;

    //Создаём переменную для хранения волны.
    public GameObject wave;

    //Добавляем bool переменную отвечающую за конец игры. if is_Last_Wave  = true игра заканчивается.
    public bool is_Last_Wave;
}                       


public class LevelController : MonoBehaviour
{
    // Cоздаём ссылку на самого себя.
    public static LevelController instance;

    //Добавляем массив в котором будут храниться все корабли игрока.
    public GameObject[] playerShip;

    //Также создаём массив для вражеских волн.
    public EnemyWaves[] enemyWaves;

    //Добавим bool переменную которая будет вызывать конец игры.
    private bool is_Final = false;

    private void Awake()
    {
        //Настраиваем ссылку на самого себя.
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        //Создаём вражеские волны через цикл.
        for (int i = 0; i < enemyWaves.Length; i++)
        {
            //В данном цикле запускаем со программу, которая принемает 2 значения: 1-e когда появится волна, 2-е какой тип волны будет создан.
            StartCoroutine(CreateEnemyWave(enemyWaves[i].TimeToStart, enemyWaves[i].wave, enemyWaves[i].is_Last_Wave));
        }
    }


    private void Update()
    {
        //При апдейте проеверяем победили, или проиграли: if bool = true && != объектов c тегом Enemy(победа).
        if (is_Final == true && GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            Debug.Log("Win");
        }
        else if  (Player.instance == null)
        {
            Debug.Log("Lose");
        }
    }

    IEnumerator CreateEnemyWave(float delay, GameObject Wave, bool Final)// Пишем условие для со программы 
    {
        //Делаем условие, если время создания волны != 0 , то делаем задержку на то время которое мы задаем для данной волны.
        if (delay != 0)
        {
            yield return new WaitForSeconds(delay);
        }
        if (Player.instance != null)
        {
            Instantiate(Wave);
        }

        //Добавим условие,если текущая волна будет финальной, передаём true в bool переменную которая отвечает за конец игры.
        if (Final == true)
        {
            is_Final = true;
        }
        
    }

}
