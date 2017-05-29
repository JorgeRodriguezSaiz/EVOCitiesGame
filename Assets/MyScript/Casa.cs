using UnityEngine;
using System.Collections;
using System;

public class Casa : MonoBehaviour {
    [Header ("Experiencia")]
    public float exp = 75f;
    public TimeSpan tiempoRestante;
    public DateTime tiempoActual;
    public DateTime tiempoDesconexion, tiempoFinal;
    private bool funcionar = false;
    // Use this for initialization
    void Start () {
        //ZPlayerPrefs.DeleteAll();
       
        if (!funcionar)
        {
            if (ZPlayerPrefs.HasKey("Tiempo " + GetInstanceID()))
            {
                tiempoActual = Convert.ToDateTime(ZPlayerPrefs.GetString("Tiempo " + GetInstanceID()));
            }
            else
            {
                tiempoActual = DateTime.Now;
                ZPlayerPrefs.SetString("Tiempo " + GetInstanceID(), tiempoActual.ToString());
            }
            //tiempoFinal = tiempoActual.AddMinutes(5D);
            tiempoFinal = tiempoActual.AddSeconds(20D);
            tiempoDesconexion = DateTime.Now;
            tiempoRestante = tiempoFinal - tiempoDesconexion;
        }
        if (tiempoDesconexion >= tiempoFinal)
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            funcionar = true;
            this.enabled = false;
        }
    }
	
	// Update is called once per frame
	void Update () {
        tiempoDesconexion = DateTime.Now;
        if (!GameObject.Find("Controller").GetComponent<Construction>().modoConstruccion)
        {
            if (tiempoDesconexion >= tiempoFinal)
            {
                funcionar = true;
                gameObject.transform.GetChild(0).gameObject.SetActive(false);
                GameObject.Find("God").GetComponent<Exp_controller>().exp += this.exp;
                this.enabled = false;
            }
            if (!funcionar)
            {
                string aux = string.Format("{0:D2}:{1:D2}:{2:D2}:{3:D2}", tiempoRestante.Days, tiempoRestante.Hours, tiempoRestante.Minutes, tiempoRestante.Seconds);
                gameObject.GetComponentInChildren<TextMesh>().text = aux;
                float tAux = (float)tiempoRestante.TotalSeconds;
                tAux -= 1 * Time.deltaTime;
                tiempoRestante = TimeSpan.FromSeconds(tAux);
            }
        }
    }
}
