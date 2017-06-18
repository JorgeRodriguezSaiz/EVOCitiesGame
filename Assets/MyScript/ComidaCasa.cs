﻿using UnityEngine;
using System.Collections;

public class ComidaCasa : MonoBehaviour
{
    public float comidaCasa;
    public float comidaCasaTotal;
    public bool isComida = true;
    public GameObject minaActual;
    // Use this for initialization
    void Start()
    {
        StartCoroutine(Wait());
    }

    // Update is called once per frame
    void Update()
    {
        if (minaActual && !minaActual.GetComponent<Trabajos>().finTrabajo)
        {
            comidaCasa -= minaActual.GetComponent<Trabajos>().primeraOpcion.GetComponent<DatosTrabajo>().trabajadoresNecesita;
            ZPlayerPrefs.SetFloat("comidaCasa "+ gameObject.GetComponent<Casa>().numbConstruccion, comidaCasa);
            minaActual = null;
        }
        if (comidaCasa <= 0)
        {
            isComida = false;
            gameObject.transform.GetChild(4).gameObject.SetActive(true);
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
        if (comidaCasa < comidaCasaTotal)
        {
            gameObject.transform.GetChild(3).gameObject.SetActive(true);
        }
        /*if (gameObject.GetComponent<Casa>().enabled == false && comidaCasa > 0)
        {
            comidaCasa -= (float)0.1 * Time.deltaTime;
        }*/
    }
    public void OnMouseDown()
    {
        comidaCasa = comidaCasaTotal;
        ZPlayerPrefs.SetFloat("comidaCasa " + gameObject.GetComponent<Casa>().numbConstruccion, comidaCasa);
        isComida = true;
        gameObject.transform.GetChild(4).gameObject.SetActive(false);
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        gameObject.transform.GetChild(3).gameObject.SetActive(false);
    }
    IEnumerator Wait()
    {
        yield return new WaitForSecondsRealtime(0.2f);
        if (ZPlayerPrefs.HasKey("comidaCasa " + gameObject.GetComponent<Casa>().numbConstruccion))
        {
            comidaCasa = ZPlayerPrefs.GetFloat("comidaCasa " + gameObject.GetComponent<Casa>().numbConstruccion);
        }
        else
        {
            comidaCasa = comidaCasaTotal;
        }
    }
}
