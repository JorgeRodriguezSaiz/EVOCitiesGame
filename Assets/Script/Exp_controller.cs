using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

namespace Assets.UltimateIsometricToolkit.Scripts.Core
{
    public class Exp_controller : MonoBehaviour
    {
        [Header("UI GameObjects")]
        public Scrollbar expBar;
        public Text levelText;

        [Header("Variables Numericas")]
        public float exp;
        public int level;

        private float expActual;
        private int range = 0;

        private void Start()
        {
            if (ZPlayerPrefs.HasKey("exp"))
            {
                exp = ZPlayerPrefs.GetFloat("exp");
                expActual = ZPlayerPrefs.GetFloat("exp");
            }
            else if (ZPlayerPrefs.HasKey("exp"))
            {
                exp = 0;
                expActual = 0;
            }
            if (ZPlayerPrefs.HasKey("level"))
            {
                level = ZPlayerPrefs.GetInt("level");
                if (level <= 10000)
                {
                    range = Mathf.FloorToInt(range + level * Convert.ToSingle(Math.Exp(5)));
                }
                else
                {
                    range = Mathf.FloorToInt(range + 10000 * Convert.ToSingle(Math.Exp(5)));
                }
                levelText.text = level.ToString();
            }
            else
            {
                level = 1;
                range = Mathf.FloorToInt(range + level * Convert.ToSingle(Math.Exp(5)));
            }
            /*exp = 83;
            range = Convert.ToInt64(range * level + range*level*0.1f);
            for (int i = 1; i < level + 1; i++)
            {
                rangoAnterior = range;
                range = range + Convert.ToSingle(Math.Log(i, 1.1f)) * 100;
                range = Mathf.FloorToInt(range+i*Convert.ToSingle(Math.Exp(5)));
                Debug.Log(i + "  " + range);
            }*/
            expBar.size = exp / range;
        }
        private void Update()
        {
            //Guardar experiencia
            if (exp != expActual)
            {
                expBar.size = exp / range;
                expActual = exp;
                ZPlayerPrefs.SetFloat("exp", exp);
                ZPlayerPrefs.Save();
            }
            //Subir Nivel
            if (exp >= range)
            {
                NextLevel();
            }
        }
        //Cambiar Nivel
        private void NextLevel()
        {
            if (exp == range)
            {
                exp = 0;
            }
            else
            {
                exp = exp - range;
            }
            if (level <= 10000)
            {
                range = Mathf.FloorToInt(range + level * Convert.ToSingle(Math.Exp(5)));
            }
            else
            {
                range = Mathf.FloorToInt(range + 10000 * Convert.ToSingle(Math.Exp(5)));
            }
            level++;
            levelText.text = level.ToString();
            ZPlayerPrefs.SetInt("level", level);
            ZPlayerPrefs.Save();
        }
        public void SumarExp(int expSumar)
        {
            exp += exp * Convert.ToSingle(Math.Log(level, 1.1f)) * 100;
        }
    }
}
