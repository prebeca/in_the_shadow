using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    Level currentLevel = null;
    public Menu menu = null;
    public LevelChoice lvlChoice = null;
    public Transform objectHolder = null;

    [SerializeField] int RotationSpeed = 1;

    bool isPlaying = false;
    bool isPaused = false;
    bool isSnapping = false;
    public int snappingSpeed = 1;

    public float timeBeforeValid = 3;

    float timer;

    // Update is called once per frame
    void Update()
    {
        if (isSnapping)
        {
            objectHolder.transform.localRotation = Quaternion.Lerp(objectHolder.transform.localRotation, Quaternion.Euler(currentLevel.correctRotation), snappingSpeed * Time.deltaTime);
            if (Quaternion.Angle(objectHolder.transform.localRotation, Quaternion.Euler(currentLevel.correctRotation)) == 0)
            {
                isSnapping = false;
                // call end level menu
                lvlChoice.winLevel();
            }
        }
        if (!isPlaying || isPaused)
            return;
        if (isCorrectPosition())
        {
            // end level
            if (timer > 0)
                timer -= Time.deltaTime;
            else
            {
                isPlaying = false;
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

    bool isCorrectPosition()
    {
        float angleDiff = Quaternion.Angle(objectHolder.transform.localRotation, Quaternion.Euler(currentLevel.correctRotation));
        if (angleDiff < currentLevel.anglePrecision && angleDiff > -currentLevel.anglePrecision)
            return (true);
        return (false);
    }

    public void snapToCorrectRotation()
    {
        isSnapping = true;
    }

}
