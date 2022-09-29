using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public LevelChoice lvlChoice = null;
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
        player.moveToDirect(lvlChoice.getCurrentLvl().transform.position);
        player.transform.rotation = lvlChoice.getCurrentLvl().transform.rotation;
    }

    public void unlockLevel()
    {
        lvlChoice.nextLevel();
    }
}