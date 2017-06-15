﻿using UnityEngine;
using System.Collections;


public class CrearCasasGuardadas : MonoBehaviour
{
    public GameObject[] prefabsConstruccion;
    public bool armagedon;
    // Use this for initialization
    private void Awake()
    {
        if (armagedon)
            ZPlayerPrefs.DeleteAll();
        else
            StartCoroutine(Wait());
    }
    IEnumerator Wait()
    {
        yield return new WaitForSecondsRealtime(0.01f);
        if (ZPlayerPrefs.HasKey("cantidadConstrucciones"))
        {
            Debug.Log(ZPlayerPrefs.GetInt("cantidadConstrucciones"));
            for (int i = 0; i <= ZPlayerPrefs.GetInt("cantidadConstrucciones"); i++)
            {
                if (prefabsConstruccion[ZPlayerPrefs.GetInt("tipoConstruccion" + i)].tag == "trabajo")
                {
                    prefabsConstruccion[ZPlayerPrefs.GetInt("tipoConstruccion" + i)].GetComponent<Trabajos>().numbConstruccion = i;
                }
                else if (prefabsConstruccion[ZPlayerPrefs.GetInt("tipoConstruccion" + i)].tag == "casa")
                {
                    prefabsConstruccion[ZPlayerPrefs.GetInt("tipoConstruccion" + i)].GetComponent<Casa>().numbConstruccion = i;
                }
                prefabsConstruccion[ZPlayerPrefs.GetInt("tipoConstruccion" + i)].transform.position =
                    new Vector3(ZPlayerPrefs.GetFloat("posX" + i), ZPlayerPrefs.GetFloat("posY" + i), ZPlayerPrefs.GetFloat("posZ" + i));
                Instantiate(prefabsConstruccion[ZPlayerPrefs.GetInt("tipoConstruccion" + i)]);
            }
        }
        else
        {
            ZPlayerPrefs.SetInt("cantidadConstrucciones", -1);
        }

    }
}
