using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;


public enum GameMode
{
    idle,
    playing,
    levelEnd
}

public class MissionDemolition : MonoBehaviour
{
    static private MissionDemolition S; // Singleton

    [Header("Inscribed")]
    public TextMeshProUGUI uitLevel; // Current level
    public TextMeshProUGUI uitShots; // Shots taken
    public Vector3 castlePos; // Where to place castles
    public GameObject[] castles; // Array of castles

    [Header("Dynamic")]
    public int level; // Current level
    public int levelMax; // Number of levels
    public int shotsTaken; // Shots taken
    public GameMode mode = GameMode.idle;
    public GameObject castle; // Current castle
    public string showing = "Show Slingshot"; // Follow or Show Slingshot



    // Start is called before the first frame update
    void Start()
    {
        S = this; // Set the singleton
        level = 0;
        shotsTaken = 0;
        levelMax = castles.Length;
        StartLevel();
    }

    void StartLevel()
    {
       // If there is a castle, get rid of it
        if (castle != null)
        {
            Destroy(castle);
        }
        // Destroy old projectiles
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Projectile");
        foreach (GameObject pTemp in gos)
        {
            Destroy(pTemp);
        }

        // Instantiate the new castle
        castle = Instantiate(castles[level]);
        castle.transform.position = castlePos;
        shotsTaken = 0;
        Goal.goalMet = false;
        UpdateGUI();
        mode = GameMode.playing;
    }

    void UpdateGUI()
    {
       uitLevel.text = "Level: " + (level + 1) + " of " + levelMax;
       uitShots.text = "Shots: " + shotsTaken;

    }

    // Update is called once per frame
    void Update()
    {
        UpdateGUI();
        //check for level end
        if ((mode == GameMode.playing) && (Goal.goalMet))
        {
            mode = GameMode.levelEnd;
            Invoke("NextLevel", 2f);
        }
    }
    void NextLevel()
    {
        level++;
        if (level == levelMax)
        {
            level = 0;
            shotsTaken = 0;
        }
        StartLevel();

    }
}
