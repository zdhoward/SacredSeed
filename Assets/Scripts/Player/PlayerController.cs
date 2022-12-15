using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using MoreMountains.Feedbacks;
using MoreMountains.FeedbacksForThirdParty;
using MoreMountains.Tools;

public class PlayerController : MonoBehaviour
{
    public static event EventHandler<int> OnHealthChange;
    public static event EventHandler OnStart;
    public static event EventHandler OnDeath;

    [SerializeField] private int maxHealth = 3;
    [SerializeField] private float invulnerabilityTime = 1f;
    [SerializeField] private float timeToRespawn = 2f;

    [SerializeField] private Transform flowerPrefab;

    private Transform playerSpawnPosition;

    private MMF_Player mmfPlayer_OnHit;

    private int health;
    public bool isDead { get; private set; } = false;
    private bool isInvulnerable = false;

    private void Awake()
    {
        mmfPlayer_OnHit = transform.Find("MMF_Player_OnHit").GetComponent<MMF_Player>();
        mmfPlayer_OnHit.Initialization();

        playerSpawnPosition = GameObject.FindWithTag("PlayerSpawn").transform;
    }

    private void Start()
    {
        transform.position = playerSpawnPosition.position;

        OnStart?.Invoke(this, EventArgs.Empty);

        health = maxHealth;
        OnHealthChange?.Invoke(this, health);

        isDead = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "Hazards":
                TakeDamage();
                break;
            case "NextExtentTrigger":
                LevelGenerator.Instance.GenerateNextExtent();
                other.gameObject.SetActive(false);
                break;
            case "LevelExit":
                if (SceneManager.GetActiveScene().name == "AM_Depths")
                    LoadingManager.Instance.LoadScene("Level_2");
                if (SceneManager.GetActiveScene().name == "Level_2")
                    LoadingManager.Instance.LoadScene("Level_3");
                if (SceneManager.GetActiveScene().name == "Level_3")
                    LoadingManager.Instance.LoadScene("WinScreen");
                break;
            case "ItemHealth":
                RestoreHealth();
                Destroy(other.gameObject);
                break;
            default:
                break;
        }
    }

    private void RestoreHealth()
    {
        health++;
        if (health > maxHealth)
            health = maxHealth;

        OnHealthChange?.Invoke(this, health);
    }

    private void TakeDamage()
    {
        if (isDead)
            return;

        if (!isInvulnerable)
        {
            mmfPlayer_OnHit.PlayFeedbacks();
            StartCoroutine(InvulnerabilityAfterDamage());
            health--;
            if (health <= 0)
            {
                Die();
            }

            OnHealthChange?.Invoke(this, health);
        }
    }

    private void Die()
    {
        OnDeath?.Invoke(this, EventArgs.Empty);

        isDead = true;
        health = 0;
        Invoke("Respawn", timeToRespawn);
    }

    private void Respawn()
    {
        Instantiate(flowerPrefab, transform.position, Quaternion.identity);
        Start();
    }

    IEnumerator InvulnerabilityAfterDamage()
    {
        isInvulnerable = true;
        yield return new WaitForSeconds(invulnerabilityTime);
        isInvulnerable = false;
    }
}
