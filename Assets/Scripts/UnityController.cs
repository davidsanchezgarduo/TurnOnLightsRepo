using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnityController : MonoBehaviour
{
    private enum UnitState
    {
        SEARCHING,
        ATTACKING,
        DIYING
    }

    public int type;
    public string typeName;
    public string socialName;
    public int forceAttack;
    public float lightRange;
    public float speedAttack;
    public int lives;
    public Vector2 myTextCoord;

    public Transform areaCircle;
    public Image lifeImage;
    private EnemyController currentTarget;
    private int initialLives;

    private UnitState currentState;
    private float currentTimeAttack;

    // Start is called before the first frame update
    void Start()
    {
        currentTimeAttack = 0;
        currentState = UnitState.SEARCHING;
        initialLives = lives;
        currentTarget = null;
        areaCircle.localScale = new Vector3(lightRange*2,0.01f,lightRange*2);


    }

    // Update is called once per frame
    public void UpdateUnit()
    {

        if (currentState == UnitState.SEARCHING)
        {
            currentTarget = EnemyGenerator.instance.CheckEnemyInRange(transform.position, lightRange);
            if (currentTarget != null) {
                currentState = UnitState.ATTACKING;
                //transform.LookAt(currentTarget.transform);
            }
            
        }
        else if (currentState == UnitState.ATTACKING) {
            if (currentTarget.lives > 0)
            {
                currentTimeAttack += Time.deltaTime;
                transform.LookAt(new Vector3(currentTarget.transform.position.x, 0.5f, currentTarget.transform.position.z));
                if (currentTimeAttack > speedAttack)
                {
                    currentTimeAttack = 0;
                    if (currentTarget.ReciveDamage(this, forceAttack))
                    {
                        currentTarget = null;
                        currentState = UnitState.SEARCHING;
                    }
                }
            }
            else {
                currentTimeAttack = 0;
                currentTarget = null;
                currentState = UnitState.SEARCHING;
            }
        }
    }

    public bool ReciveDamage(int damage) {
        lives-= damage;
        //Debug.Log("Unit Recive damage");
        if (lives <= 0) {
            //Anim die
            //Remove light
            lifeImage.fillAmount = 0;
            currentState = UnitState.DIYING;
            UnitiesManager.instance.RemoveUnity(this);
            Destroy(this.gameObject,0.2f);
            return true;
        }
        else {
            lifeImage.fillAmount = (float)lives / (float)initialLives;
        }
        return false;
    }
}
