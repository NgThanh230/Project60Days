using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public CharacterScriptableObject characterData;
    float currentHealth;
    float currentMoveSpeed;
    float currentMight;
    float currentProjectileSpeed;

    [Header("Experience/Level")]
    public int experience = 0;
    public int level = 1;
    public int experienceCap = 100;
    public int experienceCapIncrease;

    [Header("I-Frames")]
    public float invincibilityDuration;
    float invincibilityTimer;
    bool isInvincible;

    private void Update()
    {
        if (invincibilityTimer > 0)
        {
            invincibilityTimer -= Time.deltaTime;
        }else if (isInvincible)
        {
            isInvincible = false;
        }
    }
    void Awake()
    {
        currentHealth = characterData.Maxhealth;
        currentMoveSpeed = characterData.MoveSpeed;
        currentMight = characterData.Might;
        currentProjectileSpeed = characterData.ProjectileSpeed;
    }
    
    public void IncreaseExperience(int amount)
    {
        experience += amount;
        LevelUpChecker();
    }
    void LevelUpChecker()
    {
        //kinh nghiệm vượt quá số kn trong 1 cấp thì lên cấp và cộng bù số kn vượt quá
        if (experience >= experienceCap)
        {
            level++;
            experience -= experienceCap;
            experienceCap += experienceCapIncrease;
        }
    }
    public void TakeDamage(float damage)
    {
        //nếu không trong trạng thái miễn nhiễm thì cho phép mất máu và set lại tgian miễn nhiễm 
        if (!isInvincible)
        {
            currentHealth -= damage;
            invincibilityTimer = invincibilityDuration;
            isInvincible = true;
            //khi máu < 0 nv sẽ chết
            if (currentHealth <= 0)
            {
                Kill();
            }
        }
       
    }

    public void RestoreHealth(float amount)
    {
        //chỉ heal khi máu của nhân vật dưới chỉ số máu tối đa 
        if (currentHealth < characterData.Maxhealth)
        {
            currentHealth += amount;
            //khi hồi thừa máu quá mức thì set cho bằng chỉ số máu tối đa của nhân vật
            if (currentHealth > characterData.Maxhealth)
            {
                currentHealth = characterData.Maxhealth;
            }
        }
        
    }

    public void Kill()
    {
        Debug.Log("Dead");
    }
}
