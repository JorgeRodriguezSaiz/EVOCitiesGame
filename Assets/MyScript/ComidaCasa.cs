using UnityEngine;
using System.Collections;

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
        if (minaActual && !minaActual.GetComponent<Trabajos>().finTrabajo)
        {
            comidaCasa -= minaActual.GetComponent<Trabajos>().primeraOpcion.GetComponent<DatosTrabajo>().trabajadoresNecesita;
            minaActual = null;
        }
        if(comidaCasa <= 0)
        {
            isComida = false;
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
        isComida = true;
    }
}
