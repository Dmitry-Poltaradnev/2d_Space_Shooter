using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;//Подключаем UI для работы с текстом.

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

    //Добавим переменную через которую будем работать с текстом Score на панели.
    public Text text_Score;


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
        //Если не добавить данный код, то игровое время после выхода и последующего входа в игру будет заморожено.
        Time.timeScale = 1;
        //Вызываем метод загрузки корабля при старте.
        for (int i = 0; i < DataBase.instance.playerShipInfo.Length; i++)
        {
            //Проверяем в каком подмассиве находится 1 в первом элементе, его и будем загружать.
            if (DataBase.instance.playerShipInfo[i][0] == 1)
            {
                LoadPlayer(i);
            }
        }
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
        else if (Player.instance == null && !_isPause)
        {
            Debug.Log("Lose");
            GamePause();
        }
    }

    //Добавим метод для работы с призовыми очками.
    public void ScoreInGame(int score)
    {
        DataBase.instance.Score_Game += score;
        //Отобразим данные очки в панели игрока.
        text_Score.text = "Score: " + DataBase.instance.Score_Game.ToString();
    }

    //Добавим метод который будет загружать корабль игрока с метода DataBase
    public void LoadPlayer(int ship)//Данный метод принимает int параметр через него он будет загружать корабль из массива  ( public GameObject[] playerShip; )
    {
        Instantiate(playerShip[ship]);
        Player.instance.player_Health = DataBase.instance.playerShipInfo[ship][2];
        MovePlayer.instanse.speed_Player = DataBase.instance.playerShipInfo[ship][3];
        Player.instance.shield_Health = DataBase.instance.playerShipInfo[ship][4];
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
        //При нажатии Restart, то значение промежуточных очков сбрасывается до 0.
        DataBase.instance.Score_Game = 0;

        //При нажатии данной кнопки, снимаем игровую паузу.
        Time.timeScale = 1;
        //Далее загружаем текущую сцену по имени
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //Добавим метод для кнопки выхода.
    public void BtnExitGame()
    {
        //При нажатии на эту кнопку мы будем сохранять промежуточные очки в основные, и переходить в главное меню игры.
        DataBase.instance.SaveGame();
        DataBase.instance.GameLoadScene("Menu");
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
