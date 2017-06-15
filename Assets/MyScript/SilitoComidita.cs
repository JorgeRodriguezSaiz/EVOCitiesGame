using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SilitoComidita : MonoBehaviour {
    public GameObject siloWindow;
    public float comidaMax = 1000000;
    public Scrollbar barraComida;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        barraComida.value = GameObject.Find("Controller").GetComponent<GestionRecursos>().comida / comidaMax;
	}
    void OnMouseDown()
    {
        siloWindow.SetActive(true);
    }
}
