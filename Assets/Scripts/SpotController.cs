using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotController : MonoBehaviour
{
    public float timeSpawn;
    public float maxEnemimesPerHorde;
    public bool isActive;
    private float currentTimeSpawn;
    private int currentEnemiesSpawn;
    public bool finishSpawn;
    private int currenMaxEnemies;
    // Start is called before the first frame update
    void Start()
    {
        //StartHorde();
        maxEnemimesPerHorde = 5;
        GameManager.instance.AddSpot(this);
    }

    // Update is called once per frame
    public bool UpdateSpot()
    {
        //Debug.Log("Spot");
        if (isActive && !finishSpawn) {
            //Debug.Log("time Spawn "+currentTimeSpawn);
            currentTimeSpawn += Time.deltaTime;
            if (currentTimeSpawn > timeSpawn) {
                EnemyGenerator.instance.CreateEnemy(0,transform.position);
                currentEnemiesSpawn ++;
                currentTimeSpawn = 0;
                if (currentEnemiesSpawn >= currenMaxEnemies) {
                    finishSpawn = true;
                }
            }
        }

        return finishSpawn;

    }

    public void StartHorde(float plusDifficult) {
        maxEnemimesPerHorde += plusDifficult;
        currenMaxEnemies = Mathf.RoundToInt(maxEnemimesPerHorde);
        currentTimeSpawn = 0;
        currentEnemiesSpawn = 0;
        finishSpawn = false;
    }
}
