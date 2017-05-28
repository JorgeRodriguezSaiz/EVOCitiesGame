using UnityEngine;
using System.Collections;
using LitJson;
using System.IO;
using System.Text;
using System;
using System.Security.Cryptography;

public class Inicilización_Recursos : MonoBehaviour {
    public int [] recurso = new int[10];
    public int monedas;
    public int diamantes;
    public int comida;
    private bool one = true;
    // Use this for initialization
    void Start () {
        ZPlayerPrefs.Initialize("#45sf@5f#", "S@ALT&KEY");
        if (ZPlayerPrefs.HasKey("comida"))
        {
            for (int i = 0; i < recurso.Length; i++)
            {
                recurso[i] = ZPlayerPrefs.GetInt("recursos" + i);
            }

            comida = ZPlayerPrefs.GetInt("comida");
            monedas = ZPlayerPrefs.GetInt("monedas");
            diamantes = ZPlayerPrefs.GetInt("diamantes");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (one)
        {
            one = false;
            StartCoroutine(SaveWait());
        }
    }
    public void Guardado()
    {
        for (int i = 0; i < recurso.Length; i++)
        {
            //ZPlayerPrefs.SetInt("recursos" + i, recurso[i]);
        }
        ZPlayerPrefs.SetInt("comida", comida);
        ZPlayerPrefs.SetInt("monedas",monedas);
        ZPlayerPrefs.SetInt("diamantes", diamantes);
        ///ZPlayerPrefs.Save();
    }
    public void Guardado2()
    {
        for (int i = 0; i < recurso.Length; i++)
        {
            ZPlayerPrefs.SetInt("recursos" + i, recurso[i]);
        }
        ///ZPlayerPrefs.Save();
    }
    IEnumerator SaveWait()
    {
        yield return new WaitForSecondsRealtime(1f);
        Debug.Log("Guardado");
        /*Guardado();
        for (int i = 0; i < recurso.Length; i++)
        {
            yield return new WaitForSecondsRealtime(0.2f);
            ZPlayerPrefs.SetInt("recursos" + i, recurso[i]);
        }
        //Guardado2();
        ZPlayerPrefs.Save();
        one = true;*/
    }
}
