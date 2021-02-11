﻿using System.Collections;
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

    //Добавим переменную типа текст.
    public Text Score;

    [Header("Shop panel")]
    //Добавим массив в котором будут корабли для продажи и покупки.
    public GameObject[] shop_Ships;
    //Добавим массив для текста который будет под кораблями с информацией.
    public Text[] shop_Ship_Text;
    //Добавим переменную типа кнопки для работы Buy в магазине, если у корабля нет цены данная кнопка будет скрыта.
    public GameObject btn_Shop_Buy;
    //Добавим переменную типа текст для работы с текстом в кнопке Buy, в неё мы будем добавлять цену корабля.
    public Text shop_Btn_Buy_Cost_Text;


    //Добавим Панель Upgrade.
    [Header("Upgrade panel")]
    //Добавим массив с изображениями наших кораблей.
    public Sprite[] upgrade_Sprite_Ships;
    //Добавим ссылку на изображение корабля в панели Upgrade, в неё будем помещать изображение выбранного корабля.
    public GameObject upgrade_Sprite_Ship;
    //Добавим массив для хранения ползунков.
    public Slider[] uprade_Sliders;
    //Добавим массив для текста, он нужен для отображения цен Upgarada.
    public Text[] upgrade_Show_Cost;

    //Добавим индекс корабля который уже куплен и выбран на данный момент.
    private int _index;
    //Добавим второй индекс в котором будет сохранён выбранный корабль если он ещё не куплен, если мы купим данный корабль значение этого индекса перейдёт в основной индекс.
    private int _indexBuy;

    #region Buttons Save Load Debug Exit Choise...
    //Добавим метод сохранения игры.
    public void BtnSave()
    {
        DataBase.instance.SaveGame();
    }
    //Добавим метод загрузки игры.
    public void BtnLoadGameSave()
    {
        DataBase.instance.LoadGameSave();
    }
    //Добавим метод для отладки при вызове данного метода будем удалять все сохранения в игре.
    public void BtnDeleteSaveGame_Debug()
    {
        PlayerPrefs.DeleteAll();
    }
    //Добавим метод загрузки уровней
    public void BtnChoiceLevelGame(string name)
    {
        //Вызывеам его из DataBase и передаём ему имя сцены.
        DataBase.instance.GameLoadScene(name);
    }
    //Добавим метод выхода в нём мы будем сохранять и выходить из игры. Выход работает для телефонов, но не для Unity
    public void BtnExitGame()
    {
        //SaveGame
        BtnSave();
        //ExitGame
        Application.Quit();
    }
    #endregion

    private void Start()
    {
        //При старте ищем файл с сохранением, если он найден, то автоматом его загружаем.
        if (PlayerPrefs.HasKey("InfoSaveScor"))
        {
            Debug.Log("Found save game!!!");
            BtnLoadGameSave();
        }
        //При старте будем вызывать 2 метода: 1-отвечает за обновление очков игрока, 2-для загрузки базовых параметров.
        UpdateScore();
        ShopShipHighLighting();
    }
    //Добавим метод обновления очков в игре.
    public void UpdateScore()
    {
        Score.text = DataBase.instance.Score.ToString();
    }

    #region Shop...
    //Данный метод будет подсвечивать кнопки кораблей по разному в зависимости куплен корабль, выбран или не выбран.
    public void ShopShipHighLighting()
    {
        //В цикле проверяем какой корабль был куплен и изначально загружен в игру.
        for (int i = 0; i < DataBase.instance.playerShipInfo.Length; i++)
        {
            //Создаём условие в котором ищем 1 в первом элементе подмассива.
            if (DataBase.instance.playerShipInfo[i][0] == 1)
            {
                //Задаём компоненту Image белый цвет
                shop_Ships[i].GetComponent<Image>().color = Color.white;
                shop_Ship_Text[i].color = Color.green;
                //Сохраним индекс корабля, через него мы можем переходить в панель upgrade и улучшать данный корабль.
                _index = i;
            }
            //Для остальных кораблей делаем серый цвет и текст красного цвета.
            else
            {
                shop_Ships[i].GetComponent<Image>().color = Color.gray;
                shop_Ship_Text[i].color = Color.red;
            }
            //Настроем текст под каждым кораблём, в цикле проверяем не только 1-й элемент подмассива,но и второй отвечающий за стоимость корабля.
            //Создаём условие, если значение равно 0 то корабль куплен и текст под ним будет иметь значение Open.
            if (DataBase.instance.playerShipInfo[i][1] == 0)
            {
                shop_Ship_Text[i].text = "Open";
            }
            else
            {
                shop_Ship_Text[i].text = "Cost: " + DataBase.instance.playerShipInfo[i][1].ToString();
            }
        }
    }

    //Добавим метод проверки выбранного корабля, он принемает 1 параметр.
    public void ShopCheckPlayerShip(int num)
    {
        //Добавляем условие, если у корабля который мы выбрали нет цены, то мы можем выбрать данный корабль для игры.
        if (DataBase.instance.playerShipInfo[num][1] == 0)
        {
            //Через цикл обнуляем первый элемент у всех подмассивов, и сохранаяем 1 в выбранном подмассиве, тоесть этот корабль появится у нас в игре.
            for (int i = 0; i < DataBase.instance.playerShipInfo.Length; i++)
            {
                DataBase.instance.playerShipInfo[i][0] = 0;
            }
            //Сохраним его индекс 
            DataBase.instance.playerShipInfo[num][0] = 1;
            _index = num;
            //Далее прячем кнопку покупки.
            btn_Shop_Buy.SetActive(false);
        }
    }


    #endregion


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
