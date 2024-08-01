using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Obstacle : Entity
{
    [SerializeField] private int health;
    public TextMeshPro healthText;
    private int curHealth;

    public override void Start()
    {
        base.Start();
        health = Random.Range(GameManager.Instance.Round, GameManager.Instance.Round*3);
        curHealth = health;

        healthText.text = curHealth.ToString();
    }

    public override void Update()
    {
        base.Update();
        healthText.text = curHealth.ToString();


        if (transform.position.y <= 1)
        {
            GameManager.Instance.isGameEnd = true;
        }
    }

    public void takeDamage() {
        curHealth -= 1;
        if (curHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

}
