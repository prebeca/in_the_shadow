using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public Camera cam;
    public List<GameObject> logoParts = new List<GameObject>();

    public List<GameObject> levels = new List<GameObject>();
    public GameObject optionsObj = null;
    public GameObject menuObj = null;
    public GameObject gameZone = null;

    public GameObject nextLevelBtn = null;
    public GameObject prevLevelBtn = null;
    public GameObject homeBtn = null;

    int targetLevel = -1;
    bool toLevel = false;

    Vector3 targetPos;

    public float cameraSpeed = 1;

    // Start is called before the first frame update
    void Start()
    {
        float b = 1f;
        float speed = Random.Range(0.25f, 0.75f);
        for (int i = 0; i < logoParts.Count; ++i, b *= -1f, speed *= b)
            logoParts[i].GetComponent<Rigidbody>().angularVelocity = new Vector3(speed, speed, speed);
        targetPos = cam.transform.position;
    }

    void Update()
    {
        if (targetPos != cam.transform.position)
        {
            cam.transform.position = Vector3.Lerp(cam.transform.position, targetPos, cameraSpeed * Time.deltaTime);
            if (toLevel && cam.transform.position.z > 4)
            {
                toGameZone();
                toLevel = false;
            }
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

    void handleNavHUD()
    {
        prevLevelBtn.SetActive(!(targetLevel <= 0));
        nextLevelBtn.SetActive(!(targetLevel >= levels.Count - 1));
    }

    public void previousLevel()
    {
        homeBtn.SetActive(true);
        --targetLevel;
        if (targetLevel < 0)
            targetLevel = 0;
        targetPos = cam.transform.position;
        targetPos.x = levels[targetLevel].transform.position.x;
        handleNavHUD();
    }

    public void nextLevel()
    {
        homeBtn.SetActive(true);
        ++targetLevel;
        if (targetLevel >= levels.Count)
            targetLevel = levels.Count - 1;
        targetPos = cam.transform.position;
        targetPos.x = levels[targetLevel].transform.position.x;
        handleNavHUD();
    }

    public void toOptions()
    {
        targetPos = cam.transform.position;
        targetPos.x = optionsObj.transform.position.x;
    }

    public void toMainMenu()
    {
        targetLevel = -1;
        targetPos = cam.transform.position;
        targetPos.x = menuObj.transform.position.x;

        prevLevelBtn.SetActive(false);
        nextLevelBtn.SetActive(false);
        homeBtn.SetActive(false);
    }

    public void startLevel()
    {
        prevLevelBtn.SetActive(false);
        nextLevelBtn.SetActive(false);
        homeBtn.SetActive(false);

        targetPos = cam.transform.position;
        targetPos.z = 5;
        toLevel = true;
    }

    public void toGameZone()
    {
        cam.transform.position = gameZone.transform.position;
        cam.transform.rotation = gameZone.transform.rotation;

        targetPos = gameZone.transform.position;
    }

}