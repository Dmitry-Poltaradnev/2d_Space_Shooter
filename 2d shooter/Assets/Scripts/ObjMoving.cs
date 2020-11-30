using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjMoving : MonoBehaviour
{
    public float speed;//Указываем скорость перемещения объекта на котором будет висеть данный скрипт.

    private void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime); // Передвижение объекта по вертикальной плоскости. Зависящее от скорости.
    }
}
