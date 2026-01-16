using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] Sprite Closed;
    [SerializeField] Sprite Open;
    [SerializeField] Sprite PartOpen;
    [SerializeField] Sprite PartClosed;
    [SerializeField] private SpriteRenderer DoorSprite;
    [SerializeField] BoxCollider2D doorCheckHitbox;
    [SerializeField] BoxCollider2D doorHitbox;
    [SerializeField] float OpenTime;
    [SerializeField] private GameObject Player;
    private bool SealOpened;
    private int DoorOpen = 0;

    void Update()
    {
        if(Player.GetComponent<PlayerMovement>().OpeningDoor) {
            ChangeDoor();
        }
    }
    public void ChangeDoor() {
        if (doorCheckHitbox.IsTouchingLayers(LayerMask.GetMask("Player"))) {
            if (DoorOpen == 0) {
                DoorOpen = 2;
                StartCoroutine(OpenTheDoor(OpenTime));
            } else if (DoorOpen == 1) {
                DoorOpen = 2;
                StartCoroutine(CloseTheDoor(OpenTime));
            }
        }
    }



    private IEnumerator OpenTheDoor(float time) {
        DoorSprite.sprite = PartClosed;
        yield return new WaitForSeconds(time);
        DoorSprite.sprite = PartOpen;
        yield return new WaitForSeconds(time);
        DoorSprite.sprite = Open;
        doorHitbox.isTrigger = true;
        yield return new WaitForSeconds(time);
        DoorOpen = 1;
    }

    private IEnumerator CloseTheDoor(float time) {
        DoorSprite.sprite = PartOpen;
        doorHitbox.isTrigger = false;
        yield return new WaitForSeconds(time);
        DoorSprite.sprite = PartClosed;
        yield return new WaitForSeconds(time);
        DoorSprite.sprite = Closed;
        yield return new WaitForSeconds(time);
        DoorOpen = 0;
    }

    public int GetDoorStatus()
    {
        return DoorOpen;
    }
}
