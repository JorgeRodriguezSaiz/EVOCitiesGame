using UnityEngine;
using System.Collections;

public class Active_DesactiveBottom : MonoBehaviour {
    public GameObject[] objIn, objOut;
    public void ActiveDesactive()
    {
        for (int i = 0; i < objOut.Length; i++)
        {
            objOut[i].SetActive(false);
        }
        for (int i = 0; i < objIn.Length; i++)
        {
            objIn[i].SetActive(true);
        }
    }
}
