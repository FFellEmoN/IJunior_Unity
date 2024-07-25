using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomNumber : MonoBehaviour
{
    private readonly Random _random = new Random();
    private Random _random = new Random();
    public int GetRandomNumber(int number)
    {
        return _random.Next(number);
    }

}
