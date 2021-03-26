using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : EnemyShip {

    protected override void Move() {
        base.Move();
        if (transform.position.x < screenbounds.x * 0.5f) {
            Vector3 pos = transform.position;
            pos.x = screenbounds.x * 0.5f;
            transform.position = pos;
        }
    }

    protected override void Die() {
        GameManager.Instance.WinGame();
        base.Die();
    }
}