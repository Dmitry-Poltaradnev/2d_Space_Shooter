﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowThePath : MonoBehaviour
{
    //Нам необходим массив в котором будут точки пути по которому будет передвигаться наш враг
    [HideInInspector] public Transform[] path_Points;

    //Необходима переменная скорости с которой будет перемещаться враг.
    [HideInInspector] public float speed_Enemy;

    //Логическая переменная исходя из которой когда враг будет в последней точке пути в зависимости от значения данной переменной, он будет либо уничтожен,либо начнёт свой путь с первой точки пути.
    [HideInInspector] public bool is_Return;


    //Необходим массив для хранения векторов.--------------------------------------
    [HideInInspector] public Vector3[] _new_Position;

    //Нам необходима переменная для хранения порядкового номера точки пути. Благодаря ей враг будет знать куда ему двигаться.
    private int cur_Pos;


    private void Start()
    {
        _new_Position = NewPositionByPath(path_Points);// Помещаем значения векторов наших точек пути в массив для хранения векторов.

        //В старте отправляем врага в начальную точку пути.
        transform.position = _new_Position[0];

    }
    private void Update()
    {
        //Перемещение врага, Переместим объект на котором висит данный скрипт в точку пути с заданной скоростью.
        transform.position = Vector3.MoveTowards(transform.position, _new_Position[cur_Pos], speed_Enemy * Time.deltaTime);

        //Создаём условия если враг добрался до точки пути, то мы добавляем к переменной для порядкового номера хранения точки пути 1-цу, таким образом у врага будет новая точка для движения.
        if (Vector3.Distance(transform.position, _new_Position[cur_Pos]) < 0.2f) 
            
        {
            cur_Pos++;


            // Если текущий враг добрался до последней точки пути,и  (public bool is_Return = true), то мы отправляем врага в начальную точку пути.
            if (is_Return && Vector3.Distance(transform.position, _new_Position[_new_Position.Length - 1]) < 0.3f)
            {
                cur_Pos = 0;
            }           
        }


        //  Если текущий враг добрался до последней точки пути,и  (public bool is_Return = false), то мы уничтожаем текущего врага.
        if (Vector3.Distance(transform.position, _new_Position[_new_Position.Length - 1]) < 0.2f && !is_Return)
        {
            Destroy(gameObject);
        }


    }

    //тоже самое делаем это отдельно и в цикле.
    Vector3[] NewPositionByPath(Transform[] pathPos)
    {
        Vector3[] pathPositions = new Vector3[pathPos.Length];
        for (int i = 0; i < path_Points.Length; i++)
        {
            pathPositions[i] = pathPos[i].position;
        }
        pathPositions = Smoothing(pathPositions);
        pathPositions = Smoothing(pathPositions);
        pathPositions = Smoothing(pathPositions);
        return pathPositions;
    }
    //Добавляем метод из скрипта wave для сглаживания движения.
    Vector3[] Smoothing(Vector3[] path_Positions)
    {
        Vector3[] new_Path_Positions = new Vector3[(path_Positions.Length - 2) * 2 + 2];
        new_Path_Positions[0] = path_Positions[0];
        new_Path_Positions[new_Path_Positions.Length - 1] = path_Positions[path_Positions.Length - 1];

        int j = 1;
        for (int i = 0; i < path_Positions.Length - 2; i++)
        {
            new_Path_Positions[j] = path_Positions[i] + (path_Positions[i + 1] - path_Positions[i]) * 0.75f;
            new_Path_Positions[j + 1] = path_Positions[i + 1] + (path_Positions[i + 2] - path_Positions[i + 1]) * 0.25f;
            j += 2;
        }
        return new_Path_Positions;
    }

}
