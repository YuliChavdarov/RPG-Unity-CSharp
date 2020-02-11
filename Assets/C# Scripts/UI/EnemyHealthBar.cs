using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour {

    RectTransform healthBar;
    CharacterStats characterStats;
    float healthPercentage;
    Text healthText;

    public Enemy hoveredEnemy = null;

    public static EnemyHealthBar instance;
    void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start()
    {
        healthBar = this.GetComponent<RectTransform>();
        healthText = this.transform.parent.GetComponentInChildren<Text>();

    }

    void Update()
    {
        if (hoveredEnemy != null)
        {
            ShowEnemyHealth(hoveredEnemy);
        }
    }

    // Update is called once per frame
    public void ShowEnemyHealth(Enemy enemy)
    {
        healthBar.gameObject.SetActive(true);

        characterStats = enemy.GetComponentInParent<CharacterStats>();

        healthPercentage = (float)characterStats.currentHealth / characterStats.maxHealth.GetValue();
        healthBar.localScale = new Vector3(healthPercentage, 1f, 1f);

        if (healthPercentage <= 0.33f)
        {
            this.GetComponent<Image>().color = Color.red;
        }
        if (healthPercentage > 0.33f && healthPercentage <= 0.67f)
        {
            this.GetComponent<Image>().color = Color.yellow;
        }
        if (healthPercentage > 0.67f)
        {
            this.GetComponent<Image>().color = Color.green;
        }
        healthText.text = enemy.name;
        //characterStats.currentHealth + "/" + characterStats.maxHealth.GetValue()
    }
}
