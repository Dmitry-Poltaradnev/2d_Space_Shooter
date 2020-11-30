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

    private void Start()
    {
        
        //Вызываем метод OpenFire который будет вызываться в случайный промежуток времени нашего интервала.
        Invoke("OpenFire", Random.Range(shot_Time_Min, shot_Time_Max));
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
