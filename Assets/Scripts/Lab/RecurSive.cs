using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecurSive : MonoBehaviour
{

    void Start()
    {
        
    }

    
    void Update()
    {
        HaNoiTower(5, 'A', 'B', 'C');
    }

    int Factoryal(int number)
    {
        if (number <= 1)
        {
            return 1;
        }
        else
        {
            return number * Factoryal(number - 1);
        }
    }
    int Fibonacci(int number)
    {
        if ( number <= 1)
        {
            return number;
        }
        else
        {
            Debug.Log(number);
            return Fibonacci(number - 1) + Fibonacci(number - 2);
        }
    }
    int Sum(int number)
    {
        if (number <= 1)
        {
            return number;
        }
        else
        {
            return number + Sum(number - 1);
        }
    }
    string Reverse(string s)
    {
        if (s.Length <= 1)
        {
            return s;
        }
        else
        {
            return Reverse(s.Substring(1)) + s[0];
        }
    }
    void HaNoiTower(int number, char cotNguon, char cotTrungGian, char cotDich)
    {
        if (number == 1)
        {
            Debug.Log($"Di chuyển đĩa từ {cotNguon} sang {cotDich}");
            return;
        }
        else
        {
            HaNoiTower(number - 1, cotNguon, cotDich, cotTrungGian);

            Debug.Log($"Di chuyển đĩa {number} từ cột {cotNguon} sang {cotDich}");

            HaNoiTower(number - 1, cotTrungGian, cotNguon, cotDich);
        }
    }
}
