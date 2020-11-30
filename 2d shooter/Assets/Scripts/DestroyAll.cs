using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAll : MonoBehaviour
{
    //Необходима privat ссылка с box collinder2d 
    private BoxCollider2D _boundare_Collinder;


    // privat переменная для угла камеры
    private Vector2 _viewport_Size;


    private void Awake()
    {
        _boundare_Collinder = GetComponent<BoxCollider2D>();
    }
    private void Start()
    {
        // Вызываем метод для расчёта BoxCollider2D
        ResizeCollinder();// Данный метод будет автоматически изменять размер BoxCollider2D под любой размер экрана автоматически.
    }

    void ResizeCollinder()
    {
        // Необходимо получить значение верхнего правого угла камеры и умножить его на 2
        _viewport_Size = Camera.main.ViewportToWorldPoint(new Vector2(1, 1)) * 2;


        // Умножим ширину и высоту на 1.5
        _viewport_Size.x *= 1.5f;
        _viewport_Size.y *= 1.5f;

        // Изменяем размеры BoxCollider2D используя наши расчёты.
        _boundare_Collinder.size = _viewport_Size;

    }

    //Логика уничтожения  для объектов покидающих BoxCollider2D
    public void OnTriggerExit2D(Collider2D coll)
    {
        // Уничтожаем любые объекты с тегами в данном коде.
        switch (coll.tag)
        {
            case "Planet":
                Destroy(coll.gameObject);
                break;             
        }
    }


}
