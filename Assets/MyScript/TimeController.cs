using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class TimeController : MonoBehaviour {
    public DateTime tiempoActual;
    public DateTime tiempoDesconexion;
    //public DateTime tPrueba = new DateTime(2017, 5, 28, 18, 00, 00);
    public TimeSpan tiempoRestante;
    public Text texto;
	// Use this for initialization
	void Start () {
        texto = GameObject.Find("Timer").GetComponent<Text>();
        tiempoActual = DateTime.Now;
        tiempoRestante = tiempoActual - tiempoDesconexion;
        texto.text = tiempoActual.ToString();
        //DateTime tiempo = DateTime.ParseExact(tiempoRestante, "ddMMyyyy", CultureInfo.InvariantCulture);
        tiempoRestante.ToString();
        Debug.Log(tiempoRestante);
        Debug.Log(tiempoActual);
        Debug.Log(tiempoDesconexion);
	}
	
	// Update is called once per frame
	void Update () {
        tiempoDesconexion = DateTime.Now;
        tiempoDesconexion.ToString();

	}
}
