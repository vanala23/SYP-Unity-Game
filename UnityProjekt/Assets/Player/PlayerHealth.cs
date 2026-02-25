using UnityEngine;

public class PlayerHealth : MonoBehaviour{
    [Header("Stats")]
    [SerializeField] private int maxHP = 5;

    private Animator animator;
    private int currentHP;
    private bool isInvincible;
    private float invincibleTime = 0.5f;

    private void Awake(){
        currentHP = maxHP;
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(int amount){
        if(isInvincible)
            return;

        currentHP -= amount;

        Debug.Log("Player HP: " + currentHP);

        if(currentHP <= 0)
            Die();
        else
            StartCoroutine(Invincibility());
    }

    private System.Collections.IEnumerator Invincibility(){
        isInvincible = true;
        yield return new WaitForSeconds(invincibleTime);
        isInvincible = false;
    }

    private void Die(){
        Debug.Log("Player died");
        gameObject.SetActive(false);
    }
}