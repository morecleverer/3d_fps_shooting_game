using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Player : MonoBehaviour
{
    public float health = 100f;
    public Image bloodScreen;
    public Image healthbar;
    public Image bosshealthbar;
    public GameObject bosshealth;
    public TextMeshProUGUI stageText;
    public GameObject door1;
    public GameObject door2;
    public GameObject door3;
    public GameObject door4;
    public GameObject door5;
    public GameObject door6;
    public GameObject GameOver;
    public GameObject gameClear;
    public GameObject Spawner1;
    public GameObject Spawner2;
    public GameObject Spawner3;
    public GameObject Spawner4;
    public GameObject boss;
    public Animator panel_Anim;
    public WeaponAssaultRifle weaponAssaultRifle;
    public bool isClose1;
    public bool isClose2;
    public bool isDie;
    public bool isDone;
    public int stage = 0;
    public int catchEnemy = 0;
    public int stage1enemy = 30;
    public Text enemyText;
    void Awake()
    {
    }

    // Update is called once per frame
    void Update()
    {
        healthbar.fillAmount = health / 100;
        bosshealthbar.fillAmount = (float)boss.GetComponent<Enemy>().health / 5000;

        if(stage==0 && catchEnemy >= stage1enemy)
        {
            stage++;
            door3.transform.Rotate(new Vector3(0, 80, 0), Space.Self);
            door4.transform.Rotate(new Vector3(0, -80, 0), Space.Self);
            panel_Anim.SetTrigger("not");
            stageText.text = "STAGE1 CLEAR";
            health = 100;
        }

        if(health<=0 && !isDie)
        {
            isDie = true;

            weaponAssaultRifle.weaponSetting.currentAmmo = 0;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            GameOver.SetActive(true);
        }

        if(boss.GetComponent<Enemy>().health<=60 && catchEnemy < 84)
        {
            if (!isDone)
            {
                isDone = true;
                panel_Anim.SetTrigger("not");
                stageText.color = Color.white;
                stageText.text = "Kill All Enemy";

            }
            
            boss.GetComponent<Enemy>().health = 60;
        }

        if (boss.GetComponent<Enemy>().health <= 0)
        {
            Invoke("GameClear", 3f);
        }
        if (isDone && catchEnemy >= 84)
            boss.GetComponent<Enemy>().speed = 4;

        enemyText.text = catchEnemy.ToString();
    }

    void GameClear()
    {
        isDie = true;

        weaponAssaultRifle.weaponSetting.currentAmmo = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        gameClear.SetActive(true);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ImpactObstacle")
        {
            health -= 10f;
            StartCoroutine(BloodScreen());
        }
        if (  other.tag == "EnemyWeapon")
        {
            health -= 20f;
            StartCoroutine(BloodScreen());
        }
        if (other.tag == "StartWall" && !isClose1)
        {
            isClose1 = true;
            Spawner1.SetActive(true);
            Spawner2.SetActive(true);
            door1.transform.Rotate(new Vector3(0, 80, 0),Space.Self);
            door2.transform.Rotate(new Vector3(0, -80, 0),Space.Self);
            stageText.text = "STAGE 1";
            panel_Anim.SetTrigger("not");
        }
        if (other.tag == "BossWall" && !isClose2)
        {
            isClose2 = true;
            Spawner3.SetActive(true);
            Spawner4.SetActive(true);
            door5.transform.Rotate(new Vector3(0, 80, 0), Space.Self);
            door6.transform.Rotate(new Vector3(0, -80, 0), Space.Self);
            stageText.text = "STAGE BOSS";
            panel_Anim.SetTrigger("not");
            stageText.color = Color.red;
            boss.SetActive(true);
            bosshealth.SetActive(true);
        }
        if(other.tag == "Falling")
        {
            health = 0;
        }
    }

    public void Replay()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    IEnumerator BloodScreen()
    {
        bloodScreen.color = new Color(1, 0, 0, UnityEngine.Random.Range(0.3f, 0.4f));
        yield return new WaitForSeconds(0.2f);
        bloodScreen.color = Color.clear;
    }
}
