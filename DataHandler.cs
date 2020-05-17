using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataHandler : MonoBehaviour
{

    public int carSel;

    void Awake()
    {

        GameObject.DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {

        carSel = 1;
    }

    public void selectCar(int a)
    {

        carSel = a;
    }
}
