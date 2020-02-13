﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int levelId;
    public LevelScriptableObject levelData;
    public int horde;
    public int unitsToNextHorde;
    public int unitsPerHorde = 2;
    public bool inHorde;
    public float plusSpawnDifficult = 0.5f;
    public int lives = 5; 

    private List<SpotController> spots;
    public bool hasFinishSpawn;
    
    public GameObject civilPrefab;
    public Transform civilParent;

    public int unitsDeployed;
    public int zombiesKilled;
    public int points;
    private int pointsPerZombie = 50;
    private int restantCivils;

    private void Awake()
    {
        instance = this;
        spots = new List<SpotController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        unitsDeployed = 0;
        zombiesKilled = 0;
        points = 0;
        horde = 1;
        inHorde = false;
        unitsToNextHorde = levelData.levels[levelId].unitsToHorde;
        restantCivils = levelData.levels[levelId].civilPositions.Length;
        UIController.instance.SetLivesText(lives);

        for(var i =0; i < levelData.levels[levelId].civilPositions.Length; i++) {
            GameObject civil = Instantiate(civilPrefab, new Vector3(levelData.levels[levelId].civilPositions[i].x,civilPrefab.transform.position.y, levelData.levels[levelId].civilPositions[i].y),Quaternion.identity);
            civil.transform.parent = civilParent;
        }

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
        unitsDeployed++;
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
        if(lives == 0)
        {
            FinishLevel(false);
        }
    }

    public void FinishLevel(bool pass) {
        points = zombiesKilled * pointsPerZombie;
        DataController.instance.PlusData(points,zombiesKilled,unitsDeployed,pass);
    }

    public void RescueCivil() {
        restantCivils--;
        if(restantCivils == 0) {
            FinishLevel(true);
        }
    }

}
