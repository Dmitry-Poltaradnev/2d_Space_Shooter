using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    //Нам необходима ссылка на самого себя (через эту ссылку мы сможем наносить урон игроку и взаимодействовать с игровым магазином)
    public static Player instance = null;


    // Необходима переменная для хранения очков жизни нашего игрока
    public int player_Health = 1;

    //Создаём ссылку на объект щит игрока.
    public GameObject obj_Shield;
    //Создаём переменную для очков жизней щита.
    public int shield_Health = 1;

    //Создаём ссылку на ползунок жизни игрока.
    private Slider slider_hp_Player;//Данные компоненты являются UI, для работы с ними необходимо подключать (using UnityEngine.UI;) библотеку!!.
    //Создаём ссылку на ползунок жизни щита.
    private Slider slider_hp_Shield;

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

        //Находим объект с тэгом жизни игрока, и берём его слайдер компонент.
        slider_hp_Player = GameObject.FindGameObjectWithTag("sl_HP").GetComponent<Slider>();
        //Находим объект с тэгом жизни щита, и берём его слайдер компонент.
        slider_hp_Shield = GameObject.FindGameObjectWithTag("sl_Shield").GetComponent<Slider>();
    }

    private void Start()
    {
        //Устанавливаем ползунок равный жизни игрока.
        slider_hp_Player.value = (float)player_Health / 15; // Делим на 10 т.к значение ползунка от 0 до 10.          

        //Проверяем щит, если есть очки жизни делаем его видимым.
        if (shield_Health != 0)
        {
            //Показать щит.
            obj_Shield.SetActive(true);
            //Устанавливаем ползунок равный щиту игрока.
            slider_hp_Shield.value = (float)shield_Health / 6;
        }
        //При отсутствии хп, щит не появляется.
        else
        {
            obj_Shield.SetActive(false);
            slider_hp_Shield.value = 0;
        }
    }
    //Добавим метод получение урона щитом.
    public void GetDamageShield(int damage)
    {
        //Уменьшаем кол-во хп щита, при получении урона игроком.
        shield_Health -= damage;  
        //После получения урона щитом, обновляем уровень щита у ползунка.
        slider_hp_Shield.value = (float)shield_Health / 10;

        //Условие при хп = 0, щит пропадает.
        if (shield_Health <= 0)
        {
            obj_Shield.SetActive(false);
        }
    }

    public void GetDamage(int damage)//Метод получения урона игроком.
    {
        player_Health -= damage; // Уменьшаем здоровье игрока на полученный урон.
        //После уменьшения жизни у игрока, уменьшаем уровень жизни у ползунка.
        slider_hp_Player.value = (float)player_Health / 10;

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
