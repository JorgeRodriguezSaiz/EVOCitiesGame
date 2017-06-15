using UnityEngine;
using System.Collections;


public class DatosTrabajo : MonoBehaviour
{
    public int trabajadoresNecesita;
    public string[] recursos;
    public int tipoTrabajo;
    public int[] cantidadRecursos;
    public float tiempoTrabajo;
    public float recursoNecesitan = 0;
    public string recursoNecesario = "gold";
    public GameObject trabajo;
    // Use this for initialization
    public void PonerATrabajar()
    {
        if (trabajadoresNecesita <= GameObject.Find("Controller").GetComponent<GestionRecursos>().poblacion && recursoNecesitan <= ZPlayerPrefs.GetFloat(recursoNecesario))
        {
            trabajo.GetComponent<Trabajos>().Trabajar(tipoTrabajo);
            ZPlayerPrefs.SetFloat(recursoNecesario, ZPlayerPrefs.GetFloat(recursoNecesario) - recursoNecesitan);
            for (int i = 0; i < GameObject.Find("Controller").GetComponent<InterfazTrabajoIn>().primeraOpcion.Length; i++)
            {
                GameObject.Find("Controller").GetComponent<InterfazTrabajoIn>().primeraOpcion[i].gameObject.SetActive(false);
            }
            GameObject.Find("Controller").GetComponent<InterfazTrabajoIn>().interfaz.SetActive(false);
        }
    }
}

