﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitiesManager : MonoBehaviour
{
    public GameObject[] prefabsUnities;
    public static UnitiesManager instance;
    private List<UnityController> unities;
    private List<LightController> ligths;
    private List<DoorControl> doors;
    private float distanceMinBetween = 0.5f;

    public bool inHorde;

    private void Awake()
    {
        instance = this;
        doors = new List<DoorControl>();
    }

    // Start is called before the first frame update
    void Start()
    {
        inHorde = false;
        ligths = new List<LightController>();
        unities = new List<UnityController>();
        ligths.Add(GameObject.FindGameObjectWithTag("Goal").GetComponent<LightController>());
    }

    // Update is called once per frame
    void Update()
    {
        if (inHorde) {
            for (int i = 0; i < unities.Count; i++) {
                unities[i].UpdateUnit();
            }
        }
    }

    public bool CheckPosition(Vector3 chek) {
        int canPos = 0;
        for (int i = 0; i < unities.Count; i++)
        {
            float dis = Vector3.Distance(unities[i].transform.position, chek);
            if (dis > distanceMinBetween)
            {
                if (dis < unities[i].lightRange)
                {
                    canPos = 2;
                }
            }
            else {
                canPos = 1;
                break;
            }
        }
        if (canPos != 1) {
            for (int i = 0; i < ligths.Count; i++)
            {
                float dis = Vector3.Distance(ligths[i].transform.position, chek);
                if (dis > distanceMinBetween)
                {
                    if (dis < ligths[i].lightRange)
                    {
                        canPos = 2;
                    }

                }
                else
                {
                    canPos = 1;
                    break;
                }
            }
        }

        bool isPosiblePos = canPos == 2;

        return isPosiblePos;
    }

    public void AddUnity(GameObject unityToAdd) {
        GameManager.instance.SetUnit();
        UnityController u = unityToAdd.GetComponent<UnityController>();
        unities.Add(u);
        SearchInteractableObjects(u.transform.position,u.lightRange);
    }

    public void RemoveUnity(UnityController unityToRemove) {
        unities.Remove(unityToRemove);
        //Destroy(unityToRemove);
    }

    public GameObject GetUnityPrefab(int id) {
        return prefabsUnities[id];
    }

    public void AddDoor(DoorControl d) {
        doors.Add(d);
    }

    public UnityController SearchUnit(Vector3 pos, float range) {
        for (int i = 0; i < unities.Count; i++)
        {
            float dis = Vector3.Distance(unities[i].transform.position, pos);
            if (dis < range)
            {
                return unities[i];
            }

        }
        return null;
    }

    private void SearchInteractableObjects(Vector3 pos, float range) {
        for (int i = 0; i < doors.Count; i++) {
            float dist = Vector3.Distance(doors[i].transform.position, pos);
            if (dist <= range)
            {
                doors[i].canOpen = true;
            }
        }
    }
}