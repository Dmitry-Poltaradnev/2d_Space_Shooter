using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Нам необходима ссылка на самого себя (через эту ссылку мы сможем наносить урон игроку и взаимодействовать с игровым магазином)
    public static Player instance = null;


    // Необходима переменная для хранения очков жизни нашего игрока
    public int player_Health = 1;

    //Создаём ссылку на объект щит игрока.
    public GameObject obj_Shield;
    //Cоздаём переменную для очков жизней щита.
    public int shield_Health = 1;

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
        //Проверяем щит, если есть очки жизни делаем его видимым.
        if (shield_Health != 0)
        {
            //Показать щит.
            obj_Shield.SetActive(true);
        }
        //При отсутствии хп, щит не появляется.
        else
        {
            obj_Shield.SetActive(false);
        }
    }
    //Добавим метод получение урона щитом.
    public void GetDamageShield(int damage)
    {
        //Уменьшаем кол-во хп щита, при получении урона игроком.
        shield_Health -= damage;

        //Условие при хп = 0, щит пропадает.
        if (shield_Health <= 0)
        {
            obj_Shield.SetActive(false);
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
