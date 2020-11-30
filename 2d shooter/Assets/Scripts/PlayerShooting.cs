using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    //Данный код необходим для отладки и в последствии будет переписан.--------

    //Переменная для хранения пули игрока.
    public GameObject obj_Bullet;


    //Задержка между выстрелами игрока. В зависимости от значения меняется скорострельность игрока.
    public float time_Bullet_Spawn = 0.3f;


    //Переменная для создания таймера.
    [HideInInspector]
    public float timer_Shoot;

    private void Update()
    {
        //Используя таймеры задержки, создаём пули игрока.
        if (Time.time > timer_Shoot)
        {
            timer_Shoot = Time.time + time_Bullet_Spawn;

            //Создаём пули
            Instantiate(obj_Bullet, transform.position, Quaternion.identity);
        }
    }
    //----------------------



}
