using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum BattleState {START, PLAYERTURN, ENEMYTURN, WIN, LOSE }

public class battlesystem : MonoBehaviour
{
    public BattleState state;
    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public Transform playerBattleStation;
    public Transform enemyBattleStation;

    Unit enemyUnit;
    Unit playerUnit;

    public Text dialogueText;

    public sliderhealth playerHUD;
    public sliderhealth enemyHUD;

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
       StartCoroutine (SetUpBattle());
        
    }

    IEnumerator SetUpBattle ()
    {
       GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
        playerUnit = playerGO.GetComponent<Unit>();

       GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleStation);
        enemyUnit = enemyGO.GetComponent<Unit>();

        dialogueText.text = "A " + enemyUnit.unitName + " approaches!";

        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();

       
    }
    void PlayerTurn()
    {
        dialogueText.text = "Choose an action...";
    }

    IEnumerator PlayerHeal()
    {
        playerUnit.Heal(5);

        playerHUD.SetHP(playerUnit.currentHP);
        dialogueText.text = "You healed yourself!";

        yield return new WaitForSeconds(2f);

        state = BattleState.ENEMYTURN;
        StartCoroutine (EnemyTurn());
    }

    IEnumerator PlayerAttack()
    {
        bool isDead = enemyUnit.TakeDamage(playerUnit.damage);

        enemyHUD.SetHP(enemyUnit.currentHP);
        dialogueText.text = "Enemy damaged.";

        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            state = BattleState.WIN;
            EndBattle();
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
        
    }



    IEnumerator EnemyTurn()
    {
        dialogueText.text = enemyUnit.unitName + " attacks!";

        yield return new WaitForSeconds(1f);

        bool isDead = playerUnit.TakeDamage(enemyUnit.damage);

        playerHUD.SetHP(playerUnit.currentHP);

        yield return new WaitForSeconds(1f);

        if(isDead)
        {
            state = BattleState.LOSE;
            EndBattle();


        }
        else
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }
    

    void EndBattle()
    {
        

        if (state ==BattleState.WIN)
        {
            dialogueText.text = "You won!";


        }
        else
        {
            dialogueText.text = "You lose...";
        }

       

        StartCoroutine (WaitForScene());
        
        
    }
    
    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

       StartCoroutine (PlayerAttack());

    }

    public void OnHealButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerHeal());

    }

    IEnumerator WaitForScene()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("ok");
    }



}
