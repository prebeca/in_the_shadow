using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Level : MonoBehaviour
{
    public GameObject prefab;
    public Menu menu;
    public GameObject item = null;
    public string namePlate;
    public TextMeshProUGUI namePlateTMP;
    public string levelNo;
    public TextMeshProUGUI levelNoTMP;
    public Vector3 rotation;
    public Vector3 correctRotation;
    public int anglePrecision = 10;
    Quaternion correctRotationQ;
    public Vector3 scale = new Vector3(1, 1, 1);
    public bool islocked = true;


    // Start is called before the first frame update
    void Start()
    {
        namePlateTMP.text = namePlate;
        levelNoTMP.text = levelNo;

        correctRotationQ = Quaternion.Euler(correctRotation);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void setLocked()
    {
        this.GetComponent<Animator>().SetTrigger("lock");
        islocked = true;
    }

    public void setUnlock(bool anim = true)
    {
        if (anim)
            this.GetComponent<Animator>().SetTrigger("unlockAnim");
        else
            this.GetComponent<Animator>().SetTrigger("unlock");
        islocked = false;
    }
}
