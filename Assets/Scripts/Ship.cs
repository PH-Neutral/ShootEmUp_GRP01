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
        objectWidth = GetComponentInChildren<SpriteRenderer>().bounds.extents.x;
        objectHeight = GetComponentInChildren<SpriteRenderer>().bounds.extents.y;
    }

    private void Update() {
        Move();
        Shoot();
    }

    protected abstract void Move();

    protected abstract void Shoot();

    protected void SpawnMissile() {
        Missile missile = Instantiate(prefabMissile, transform.position + shootOffset, transform.rotation);
    }

    protected void Reload() {
        canFire = true;
    }

    protected virtual void OnHit() {
    }
    protected virtual void Die() {
    }
}