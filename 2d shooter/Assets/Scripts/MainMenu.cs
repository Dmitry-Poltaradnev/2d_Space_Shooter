using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
