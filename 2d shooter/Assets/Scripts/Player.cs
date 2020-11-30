using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Нам необходима ссылка на самого себя (чечез эту ссылку мы сможем наносить урон игроку и взаимодействовать с игровым магазином)
    public static Player instance = null;


    // Необходима переменная для хранения очков жизни нашего игрока
    public int player_Health = 1;


    private void Awake()
    {
        //Настраиваем ссылку на самого себя.
        if (instance ==null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    
    public void GetDamage(int damage)//Метод получения урона игроком.
    {
        player_Health -= damage; // Уменьшаем здороье игрока на полученный урон.


        if (player_Health <= 0)// Если здоровье равно 0 уничтожаем игрока.
        {
            Destruction();
        }

    }


    void Destruction() // метод разрушения игрока.
    {
        Destroy(gameObject);
    }
}
