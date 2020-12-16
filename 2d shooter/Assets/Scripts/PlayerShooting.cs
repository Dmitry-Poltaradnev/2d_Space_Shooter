using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Guns// Cоздаём новый класс, который отвечает за вооружение игрока.
{
    public GameObject obj_Central_Gun, obj_Right_Gun, obj_Left_Gun;// Добавляем в данный класс 3 переменные для оружия.
}

public class PlayerShooting : MonoBehaviour
{
    // Создаём ссылку на данный скрипт.
    public static PlayerShooting instance;

    // Создаём ссылку на созданный выше класс Guns
    public Guns guns;

    // Создаём переменную в которой задаём режим стрельбы.
    [HideInInspector]
    public int max_Power_Level_Guns = 5;

    // Создадим переменную для хранения пуль
    public GameObject obj_Bullet;

    // Добавим переменну для задержки между высрелами игрока.
    public float timer_Bullet_Spawn = 0.3f;

    // Добавим переменную для создания таймера.
    [HideInInspector]
    public float timer_Shoot;

    // Добавим переменую для текущего режима стрельбы.
    [Range(1, 5)]//сделаем его через ползунок с ограничение от 1 до 5.
    public int cur_Power_Level_Guns = 1;

    private void Awake()
    {
        //Настраиваем ссылку.
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        //Используя таймер запускаем метод MakeAShot
        if (Time.time > timer_Shoot)
        {
            timer_Shoot = Time.time + timer_Bullet_Spawn;

            //Вызываем метод
            MakeAShot();
        }
    }
    //Добавим метод создания пуль, он принимает 3 параметра : 1)Префаб пули, 2)Точку выстрела, 3)Наклон.
    private void CreateBullet(GameObject bullet, Vector3 position_Bullet, Vector3 rotation_Bullet)
    {
        Instantiate(bullet, position_Bullet, Quaternion.Euler(rotation_Bullet));
    }

    //Создадим метод MakeAShot
    //Данный метод будет создавать выбранный режим стрельбы.

    private void MakeAShot()
    {
        switch (cur_Power_Level_Guns)
        {
            //Первый режим стрельбы.
            case 1:
                CreateBullet(obj_Bullet, guns.obj_Central_Gun.transform.position, Vector3.zero);
                break;

            //Второй режим стрельбы.
            case 2:
                CreateBullet(obj_Bullet, guns.obj_Right_Gun.transform.position, Vector3.zero);
                CreateBullet(obj_Bullet, guns.obj_Left_Gun.transform.position, Vector3.zero);
                break;

            //Третий режим стрельбы. Стрельба из центра + боковые пушки под углом.
            case 3:
                CreateBullet(obj_Bullet, guns.obj_Central_Gun.transform.position, Vector3.zero);
                CreateBullet(obj_Bullet, guns.obj_Right_Gun.transform.position, new Vector3(0, 0, -5));
                CreateBullet(obj_Bullet, guns.obj_Left_Gun.transform.position, new Vector3(0, 0, 5));
                break;

            //Четвёртый режим стрельбы. Сразу 5 пуль, центр и боковые пушки под углом.
            case 4:
                CreateBullet(obj_Bullet, guns.obj_Central_Gun.transform.position, Vector3.zero);
                CreateBullet(obj_Bullet, guns.obj_Right_Gun.transform.position, new Vector3(0, 0, 0));
                CreateBullet(obj_Bullet, guns.obj_Right_Gun.transform.position, new Vector3(0, 0, -5));
                CreateBullet(obj_Bullet, guns.obj_Left_Gun.transform.position, new Vector3(0, 0, 0));
                CreateBullet(obj_Bullet, guns.obj_Left_Gun.transform.position, new Vector3(0, 0, 5));
                break;

            // Пятый режим стрельбы, 5 пуль режим веера.
            case 5:
                CreateBullet(obj_Bullet, guns.obj_Central_Gun.transform.position, Vector3.zero);
                CreateBullet(obj_Bullet, guns.obj_Right_Gun.transform.position, new Vector3(0, 0, -5));
                CreateBullet(obj_Bullet, guns.obj_Right_Gun.transform.position, new Vector3(0, 0, -15));
                CreateBullet(obj_Bullet, guns.obj_Left_Gun.transform.position, new Vector3(0, 0, 5));
                CreateBullet(obj_Bullet, guns.obj_Left_Gun.transform.position, new Vector3(0, 0, 15));
                break;
        }
    }

}
