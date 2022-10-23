using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public HotColdDisplay ht_display = null;
    Level currentLevel = null;
    public Menu menu = null;
    public LevelChoice lvlChoice = null;
    public Transform objectHolder = null;

    [SerializeField] int RotationSpeed = 1;

    public GameObject reStartBtn = null;
    public GameObject quitBtn = null;

    public Animator winAnim = null;

    public bool isPlaying = false;
    bool isSnapping = false;
    public int snappingSpeed = 1;

    public float timeBeforeValid = 3;

    float timer;

    // Update is called once per frame
    void Update()
    {
        if (!isPlaying)
            return;
        if (isSnapping)
        {
            objectHolder.transform.localRotation = Quaternion.Lerp(objectHolder.transform.localRotation, Quaternion.Euler(currentLevel.correctRotation), snappingSpeed * Time.deltaTime);
            if (Quaternion.Angle(objectHolder.transform.localRotation, Quaternion.Euler(currentLevel.correctRotation)) == 0)
            {
                isPlaying = false;
                isSnapping = false;
                // call end level menu
                winAnim.SetTrigger("Win");
                // lvlChoice.winLevel();
            }
            return;
        }
        if (isCorrectPosition())
        {
            if (timer > 0)
                timer -= Time.deltaTime;
            else
            {
                reStartBtn.SetActive(false);
                quitBtn.SetActive(false);
                isSnapping = true;
            }
        }
        else
            timer = timeBeforeValid;
        if (Input.GetMouseButton(0))
        {
            if (Mathf.Abs(Input.GetAxis("Mouse X")) > Mathf.Abs(Input.GetAxis("Mouse Y")))
                objectHolder.GetComponent<Rigidbody>().AddTorque(Vector3.down * Input.GetAxis("Mouse X") * RotationSpeed * Time.deltaTime);
            else
                objectHolder.GetComponent<Rigidbody>().AddTorque(Vector3.right * Input.GetAxis("Mouse Y") * RotationSpeed * Time.deltaTime);
        }
    }

    public void startLevel(Level level)
    {
        reStartBtn.SetActive(true);
        quitBtn.SetActive(true);
        objectHolder.localRotation = new Quaternion();
        if (objectHolder.childCount > 0)
            Destroy(objectHolder.GetChild(objectHolder.childCount - 1).gameObject);
        currentLevel = level;
        GameObject obj = Instantiate(currentLevel.prefab);
        obj.transform.parent = objectHolder;
        obj.transform.localPosition = new Vector3();
        obj.transform.localScale = currentLevel.scale;

        objectHolder.localRotation = Quaternion.Euler(currentLevel.rotation);
        isPlaying = true;
    }

    public void reStartLevel()
    {
        objectHolder.localRotation = Quaternion.Euler(currentLevel.rotation);
    }

    bool isCorrectPosition()
    {
        float angleDiff = Quaternion.Angle(objectHolder.transform.localRotation, Quaternion.Euler(currentLevel.correctRotation));

        if (angleDiff > 100)
            ht_display.percent = 0;
        else
            ht_display.percent = 100 - 100 * Mathf.Abs(angleDiff) / 100;

        if (angleDiff < currentLevel.anglePrecision && angleDiff > -currentLevel.anglePrecision)
            return (true);
        return (false);
    }

    public void snapToCorrectRotation()
    {
        isSnapping = true;
    }

}
