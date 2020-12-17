using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //Переменная через которую можно регулировать силу наносимого урона от пули
    public int damage;


    //Логическая переменная, через неё определяем чья пуля игрока либо врага. тк скрипт универсальный
    public bool is_Enemy_Bullet;// Если значение верно, то игрок получает урон, если false то враг.



    //Метод разрушения пули, при его вызове мы её разрушаем.
    private void Destruction()
    {
        Destroy(gameObject);
    }


    //Логика при столкновении пули с игроком либо врагом
    private void OnTriggerEnter2D(Collider2D coll)
    {
        //Если пуля принадлежит врагу ,  и сталкивается с игроком , то она наносит урон игроку.
        if (is_Enemy_Bullet && coll.tag == "Player")
        {
            Player.instance.GetDamage(damage);


            // После столкновения мы вызываем метод разрушения пули
            Destruction();
        }
        //Если пуля принадлежит игроку и сталкивается с врагом, то мы через Getcomponent находим компонент Enemy и вызываем в нём метод повреждения врага.
        else if (!is_Enemy_Bullet && coll.tag == "Enemy")
        {
            coll.GetComponent<MoveEnemy>().GetDamage(damage);

            //Также после столкновения вызываем метод  Destruction();
            Destruction();
        }
        //При условии принадлежности пули врагу, и столкновении со щитом, то вызываем метод повреждения щита.
        else if (is_Enemy_Bullet && coll.tag == "Shield")
        {
            Player.instance.GetDamageShield(damage);
            //После столкновения вызываем метод разрушения пули.
            Destruction();
        }
    }




}
