using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class TimeController : MonoBehaviour {
    public DateTime tiempoActual;
    public DateTime tiempoDesconexion;
    public TimeSpan tiempoRestante;
    public Text texto;
	// Use this for initialization
	void Start () {
        texto = GameObject.Find("Timer").GetComponent<Text>();
        tiempoActual = DateTime.Now;
        tiempoRestante = tiempoActual - tiempoDesconexion;
        texto.text = tiempoActual.ToString();
        Debug.Log(tiempoRestante);
        Debug.Log(tiempoActual);
	}
	
	// Update is called once per frame
	void Update () {
        tiempoDesconexion = DateTime.Now;
	}
}
