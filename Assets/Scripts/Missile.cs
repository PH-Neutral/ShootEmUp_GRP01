using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour {
    public Vector3 moveVector;
    Vector3 boundsMin, boundsMax;

    private void Awake() {
        transform.SetParent(GameManager.Instance.poolMissile.transform);
    }

    void Start() {
        Vector3 screenbounds = GameManager.GetScreenBounds();
        float objectWidth = GetComponent<SpriteRenderer>().bounds.extents.x;
        float objectHeight = GetComponent<SpriteRenderer>().bounds.extents.y;
        boundsMin = new Vector3(-screenbounds.x - objectWidth, -screenbounds.y - objectHeight);
        boundsMax = new Vector3(screenbounds.x + objectWidth, screenbounds.y + objectHeight);
    }

    void Update() {
        if (transform.position.x < boundsMin.x - 2 || transform.position.x > boundsMax.x + 2
            || transform.position.y < boundsMin.y - 2 || transform.position.y > boundsMax.y + 2) {
            Die();
        }
        transform.Translate(moveVector * Time.deltaTime);
    }

    void Die() {
        Destroy(gameObject);
    }
}
