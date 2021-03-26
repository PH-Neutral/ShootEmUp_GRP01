using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : Ship {
    [SerializeField] int hp = 1;
    private Vector3 deplacement = new Vector3(-1,1,0);

    protected override void Move() {
        if (transform.position.x < -screenbounds.x - objectWidth) {
            Die();
        }
        transform.Translate(deplacement * speed * Time.deltaTime);

        if (transform.position.y < -screenbounds.y - objectHeight)
        {
            deplacement = new Vector3(-1, 1, 0);
        }

        if (transform.position.y > screenbounds.y + objectHeight)
        {
            deplacement = new Vector3(-1, -1, 0);
        } 
    }

    protected override void Shoot() {
        if(canFire) {
            SpawnMissile();
            Invoke("Reload", 1f / fireRate);
            canFire = false;
        }
    }

    protected override void OnHit() {
        if (--hp < 1) {
            GameManager.Instance.AddScore(10);
            Die();
        }
    }

    protected override void Die() {
        Vector3 newPos = transform.position;
        newPos.x = screenbounds.x + objectWidth + 4;
        transform.position = newPos;
    }


    void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag == tagPlayer) {
            OnHit();
            if(collision.TryGetComponent<Missile>(out Missile missile)) {
                Destroy(collision.gameObject);
            }
        }
    }

}