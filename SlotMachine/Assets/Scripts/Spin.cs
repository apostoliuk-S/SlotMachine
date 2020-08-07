using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spin : MonoBehaviour
{
    public Button GO;

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

    // Игровой таймер + возможные комбинации //
    float timer;
    float[] posY = { -8.2f, -4f, 0.5f, 4.5f, 8.5f };


    void Start()
    {
        // Задаем случайную скорость для спинов //
        speedSpin_1 = Random.Range(25f, 60f);
        speedSpin_2 = Random.Range(25f, 60f);
        speedSpin_3 = Random.Range(25f, 60f);

        // Задаем стартовую позицию спинов //
        startPos_1 = new Vector3(-4.6f, -8.2f, 0);
        startPos_2 = new Vector3(0, -8.2f, 0);
        startPos_3 = new Vector3(4.6f, -8.2f, 0);

        // Задаем случайное выпадение символа //
        var rand_1 = Random.Range(0, posY.Length);
        stopPos_1 = new Vector3(-4.6f, posY[rand_1], 0);

        var rand_2 = Random.Range(0, posY.Length);
        stopPos_2 = new Vector3(0, posY[rand_2], 0);

        var rand_3 = Random.Range(0, posY.Length);
        stopPos_3 = new Vector3(4.6f, posY[rand_3], 0);

    }

    void Update()
    {
        timer += Time.deltaTime; // Наш игровой таймер...

        // Запуск всех барабанов в едином методе //
        StartAllSpin();
    }
    public void ResetTimer()
    {
        timer = 0;

        var rand_1 = Random.Range(0, posY.Length);
        stopPos_1 = new Vector3(-4.6f, posY[rand_1], 0);

        var rand_2 = Random.Range(0, posY.Length);
        stopPos_2 = new Vector3(0, posY[rand_2], 0);

        var rand_3 = Random.Range(0, posY.Length);
        stopPos_3 = new Vector3(4.6f, posY[rand_3], 0);

    }

    #region // Запуск вращения всех барабанов //
    public void StartAllSpin()
    {
        Start_1();
        Invoke("Start_2", 0.4f);
        Invoke("Start_3", 0.4f);
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

}
