﻿using UnityEngine;
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
    public float manzanas;
    public float gold = 100;
    public float semillasManzanas = 0;
    [Header("Textos")]
    public Text textoPoblacion;
    public Text textoPoblacionTotal;
    public Text comidaText;
    public Text dineroText;
    // Use this for initialization
    void Start () {
        if (ZPlayerPrefs.HasKey("poblacionTotal"))
        {
            poblacionTotal = ZPlayerPrefs.GetFloat("poblacionTotal");
            poblacion = ZPlayerPrefs.GetFloat("poblacion");
            piedra = ZPlayerPrefs.GetFloat("piedra");
            madera = ZPlayerPrefs.GetFloat("madera");
            comida = ZPlayerPrefs.GetFloat("comida");
            manzanas = ZPlayerPrefs.GetFloat("manzanas");
            gold = ZPlayerPrefs.GetFloat("gold");
            semillasManzanas = ZPlayerPrefs.GetFloat("semillasManzanas");
        }
        else
        {
            ZPlayerPrefs.SetFloat("poblacionTotal", poblacionTotal);
            ZPlayerPrefs.SetFloat("poblacion", poblacion);
            ZPlayerPrefs.SetFloat("madera", madera);
            ZPlayerPrefs.SetFloat("piedra", piedra);
            ZPlayerPrefs.SetFloat("comida", comida);
            ZPlayerPrefs.SetFloat("manzanas", manzanas);
            ZPlayerPrefs.SetFloat("gold", gold);
            ZPlayerPrefs.SetFloat("semillasManzanas", semillasManzanas);
        }
	}
	
	// Update is called once per frame
	void Update () {
        textoPoblacion.text = Convert.ToString(poblacion) + "/";
        textoPoblacionTotal.text = Convert.ToString(poblacionTotal);
        comidaText.text = Convert.ToString(manzanas);
        dineroText.text = Convert.ToString((int)gold);
        if(ZPlayerPrefs.GetFloat("madera") != madera)
        {
            madera = ZPlayerPrefs.GetFloat("madera");
        }
        if (ZPlayerPrefs.GetFloat("manzanas") != manzanas)
        {
            manzanas = ZPlayerPrefs.GetFloat("manzanas");
        }
        if (ZPlayerPrefs.GetFloat("piedra") != piedra)
        {
            piedra = ZPlayerPrefs.GetFloat("piedra");
        }
        if (ZPlayerPrefs.GetFloat("comida") != comida)
        {
            comida = ZPlayerPrefs.GetFloat("comida");
        }
        if (ZPlayerPrefs.GetFloat("comida") != comida)
        {
            comida = ZPlayerPrefs.GetFloat("comida");
        }
        if (ZPlayerPrefs.GetFloat("semillasManzanas") != semillasManzanas)
        {
            semillasManzanas = ZPlayerPrefs.GetFloat("semillasManzanas");
        }
    }
}
