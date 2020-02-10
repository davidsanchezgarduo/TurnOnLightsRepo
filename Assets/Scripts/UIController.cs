using System.Collections;
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

    public void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        pauseAnimator.gameObject.SetActive(false);
        dataText.text = "Unidad: \nFuerza: \nVelocidad: \nRango: \nVitalidad: ";
        hordeText.text = "Horde: 0";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnterMenu() {
        CameraControl.instance.inMovement = false;
        menuAnimator.SetTrigger("In");
    }

    public void CloseMenu() {
        CameraControl.instance.inMovement = true;
        menuAnimator.SetTrigger("Out");
    }

    public void ClickPause() {
        pauseAnimator.gameObject.SetActive(true);
        pauseAnimator.SetTrigger("In");
        
    }

    public void ResumeGame() {
        Time.timeScale = 1;
        pauseAnimator.SetTrigger("Out");
    }

    public void ExitGame() {

    }

    public void SetUnitData(string _name, float _force, float speed, float _range, int _live) {
        dataText.text = "Unidad: "+ _name + "\nFuerza: "+ _force + "\nVelocidad: "+ speed + "\nRango: "+ _range + "\nVitalidad: "+ _live;
    }
    public void ClearUnitData() {
        dataText.text = "Unidad: \nFuerza: \nVelocidad: \nRango: \nVitalidad: ";
    }

    public void SetHorde(int h)
    {
        if (!CameraControl.instance.inMovement)
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

}
