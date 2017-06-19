using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public class SilitoComidita : MonoBehaviour {
    public GameObject siloWindow;
    public float comidaMax = 1000000;
    public Slider barraComida;
    public Text semillasManzanas;
    public Text semillasFrambuesa;
    public Text manzanas;
    public Text frambuesas;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        barraComida.value = GameObject.Find("Controller").GetComponent<GestionRecursos>().comida / comidaMax;
        semillasManzanas.text = Convert.ToString(GameObject.Find("Controller").GetComponent<GestionRecursos>().semillasManzanas);
        semillasFrambuesa.text = Convert.ToString(GameObject.Find("Controller").GetComponent<GestionRecursos>().semillasManzanas);
        manzanas.text = Convert.ToString(GameObject.Find("Controller").GetComponent<GestionRecursos>().manzanas);
        frambuesas.text = Convert.ToString(GameObject.Find("Controller").GetComponent<GestionRecursos>().frambuesas);
    }
    void OnMouseUp()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
            siloWindow.SetActive(true);
    }
}
