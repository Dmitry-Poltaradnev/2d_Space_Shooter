using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Serializable - поля данных классов видны в инспекторе.
[System.Serializable]
public class Borders// наши поля с границами.лево право низ и верх
{
    public float minX_Offset = 1.1f;
    public float maxX_Offset = 1.1f;
    public float minY_Offset = 1.1f;
    public float maxY_Offset = 1.1f;

    // Также добавим несколько публичных полей, которые необходимы для расчёта.

    // Добавим 4 поля которые будут скрыты от инспектора. Границы созданные в них мы ограничим перемещение игрока.
    [HideInInspector]
    public float minX, maxX, minY, maxY;// Для взаимодействия с данным классом мы передаём ссылку в  MovePlayer (1)


}

public class MovePlayer : MonoBehaviour
{
    public static MovePlayer instanse;// public переменная  для скорости, которую можем изменить.
    public Borders borders;// Переданная ссылка (1).
    public int speed_Player = 5; // Переменная для хранения скорости игрока (при изменении скорость меняеться)
    private Camera _camera; // private ссылка на камеру для взаимодействия с экраном.
    private Vector2 _mouse_Position;// Пременная для хранения 2d координат от нашего нажатия на экран, в данные координаты будет двигаться наш игрок.


    private void Awake()
    {
        if (instanse == null)// если в переменной пусто, то мы добавляем ссылку на данный скрипт.
        {
            instanse = this;
        }
        else
        {
            Destroy(gameObject);// если есть другие ссылки на данный объект, то они удаляются.
        }
        _camera = Camera.main;// Настраиваем ссылку на камеру.
    }



    private void Start()
    {
        ResizeBorders();// Вызываем метод расчёта границ, которые ограничивают перемещение нашего игрока. (2)
    }


    private void Update()
    {
        if (Input.GetMouseButton(0))//Проверяем была ли нажата левая клавиша мыши
        {
            _mouse_Position = _camera.ScreenToWorldPoint(Input.mousePosition);// При нажатии на экран записываем координаты места нажатия по экрану.
            transform.position = Vector2.MoveTowards(transform.position, _mouse_Position, speed_Player * Time.deltaTime); // Перемещаем нашего игрока на котором висит данный скрипт в 2D координаты от нашего нажатия по экрану.
        }
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, borders.minX, borders.maxX), // Если игрок пытаеться выйти за границы экрана, то он останавливается.
                                         Mathf.Clamp(transform.position.y, borders.minY, borders.maxY));
    }



    // Данный метод (2) для создания границ использует отступы камеры. То-есть он будет работать со всеми экранами.
    private void ResizeBorders()
    {
        borders.minX = _camera.ViewportToWorldPoint(Vector2.zero).x + borders.minX_Offset;// + и ебаные минусики !!!!!  обратить внимание.    

        borders.minY = _camera.ViewportToWorldPoint(Vector2.zero).y + borders.minY_Offset;

        borders.maxX = _camera.ViewportToWorldPoint(Vector2.right).x - borders.maxX_Offset;

        borders.maxY = _camera.ViewportToWorldPoint(Vector2.up).y - borders.maxY_Offset;
    }
   
    
}
