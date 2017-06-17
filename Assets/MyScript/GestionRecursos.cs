using UnityEngine;
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
    public float variscita;
    public float carbon;
    public float arcilla;
    public float jarronBasico;
    public float jarronDecorado;
    public float frambuesas;
    public float diamantes = 5;
    public float gold = 100;
    public float semillasManzanas = 0;
    public float semillasFrambuesa = 0;
    [Header("Textos")]
    public Text textoPoblacion;
    public Text textoPoblacionTotal;
    public Text comidaText;
    public Text dineroText;
    public Text dineroTextConstruir;
    public Text diamantesText;
    public Text diamantesTextConstruir;

    private bool guardado;
    // Use this for initialization
    void Start () {
        ZPlayerPrefs.Initialize("#45sf@5f#", "S@ALT&KEY");
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
            semillasFrambuesa = ZPlayerPrefs.GetFloat("semillasFrambuesa");
            frambuesas = ZPlayerPrefs.GetFloat("frambuesas");
            variscita = ZPlayerPrefs.GetFloat("variscita");
            carbon = ZPlayerPrefs.GetFloat("carbon");
            arcilla = ZPlayerPrefs.GetFloat("arcilla");
            jarronBasico = ZPlayerPrefs.GetFloat("jarronBasico");
            jarronDecorado = ZPlayerPrefs.GetFloat("jarronDecorado");
            diamantes = ZPlayerPrefs.GetFloat("diamantes");
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
            ZPlayerPrefs.SetFloat("semillasFrambuesa", semillasFrambuesa);
            ZPlayerPrefs.SetFloat("frambuesas", frambuesas);
            ZPlayerPrefs.SetFloat("variscita", variscita);
            ZPlayerPrefs.SetFloat("carbon", carbon);
            ZPlayerPrefs.SetFloat("arcilla", arcilla);
            ZPlayerPrefs.SetFloat("jarronBasico", jarronBasico);
            ZPlayerPrefs.SetFloat("jarronDecorado", jarronDecorado);
            ZPlayerPrefs.SetFloat("diamantes", diamantes);
        }
        //Debug.Log(ZPlayerPrefs.GetFloat("semillasFrambuesa"));
    }
	
	// Update is called once per frame
	void Update () {
        textoPoblacion.text = Convert.ToString(poblacion) + "/" + Convert.ToString(poblacionTotal);
        textoPoblacionTotal.text = Convert.ToString(poblacion);
        comidaText.text = Convert.ToString(manzanas);
        dineroText.text = Convert.ToString((int)gold);
        dineroTextConstruir.text = Convert.ToString((int)gold);
        diamantesText.text = Convert.ToString((int)diamantes);
        diamantesTextConstruir.text = Convert.ToString((int)diamantes);
        if (!guardado)
        {
            guardado = true;
            StartCoroutine(GuardarDatos());

        }
    }
    IEnumerator GuardarDatos()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        ZPlayerPrefs.SetFloat("gold", gold);
    }
}
