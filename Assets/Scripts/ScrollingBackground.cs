using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour {
    public float ScrollSpeed {
        get { return scrollBaseSpeed * speedRatio; }
    }
    public readonly float scrollBaseSpeed = 4f;
    public float speedRatio = 1f;

    Renderer _bgRend;
    float _backgroundWidth;

    private void Start() {
        _bgRend = GetComponent<Renderer>();
        _backgroundWidth = transform.localScale.x; // assuming the background component is on a quad
    }

    private void Update() {
        _bgRend.material.mainTextureOffset += new Vector2((ScrollSpeed / _backgroundWidth) * Time.deltaTime, 0f);
    }
}
