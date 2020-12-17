using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //При столкновении с игроком
        if (collision.tag == "Player")
        {
            //Проверяем режим стрельбы, если он не максимален.
            if (PlayerShooting.instance.cur_Power_Level_Guns < PlayerShooting.instance.max_Power_Level_Guns)
            {
                //Если он не максимален, мы меняем режим стрельбы.
                PlayerShooting.instance.cur_Power_Level_Guns++;
            }
            // И впоследствии уничтожаем наш бонус.
            Destroy(gameObject);
        }
    }
}
