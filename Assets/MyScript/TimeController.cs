using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class TimeController : MonoBehaviour {
    public DateTime tiempoActual;
    public DateTime tiempoDesconexion, tiempoFinal;
    public Text texto;
    private bool funcionar;
	// Use this for initialization
	void Start () {
        //texto = GameObject.Find("Timer").GetComponent<Text>();
        if(ZPlayerPrefs.HasKey("Tiempo " + GetInstanceID())){
            tiempoActual = Convert.ToDateTime( ZPlayerPrefs.GetString("Tiempo " + GetInstanceID()));
        }
        else
        {
            tiempoActual = DateTime.Now;
            ZPlayerPrefs.SetString("Tiempo " + GetInstanceID(), tiempoActual.ToString());
        }
        tiempoFinal = tiempoActual.AddHours(5D);
        tiempoDesconexion = DateTime.Now;
        if(tiempoDesconexion >= tiempoFinal)
        {
            funcionar = true;
        }
        //tiempoRestante = tiempoActual - tiempoDesconexion;
        //texto.text = tiempoActual.ToString();
        //DateTime tiempo = DateTime.ParseExact(tiempoRestante, "ddMMyyyy", CultureInfo.InvariantCulture);
        //tiempoRestante.ToString();
        Debug.Log(tiempoActual);
        Debug.Log(tiempoFinal);
        Debug.Log(tiempoDesconexion);
	}
	
	// Update is called once per frame
	void Update () {
        tiempoDesconexion = DateTime.Now;
        if (tiempoDesconexion >= tiempoFinal)
        {
            funcionar = true;
        }
    }
}
