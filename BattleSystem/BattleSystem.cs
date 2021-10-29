using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST}


public class BattleSystem : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    Unit playerUnit;
    Unit enemyUnit;

    public Text dialogueText;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;

    public GameObject WonUI;
    public GameObject LossUI;

    public BattleState state;

    //for animations
    CharacterController MyController;

    Animator MyAnimator;


    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());
        MyAnimator = GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.None;

    }

    IEnumerator SetupBattle()
    {
        GameObject playerGo = Instantiate(playerPrefab);
        playerUnit = playerGo.GetComponent<Unit>();

        GameObject enemyGo = Instantiate(enemyPrefab);
        enemyUnit = enemyGo.GetComponent<Unit>();

        dialogueText.text = "A wild " + enemyUnit.unitName + " is coming......";

        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    IEnumerator PlayerAttack()
    {
        bool isDead = enemyUnit.TakeDamage(playerUnit.damage);

        enemyHUD.setHP(enemyUnit.currentHP);
        dialogueText.text = "The attack is successful";

        yield return new WaitForSeconds(2f);

        if(isDead)
        {
            WonUI.SetActive(true);
            state = BattleState.WON;
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
        MyAnimator.SetTrigger("take hit");
        yield return new WaitForSeconds(1f);

        bool isDead = playerUnit.TakeDamage(enemyUnit.damage);

        playerHUD.setHP(playerUnit.currentHP);

        

        yield return new WaitForSeconds(1f);

        if(isDead)
        {
            LossUI.SetActive(true);
            state = BattleState.LOST;
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
        if(state == BattleState.WON)
        {
            dialogueText.text = "You had won the battle!!!!";
        }
        else if(state == BattleState.LOST)
        {
            dialogueText.text = "You had lost the battle...";
        }
    }

    void PlayerTurn()
    {
        dialogueText.text = "Choose an action: ";
    }

    IEnumerator PlayerHeal()
    {
        playerUnit.Heal(7);

        playerHUD.setHP(playerUnit.currentHP);
        dialogueText.text = "You feel stronger now...";

        yield return new WaitForSeconds(2f);

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerAttack());
    }

    public void OnAHealButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerHeal());
    }
}
