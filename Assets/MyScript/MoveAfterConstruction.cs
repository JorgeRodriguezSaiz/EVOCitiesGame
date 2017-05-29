using UnityEngine;
using System.Collections;

public class MoveAfterConstruction : MonoBehaviour {
    public float tiempoParaMover = 1.5f;
    private float timeCounter = 0;
    public GameObject controller;
    public GameObject aceptarConstruccion;
    public GameObject barraExp;
    public GameObject tienda;
    public GameObject ajustes;
    public GameObject construir;
    // Use this for initialization
    void Start () {
        controller = GameObject.Find("Controller");
        //aceptarConstruccion = GameObject.Find("AceptarConstruction");
    }
	
	// Update is called once per frame
	void Update () {
        
        if (Input.GetMouseButton(0))
        {
            timeCounter += Time.deltaTime;
            if(timeCounter > tiempoParaMover)
            {
                controller.GetComponent<Construction>().modoConstruccion = true;
                aceptarConstruccion.SetActive(true);
                barraExp.SetActive(false);
                tienda.SetActive(false);
                ajustes.SetActive(false);
                construir.SetActive(false);
            }
        }
        else
        {
            timeCounter = 0;
        }
	}
    void OnMouseDown()
    {
        controller.GetComponent<Construction>().instancia = gameObject;
        //barraExp = GameObject.Find("/UI/BarraExp");
        //tienda = GameObject.Find("Tienda");
        //ajustes = GameObject.Find("Ajustes");
        //construir = GameObject.Find("Construir");
    }
}
