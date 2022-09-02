using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Level : MonoBehaviour
{
    public GameObject prefab;
    public string namePlate;
    public TextMeshProUGUI namePlateTMP;
    public string levelNo;
    public TextMeshProUGUI levelNoTMP;
    public Vector3 rotation;
    public Vector3 scale = new Vector3(1, 1, 1);

    // Start is called before the first frame update
    void Start()
    {
        //create
        GameObject newObj = Instantiate(prefab);

        //position
        newObj.transform.parent = this.transform;
        newObj.transform.localPosition = new Vector3();

        //scale
        newObj.transform.localScale = scale;

        //rotate
        newObj.transform.localRotation = Quaternion.Euler(rotation);

        //set shadow only
        newObj.GetComponentInChildren<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;

        // set namePlate
        namePlateTMP.text = namePlate;
        levelNoTMP.text = levelNo;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
