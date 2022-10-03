using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterMenu : MonoBehaviour
{
    public Player player = null;
    public SettingsMenu settings = null;
    public GameObject logo3D = null;
    public LevelChoice lvlChoice = null;
    public GameObject levelsParent = null;
    List<Level> levels;

    void Start()
    {
        // if (!PlayerPrefs.HasKey("LevelUnlock") || PlayerPrefs.GetInt("LevelUnlock") < 1)
        PlayerPrefs.SetInt("LevelUnlock", 1);

        levels = new List<Level>(levelsParent.GetComponentsInChildren<Level>());
        levels[0].setUnlock(false);
        animateLogo();
    }

    void unlockLevels(bool isTesting = false)
    {
        int maxLevel = levels.Count;
        if (!isTesting)
            maxLevel = PlayerPrefs.GetInt("LevelUnlock");
        for (int i = 1; i < maxLevel && i < levels.Count; ++i)
            levels[i].setUnlock(false);
    }

    void lockLevels(bool isTesting = false)
    {
        int maxLevel = PlayerPrefs.GetInt("LevelUnlock");
        for (int i = maxLevel; i < levels.Count; ++i)
            levels[i].setLocked();
    }

    void animateLogo()
    {
        List<Rigidbody> logoParts = new List<Rigidbody>(logo3D.GetComponentsInChildren<Rigidbody>());
        int modifier = 1;
        foreach (Rigidbody rb in logoParts)
        {
            float speed = 0.5f * modifier;
            rb.angularVelocity = new Vector3(speed, speed, speed);
            modifier *= -1;
        }
    }

    public void startPlaying()
    {
        unlockLevels();
        lvlChoice.currentIndex = 0;
        lvlChoice.moveTo(PlayerPrefs.GetInt("LevelUnlock") - 1);
    }

    public void startPlayTesting()
    {
        unlockLevels(true);
        lvlChoice.currentIndex = 0;
        lvlChoice.moveTo(0);
    }

    public void toOptions()
    {
        player.moveTo(settings.transform.position);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
