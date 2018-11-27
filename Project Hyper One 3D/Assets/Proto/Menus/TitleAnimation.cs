using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TitleAnimation : MonoBehaviour {

    [SerializeField] float speed = 0.15f;
    [SerializeField] float wait = 1f;
    Color[] colors = { new Color(255, 0, 0), new Color(0, 203, 0), new Color(0, 0, 255) };
    [SerializeField] TMP_Text[] titles;
    int i;
    int j;

	// Use this for initialization
	void Start () {
        i = 0;
        j = 0;
        StartCoroutine(colorSweep());
	}

    IEnumerator colorSweep(){

        while (true)
        {
            yield return new WaitForSeconds(speed);

            titles[i].color = colors[j];

            if (i < colors.Length-1)
            {
                i++;
            }
            else
            {
                i = 0;
                if (j < colors.Length - 1)
                {
                    j++;
                }
                else
                {
                    j = 0;

                }
            }



            yield return new WaitForSeconds(speed);

            if (i > 0){
                titles[i - 1].color = Color.white;
            }else if(i == 0){
                titles[colors.Length-1].color = Color.white;
                yield return new WaitForSeconds(wait);
            }
        }
    }
}
