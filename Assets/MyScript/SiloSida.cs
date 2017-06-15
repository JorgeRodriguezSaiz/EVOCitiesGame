using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class SiloSida : MonoBehaviour
{
    public GameObject siloWindow;
    public float recursosMax = 1000000;
    public Slider barraSida;
    private float recursosTot;
    public Text madera;
    public Text piedra;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        recursosTot = GameObject.Find("Controller").GetComponent<GestionRecursos>().madera + GameObject.Find("Controller").GetComponent<GestionRecursos>().piedra;
        barraSida.value =  recursosTot/ recursosMax;
        madera.text = Convert.ToString(GameObject.Find("Controller").GetComponent<GestionRecursos>().madera);
        piedra.text = Convert.ToString(GameObject.Find("Controller").GetComponent<GestionRecursos>().piedra);
    }
    void OnMouseDown()
    {
        siloWindow.SetActive(true);
    }
}
