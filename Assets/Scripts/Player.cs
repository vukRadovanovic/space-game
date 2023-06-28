using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    // Movement
    [SerializeField] float moveSpeed = 5.0f;
    Vector2 rawInput;

    // Playable Area
    Vector2 minBounds;
    Vector2 maxBounds;

    Shooter shooter;

    void Awake() {
        shooter = GetComponent<Shooter>();
    }

    void Start() {
        InitBounds();
    }

    void InitBounds() {
        Camera mainCamera = Camera.main;
        minBounds = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
        maxBounds = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));

        SpriteRenderer playerSprite = GetComponentInChildren<SpriteRenderer>();

        Vector2 spriteBounds = new Vector2(playerSprite.bounds.extents.x, 
                                            playerSprite.bounds.extents.y);

        minBounds.x += spriteBounds.x;
        minBounds.y += spriteBounds.y;
        maxBounds.x -= spriteBounds.x;
        maxBounds.y -= spriteBounds.y;
    }

    void Update()
    {
        PlayerMove();
    }

    void PlayerMove()
    {
        Vector2 delta = rawInput * moveSpeed * Time.deltaTime;
        Vector2 newPos = new Vector2();
        newPos.x = Mathf.Clamp(transform.position.x + delta.x, minBounds.x, maxBounds.x);
        newPos.y = Mathf.Clamp(transform.position.y + delta.y, minBounds.y, maxBounds.y);
        transform.position = newPos;
    }

    void OnMove(InputValue value) {
        rawInput = value.Get<Vector2>();
    }

    void OnFire(InputValue value) {
        if(shooter != null) {
            shooter.isFiring = value.isPressed;
        }
    }

}
