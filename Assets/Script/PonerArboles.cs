using UnityEngine;
using System.Collections;


namespace Assets.UltimateIsometricToolkit.Scripts.Core
{
    [ExecuteInEditMode]
    public class PonerArboles : MonoBehaviour
    {
        public float maxX, maxZ, xActual, zActual;
        public GameObject instanciar;
        Random numero;
        // Update is called once per frame
        void Update()
        {

            for (int j = 0; j <= maxZ; j++)
            {
                Instantiate(instanciar);
                instanciar.GetComponent<IsoTransform>().Position = new Vector3(Random.Range(0, 50), 2, Random.Range(0, 50));
                instanciar.name = "Arbol" + j;
                instanciar.GetComponent<IsoTransform>().ShowBounds = false;
                //instanciar.transform.position = new Vector3(i, 0, j);
            }

        }
    }
}
