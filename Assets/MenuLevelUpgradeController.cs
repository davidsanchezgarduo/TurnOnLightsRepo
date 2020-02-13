using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuLevelUpgradeController : MonoBehaviour
{

    public GameObject LevelMenu;
    public GameObject UpgradeMenu;
    public Text buttonText;

    // Start is called before the first frame update
    void Start()
    {
        buttonText.text = "Upgrade";
        LevelMenu.SetActive(true);
        UpgradeMenu.SetActive(false);
    }

    public void ChangeMenu() {
        if (LevelMenu.activeSelf)
        {
            buttonText.text = "Levels";
            LevelMenu.SetActive(false);
            UpgradeMenu.SetActive(true);
        }
        else {
            buttonText.text = "Upgrade";
            LevelMenu.SetActive(false);
            UpgradeMenu.SetActive(true);
        }
    }
}
