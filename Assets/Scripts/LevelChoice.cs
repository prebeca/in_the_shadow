using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelChoice : MonoBehaviour
{
    public Player player = null;
    public GameController gameController = null;

    public GameObject levelsParent = null;
    List<Level> levels = null;
    public int currentIndex = -1;

    public GameObject nextLevelBtn = null;
    public GameObject prevLevelBtn = null;
    public GameObject homeBtn = null;

    void Start()
    {
        levels = new List<Level>(levelsParent.GetComponentsInChildren<Level>());
    }

    void handleNavHUD()
    {
        homeBtn.SetActive(currentIndex >= 0);
        prevLevelBtn.SetActive(currentIndex > 0);
        nextLevelBtn.SetActive(currentIndex >= 0 && currentIndex < levels.Count - 1 && !levels[currentIndex + 1].islocked);
    }

    public void startLevel()
    {

        prevLevelBtn.SetActive(false);
        nextLevelBtn.SetActive(false);
        homeBtn.SetActive(false);

        // Trigger to level anim
        player.GetComponent<Animator>().SetTrigger("ToGame");

        gameController.startLevel(levels[currentIndex]);
    }

    public void winLevel()
    {
        if (currentIndex + 1 < levels.Count && levels[currentIndex + 1].islocked)
        {
            PlayerPrefs.SetInt("LevelUnlock", PlayerPrefs.GetInt("LevelUnlock") + 1);
            levels[currentIndex + 1].setUnlock();
        }
        player.GetComponent<Animator>().SetTrigger("ToMenu");
    }

    public void moveTo(int i)
    {
        if (i >= 0 && i < levels.Count)
        {
            player.moveTo(levels[i].transform.position);
            currentIndex = i;
        }
        handleNavHUD();
    }

    public void previousLevel()
    {
        moveTo(currentIndex - 1);
    }
    public void nextLevel()
    {
        moveTo(currentIndex + 1);
    }

    public Level getCurrentLvl()
    {
        return (levels[currentIndex]);
    }

    public void toMenu()
    {
        currentIndex = -1;
        handleNavHUD();
        player.moveTo(new Vector3());
    }
}
