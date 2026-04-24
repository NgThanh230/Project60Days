using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    CharacterScriptableObject characterData;
    
    float currentHealth;  
    float currentRecovery;
    float currentMoveSpeed;
    float currentMight;
    float currentProjectileSpeed;
    float currentMagnet;
    //kinh nghiệm và level cho nhân vật
    [Header("Experience/Level")]
    public int experience = 0;
    public int level = 1;
    public int experienceCap = 100;
    public int experienceCapIncrease;
   

    [Header("I-Frames")]
    public float invincibilityDuration;
    float invincibilityTimer;
    bool isInvincible;

    InventoryManager inventory;
    public int weaponIndex;
    public int passiveIndex;

    public GameObject passiveItemCheck1, passiveItemCheck2;
    public GameObject spawnWeaponCheck;

    #region Stats Properties
    public float CurrentHealth
    {
        get { return currentHealth; }
        set
        {
            //check nếu giá trị thay đổi thì set lại
            if (value != currentHealth)
            {
                currentHealth = value;
                if (GameManager.instance != null)
                {
                    GameManager.instance.HealthDisplay.text = "Health: " + currentHealth;
                }
            }   
        }
    }
    public float CurrentRecovery
    {
        get { return currentRecovery; }
        set
        {
            //check nếu giá trị thay đổi thì set lại
            if (value != currentRecovery)
            {
                currentRecovery = value;
                if (GameManager.instance != null)
                {
                    GameManager.instance.RecoveryDisplay.text = "Recovery: " + currentRecovery;
                }
            }
        }
    }
    public float CurrentMoveSpeed
    {
        get { return currentMoveSpeed; }
        set
        {
            //check nếu giá trị thay đổi thì set lại
            if (value != currentMoveSpeed)
            {
                currentMoveSpeed = value;
                if (GameManager.instance != null)
                {
                    GameManager.instance.MoveSpeedDisplay.text = "Move Speed: " + currentMoveSpeed;
                }
            }
        }
    }
    public float CurrentMight
    {
        get { return currentMight; }
        set
        {
            //check nếu giá trị thay đổi thì set lại
            if (value != currentMight)
            {
                currentMight = value;
                if (GameManager.instance != null)
                {
                    GameManager.instance.MightDisplay.text = "Might: " + currentMight;
                }
            }
        }
    }
    public float CurrentProjectileSpeed
    {
        get { return currentProjectileSpeed; }
        set
        {
            //check nếu giá trị thay đổi thì set lại
            if (value != currentProjectileSpeed)
            {
                currentProjectileSpeed = value;
                if (GameManager.instance != null)
                {
                    GameManager.instance.ProjectileSpeedDisplay.text = "Projectile Speed: " + currentProjectileSpeed;
                }
            }
        }
    }
    public float CurrentMagnet
    {
        get { return currentMagnet; }
        set
        {
            //check nếu giá trị thay đổi thì set lại
            if (value != currentMagnet)
            {
                currentMagnet = value;
                if (GameManager.instance != null)
                {
                    GameManager.instance.MagnetDisplay.text = "Magnet: " + currentMagnet;
                }
            }
        }
    }
    #endregion
    private void Start()
    {
        GameManager.instance.HealthDisplay.text = "Health: " + currentHealth;
        GameManager.instance.RecoveryDisplay.text = "Recovery: " + currentRecovery;
        GameManager.instance.MoveSpeedDisplay.text = "Move Speed: " + currentMoveSpeed;
        GameManager.instance.MightDisplay.text = "Might: " + currentMight;
        GameManager.instance.ProjectileSpeedDisplay.text = "Projectile Speed: " + currentProjectileSpeed;
        GameManager.instance.MagnetDisplay.text = "Magnet: " + currentMagnet;
    }
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

        inventory = GetComponent<InventoryManager>();//tham chiếu tới InventoryManager
        CurrentHealth = characterData.Maxhealth;
        CurrentRecovery = characterData.Recovery;
        CurrentMoveSpeed = characterData.MoveSpeed;
        CurrentMight = characterData.Might;
        CurrentProjectileSpeed = characterData.ProjectileSpeed;
        CurrentMagnet = characterData.Magnet;

        SpawnWeapon(characterData.StartingWeapon);
        SpawnWeapon(spawnWeaponCheck);
        SpawnPassiveItem(passiveItemCheck1);
        SpawnPassiveItem(passiveItemCheck2);
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
            CurrentHealth -= damage;
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
        if (CurrentHealth < characterData.Maxhealth)
        {
            CurrentHealth += amount;
            //khi hồi thừa máu quá mức thì set cho bằng chỉ số máu tối đa của nhân vật
            if (CurrentHealth > characterData.Maxhealth)
            {
                CurrentHealth = characterData.Maxhealth;
            }
        }
        
    }
    void Recover()
    {
        if (CurrentHealth < characterData.Maxhealth)
        {
            //hồi máu theo chỉ số * thời gian thực
            CurrentHealth += CurrentRecovery * Time.deltaTime;
            //khi hồi thừa máu quá mức thì set cho bằng chỉ số máu tối đa của nhân vật
            if (CurrentHealth > characterData.Maxhealth)
            {
                CurrentHealth = characterData.Maxhealth;
            }
        }
    }

    public void SpawnWeapon(GameObject weapon)
    {
        //check slot trong inventory full chưa, -1 vì .count không đếm từ số 0 còn list bắt đầu từ 0)
        if (weaponIndex >= inventory.weaponSlots.Count - 1)
        {
            Debug.LogError("Inventory full");
            return;
        }
        //spawn vũ khí khởi đầu
        GameObject spawnedWeapon = Instantiate(weapon, transform.position, Quaternion.identity);
        spawnedWeapon.transform.SetParent(transform); //set vũ khí cho nhân vật
        inventory.AddWeapon(weaponIndex, spawnedWeapon.GetComponent<WeaponController>());// thêm vũ khí vào inventory
        weaponIndex++;
    }

    public void SpawnPassiveItem(GameObject passiveItem)
    {
        //check slot trong inventory full chưa, -1 vì .count không đếm từ số 0 còn list bắt đầu từ 0)
        if (passiveIndex >= inventory.passiveItemSlots.Count - 1)
        {
            Debug.LogError("Inventory full");
            return;
        }
        //spawn item thụ động khởi đầu
        GameObject spawnedPassiveItem = Instantiate(passiveItem, transform.position, Quaternion.identity);
        spawnedPassiveItem.transform.SetParent(transform); //set vũ khí cho nhân vật
        inventory.AddPassiveItem(passiveIndex, spawnedPassiveItem.GetComponent<PassiveItem>());// thêm vũ khí vào inventory
        passiveIndex++; 
    }
    public void Kill()
    {
        Debug.Log("Dead");
    }
}
