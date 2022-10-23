using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public LevelChoice lvlChoice = null;
    public BetterMenu menu = null;
    public Transform gameZoneCam = null;
    public Player player = null;

    // Start is called before the first frame update
    public void toGameZone()
    {
        player.moveToDirect(gameZoneCam.position);
        player.transform.rotation = gameZoneCam.rotation;
    }

    public void toLevelChoice()
    {
        if (lvlChoice.getCurrentLvl() == null)
        {
            toMenu();
            return;
        }

        player.moveToDirect(lvlChoice.getCurrentLvl().transform.position);
        player.transform.rotation = lvlChoice.getCurrentLvl().transform.rotation;
    }

    public void toMenu()
    {
        player.moveToDirect(menu.transform.position);
        player.transform.rotation = menu.transform.rotation;
    }

    public void unlockLevel()
    {
        if (lvlChoice.getCurrentLvl() != null)
            lvlChoice.nextLevel();
    }
}