using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SiloSida : MonoBehaviour
{
    public GameObject siloWindow;
    public float recursosMax = 1000000;
    public Scrollbar barraSida;
    private float recursosTot;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        recursosTot = GameObject.Find("Controller").GetComponent<GestionRecursos>().madera + GameObject.Find("Controller").GetComponent<GestionRecursos>().piedra;
        barraSida.value =  recursosTot/ recursosMax;
    }
    void OnMouseDown()
    {
        siloWindow.SetActive(true);
    }
}
