﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public static UIController instance;
    public TextMeshProUGUI dataText;
    public Animator menuAnimator;
    public Animator pauseAnimator;
    public Button menuButton;
    public TextMeshProUGUI hordeText;
    public TextMeshProUGUI livesText;
    private bool menuOpen;
    public UnitScriptableObject unitScriptable;
    private GameObject unitPrefab;
    public Transform content;
    public GameObject gameOverPanel;
    public TextMeshProUGUI messageText;

    private float widthButton = 120;
    private float offsetButton = 30;

    public void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        gameOverPanel.SetActive(false);
        pauseAnimator.gameObject.SetActive(false);
        dataText.text = "Unidad: \nFuerza: \nVelocidad: \nRango: \nVitalidad: ";
        hordeText.text = "Horde: 0";
        menuOpen = false;

        for (int i = 0; i < unitScriptable.units.Length; i++) {
            GameObject g = Instantiate(unitPrefab,Vector3.zero,Quaternion.identity);
            g.transform.parent = content;
            g.GetComponent<RectTransform>().position = Vector3.zero;
            g.GetComponent<RectTransform>().anchoredPosition = new Vector2(((i + 1) * offsetButton) + (i * widthButton), 50);
            g.GetComponent<RectTransform>().localScale = new Vector3(1,1,1);
            g.GetComponent<UnityCreator>().Init(i,unitScriptable.units[i].image, unitScriptable.units[i].typeName);
        }
    }

    public void EnterMenu() {
        //CameraControl.instance.inMovement = false;
        menuAnimator.SetTrigger("In");
        menuOpen = true;
    }

    public void CloseMenu() {
        //CameraControl.instance.inMovement = true;
        menuAnimator.SetTrigger("Out");
        menuOpen = false;
    }

    public void ClickPause() {
        pauseAnimator.gameObject.SetActive(true);
        pauseAnimator.SetTrigger("In");

        UnitiesManager.instance.PausedGame(true);
        EnemyGenerator.instance.PausedGame(true);
        CameraControl.instance.PausedGame(true);
    }

    public void ResumeGame() {
        Time.timeScale = 1;
        pauseAnimator.SetTrigger("Out");
        UnitiesManager.instance.PausedGame(false);
        EnemyGenerator.instance.PausedGame(false);
        CameraControl.instance.PausedGame(false);
    }

    public void ExitGame() {
        SceneManager.LoadScene("MenuScene");
    }

    public void SetUnitData(string _name, float _force, float speed, float _range, int _live) {
        dataText.text = "Unidad: "+ _name + "\nFuerza: "+ _force + "\nVelocidad: "+ speed + "\nRango: "+ _range + "\nVitalidad: "+ _live;
    }
    public void ClearUnitData() {
        dataText.text = "Unidad: \nFuerza: \nVelocidad: \nRango: \nVitalidad: ";
    }

    public void SetHorde(int h)
    {
        if (menuOpen)
        {
            CloseMenu();
        }
        menuButton.interactable = false;
        menuButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Horda";
        hordeText.text = "Horda: " + h;
    }

    public void FinishHorde() {
        menuButton.interactable = true;
        menuButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Unidades";
    }

    public void SetLivesText(int lives) {
        livesText.text = "Vidas: " + lives;
    }

    public void FinishGame(bool win) {
        if (win)
        {
            messageText.text = "Ganaste";
        }
        else {
            messageText.text = "Perdiste";
        }

        gameOverPanel.SetActive(false);
    }

    public void ClickContinue() {
        SceneManager.LoadScene("LevelSelector");
    }

    public void ClickRetry() {
        SceneManager.LoadScene(DataController.instance.LastLevelSelected);
    }

}
