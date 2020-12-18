using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemy : MonoBehaviour
{
    // Переменная для хранения жизни врага
    public int enemy_Health; 


    [Space]// Добавляем переменную для пули врага.
    public GameObject obj_Bullet;
    //Создаём интервал времени когда будет стрелять враг(делается для того чтоб он не стрелял в тот момент когда его не видит игрок)
    public float shot_Time_Min, shot_Time_Max;
    //Также добавим шанс выстрела что-бы не все враги производили выстрелы
    public int shot_Chance;

    [Header("Boss")]
    //Добавим логическую переменную if true(это босс), if false(обычный враг).
    public bool is_Boss;
    //Добавим переменную для ультимы босса.
    public GameObject obj_Bullet_Boss;
    //Добавим кулдаун между выстрелами босса.
    public float time_Bullet_Boss_Spawn;
    //Добавим переменную для создания таймера.
    private float timer_Shot_Boss;
    //Добавим переменную для шанса выстрела (для настройки силы босса).
    public int shot_Chance_Boss;

    private void Start()
    {
        //Добавим условия если данный враг не является боссом, делаем 1 выстрел и всё.
        if (!is_Boss)
        {
            //Вызываем метод OpenFire который будет вызываться в случайный промежуток времени нашего интервала.
            Invoke("OpenFire", Random.Range(shot_Time_Min, shot_Time_Max));
        }      
        
    }

    private void Update()
    {
        //Добавим условия, если данный враг является боссом, то используя таймер он будет использовать 2 метода стрельбы.
        if (is_Boss)
        {
            if (Time.time > timer_Shot_Boss)
            {
                timer_Shot_Boss = Time.time + time_Bullet_Boss_Spawn;
                OpenFire();
                OpenFireBoss();
            }
        }
    }

    //Добавим метод OpenFireBoss, он позволит стрелять веером.
    private void OpenFireBoss()
    {
        //Добавляем условие на шанс выстрела.
        if (Random.value < (float)shot_Chance_Boss / 100)
        {
            //Если мы можем сделать выстрел, используя цикл создаём выстрел. Цикл нужен для создания множества пуль меняя им угол по оси z.
            for (int zZz = -40; zZz < 40; zZz += 10)
            {
                Instantiate(obj_Bullet_Boss, transform.position, Quaternion.Euler(0, 0, zZz));
            }
        }
    }

    //Прописываем метод OpenFire
    private void OpenFire()
    {
        //Создаём условие в котором проверяем шанс выстрела
        if (Random.value <(float)shot_Chance / 100)
        {
            //Если мы можем сделать выстрел создаём пулю из позиции врага без вращения
            Instantiate(obj_Bullet, transform.position, Quaternion.identity);
        }
    }




    //Метод для получения урона от врага.
    public void GetDamage(int damage)
    {
        enemy_Health -= damage;// уменьшаем очки жизни на кол-во полученного урона.

        //если у врага нет очков здоровья, то мы вызываем метод разрушения врага.
        if (enemy_Health <= 0)
        {
            Destruction(); 
        }
    }

    public void Destruction()// метод разрушения врага. При его вызове объект уничтожается.
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D coll)  //Метод при столкновении врага с игроком.
    {
        if (coll.tag == "Player")// Если объект столкнулся с объектом у которого есть тэг Player, то враг получает 1d.
        {
            GetDamage(1);
            Player.instance.GetDamage(1); // Через ссылку созданную в классе Player передаём метод нанесения 1 damage игроку.
        }
    } 

}
