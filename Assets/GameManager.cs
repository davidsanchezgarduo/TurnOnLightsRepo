using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int horde;
    public int unitsToNextHorde;
    public int unitsPerHorde = 2;
    public bool inHorde;
    public float plusSpawnDifficult = 0.5f;
    public int lives = 5; 

    private List<SpotController> spots;
    public bool hasFinishSpawn;

    private void Awake()
    {
        instance = this;
        spots = new List<SpotController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
        horde = 1;
        inHorde = false;
        unitsToNextHorde = unitsPerHorde;
        
        UIController.instance.SetLivesText(lives);
    }

    // Update is called once per frame
    void Update()
    {
        if (inHorde) {
            if (!hasFinishSpawn)
            {
                bool tempFinish = true;
                for (int i = 0; i < spots.Count; i++)
                {
                    bool finish = spots[i].UpdateSpot();
                    if (!finish && tempFinish) {
                        tempFinish = false;
                    }
                }
                hasFinishSpawn = tempFinish;
            }
        }
    }

    public void AddSpot(SpotController s) {
        spots.Add(s);
    }

    public void SetUnit() {
        unitsToNextHorde--;
        Debug.Log("SetUnit");
        if (unitsToNextHorde == 0) {
            Debug.Log("StartHorde");
            UIController.instance.SetHorde(horde);
            inHorde = true;
            UnitiesManager.instance.inHorde = true;
            hasFinishSpawn = false;
            for (int i = 0; i < spots.Count; i++)
            {
                spots[i].StartHorde(plusSpawnDifficult);
            }
        }
    }

    public void FinishHorde() {
        horde++;
        inHorde = false;
        unitsToNextHorde = unitsPerHorde;
        UnitiesManager.instance.inHorde = false;
        UIController.instance.FinishHorde();
    }

    public void LoseLive() {
        lives--;
        UIController.instance.SetLivesText(lives);
    }

}
