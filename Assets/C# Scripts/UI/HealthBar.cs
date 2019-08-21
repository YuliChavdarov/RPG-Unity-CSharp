using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

    RectTransform healthBar;
    CharacterStats characterStats;
    float healthPercentage;
    Text healthText;

	// Use this for initialization
	void Start () {
        healthBar = GetComponent<RectTransform>();
        characterStats = GetComponentInParent<CharacterStats>();
        healthText = transform.parent.GetComponentInChildren<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        healthPercentage = (float) characterStats.currentHealth / characterStats.maxHealth.GetValue();
        healthBar.localScale = new Vector3(healthPercentage, 1f, 1f);

        if (healthPercentage <= 0.33f)
        {
            this.GetComponent<Image>().color = Color.red;
        }
        if(healthPercentage > 0.33f && healthPercentage <= 0.67f)
        {
            this.GetComponent<Image>().color = Color.yellow;
        }
        if(healthPercentage > 0.67f)
        {
            this.GetComponent<Image>().color = Color.green;
        }
        healthText.text = characterStats.currentHealth + "/" + characterStats.maxHealth.GetValue();
	}
}
