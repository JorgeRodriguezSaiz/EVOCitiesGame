using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class GestionRecursos : MonoBehaviour {
    [Header("Recursos")]
    public float madera = 0;
    public float piedra = 0;
    public float poblacion = 1f;
    public float poblacionTotal = 10f;
    public float comida;
    [Header("Textos")]
    public Text textoPoblacion;
    public Text textoPoblacionTotal;
    public Text comidaText;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        textoPoblacion.text = Convert.ToString(poblacion) + "/";
        textoPoblacionTotal.text = Convert.ToString(poblacionTotal);
        comidaText.text = Convert.ToString(comida);
    }
}
