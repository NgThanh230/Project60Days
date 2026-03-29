using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    CharacterScriptableObject characterData;
    [HideInInspector]
    public float currentHealth;
    [HideInInspector]
    public float currentRecovery;
    [HideInInspector]
    public float currentMoveSpeed;
    [HideInInspector]
    public float currentMight;
    [HideInInspector]
    public float currentProjectileSpeed;
    [HideInInspector]
    public float currentMagnet;
    //kinh nghiệm và level cho nhân vật
    [Header("Experience/Level")]
    public int experience = 0;
    public int level = 1;
    public int experienceCap = 100;
    public int experienceCapIncrease;
    //vũ khí khởi đầu 
    public List<GameObject> spawnedWeapons;

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
        Recover();
    }
    void Awake()
    {
        characterData = CharacterSelector.GetData();
        CharacterSelector.instance.DestroySingleton();

        currentHealth = characterData.Maxhealth;
        currentRecovery = characterData.Recovery;
        currentMoveSpeed = characterData.MoveSpeed;
        currentMight = characterData.Might;
        currentProjectileSpeed = characterData.ProjectileSpeed;
        currentMagnet = characterData.Magnet;

        SpawnWeapon(characterData.StartingWeapon);
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
    void Recover()
    {
        if (currentHealth < characterData.Maxhealth)
        {
            //hồi máu theo chỉ số * thời gian thực
            currentHealth += currentRecovery * Time.deltaTime;
            //khi hồi thừa máu quá mức thì set cho bằng chỉ số máu tối đa của nhân vật
            if (currentHealth > characterData.Maxhealth)
            {
                currentHealth = characterData.Maxhealth;
            }
        }
    }

    public void SpawnWeapon(GameObject weapon)
    {
        //spawn vũ khí khởi đầu
        GameObject spawnedWeapon = Instantiate(weapon, transform.position, Quaternion.identity);
        spawnedWeapon.transform.SetParent(transform); //set vũ khí cho nhân vật
        spawnedWeapons.Add(spawnedWeapon); //add vào list vũ khí khởi đầu
    }
    public void Kill()
    {
        Debug.Log("Dead");
    }
}
