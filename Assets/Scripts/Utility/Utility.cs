using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace MultiPlayerGame.Utility
{

    public class Utility : MonoBehaviour
    {
        static public IEnumerator ImageFadeIn(GameObject gobj)
        {
            while (gobj.GetComponent<Image>().color.a < 1)
            {
                Color objColor = gobj.GetComponent<Image>().color;
                float fadeAmount = objColor.a + (0.3f * Time.deltaTime);
                objColor = new Color(objColor.r, objColor.g, objColor.b, fadeAmount);
                gobj.GetComponent<Image>().color = objColor;
                yield return null;
            }

        }
        static public IEnumerator ImageFadeOut(GameObject gobj)
        {
            while (gobj.GetComponent<Image>().color.a > 0)
            {
                Color objColor = gobj.GetComponent<Image>().color;
                float fadeAmount = objColor.a - (1.2f * Time.deltaTime);
                objColor = new Color(objColor.r, objColor.g, objColor.b, fadeAmount);
                gobj.GetComponent<Image>().color = objColor;
                yield return null;
            }
        }
    }
}