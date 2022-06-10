using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private Transform healthBar;
    [SerializeField] private Text healthText;
    [SerializeField] private float unfocusTimeLimit;

    private int health;
    private Vector2 healthBarDirection;
    private Image healthUI;
    private Image healthBarUI;

    public delegate void HealthAction();
    public event HealthAction OnDeath;

    // Start is called before the first frame update
    private void Start()
    {
        health = maxHealth;
        healthBarDirection = healthBar.localScale;
        healthUI = GetComponent<Image>();
        healthBarUI = healthBar.GetComponent<Image>();

        healthText.text = maxHealth.ToString();
        StartCoroutine(DecreaseOpacity());
    }

    public void TakeDamage(int damage)
    {
        Focus();

        if (damage >= health)
        {
            health = 0;
            OnDeath();
        }
        else
        {
            health -= damage;
        }

        float percent = ((float)health) / ((float)maxHealth);
        healthBar.localScale = Vector2.Scale(healthBarDirection, new Vector2(percent,1f));
        healthText.text = health.ToString();

        Unfocus();
    }

    public void Focus()
    {
        StopAllCoroutines();

        healthUI.color = new Color(1f, 1f, 1f, 1f);
        healthBarUI.color = new Color(19f / 256f, 188f / 256f, 34f / 256f, 1f);
        healthText.color = new Color(1f, 1f, 1f, 1f);
    }

    public void Unfocus()
    {
        StartCoroutine(DecreaseOpacity());
    }

    IEnumerator DecreaseOpacity()
    {
        yield return new WaitForSeconds(unfocusTimeLimit);

        for (float alpha = 1f; alpha >= 0.5; alpha -= 0.01f)
        {
            healthUI.color = new Color(1f, 1f, 1f, alpha);
            healthBarUI.color = new Color(19f / 256f, 188f / 256f, 34f / 256f, alpha);
            healthText.color = new Color(1f, 1f, 1f, alpha);
            yield return new WaitForSeconds(.1f);
        }

        yield return null;
    }
}
