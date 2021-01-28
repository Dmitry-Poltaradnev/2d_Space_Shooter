using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]/* Добавим новый класс через который будем взаимодействовать с волной. Делаем его Serializable 
                      для того чтобы его поля отображались в инспекторе.*/
public class EnemyWaves
{
    //Добавим время через которое мы будем создавать волну
    public float TimeToStart;

    //Создаём переменную для хранения волны.
    public GameObject wave;

    //Добавляем bool переменную отвечающую за конец игры. if is_Last_Wave  = true игра заканчивается.
    public bool is_Last_Wave;
}                       


public class LevelController : MonoBehaviour
{
    // Cоздаём ссылку на самого себя.
    public static LevelController instance;

    //Добавляем массив в котором будут храниться все корабли игрока.
    public GameObject[] playerShip;

    //Также создаём массив для вражеских волн.
    public EnemyWaves[] enemyWaves;

    //Добавим bool переменную которая будет вызывать конец игры.
    private bool is_Final = false;

    // Добавим меню паузы
    public GameObject panel;
    //Также добавил bool переменную для отслеживания игровой паузы
    private bool _isPause;
    //Добавим массив для хранения кнопок(Exit, Return, Restart)
    public GameObject[] btnPause;


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
    }
    private void Start()
    {
        //Создаём вражеские волны через цикл.
        for (int i = 0; i < enemyWaves.Length; i++)
        {
            //В данном цикле запускаем со программу, которая принемает 2 значения: 1-e когда появится волна, 2-е какой тип волны будет создан.
            StartCoroutine(CreateEnemyWave(enemyWaves[i].TimeToStart, enemyWaves[i].wave, enemyWaves[i].is_Last_Wave));
        }
    }


    private void Update()
    {
        //При апдейте проеверяем победили, или проиграли: if bool = true && != объектов c тегом Enemy(победа). И проверка на нажатие кнопки Pause
        if (is_Final == true && GameObject.FindGameObjectsWithTag("Enemy").Length == 0 && !_isPause)
        {
            Debug.Log("Win");
            GamePause();
            btnPause[1].SetActive(false);
        }
        else if  (Player.instance == null && !_isPause)
        {
            Debug.Log("Lose");
            GamePause();            
        }
    }

    //Добавим метод GamePause()
    public void GamePause()
    {
        //Вызов панели паузы
        if (!_isPause)
        {
            _isPause = true;
            //Останавливаем время игры
            Time.timeScale = 0;
            //Отобразим панель
            panel.SetActive(true);
            //Добавим условие где проверяем жив ли игрок, если жив значит будут доступны 2 кнопки Return и Exit
            if (Player.instance != null)
            {
                btnPause[0].SetActive(false);
                btnPause[1].SetActive(true);
            }
            //Добавим условие, если игрок погиб, то пауза появляется автоматом, и будут активны Restart и Exit
            else
            {
                btnPause[0].SetActive(true);
                btnPause[1].SetActive(false);
            }

        }
        //Условие когда панель должна быть сокрыта
        else
        {
            _isPause = false;
            //Снимаем с паузы игру.
            Time.timeScale = 1;
            //Cкрываем панель паузы
            panel.SetActive(false);
        }
    }
    //Добавим метод для кнопки Restart
    public void BtnRestartGame()
    {
        //При нажатии данной кнопки, снимаем игровую паузу.
        Time.timeScale = 1;
        //Далее загружаем текущую сцену по имени
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator CreateEnemyWave(float delay, GameObject Wave, bool Final)// Пишем условие для со программы 
    {
        //Делаем условие, если время создания волны != 0 , то делаем задержку на то время которое мы задаем для данной волны.
        if (delay != 0)
        {
            yield return new WaitForSeconds(delay);
        }
        if (Player.instance != null)
        {
            Instantiate(Wave);
        }

        //Добавим условие,если текущая волна будет финальной, передаём true в bool переменную которая отвечает за конец игры.
        if (Final == true)
        {
            is_Final = true;
        }
        
    }

}
