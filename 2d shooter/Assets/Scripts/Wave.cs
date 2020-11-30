using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Пишем новый класс через который будем настраивать стрельбу в инспекторе Serializable чтобы поля отображались в инспекторе.
[System.Serializable]
public class ShottingSettings
{
    //Переменная для настройки шанса выстрела врага в волне, эту переменную мы сделаем используя ползунок от 0-100%
    [Range(0, 100)]
    public int shot_Chance;

    //Создаём интервал внутри которого будет происходить выстрел
    public float shot_Timer_Min, shot_Timer_Max;
}



public class Wave : MonoBehaviour
{
    //Создаём ссылку на класс для взаимодействия  ShottingSettings 
    public ShottingSettings shottingSettings;

    [Space]


    //Создаём переменную для хранения врага которого мы будем генерировать в конкретной волне.
    public GameObject obj_Enemy;

    //Кол-во врагов в волне.
    public int count_in_Wave;

    //Скорость с которой будет двигаться враг в волне.
    public float speed_Enemy;

    //Задержка между генерацией врагов в волне
    public float time_Spawn;

    //Массив для хранения точек по которому будет двигаться волна
    public Transform[] path_Points;

    //Логическая переменная суть которой( когда враг будет в последней точке пути, он будет либо уничтожен , либо начнёт путь с первой точки пути)
    public bool is_return;

    //Логическая переменная для теста если её значение true, то мы будем генерировать волну каждые 5 сек.
    [Header("Test wave!")]
    public bool is_Test_Wave;


    //Через данную переменную будем передавать данные врагу
    private FollowThePath follow_Component;


    //Через данную ссылку передаём данные врагу
    private MoveEnemy enemy_Component_Script;

    private void Start()
    {
        //Запускаем программу которая будет генерировать волну.
        StartCoroutine(CreateEnemyWave());
    }

    IEnumerator CreateEnemyWave()
    {
        //Создаём цикл который зависит от кол-ва врагов в волне.
        for (int i = 0; i < count_in_Wave; i++)
        {
            // Создаём врага
            GameObject new_enemy = Instantiate(obj_Enemy, obj_Enemy.transform.position, Quaternion.identity);


            //Получаем компонент FollowThePath который висит на созданном враге.
            follow_Component = new_enemy.GetComponent<FollowThePath>();

            // Через ссылку передадим точки пути по которым должен перемещаться враг
            follow_Component.path_Points = path_Points;

            //Через ссылку передадим скорость с которой должен перемещаться враг
            follow_Component.speed_Enemy = speed_Enemy;

            //Через ссылку передадим значение логической переменной. Если true - движемся бесконечно, если false - уничтожаем врага в конце пути.
            follow_Component.is_Return = is_return;

            //Получим ссылку на компонент MoveEnemy который висит на созданном враге.
            enemy_Component_Script = new_enemy.GetComponent<MoveEnemy>();

            //Через ссылку передадим врагу шанс выстрела.
            enemy_Component_Script.shot_Chance = shottingSettings.shot_Chance;

            //Через ссылку передадим врагу интервал через который будет происходить выстрел
            enemy_Component_Script.shot_Time_Min = shottingSettings.shot_Timer_Min;
            enemy_Component_Script.shot_Time_Max = shottingSettings.shot_Timer_Max;

            new_enemy.SetActive(true);

            //Делаем задержку после которой продолжаем выполнения кода и создаём нового врага
            yield return new WaitForSeconds(time_Spawn);
        }
        // Cоздаём условия( если логическая переменная для теста имеет значение true ждём 5 секунд и запускаем волну)
        if (is_Test_Wave)
        {
            yield return new WaitForSeconds(5f);
            StartCoroutine(CreateEnemyWave());
        }

        //Если значение false уничтожаем врага в конце пути.
        if (!is_Test_Wave)
        {
            Destroy(gameObject);
        }
    }
    //Соеденим линиями точки по которым двигается волна(даёт возможность визуализации пути и его последующей настройки)
    void OnDrawGizmos()
    {
        NewPositionByPath(path_Points);
    }
    void NewPositionByPath(Transform[] path)
    {
        Vector3[] path_Positions = new Vector3[path.Length];
        for (int i = 0; i < path.Length; i++)
        {
            path_Positions[i] = path[i].position;
        }
        path_Positions = Smoothing(path_Positions);// Данная строка отвечает за дополнительные точки для сглаживания пути Smoothing.
        path_Positions = Smoothing(path_Positions);
        path_Positions = Smoothing(path_Positions);
        for (int i = 0; i < path_Positions.Length - 1; i++)
        {
            Gizmos.DrawLine(path_Positions[i], path_Positions[i +1]);
        }

        
    }
    //Весь данный блок Smoothing для сглаживания пути перемещения.
    Vector3[] Smoothing(Vector3[] path_Positions)
    {
        Vector3[] new_Path_Positions = new Vector3[(path_Positions.Length - 2) * 2 + 2];
        new_Path_Positions[0] = path_Positions[0];
        new_Path_Positions[new_Path_Positions.Length - 1] = path_Positions[path_Positions.Length - 1];

        int j = 1;
        for (int i = 0; i < path_Positions.Length - 2; i++)
        {
            new_Path_Positions[j] = path_Positions[i] + (path_Positions[i + 1] - path_Positions[i]) * 0.75f;
            new_Path_Positions[j + 1] = path_Positions[i + 1] + (path_Positions[i + 2] - path_Positions[i + 1]) * 0.25f;
            j += 2;
        }
        return new_Path_Positions;
    }
}
