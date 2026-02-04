using UnityEngine;
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerHealth: MonoBehaviour, IDamageable
{
    [SerializeField] float maxHealth = 100f;
    [SerializeField] float invulnerabilityDuration = 1f;
    [SerializeField] float blinkInterval = 0.1f;

    float currentHealth;
    float invulnerabilityTimer;
    SpriteRenderer sprite;
    float blinktimer;
    bool blinking;

    void Awake()
    {
        currentHealth = maxHealth;
        sprite = GetComponent<SpriteRenderer>();
    }
    void update()
    {
        if(invulnerabilityTimer > 0f)
        {
            invulnerabilityTimer-=Time.deltaTime;
            HandleBlink();
        }
    }
    public bool ApplyDamage(float amount)
    {
        if(currentHealth <= 0f || invulnerabilityTimer > 0f)
        return(false);
        currentHealth -= amount;
        if(currentHealth <= 0f)
        {
            Die();
            return true;
        
        }
        invulnerabilityTimer = invulnerabilityDuration;
        StartBlink(invulnerabilityDuration);
        return true;
    }
    void StartBlink(float duration)
    {
        blinking = true;
        blinktimer = duration;
    }
    void HandleBlink()
    {
        if(!blinking) 
        {
            return;
        }
        blinktimer -= Time.deltaTime;
        if(blinktimer <= 0f)
        {
            blinking = false;
            sprite.enabled = true;
            return;
        }
        sprite.enabled = Mathf.FloorToInt(blinktimer/blinkInterval) % 2 == 0;
    }
    void Die()
    {
        gameObject.SetActive(false);
    }
}