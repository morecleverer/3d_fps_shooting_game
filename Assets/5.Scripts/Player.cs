using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float health = 100f;
    public Image bloodScreen;
    public Image healthbar;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        healthbar.fillAmount = health / 100;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ImpactObstacle")
        {
            health -= 10f;
            StartCoroutine(BloodScreen());
        }
    }

    IEnumerator BloodScreen()
    {
        bloodScreen.color = new Color(1, 0, 0, UnityEngine.Random.Range(0.3f, 0.4f));
        yield return new WaitForSeconds(0.2f);
        bloodScreen.color = Color.clear;
    }
}
