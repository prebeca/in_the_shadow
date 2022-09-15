using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Level : MonoBehaviour
{
    public GameObject prefab;
    public Menu menu;
    GameObject item = null;
    public string namePlate;
    public TextMeshProUGUI namePlateTMP;
    public string levelNo;
    public TextMeshProUGUI levelNoTMP;
    public Vector3 rotation;
    public Vector3 correctRotation;
    Quaternion correctRotationQ;
    public Vector3 scale = new Vector3(1, 1, 1);

    bool moving = false;

    // Start is called before the first frame update
    void Start()
    {
        //create
        item = Instantiate(prefab);

        //position
        item.transform.parent = this.transform;
        item.transform.localPosition = new Vector3();

        //scale
        item.transform.localScale = scale;

        //rotate
        item.transform.localRotation = Quaternion.Euler(rotation);

        //set shadow only
        item.GetComponentInChildren<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;

        // set namePlate
        namePlateTMP.text = namePlate;
        levelNoTMP.text = levelNo;

        correctRotationQ = Quaternion.Euler(correctRotation.x, correctRotation.y, correctRotation.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (moving == true)
        {
            item.transform.rotation = Quaternion.Lerp(item.transform.rotation, correctRotationQ, 1 * Time.deltaTime);
            if (item.transform.rotation == correctRotationQ)
            {
                moving = false;
                menu.nextLevel();
            }
        }
    }

    public void snapToCorrectRotation()
    {
        moving = true;
    }
}
