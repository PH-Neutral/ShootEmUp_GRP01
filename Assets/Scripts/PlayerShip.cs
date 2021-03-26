using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : Ship {

    protected override void Move() {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");
        Vector3 inputs = new Vector3(inputX, inputY);
        Vector3 pos = transform.position + inputs * speed * Time.deltaTime;
        // clamp position to the camera bounds
        pos.x = Mathf.Clamp(pos.x, -screenbounds.x + objectWidth, screenbounds.x - objectWidth);
        pos.y = Mathf.Clamp(pos.y, -screenbounds.y + objectHeight, screenbounds.y - objectHeight);
        transform.position = pos;
    }

    protected override void Shoot() {
        if(Input.GetAxis("Fire1") >= 1 && canFire) {
            SpawnMissile();
            Invoke("Reload", 1f / fireRate);
            canFire = false;
        }
    }

    protected override void OnHit()
    {
        GameManager.Instance.AddLife(-1);
        if (GameManager.Instance.life < 1)
        {
            IsAlive = false;
        }
    }

  /*  protected override void Die()
    {
        // play explosion
        Destroy(gameObject);
    } */

     void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == tagEnemy)
        {
            OnHit();
            if (collision.TryGetComponent<Missile>(out Missile missile))
            {
                Destroy(collision.gameObject);
            }
        }
    }
}
