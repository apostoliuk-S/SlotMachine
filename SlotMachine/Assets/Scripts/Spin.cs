using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spin : MonoBehaviour
{
    public Button GO;
    public Button ButtonBet_5;
    public Button ButtonBet_10;
    public Button ButtonBet_20;
    public Button ButtonBet_50;

    public Transform row_1;
    public Transform row_2;
    public Transform row_3;

    [SerializeField] private float speedSpin_1;
    [SerializeField] private float speedSpin_2;
    [SerializeField] private float speedSpin_3;

    [SerializeField] private Vector3 startPos_1;
    [SerializeField] private Vector3 startPos_2;
    [SerializeField] private Vector3 startPos_3;

    [SerializeField] private Vector3 stopPos_1;
    [SerializeField] private Vector3 stopPos_2;
    [SerializeField] private Vector3 stopPos_3;

    [SerializeField] int x;
    [SerializeField] int y;
    [SerializeField] int z;

    public ParticleSystem ps;
    [SerializeField] private bool workMainMethod;

    public Text betValue;
    public Text coinsText;
    public Text gain;

    [SerializeField] private int coinsInt;

    public GameObject CreditPanel;
    public GameObject winPanel;

    // Игровой таймер + возможные комбинации //
    float timer;
    float[] posY = { -8.2f, -4f, 0.5f, 4.5f, 8.5f };

    [SerializeField] private int bet;
    [SerializeField] private int tmp;


    void Start()
    {
        GO.interactable = false;
        workMainMethod = false;
        ps.Stop();
        coinsInt = 50;              // Задаем стартовое кол-во очков...

        
        // Задаем стартовую позицию спинов //
        startPos_1 = new Vector3(-4.6f, -8.2f, 0);
        startPos_2 = new Vector3(0, -8.2f, 0);
        startPos_3 = new Vector3(4.6f, -8.2f, 0);
    }


    void Update()
    {
        // Приведение типов //
        gain.text = tmp.ToString();
        coinsText.text = coinsInt.ToString();
        betValue.text = bet.ToString();

        timer += Time.deltaTime; // Наш игровой таймер...

        StartAllSpin();   // Запуск всех барабанов в едином методе...
        ACT();

        StartCoroutine(HHH(1, 10, 2));

    }



    public void ButtonGO()
    {
        // Каждый раз обновляем случайное выпадение символа по клику кнопки GO //
        var rand_1 = Random.Range(0, posY.Length);
        stopPos_1 = new Vector3(-4.6f, posY[rand_1], 0);
        for(int i = 0; i <= rand_1; i++)
        {
            if(i == 0)
            {
                x = 1;
            }
            x = i + 1;
        }

        var rand_2 = Random.Range(0, posY.Length);
        stopPos_2 = new Vector3(0, posY[rand_2], 0);
        for (int i = 0; i <= rand_2; i++)
        {
            if (i == 0)
            {
                y = 1;
            }
            y = i + 1;
        }

        var rand_3 = Random.Range(0, posY.Length);
        stopPos_3 = new Vector3(4.6f, posY[rand_3], 0);
        for (int i = 0; i <= rand_3; i++)
        {
            if (i == 0)
            {
                z = 1;
            }
            z = i + 1;
        }

        // Обновляем случайную скорость по клику кнопки GO //
        speedSpin_1 = Random.Range(25f, 50f);
        speedSpin_2 = Random.Range(25f, 50f);
        speedSpin_3 = Random.Range(25f, 50f);

        timer = 0;
        workMainMethod = true;

        coinsInt -= bet;
        DeleteCredit();
        Invoke("CheckCredit", 5.5f);


        CheckWinning();
    }

    void ACT()
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

    void DisableBetButton()
    {
        ButtonBet_5.interactable = false;
        ButtonBet_10.interactable = false;
        ButtonBet_20.interactable = false;
        ButtonBet_50.interactable = false;
    }
    void EnableBetButton()
    {
        ButtonBet_5.interactable = true;
        ButtonBet_10.interactable = true;
        ButtonBet_20.interactable = true;
        ButtonBet_50.interactable = true;
    }
    void CheckCredit()
    {
        if(coinsInt < 0)
        {
            CreditPanel.SetActive(true);
            coinsText.color = Color.red;
        }
    }
    void DeleteCredit()
    {
        if (coinsInt > 0)
        {
            CreditPanel.SetActive(false);
            coinsText.color = Color.yellow;
        }
    }
    void CheckWinning()
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


    IEnumerator ShowWinPanel()
    {
        yield return new WaitForSeconds(5.5f);
        winPanel.SetActive(true);
    }
    IEnumerator FlyCoins()
    {
        ps.Play();
        yield return new WaitForSeconds(2.5f);
        ps.Stop();
        winPanel.SetActive(false);
    }
    IEnumerator HHH(float startVal, float endVal, float duration)
    {
        float elapsed = 0;
        float nextVal;
        while(elapsed < duration)
        {
            nextVal = Mathf.Lerp(startVal, endVal, elapsed / duration);
            coinsInt += 1;
            elapsed += Time.deltaTime;
            yield return null;
        }
    }

    public void CloseWinPanel()
    {
        StartCoroutine("FlyCoins");
    }
}
