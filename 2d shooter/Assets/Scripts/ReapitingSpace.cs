using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReapitingSpace : MonoBehaviour
{
    // Переменная для хранения высоты спрайта в пикселях. Высота изображения должна быть больше высоты камеры.Чтобы не видеть стык изображений.
    public float vertical_Size;

    private Vector2 _offSet_Up; // private переменная для расчёта высоты на которую должен подняться спрайт, зависит от высоты спрайта.


    private void Update()
    {
        // Проверяем находиться ли спрайт ниже своей высоты.
        if (transform.position.y < -vertical_Size)
        {
            RepeatBackGround();
        }
        
    }
    void RepeatBackGround()// Данный метод перемещает спрайты друг за другом
    {
        _offSet_Up = new Vector2(0, vertical_Size * 2f); // расчёт смещения для private переменной  2-ка отображает кол-во изменяющихся повторяющихся спрайтов фона.

        transform.position = (Vector2)transform.position + _offSet_Up; // Создаём новую позицию для приватной переменной 

    }
}
