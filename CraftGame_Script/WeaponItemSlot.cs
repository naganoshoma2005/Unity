using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

[System.Serializable]
public class WeaponInformation
{
    public string WeaponName;
    public Sprite WeaponSprite;
}
[System.Serializable]
public class SlotImage
{
    public string SlotWeaponName;
    public Image WeaponImage;
}

public class WeaponItemSlot : MonoBehaviour
{
    public List<SlotImage> ImageList;
    public List<WeaponInformation> WeaponList;
    public PlayerInventory PlayerInventory;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerInventory = FindAnyObjectByType<PlayerInventory>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (WeaponInformation weaponInformation in WeaponList)
        {
            
            if (PlayerInventory.craftInventry.ContainsKey(weaponInformation.WeaponName))
            {
                
                foreach (SlotImage slotImage in ImageList)
                {
                    if (string.IsNullOrEmpty(slotImage.SlotWeaponName))
                    {
                        slotImage.WeaponImage.sprite = weaponInformation.WeaponSprite;
                        slotImage.SlotWeaponName = weaponInformation.WeaponName;
                    }
                }
            }
        }


    }
}
