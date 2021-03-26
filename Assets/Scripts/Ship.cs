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
    [SerializeField] protected float missileSpeed;
    [SerializeField] protected Vector3 shootOffset;
    [SerializeField] protected Sprite missileSprite;
    [SerializeField] protected bool flipSprite;
    protected float objectWidth, objectHeight;
    protected Vector3 screenbounds;
    protected bool canFire = true;
    protected Animator animExplosion;//, animShoot;
    protected SpriteRenderer srModel;
    protected Sprite sprite;
    protected bool IsAlive {
        get { return _isAlive; }
        set { 
            canFire = _isAlive = value;
            srModel.sprite = value ? sprite : null;
            animExplosion.SetBool("Alive", IsAlive);
            //Debug.Log("sprite: " + sr.sprite);
        }
    }
    protected bool _isAlive = true;

    protected virtual void Awake() {
        animExplosion = GetComponent<Animator>();
        //animShoot = transform.GetChild(1).GetComponent<Animator>();
        srModel = transform.GetChild(0).GetComponent<SpriteRenderer>();
        sprite = srModel.sprite;

        screenbounds = GameManager.GetScreenBounds();
        objectWidth = srModel.bounds.extents.x;
        objectHeight = srModel.bounds.extents.y;

    }

    private void Start() {
        IsAlive = true;
    }

    protected virtual void Update() {
        Move();
        Shoot();
    }

    protected abstract void Move();

    protected abstract void Shoot();

    protected void SpawnMissile() {
        Missile missile = Instantiate(prefabMissile, transform.position + shootOffset, transform.rotation);
        missile.moveVector *= missileSpeed;
        missile.sr.sprite = missileSprite;
        missile.sr.flipX = flipSprite;
    }

    protected void Reload() {
        canFire = IsAlive;
    }

    protected virtual void OnHit() {
    }
    protected virtual void Die() {
        Destroy(gameObject);
    }
}