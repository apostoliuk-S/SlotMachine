using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Spin : MonoBehaviour
{

    public Button GO; // Кнопка запуска механизма...

    // Кнопки ставок //
    public Button ButtonBet_5;
    public Button ButtonBet_10;
    public Button ButtonBet_20;
    public Button ButtonBet_50;

    public Button Button_On_Off;
    public Sprite[] Switch_On_Off;

    // Кнопка закрытия попапа с выиграшрм //
    public Button OK;

    public Transform row_1;   //
    public Transform row_2;  // Лента символов в барабанах...
    public Transform row_3; //

    [SerializeField] private float speedSpin_1;   //
    [SerializeField] private float speedSpin_2;  // Переменные для задания случайной скорости (от 25 до 50)...
    [SerializeField] private float speedSpin_3; //

    [SerializeField] private Vector3 startPos_1;   //
    [SerializeField] private Vector3 startPos_2;  // Стартовая позиция барабанов...
    [SerializeField] private Vector3 startPos_3; //

    [SerializeField] private Vector3 stopPos_1;   //
    [SerializeField] private Vector3 stopPos_2;  // Рандомная позиция остановки барабанов (получает случайное значение из мпссива)...
    [SerializeField] private Vector3 stopPos_3; //

    private Vector3 top_1_Pos;
    private Vector3 top_2_Pos;
    private Vector3 top_3_Pos;
    private Vector3 top_4_Pos;
    private Vector3 top_5_Pos;

    [SerializeField] int x;   //
    [SerializeField] int y;  // Тут 3 переменных для перевода рандомных значений с 3-х барабанов в их ценность: "1, 2, 3, 4, 5"...
    [SerializeField] int z; //

    public ParticleSystem ps; // Партикл полета поинтов...
    [SerializeField] private bool workMainMethod;        // Активация барабанов булевым значением...
    [SerializeField] private bool activationProgResult; //  Переменная активации программируемого результата...

    // Активация методов присваивания Монет и Никнеймов в Лидерборд
    [SerializeField] private bool P1;
    [SerializeField] private bool P2;
    [SerializeField] private bool P3;
    [SerializeField] private bool P4;
    [SerializeField] private bool P5;


    public Text betValue;   // Отображение ставки в панели с выбором ставки...
    public Text coinsText; //  Текстовое поле в правом углу (общий баланс)...
    public Text gain;     //   Отображение нашего выиграша в попапе выиграша...
    public Text Nickname;

    public int coinsInt;
    public string player;

    public GameObject CreditPanel; // Панель кредита...
    public GameObject winPanel;   //  Панель выиграша...

    // Игровой таймер + возможные комбинации //
    float timer;
    float[] posY = { -8.2f, -4f, 0.5f, 4.5f, 8.5f };
    int counter;

    [SerializeField] private int bet;  // Значение нашей ставки...
    [SerializeField] private int tmp; //  Временная переменная для хранения выиграша...

     // Поля для таблицы лидерборда //
    /////////////////////////////////
    public Transform PlayerField_1;
    public Transform PlayerField_2;
    public Transform PlayerField_3;
    public Transform PlayerField_4;
    public Transform PlayerField_5;

    public Text NamePlayer_1;
    public Text ScorePlayer_1;

    public Text NamePlayer_2;
    public Text ScorePlayer_2;

    public Text NamePlayer_3;
    public Text ScorePlayer_3;

    public Text NamePlayer_4;
    public Text ScorePlayer_4;

    public Text NamePlayer_5;
    public Text ScorePlayer_5;
    /////////////////////////////////

    int Score_1, Score_2, Score_3, Score_4, Score_5;
    int Top_1, Top_2, Top_3, Top_4, Top_5;

    bool ActivationSortScore;

    void Start()
    {
        activationProgResult = false;
        GO.interactable = false; // Блокируем кнопку запуска на старте игры
        workMainMethod = false; //  Выключаем метод запуска механизма вращения
        ps.Stop();             //   Партикл систем стоп

        coinsInt = 50;

        // Задаем стартовую позицию спинов //
        startPos_1 = new Vector3(-4.6f, -8.2f, 0);
        startPos_2 = new Vector3(0, -8.2f, 0);
        startPos_3 = new Vector3(4.6f, -8.2f, 0);

        // Задаем позицию согласно рейтинга //
        top_1_Pos = new Vector3(0,  1.1f, 0);
        top_2_Pos = new Vector3(0,    0f, 0);
        top_3_Pos = new Vector3(0, -1.1f, 0);
        top_4_Pos = new Vector3(0, -2.2f, 0);
        top_5_Pos = new Vector3(0, -3.3f, 0);

        Check_Players(); // Вызов метода проверки аккаунтов
    }


    void Update()
    {
        // Приведение типов //
        gain.text = tmp.ToString();
        coinsText.text = coinsInt.ToString();
        betValue.text = bet.ToString();

        timer += Time.deltaTime; // Наш игровой таймер...

        StartAllSpin();   // Запуск всех барабанов в едином методе...
        ACT();           //  Вызов метода зависимомтей кнопок...
        CheckCredit();  //   Метод проверки кредита...
        CheckCredit_LB();

        ActivationAfterPlayer_5();

        Update_All_Data();  //     Обновление всех данных по игрокам...
    }



    public void ButtonGO()
    {
        if (activationProgResult == false)
        {
            // Каждый раз обновляем случайное выпадение символа по клику кнопки GO //
            var rand_1 = Random.Range(0, posY.Length);
            stopPos_1 = new Vector3(-4.6f, posY[rand_1], 0);
            for (int i = 0; i <= rand_1; i++)
            {
                if (i == 0)
                {
                    x = 1;
                }
                x = i + 1;
            } // Присвоение ценности для: X

            var rand_2 = Random.Range(0, posY.Length);
            stopPos_2 = new Vector3(0, posY[rand_2], 0);
            for (int i = 0; i <= rand_2; i++) // Присвоение ценности для: Y
            {
                if (i == 0)
                {
                    y = 1;
                }
                y = i + 1;
            } // Присвоение ценности для: Y

            var rand_3 = Random.Range(0, posY.Length);
            stopPos_3 = new Vector3(4.6f, posY[rand_3], 0);
            for (int i = 0; i <= rand_3; i++)
            {
                if (i == 0)
                {
                    z = 1;
                }
                z = i + 1;
            } // Присвоение ценности для: Z
        } // Когда программируемая кнопка выключена (все спины в режиме рандома)
        if (activationProgResult == true)
        {
            if (counter == 7)
            {
                var rand_1 = Random.Range(0, posY.Length);
                stopPos_1 = new Vector3(-4.6f, posY[rand_1], 0);
                for (int i = 0; i <= rand_1; i++)
                {
                    if (i == 0)
                    {
                        x = 1;
                    }
                    x = i + 1;
                } // Присвоение ценности для: X

                stopPos_2 = new Vector3(0, posY[rand_1], 0);
                for (int i = 0; i <= rand_1; i++) // Присвоение ценности для: Y
                {
                    if (i == 0)
                    {
                        y = 1;
                    }
                    y = i + 1;
                } // Присвоение ценности для: Y

                stopPos_3 = new Vector3(4.6f, posY[rand_1], 0);
                for (int i = 0; i <= rand_1; i++)
                {
                    if (i == 0)
                    {
                        z = 1;
                    }
                    z = i + 1;
                } // Присвоение ценности для: Z
                counter = 0;
            }       // Каждый 8-й спин: Выпадает линия из 3-х одинаковых символов
            else if (counter == 3)
            {
                var rand_1 = Random.Range(0, posY.Length);
                stopPos_1 = new Vector3(-4.6f, posY[rand_1], 0);
                for (int i = 0; i <= rand_1; i++)
                {
                    if (i == 0)
                    {
                        x = 1;
                    }
                    x = i + 1;
                } // Присвоение ценности для: X

                var rand_2 = Random.Range(0, posY.Length);
                stopPos_2 = new Vector3(0, posY[rand_2], 0);
                for (int i = 0; i <= rand_2; i++) // Присвоение ценности для: Y
                {
                    if (i == 0)
                    {
                        y = 1;
                    }
                    y = i + 1;
                } // Присвоение ценности для: Y

                stopPos_3 = new Vector3(4.6f, posY[rand_1], 0);
                for (int i = 0; i <= rand_1; i++)
                {
                    if (i == 0)
                    {
                        z = 1;
                    }
                    z = i + 1;
                } // Присвоение ценности для: Z
                counter++;
            } //  Каждый 4-й спин: Первый и второй барабан получают одинаковые значения
            else
            {
                var rand_1 = Random.Range(0, posY.Length);
                stopPos_1 = new Vector3(-4.6f, posY[rand_1], 0);
                for (int i = 0; i <= rand_1; i++)
                {
                    if (i == 0)
                    {
                        x = 1;
                    }
                    x = i + 1;
                } // Присвоение ценности для: X

                var rand_2 = Random.Range(0, posY.Length);
                stopPos_2 = new Vector3(0, posY[rand_2], 0);
                for (int i = 0; i <= rand_2; i++) // Присвоение ценности для: Y
                {
                    if (i == 0)
                    {
                        y = 1;
                    }
                    y = i + 1;
                } // Присвоение ценности для: Y

                var rand_3 = Random.Range(0, posY.Length);
                stopPos_3 = new Vector3(4.6f, posY[rand_3], 0);
                for (int i = 0; i <= rand_3; i++)
                {
                    if (i == 0)
                    {
                        z = 1;
                    }
                    z = i + 1;
                } // Присвоение ценности для: Z
                counter++;
            }                  //   Все остальные спины будут случайными
        } //  Когда кнопка в режиме: On, то активируется программируемый мод

        // Обновляем случайную скорость по клику кнопки GO //
        speedSpin_1 = Random.Range(25f, 50f);
        speedSpin_2 = Random.Range(25f, 50f);
        speedSpin_3 = Random.Range(25f, 50f);

        timer = 0; // Обнуляем игровой таймер
        workMainMethod = true;
        OK.interactable = true;

        coinsInt -= bet; // Отнимаем стаку из общего счета
        CheckWinning(); // Проверка выиграша
    }

    void ACT() // Метод зависимостей кнопок
    {
        if(workMainMethod == true)
        {
            DisableBetButton();
            GO.interactable = false;
        }
        if(workMainMethod == false)
        {
            EnableBetButton();
        }
    }

    #region // Запуск вращения всех барабанов //
    public void StartAllSpin()
    {
        if (workMainMethod == true)
        {
            Start_1();
            Start_2();
            Start_3();
        }
    }
    #endregion

    #region // Методы случайного вращения 1-го барабана //
    void Start_1()
    {
        if (timer < 3)
        {
            StartSpin_1();
        }
        if (timer >= 3)
        {
            row_1.transform.position = stopPos_1;
        }
    }
    public void StartSpin_1()
    {
        row_1.transform.Translate(Vector3.up * Time.deltaTime * speedSpin_1);

        if (row_1.transform.position.y > 8.2f)
        {
            row_1.transform.position = startPos_1;
        }
    }
    #endregion

    #region // Методы случайного вращения 2-го барабана //
    void Start_2()
    {
        if (timer < 4)
        {
            StartSpin_2();
        }
        if (timer >= 4)
        {
            row_2.transform.position = stopPos_2;
        }
    }
    public void StartSpin_2()
    {
        row_2.transform.Translate(Vector3.up * Time.deltaTime * speedSpin_2);

        if (row_2.transform.position.y > 8.2f)
        {
            row_2.transform.position = startPos_2;
        }
    }
    #endregion

    #region // Методы случайного вращения 3-го барабана //
    void Start_3()
    {
        if (timer < 5)
        {
            StartSpin_3();
        }
        if (timer >= 5)
        {
            row_3.transform.position = stopPos_3;
            workMainMethod = false;
        }
    }
    public void StartSpin_3()
    {
        row_3.transform.Translate(Vector3.up * Time.deltaTime * speedSpin_3);

        if (row_3.transform.position.y > 8.2f)
        {
            row_3.transform.position = startPos_3;
        }
    }
    #endregion

    public void Bet_5()
    {
        bet = 5;
        GO.interactable = true;
    }

    public void Bet_10()
    {
        bet = 10;
        GO.interactable = true;
    }

    public void Bet_20()
    {
        bet = 20;
        GO.interactable = true;
    }

    public void Bet_50()
    {
        bet = 50;
        GO.interactable = true;
    }

    void DisableBetButton()    // Деактивация всех кнопок со ставками
    {
        ButtonBet_5.interactable = false;
        ButtonBet_10.interactable = false;
        ButtonBet_20.interactable = false;
        ButtonBet_50.interactable = false;
    }
    void EnableBetButton()   // Активация всех кнопок со ставками
    {
        ButtonBet_5.interactable = true;
        ButtonBet_10.interactable = true;
        ButtonBet_20.interactable = true;
        ButtonBet_50.interactable = true;
    }
    void CheckCredit()     // Проверка на кредит
    {
        if(coinsInt < 0)
        {
            CreditPanel.SetActive(true);
            coinsText.color = Color.red;
        }
        if (coinsInt > 0)
        {
            CreditPanel.SetActive(false);
            coinsText.color = Color.yellow;
        }
    }
    void CheckCredit_LB()
    {
        if(Score_1 < 0)
        {
            ScorePlayer_1.color = Color.red;
        }
        if (Score_2 < 0)
        {
            ScorePlayer_2.color = Color.red;
        }
        if (Score_3 < 0)
        {
            ScorePlayer_3.color = Color.red;
        }
        if (Score_4 < 0)
        {
            ScorePlayer_4.color = Color.red;
        }
        if (Score_5 < 0)
        {
            ScorePlayer_5.color = Color.red;
        }
        ////////////////////////////////////
        if (Score_1 > 0)
        {
            ScorePlayer_1.color = Color.yellow;
        }
        if (Score_2 > 0)
        {
            ScorePlayer_2.color = Color.yellow;
        }
        if (Score_3 > 0)
        {
            ScorePlayer_3.color = Color.yellow;
        }
        if (Score_4 > 0)
        {
            ScorePlayer_4.color = Color.yellow;
        }
        if (Score_5 > 0)
        {
            ScorePlayer_5.color = Color.yellow;
        }
    }
    void CheckWinning()  // Метод проверки выиграша
    {
        if(x == y && x == z)
        {
            tmp = 5 * x * bet;
            StartCoroutine("ShowWinPanel");
        }
        if(x == y && x != z || x == z && x != y)
        {
            tmp = 2 * x * bet;
            StartCoroutine("ShowWinPanel");
        }
        if(y == z && y != x)
        {
            tmp = 2 * y * bet;
            StartCoroutine("ShowWinPanel");
        }
    }


    IEnumerator ShowWinPanel() // Корутина для вывода панели выиграша
    {
        yield return new WaitForSeconds(5f);
        winPanel.SetActive(true);
    }
    IEnumerator FlyCoins()   // Корутина полета поинтов и закрытия окна выиграша
    {
        ps.Play();
        yield return new WaitForSeconds(2.5f);
        ps.Stop();
        winPanel.SetActive(false);
    }
    

    public void CloseWinPanel() // Кнопка активации полета поинтов и закрытия окна
    {
        StartCoroutine("FlyCoins");
        StartCoroutine(LerpValue(coinsInt, coinsInt + tmp, 3));
        OK.interactable = false;
    }

    IEnumerator LerpValue(float startValue, float endValue, float duration) // Сопрограмма для красивого зачисления поинтов
    {
        float elapsed = 0;
        float nextValue;

        while(elapsed < duration)
        {
            nextValue = Mathf.Lerp(startValue, endValue, elapsed / duration);

            coinsInt = (int)nextValue;
            coinsText.text = coinsInt.ToString();

            elapsed += Time.deltaTime;
            yield return null;
            coinsInt = (int)endValue;
        }
    }

    public void Switch_In_Programing_mod()
    {
        if(activationProgResult == false)
        {
            ActivationProg();
        }

        else if(activationProgResult == true)
        {
            DeactivationProg();
        }
    }
    void ActivationProg()
    {
        activationProgResult = true;
        Button_On_Off.GetComponent<Image>().sprite = Switch_On_Off[1];
    }
    void DeactivationProg()
    {
        activationProgResult = false;
        Button_On_Off.GetComponent<Image>().sprite = Switch_On_Off[0];
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                                                  // Save new player //


    // Условия создания профилей плееров и их авторизации
    void Check_Players()
    {
        if (PlayerPrefs.GetString("NewPlayer") == PlayerPrefs.GetString("NamePlayer_1"))
        {
            Save_Data();

            Nickname.text = PlayerPrefs.GetString("NamePlayer_1");
            NamePlayer_1.text = PlayerPrefs.GetString("NamePlayer_1");
            ScorePlayer_1.text = PlayerPrefs.GetInt("ScorePlayer_1").ToString();
            coinsInt = PlayerPrefs.GetInt("ScorePlayer_1");
            P1 = true;
        }
        if (PlayerPrefs.GetString("NewPlayer") == PlayerPrefs.GetString("NamePlayer_2"))
        {
            Save_Data();

            Nickname.text = PlayerPrefs.GetString("NamePlayer_2");
            NamePlayer_2.text = PlayerPrefs.GetString("NamePlayer_2");
            ScorePlayer_2.text = PlayerPrefs.GetInt("ScorePlayer_2").ToString();
            coinsInt = PlayerPrefs.GetInt("ScorePlayer_2");
            P2 = true;
        }
        if (PlayerPrefs.GetString("NewPlayer") == PlayerPrefs.GetString("NamePlayer_3"))
        {
            Save_Data();

            Nickname.text = PlayerPrefs.GetString("NamePlayer_3");
            NamePlayer_3.text = PlayerPrefs.GetString("NamePlayer_3");
            ScorePlayer_3.text = PlayerPrefs.GetInt("ScorePlayer_3").ToString();
            coinsInt = PlayerPrefs.GetInt("ScorePlayer_3");
            P3 = true;
        }
        if (PlayerPrefs.GetString("NewPlayer") == PlayerPrefs.GetString("NamePlayer_4"))
        {
            Save_Data();

            Nickname.text = PlayerPrefs.GetString("NamePlayer_4");
            NamePlayer_4.text = PlayerPrefs.GetString("NamePlayer_4");
            ScorePlayer_4.text = PlayerPrefs.GetInt("ScorePlayer_4").ToString();
            coinsInt = PlayerPrefs.GetInt("ScorePlayer_4");
            P4 = true;
        }
        if (PlayerPrefs.GetString("NewPlayer") == PlayerPrefs.GetString("NamePlayer_5"))
        {
            Save_Data();

            Nickname.text = PlayerPrefs.GetString("NamePlayer_5");
            NamePlayer_5.text = PlayerPrefs.GetString("NamePlayer_5");
            ScorePlayer_5.text = PlayerPrefs.GetInt("ScorePlayer_5").ToString();
            coinsInt = PlayerPrefs.GetInt("ScorePlayer_5");
            P5 = true;
        }
        if (PlayerPrefs.GetString("NewPlayer") != PlayerPrefs.GetString("NamePlayer_1"))
        {
            if (PlayerPrefs.GetString("Slot_1") != "Z")
            {
                NEW_PLAYER_1();
            }
            else if (PlayerPrefs.GetString("Slot_1") == "Z" && PlayerPrefs.GetString("Slot_2") != "Z")
            {
                NEW_PLAYER_2();
            }
            else if (PlayerPrefs.GetString("Slot_1") == "Z" && PlayerPrefs.GetString("Slot_2") == "Z" && PlayerPrefs.GetString("Slot_3") != "Z")
            {
                NEW_PLAYER_3();
            }
            else if (PlayerPrefs.GetString("Slot_1") == "Z" && PlayerPrefs.GetString("Slot_2") == "Z" && PlayerPrefs.GetString("Slot_3") == "Z" && PlayerPrefs.GetString("Slot_4") != "Z")
            {
                NEW_PLAYER_4();
            }
            else if (PlayerPrefs.GetString("Slot_1") == "Z" && PlayerPrefs.GetString("Slot_2") == "Z" && PlayerPrefs.GetString("Slot_3") == "Z" && PlayerPrefs.GetString("Slot_4") == "Z" && PlayerPrefs.GetString("Slot_5") != "Z")
            {
                NEW_PLAYER_5();
            }
        }
    }

    // Методы создания новых плееров //
    void NEW_PLAYER_1()
    {
        PlayerPrefs.SetString("NamePlayer_1", PlayerPrefs.GetString("NewPlayer"));
        PlayerPrefs.SetInt("ScorePlayer_1", coinsInt);

        Nickname.text = PlayerPrefs.GetString("NamePlayer_1");
        
        NamePlayer_1.text = PlayerPrefs.GetString("NamePlayer_1");
        ScorePlayer_1.text = PlayerPrefs.GetInt("ScorePlayer_1").ToString();
        P1 = true;
        PlayerPrefs.SetString("Slot_1", "Z");
    }
    void NEW_PLAYER_2()
    {
        PlayerPrefs.SetString("NamePlayer_1", PlayerPrefs.GetString("NamePlayer_1"));
        NamePlayer_1.text = PlayerPrefs.GetString("NamePlayer_1");
        PlayerPrefs.SetInt("ScorePlayer_1", PlayerPrefs.GetInt("ScorePlayer_1"));
        ScorePlayer_1.text = PlayerPrefs.GetInt("ScorePlayer_1").ToString();


        PlayerPrefs.SetString("NamePlayer_2", PlayerPrefs.GetString("NewPlayer"));
        PlayerPrefs.SetInt("ScorePlayer_2", coinsInt);

        Nickname.text = PlayerPrefs.GetString("NamePlayer_2");

        NamePlayer_2.text = PlayerPrefs.GetString("NamePlayer_2");
        ScorePlayer_2.text = PlayerPrefs.GetInt("ScorePlayer_2").ToString();
        P2 = true;
        PlayerPrefs.SetString("Slot_2", "Z");
    }
    void NEW_PLAYER_3()
    {
        PlayerPrefs.SetString("NamePlayer_1", PlayerPrefs.GetString("NamePlayer_1"));
        NamePlayer_1.text = PlayerPrefs.GetString("NamePlayer_1");
        PlayerPrefs.SetInt("ScorePlayer_1", PlayerPrefs.GetInt("ScorePlayer_1"));
        ScorePlayer_1.text = PlayerPrefs.GetInt("ScorePlayer_1").ToString();

        PlayerPrefs.SetString("NamePlayer_2", PlayerPrefs.GetString("NamePlayer_2"));
        NamePlayer_2.text = PlayerPrefs.GetString("NamePlayer_2");
        PlayerPrefs.SetInt("ScorePlayer_2", PlayerPrefs.GetInt("ScorePlayer_2"));
        ScorePlayer_2.text = PlayerPrefs.GetInt("ScorePlayer_2").ToString();


        PlayerPrefs.SetString("NamePlayer_3", PlayerPrefs.GetString("NewPlayer"));
        PlayerPrefs.SetInt("ScorePlayer_3", coinsInt);

        Nickname.text = PlayerPrefs.GetString("NamePlayer_3");

        NamePlayer_3.text = PlayerPrefs.GetString("NamePlayer_3");
        ScorePlayer_3.text = PlayerPrefs.GetInt("ScorePlayer_3").ToString();
        P3 = true;
        PlayerPrefs.SetString("Slot_3", "Z");
    }
    void NEW_PLAYER_4()
    {
        PlayerPrefs.SetString("NamePlayer_1", PlayerPrefs.GetString("NamePlayer_1"));
        NamePlayer_1.text = PlayerPrefs.GetString("NamePlayer_1");
        PlayerPrefs.SetInt("ScorePlayer_1", PlayerPrefs.GetInt("ScorePlayer_1"));
        ScorePlayer_1.text = PlayerPrefs.GetInt("ScorePlayer_1").ToString();

        PlayerPrefs.SetString("NamePlayer_2", PlayerPrefs.GetString("NamePlayer_2"));
        NamePlayer_2.text = PlayerPrefs.GetString("NamePlayer_2");
        PlayerPrefs.SetInt("ScorePlayer_2", PlayerPrefs.GetInt("ScorePlayer_2"));
        ScorePlayer_2.text = PlayerPrefs.GetInt("ScorePlayer_2").ToString();

        PlayerPrefs.SetString("NamePlayer_3", PlayerPrefs.GetString("NamePlayer_3"));
        NamePlayer_3.text = PlayerPrefs.GetString("NamePlayer_3");
        PlayerPrefs.SetInt("ScorePlayer_3", PlayerPrefs.GetInt("ScorePlayer_3"));
        ScorePlayer_3.text = PlayerPrefs.GetInt("ScorePlayer_3").ToString();


        PlayerPrefs.SetString("NamePlayer_4", PlayerPrefs.GetString("NewPlayer"));
        PlayerPrefs.SetInt("ScorePlayer_4", coinsInt);

        Nickname.text = PlayerPrefs.GetString("NamePlayer_4");

        NamePlayer_4.text = PlayerPrefs.GetString("NamePlayer_4");
        ScorePlayer_4.text = PlayerPrefs.GetInt("ScorePlayer_4").ToString();
        P4 = true;
        PlayerPrefs.SetString("Slot_4", "Z");
    }
    void NEW_PLAYER_5()
    {
        PlayerPrefs.SetString("NamePlayer_1", PlayerPrefs.GetString("NamePlayer_1"));
        NamePlayer_1.text = PlayerPrefs.GetString("NamePlayer_1");
        PlayerPrefs.SetInt("ScorePlayer_1", PlayerPrefs.GetInt("ScorePlayer_1"));
        ScorePlayer_1.text = PlayerPrefs.GetInt("ScorePlayer_1").ToString();

        PlayerPrefs.SetString("NamePlayer_2", PlayerPrefs.GetString("NamePlayer_2"));
        NamePlayer_2.text = PlayerPrefs.GetString("NamePlayer_2");
        PlayerPrefs.SetInt("ScorePlayer_2", PlayerPrefs.GetInt("ScorePlayer_2"));
        ScorePlayer_2.text = PlayerPrefs.GetInt("ScorePlayer_2").ToString();

        PlayerPrefs.SetString("NamePlayer_3", PlayerPrefs.GetString("NamePlayer_3"));
        NamePlayer_3.text = PlayerPrefs.GetString("NamePlayer_3");
        PlayerPrefs.SetInt("ScorePlayer_3", PlayerPrefs.GetInt("ScorePlayer_3"));
        ScorePlayer_3.text = PlayerPrefs.GetInt("ScorePlayer_3").ToString();

        PlayerPrefs.SetString("NamePlayer_4", PlayerPrefs.GetString("NamePlayer_4"));
        NamePlayer_4.text = PlayerPrefs.GetString("NamePlayer_4");
        PlayerPrefs.SetInt("ScorePlayer_4", PlayerPrefs.GetInt("ScorePlayer_4"));
        ScorePlayer_4.text = PlayerPrefs.GetInt("ScorePlayer_4").ToString();


        PlayerPrefs.SetString("NamePlayer_5", PlayerPrefs.GetString("NewPlayer"));
        PlayerPrefs.SetInt("ScorePlayer_5", coinsInt);

        Nickname.text = PlayerPrefs.GetString("NamePlayer_5");

        NamePlayer_5.text = PlayerPrefs.GetString("NamePlayer_5");
        ScorePlayer_5.text = PlayerPrefs.GetInt("ScorePlayer_5").ToString();
        P5 = true;
        PlayerPrefs.SetString("Slot_5", "Z");
        PlayerPrefs.SetInt("ActivationSort", 1);
    }


    void Save_Data()
    {
        PlayerPrefs.SetString("NamePlayer_1", PlayerPrefs.GetString("NamePlayer_1"));
        NamePlayer_1.text = PlayerPrefs.GetString("NamePlayer_1");
        PlayerPrefs.SetInt("ScorePlayer_1", PlayerPrefs.GetInt("ScorePlayer_1"));
        ScorePlayer_1.text = PlayerPrefs.GetInt("ScorePlayer_1").ToString();

        PlayerPrefs.SetString("NamePlayer_2", PlayerPrefs.GetString("NamePlayer_2"));
        NamePlayer_2.text = PlayerPrefs.GetString("NamePlayer_2");
        PlayerPrefs.SetInt("ScorePlayer_2", PlayerPrefs.GetInt("ScorePlayer_2"));
        ScorePlayer_2.text = PlayerPrefs.GetInt("ScorePlayer_2").ToString();

        PlayerPrefs.SetString("NamePlayer_3", PlayerPrefs.GetString("NamePlayer_3"));
        NamePlayer_3.text = PlayerPrefs.GetString("NamePlayer_3");
        PlayerPrefs.SetInt("ScorePlayer_3", PlayerPrefs.GetInt("ScorePlayer_3"));
        ScorePlayer_3.text = PlayerPrefs.GetInt("ScorePlayer_3").ToString();

        PlayerPrefs.SetString("NamePlayer_4", PlayerPrefs.GetString("NamePlayer_4"));
        NamePlayer_4.text = PlayerPrefs.GetString("NamePlayer_4");
        PlayerPrefs.SetInt("ScorePlayer_4", PlayerPrefs.GetInt("ScorePlayer_4"));
        ScorePlayer_4.text = PlayerPrefs.GetInt("ScorePlayer_4").ToString();

        PlayerPrefs.SetString("NamePlayer_5", PlayerPrefs.GetString("NamePlayer_5"));
        NamePlayer_5.text = PlayerPrefs.GetString("NamePlayer_5");
        PlayerPrefs.SetInt("ScorePlayer_5", PlayerPrefs.GetInt("ScorePlayer_5"));
        ScorePlayer_5.text = PlayerPrefs.GetInt("ScorePlayer_5").ToString();
    } // Метод для сохранения данных всех игроков перед входом существующего игрока

    void Update_All_Data()
    {
        updateNew_1();
        updateNew_2();
        updateNew_3();
        updateNew_4();
        updateNew_5();
    } // Метод в котором присваиваем полям данные активного на данный момент плеера (* не в лидерборде, а игровом моде *)

    void updateNew_1()
    {
        if(P1 == true)
        {
            PlayerPrefs.SetInt("ScorePlayer_1", coinsInt);
            ScorePlayer_1.text = PlayerPrefs.GetInt("ScorePlayer_1").ToString();
        }
    }
    void updateNew_2()
    {
        if (P2 == true)
        {
            PlayerPrefs.SetInt("ScorePlayer_2", coinsInt);
            ScorePlayer_2.text = PlayerPrefs.GetInt("ScorePlayer_2").ToString();
        }
    }
    void updateNew_3()
    {
        if (P3 == true)
        {
            PlayerPrefs.SetInt("ScorePlayer_3", coinsInt);
            ScorePlayer_3.text = PlayerPrefs.GetInt("ScorePlayer_3").ToString();
        }
    }
    void updateNew_4()
    {
        if (P4 == true)
        {
            PlayerPrefs.SetInt("ScorePlayer_4", coinsInt);
            ScorePlayer_4.text = PlayerPrefs.GetInt("ScorePlayer_4").ToString();
        }
    }
    void updateNew_5()
    {
        if (P5 == true)
        {
            PlayerPrefs.SetInt("ScorePlayer_5", coinsInt);
            ScorePlayer_5.text = PlayerPrefs.GetInt("ScorePlayer_5").ToString();
        }
    }


    void ConvertToInt()
    {
        Score_1 = int.Parse(ScorePlayer_1.text);
        Score_2 = int.Parse(ScorePlayer_2.text);
        Score_3 = int.Parse(ScorePlayer_3.text);
        Score_4 = int.Parse(ScorePlayer_4.text);
        Score_5 = int.Parse(ScorePlayer_5.text);
    } // Присваем переменным имеющиеся значения в Лидерборде
    void BubbleSort()
    {
        int[] arrayScore = { Score_1, Score_2, Score_3, Score_4, Score_5 };

        int temp;
        for (int i = 0; i < arrayScore.Length - 1; i++)
        {
            for (int j = i + 1; j < arrayScore.Length; j++)
            {
                if (arrayScore[i] > arrayScore[j])
                {
                    temp = arrayScore[i];
                    arrayScore[i] = arrayScore[j];
                    arrayScore[j] = temp;
                }
            }
        }

        // Присваиваем нашим ТОПАМ значение по рейтингу //

        for (int i = 0; i < arrayScore.Length; i++)
        {
            Top_5 = arrayScore[0];
            Top_4 = arrayScore[1];
            Top_3 = arrayScore[2];
            Top_2 = arrayScore[3];
            Top_1 = arrayScore[4];
        }
    } // Записываем полученные данные из Лидерборда в массив: *arrayScore* // Сортируем его // Присваиваем данные в переменные ТОП

     // Методы проверок кол-ва монет у игроков и установка позиции согласно рейтингу //
    //  Все эти методы запускаются в одном: * Leaderboard_Rating() * //
    void FindTop_1()
    {
        if(Top_1 == Score_1)
        {
            PlayerField_1.position = top_1_Pos;
        }
        else if (Top_1 == Score_2)
        {
            PlayerField_2.position = top_1_Pos;
        }
        else if (Top_1 == Score_3)
        {
            PlayerField_3.position = top_1_Pos;
        }
        else if (Top_1 == Score_4)
        {
            PlayerField_4.position = top_1_Pos;
        }
        else if (Top_1 == Score_5)
        {
            PlayerField_5.position = top_1_Pos;
        }
    }
    void FindTop_2()
    {
        if (Top_2 == Score_1)
        {
            PlayerField_1.position = top_2_Pos;
        }
        else if (Top_2 == Score_2)
        {
            PlayerField_2.position = top_2_Pos;
        }
        else if (Top_2 == Score_3)
        {
            PlayerField_3.position = top_2_Pos;
        }
        else if (Top_2 == Score_4)
        {
            PlayerField_4.position = top_2_Pos;
        }
        else if (Top_2 == Score_5)
        {
            PlayerField_5.position = top_2_Pos;
        }
    }
    void FindTop_3()
    {
        if (Top_3 == Score_1)
        {
            PlayerField_1.position = top_3_Pos;
        }
        else if (Top_3 == Score_2)
        {
            PlayerField_2.position = top_3_Pos;
        }
        else if (Top_3 == Score_3)
        {
            PlayerField_3.position = top_3_Pos;
        }
        else if (Top_3 == Score_4)
        {
            PlayerField_4.position = top_3_Pos;
        }
        else if (Top_3 == Score_5)
        {
            PlayerField_5.position = top_3_Pos;
        }
    }
    void FindTop_4()
    {
        if (Top_4 == Score_1)
        {
            PlayerField_1.position = top_4_Pos;
        }
        else if (Top_4 == Score_2)
        {
            PlayerField_2.position = top_4_Pos;
        }
        else if (Top_4 == Score_3)
        {
            PlayerField_3.position = top_4_Pos;
        }
        else if (Top_4 == Score_4)
        {
            PlayerField_4.position = top_4_Pos;
        }
        else if (Top_4 == Score_5)
        {
            PlayerField_5.position = top_4_Pos;
        }
    }
    void FindTop_5()
    {
        if (Top_5 == Score_1)
        {
            PlayerField_1.position = top_5_Pos;
        }
        else if (Top_5 == Score_2)
        {
            PlayerField_2.position = top_5_Pos;
        }
        else if (Top_5 == Score_3)
        {
            PlayerField_3.position = top_5_Pos;
        }
        else if (Top_5 == Score_4)
        {
            PlayerField_4.position = top_5_Pos;
        }
        else if (Top_5 == Score_5)
        {
            PlayerField_5.position = top_5_Pos;
        }
    }

    void Leaderboard_Rating()
    {
        FindTop_1();
        FindTop_2();
        FindTop_3();
        FindTop_4();
        FindTop_5();
    }

    void ActivationSort()
    {
        ConvertToInt();         // Метод получения данных из Лидерборда...
        BubbleSort();          //  Метод сортировки и записи в ТОПЫ...
        Leaderboard_Rating(); //   Делаем постоянную проверку и обновляем рейтинг в Лидерборде...
    }
    void ActivationAfterPlayer_5()
    {
        if(PlayerPrefs.GetInt("ActivationSort") == 1)
        {
            ActivationSort();
        }
    }
}
