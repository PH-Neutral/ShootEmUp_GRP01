using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : Ship {

    protected override void Move() {
        if (transform.position.x < -screenbounds.x - objectWidth) {
            Die();
        }
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }

    protected override void Shoot() {
        if(canFire) {
            SpawnMissile();
            Invoke("Reload", 1f / fireRate);
            canFire = false;
        }
    }

    void Die() {
        Vector3 newPos = transform.position;
        newPos.x = screenbounds.x + objectWidth + 4;
        transform.position = newPos;
    }
}