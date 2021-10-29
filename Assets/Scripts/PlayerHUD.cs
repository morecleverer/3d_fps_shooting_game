using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHUD : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]
    private WeaponAssaultRifle weapon;

    [Header("Weapon Base")]
    [SerializeField]
    private TextMeshProUGUI textWeaponName;
    [SerializeField]
    private Image imageWeaponIcon;
    [SerializeField]
    private Sprite[] spritesWeaponIcons;

    [Header("Ammo")]
    [SerializeField]
    private TextMeshProUGUI textAmmo;

    [Header("Magazine")]
    [SerializeField]
    private GameObject magazineUIPrefab;
    [SerializeField]
    private Transform magazineParent;

    private List<GameObject> magazineList;


    // Start is called before the first frame update
    void Awake()
    {
        SetupWeapon();
        SetupMagazine();

        weapon.onAmmoEvent.AddListener(UpdateAmmoHUD);
        weapon.onMagazineEvent.AddListener(UpdateMagazineHUD);
    }

    private void SetupWeapon()
    {
        textWeaponName.text = weapon.weaponName.ToString();
        imageWeaponIcon.sprite = spritesWeaponIcons[(int)weapon.weaponName];
    }
    private void UpdateAmmoHUD(int currentAmmo, int maxAmmo)
    {
        textAmmo.text = $"<size=40>{currentAmmo}/</size>{maxAmmo}";
    }

    private void SetupMagazine()
    {
        magazineList = new List<GameObject>();
        for(int i = 0; i < weapon.MaxMagazine; ++i)
        {
            GameObject clone = Instantiate(magazineUIPrefab);
            clone.transform.SetParent(magazineParent);
            clone.SetActive(false);

            magazineList.Add(clone);
        }
        for (int i = 0; i < weapon.CurrentMagazine; ++ i)
        {
            magazineList[i].SetActive(true);
        }
    }
    private void UpdateMagazineHUD(int currentMagazine)
    {
        for(int i = 0; i < magazineList.Count; ++ i)
        {
            magazineList[i].SetActive(false);
        }
        for(int i = 0; i <currentMagazine; ++ i)
        {
            magazineList[i].SetActive(true);
        }
    } 
}
