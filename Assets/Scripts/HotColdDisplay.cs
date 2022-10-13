using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotColdDisplay : MonoBehaviour
{
    public float percent = 0;
    public int base_intensity = 1;
    public Light first_quarter = null;
    public Light second_quarter = null;
    public Light third_quarter = null;
    public Light last_quarter = null;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(percent);

        float percent_cpy = percent;

        checkLigth(percent_cpy, first_quarter);
        percent_cpy -= 25;
        checkLigth(percent_cpy, second_quarter);
        percent_cpy -= 25;
        checkLigth(percent_cpy, third_quarter);
        percent_cpy -= 25;
        checkLigth(percent_cpy, last_quarter);
    }

    void checkLigth(float value, Light light)
    {
        if (value < 0)
            light.intensity = 0;
        else if (value >= 25)
            light.intensity = base_intensity;
        else
            light.intensity = base_intensity * (value / 25);
        Debug.Log(light.intensity);
    }
}
