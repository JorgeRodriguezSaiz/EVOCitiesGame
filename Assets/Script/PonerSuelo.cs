using UnityEngine;
using System.Collections;


namespace Assets.UltimateIsometricToolkit.Scripts.Core
{
    [ExecuteInEditMode]
    public class PonerSuelo : MonoBehaviour {
        public float maxX, maxZ, xActual, zActual;
        public GameObject instanciar;

        // Use this for initialization

        void Update()
        {
            for (int i = 0; i <= maxX; i++)
            {

                for (int j = 0; j <= maxZ; j++)
                {
                    Instantiate(instanciar);
                    instanciar.GetComponent<IsoTransform>().Position = new Vector3(i, 0, j);
                    instanciar.name = "suelo" + i +"" + j;
                    //instanciar.transform.position = new Vector3(i, 0, j);
                }
            }
        }
    }
}
