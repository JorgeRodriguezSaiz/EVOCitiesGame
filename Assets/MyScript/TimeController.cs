using UnityEngine;
using System.Collections;
using System;

public class TimeController : MonoBehaviour {
    public DateTime tiempoActual;
    public DateTime tiempoDesconexion;
    public TimeSpan tiempoRestante;
	// Use this for initialization
	void Start () {
        tiempoActual = DateTime.Now;
        tiempoRestante = tiempoActual - tiempoDesconexion;
        Debug.Log(tiempoRestante);
        Debug.Log(tiempoActual);
	}
	
	// Update is called once per frame
	void Update () {
        tiempoDesconexion = DateTime.Now;
	}
}
