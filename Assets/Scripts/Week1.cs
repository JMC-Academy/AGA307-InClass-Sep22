using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

public class Week1 : MonoBehaviour
{
    public int numberOne;
    public int numberTwo;
    public int numberThree;

    public int health = 100;
    public int bonus = 50;

    public GameObject go;
    public Camera cam;
    Light ourLight;

    void Start()
    {
        ourLight = go.GetComponent<Light>();
        ourLight.color = Color.green;

        float zPos = go.transform.position.z;

        AddNumbers(numberOne, numberTwo);
        AddNumbers(numberOne, numberThree);
        AddNumbers(numberTwo, numberThree);
        health = AddValues(health, bonus);
        //Debug.Log(health);
    }

    int AddValues(int _one, int _two)
    {
        return _one + _two;
    }

    /// <summary>
    /// Use this to add two numbers together
    /// </summary>
    /// <param name="_one">The first number</param>
    /// <param name="_two">The second number</param>
    void AddNumbers(int _one, int _two)
    {
        int answer = _one + _two;
        //Debug.Log(answer);
    }
}
