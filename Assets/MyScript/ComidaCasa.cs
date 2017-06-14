﻿using UnityEngine;
using System.Collections;
using Assets.UltimateIsometricToolkit.Scripts.Core;

public class ComidaCasa : MonoBehaviour {
    public float comidaCasa;
    public float comidaCasaTotal;
    public bool isComida = true;
    public GameObject minaActual;
    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (minaActual && !minaActual.GetComponent<Trabajos>().trabajando)
        {
            comidaCasa = 0;
            minaActual = null;
        }
        if(comidaCasa <= 0)
        {
            isComida = false;
        }
        /*if (gameObject.GetComponent<Casa>().enabled == false && comidaCasa > 0)
        {
            comidaCasa -= (float)0.1 * Time.deltaTime;
        }*/
	}
    public void OnMouseDown()
    {
        if (comidaCasaTotal - comidaCasa > 1)
        {
            GameObject.Find("Controller").GetComponent<GestionRecursos>().manzanas -= (int)(comidaCasaTotal - comidaCasa);
            ZPlayerPrefs.SetFloat("manzanas", GameObject.Find("Controller").GetComponent<GestionRecursos>().manzanas);
            comidaCasa = comidaCasaTotal;
            isComida = true;
        }
    }
}
