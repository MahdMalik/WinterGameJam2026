using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] Sprite Closed;
    [SerializeField] Sprite Open;
    [SerializeField] private SpriteRenderer DoorSprite;

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OpenDoor() {
        DoorSprite.sprite = Open;
    }
}
