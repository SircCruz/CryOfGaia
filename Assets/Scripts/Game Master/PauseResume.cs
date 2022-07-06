using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseResume : MonoBehaviour
{
    public Canvas pause;
    public WorldPhysics gameMaster;
    void Start()
    {
        pause.enabled = false;
    }
    public void pauseGame()
    {
        pause.enabled = true;
        gameMaster.stopPhysics = true;
    }
    public void resumeGame()
    {
        pause.enabled = false;
        gameMaster.stopPhysics = false;
    }
}
