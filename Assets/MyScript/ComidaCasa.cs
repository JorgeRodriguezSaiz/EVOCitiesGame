using UnityEngine;
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
        if (minaActual)
        {
            if (minaActual.tag == "trabajo")
            {
                if (minaActual && !minaActual.GetComponent<Trabajos>().funcionar && minaActual.GetComponent<Trabajos>().antecomer)
                {
                    comidaCasa -= minaActual.GetComponent<Trabajos>().primeraOpcion.GetComponent<DatosTrabajo>().trabajadoresNecesita;
                    ZPlayerPrefs.SetFloat("comidaCasa " + gameObject.GetComponent<Casa>().numbConstruccion, comidaCasa);
                    minaActual.GetComponent<Trabajos>().antecomer = false;
                }
                if (minaActual && !minaActual.GetComponent<Trabajos>().finTrabajo && minaActual.GetComponent<Trabajos>().antecomer)
                {
                    comidaCasa -= minaActual.GetComponent<Trabajos>().primeraOpcion.GetComponent<DatosTrabajo>().trabajadoresNecesita;
                    ZPlayerPrefs.SetFloat("comidaCasa " + gameObject.GetComponent<Casa>().numbConstruccion, comidaCasa);
                    minaActual.GetComponent<Trabajos>().antecomer = false;
                }
                if (comidaCasa < comidaCasaTotal)
                {
                    if (minaActual.GetComponent<Trabajos>().comer)
                    {
                        gameObject.transform.GetChild(3).gameObject.SetActive(true);
                    }
                }
            }
            if (minaActual.tag == "ocio")
            {
                if (!minaActual.GetComponent<Ocio>().funcionar && minaActual.GetComponent<Ocio>().antecomer)
                {
                    comidaCasa -= minaActual.GetComponent<Ocio>().trabajadoresNecesita;
                    ZPlayerPrefs.SetFloat("comidaCasa " + gameObject.GetComponent<Casa>().numbConstruccion, comidaCasa);
                    minaActual.GetComponent<Ocio>().antecomer = false;
                }
                if (comidaCasa < comidaCasaTotal)
                {
                    if (minaActual.GetComponent<Ocio>().comer)
                    {
                        gameObject.transform.GetChild(3).gameObject.SetActive(true);
                    }
                }
            }
            /*if (minaActual.tag == "casa")
            {
                if (minaActual && !minaActual.GetComponent<Casa>().funcionar && minaActual.GetComponent<Casa>().antecomer)
                {
                    comidaCasa -= minaActual.GetComponent<Casa>().trabajadoresNecesita;
                    ZPlayerPrefs.SetFloat("comidaCasa " + gameObject.GetComponent<Casa>().numbConstruccion, comidaCasa);
                    minaActual.GetComponent<Casa>().antecomer = false;
                }                
                if (comidaCasa < comidaCasaTotal)
                {
                    if (minaActual.GetComponent<Casa>().comer)
                    {
                        gameObject.transform.GetChild(3).gameObject.SetActive(true);
                    }
                }
            }*/
        }
        if (comidaCasa <= 0)
        {
            isComida = false;
            gameObject.transform.GetChild(4).gameObject.SetActive(true);
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
        
        
        /*if (gameObject.GetComponent<Casa>().enabled == false && comidaCasa > 0)
        {
            comidaCasa -= (float)0.1 * Time.deltaTime;
        }*/
    }
    public void OnMouseDown()
    {
        if (minaActual)
        {
            if (minaActual.tag == "trabajo")
            {
                if (minaActual.GetComponent<Trabajos>().comer)
                {
                    comidaCasa = comidaCasaTotal;
                    ZPlayerPrefs.SetFloat("comidaCasa " + gameObject.GetComponent<Casa>().numbConstruccion, comidaCasa);
                    isComida = true;
                    gameObject.transform.GetChild(4).gameObject.SetActive(false);
                    gameObject.GetComponent<SpriteRenderer>().enabled = true;
                    gameObject.transform.GetChild(3).gameObject.SetActive(false);
                    minaActual.GetComponent<Trabajos>().comer = false;
                }
            }
            if (minaActual.tag == "ocio")
            {
                if (minaActual.GetComponent<Ocio>().comer)
                {
                    comidaCasa = comidaCasaTotal;
                    ZPlayerPrefs.SetFloat("comidaCasa " + gameObject.GetComponent<Casa>().numbConstruccion, comidaCasa);
                    isComida = true;
                    gameObject.transform.GetChild(4).gameObject.SetActive(false);
                    gameObject.GetComponent<SpriteRenderer>().enabled = true;
                    gameObject.transform.GetChild(3).gameObject.SetActive(false);
                    minaActual.GetComponent<Ocio>().comer = false;
                }
            }
            if (minaActual.tag == "casa")
            {
                if (minaActual.GetComponent<Casa>().comer)
                {
                    comidaCasa = comidaCasaTotal;
                    ZPlayerPrefs.SetFloat("comidaCasa " + gameObject.GetComponent<Casa>().numbConstruccion, comidaCasa);
                    isComida = true;
                    gameObject.transform.GetChild(4).gameObject.SetActive(false);
                    gameObject.GetComponent<SpriteRenderer>().enabled = true;
                    gameObject.transform.GetChild(3).gameObject.SetActive(false);
                    minaActual.GetComponent<Casa>().comer = false;
                }
            }
        }
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
