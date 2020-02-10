using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class DoorControl : MonoBehaviour
{
    public Animator myAnim;
    public SpotController[] spots;
    public bool canOpen;

    private void Start()
    {
        UnitiesManager.instance.AddDoor(this);
    }

    public void OpenDoors() {
        if (canOpen)
        {
            myAnim.SetTrigger("Open");
            for (int i = 0; i < spots.Length; i++) {
                spots[i].isActive = true;
            }
        }
    }
}
