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
    private List<CivilController> civils;
    private float distanceMinBetween = 0.5f;
    private ShadowController shadow;

    public bool inHorde;

    private void Awake()
    {
        instance = this;
        doors = new List<DoorControl>();
        civils = new List<CivilController>();
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

        RaycastHit hit;
        int layerMask = 1 << 8;
        Ray ray = new Ray(new Vector3(unityToAdd.transform.position.x, unityToAdd.transform.position.y+30, unityToAdd.transform.position.z), -unityToAdd.transform.up);
        /*Vector3 up = unityToAdd.transform.TransformDirection(Vector3.up) * 10;
        Debug.DrawRay(unityToAdd.transform.position,up, Color.green,10);*/
        if (Physics.Raycast(ray, out hit, 30))
        {
            if (hit.transform.CompareTag("Shadow")) {
                unityToAdd.GetComponent<UnityController>().myTextCoord = hit.textureCoord;
                if (shadow == null)
                {
                    shadow = hit.transform.GetComponent<ShadowController>();
                    shadow.SetUnit(hit.textureCoord, unityToAdd.GetComponent<UnityController>().lightRange);
                }
                else {
                    shadow.SetUnit(hit.textureCoord, unityToAdd.GetComponent<UnityController>().lightRange);
                }
            }

        }

    }

    public void RemoveUnity(UnityController unityToRemove) {
        unities.Remove(unityToRemove);
        shadow.RemoveUnit(unityToRemove.myTextCoord,unityToRemove.lightRange);
        //Destroy(unityToRemove);
    }

    public GameObject GetUnityPrefab(int id) {
        return prefabsUnities[id];
    }

    public void AddDoor(DoorControl d) {
        doors.Add(d);
    }

    public void AddCivil(CivilController c)
    {
        civils.Add(c);
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

        for (int i = 0; i < civils.Count; i++) {
            float dist = Vector3.Distance(civils[i].transform.position, pos);
            if (dist <= range)
            {
                civils[i].ActiveCivil();
            }
        }
    }
}
