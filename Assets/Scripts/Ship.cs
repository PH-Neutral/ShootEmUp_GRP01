using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ship : MonoBehaviour {
    public static string tagPlayer = "Player", tagEnemy = "Enemy";

    [SerializeField] protected Missile prefabMissile;
    [SerializeField] protected float speed;
    /// <summary>
    /// Rate of fire in shots per second.
    /// </summary>
    [SerializeField] protected float fireRate;
    [SerializeField] protected Vector3 shootOffset;
    protected float objectWidth, objectHeight;
    protected Vector3 screenbounds;
    protected bool canFire = true;

    private void Start() {
        screenbounds = GameManager.GetScreenBounds();
        objectWidth = GetComponent<SpriteRenderer>().bounds.extents.x;
        objectHeight = GetComponent<SpriteRenderer>().bounds.extents.y;
    }

    private void Update() {
        Move();
        Shoot();
    }

    protected abstract void Move();

    protected abstract void Shoot();

    protected void SpawnMissile() {
        Missile missile = Instantiate(prefabMissile, transform.position + shootOffset, transform.rotation);
        missile.tag = tag;
    }

    protected void Reload() {
        canFire = true;
    }

    protected virtual void OnHit() {
        GameManager.Instance.AddLife(-1);
        if (GameManager.Instance.life < 1) {
            Die();
        }
    }
    protected virtual void Die() {
        // play explosion
        GameManager.Instance.LoseGame();
        Destroy(gameObject);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == tagEnemy) {
            OnHit();
            if (collision.TryGetComponent<Missile>(out Missile missile)) {
                Destroy(collision.gameObject);
            }
        }
    }
}