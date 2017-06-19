using UnityEngine;
using System.Collections;
public class Active_DesactiveBottom : MonoBehaviour
{
    public GameObject[] objIn, objOut;
    public GameObject camara;
    public bool camaraMovimiento;
    public void ActiveDesactive()
    {
        camara = GameObject.Find("Main Camera");
        for (int i = 0; i < objOut.Length; i++)
        {
            objOut[i].SetActive(false);
        }
        for (int i = 0; i < objIn.Length; i++)
        {
            objIn[i].SetActive(true);
        }
        if (camaraMovimiento)
        {
            camara.GetComponent<SmoothCamera2d>().enabled = true;
            camara.GetComponent<PinchZoom>().enabled = true;
        }
        else
        {
            camara.GetComponent<SmoothCamera2d>().enabled = false;
            camara.GetComponent<PinchZoom>().enabled = false;
        }
    }
}

