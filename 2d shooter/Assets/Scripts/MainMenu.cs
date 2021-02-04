using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;//Подключаем для возможности работать со сценами.

//Для создания вкладок прописываем #region and #endregion
#region DataBase_Using_Panel
public class DataBase //Через данный класс будет взаимодействовать с (панелями, меню, кораблями и т.д.)
{
    public static DataBase instance = new DataBase();
    //Создадим массив массивов, в котором будем хранить инф. о наших кораблях, каждый подмассив массива будет кораблём и элементы этого массива будут его параметрами.
    public int[][] playerShipInfo =
    {
        //Так как у нас 3 корабля значит будет 3 подмассива
        //Cоздаём новый подмассив.
        new int[] {1, 000, 1, 15, 0}, /* 1-й элемент отвечает за выбор корабля(если он выбран то значение равно 1 если нет 0), 2-й отвечает за стоимость корабля если он был 
                                    * куплен, то значение будет равно 0, если нет то тут находится цена за которую мы должные его приобрести. 3-й элемент отвечает за кол-во
                                    очков жизней игрока , 4-й отвечает за скорость игрока, 5-й за кол-во очков жизней щита если он ещё не куплен то не будет отображаться и 
                                    будет равен 0*/ 
        new int [] {0, 550, 2, 8, 0},
        new int [] {0, 950, 3, 6, 0}
    };

    //Настроем цены для улучшений.
    public int costHP = 250;
    public int costSPEED = 500;
    public int costShield = 2500;

    //Добавим переменную в которой будут храниться очки для последующих трат на улучшения.
    public int Score = 99999;
    /*Добавим переменную которая будет сохранять очки во время игры, данные очки будут добавляться к основным очкам если мы победим или выйдем из игры,
     * но не будут если мы перезагрузим уровень */
    public int Score_Game = 0;

    //Добавим метод который будет отвечать за загрузку сцен.
    public void GameLoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    //Добавим метод который будет сохранять данные игры
    public void SaveGame()
    {
        Score += Score_Game;
        for (int i = 0; i < playerShipInfo.Length; i++)
        {
            for (int j = 0; j < playerShipInfo[i].Length; j++)
            {
                PlayerPrefs.SetInt("InfoSave" + i + j, playerShipInfo[i][j]);
            }
        }
        PlayerPrefs.SetInt("InfoSaveScore", Score);
    }

    //Добавим метод который будет загружать данные игры 
    public void LoadGameSave()
    {
        for (int i = 0; i < playerShipInfo.Length; i++)
        {
            for (int j = 0; j < playerShipInfo[i].Length; j++)
            {
                playerShipInfo[i][j] = PlayerPrefs.GetInt("InfoSave" + i + j);
            }
        }
        Score = PlayerPrefs.GetInt("InfoSaveScore");
    }
}


#endregion DataBase_Using_Panel

public class MainMenu : MonoBehaviour
{
    //Добавим массив для хранения панелей Menu(Level panel, Shop panel, Upgrade panel)
    public GameObject[] game_Panels;

    //Добавим метод который будет который будет отображать панель.
    public void Show_Change_Panel(int index)
    {
        game_Panels[index].SetActive(true);
    }
    //Добавим метод для сокрытия панели.
    public void Hide_Change_Panel(int index)
    {
        game_Panels[index].SetActive(false);
    }
}
