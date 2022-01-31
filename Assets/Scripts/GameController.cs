using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [Space]
    [Header("Value Max Reward")]
    [SerializeField]
    private float[] value_max = new float[6]; // 0 - 500, 1 - 1gb, 2 - 2gb, 3 - 3gb, 4 - 4gb, 5 - 5gb
    private float[] value = new float[6]; // 0 - 500, 1 - 1gb, 2 - 2gb, 3 - 3gb, 4 - 4gb, 5 - 5gb

    [Space]
    [Header("Quests")]
    [SerializeField]
    private GameObject[] quest_panel = new GameObject[6];
    [SerializeField]
    private GameObject quest_panel_init;
    [SerializeField]
    private GameObject shop_panel;

    [Space]
    [Header("Buttons Quests")]
    [SerializeField]
    private Button[] btn_get_quest = new Button[6];

    [Space]
    [Header("Buttons Play Quests")]
    [SerializeField]
    private Button[] btn_play_quest = new Button[6];

    [Space]
    [Header("Buttons Menu")]
    [SerializeField]
    private Button[] btn_menu = new Button[6];

    [Space]
    [Header("Buttons Shop")]
    [SerializeField]
    private Button[] btn_shop = new Button[6];

    [Space]
    [Header("Bar Missions")]
    [SerializeField]
    private Image[] bar_mission = new Image[6];


    // Start is called before the first frame update
    void Start()
    {
        btn_get_quest[0].onClick.AddListener(() => Quest(0));

        btn_menu[0].onClick.AddListener(() => Menu(0));

        btn_shop[0].onClick.AddListener(() => Shop(0));
    }

    void Quest(int number)
    {
        quest_panel[number].SetActive(true);
        quest_panel_init.SetActive(false);
    }

    void Menu(int number)
    {
        quest_panel[number].SetActive(false);
        quest_panel_init.SetActive(true);
    }

    void Shop(int number)
    {
        shop_panel.SetActive(true);
        quest_panel[number].SetActive(false);
    }

    void Bar(int number)
    {
        bar_mission[number].fillAmount = value[number] / value_max[number];
    }
}

