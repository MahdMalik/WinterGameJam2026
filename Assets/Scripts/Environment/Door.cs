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

    void Update()
    {
        if(Player.GetComponent<PlayerMovement>().OpeningDoor) {
            OpenDoor();
        }
    }
    public void OpenDoor() {
        if (doorCheckHitbox.IsTouchingLayers(LayerMask.GetMask("Player"))) {
            Debug.Log("AAAAA");
            StartCoroutine(OpenTheDoor(OpenTime));
        }
    }



 private IEnumerator OpenTheDoor(float time) {
    DoorSprite.sprite = PartClosed;
    yield return new WaitForSeconds(time);
    DoorSprite.sprite = PartOpen;
    doorHitbox.isTrigger = true;
    yield return new WaitForSeconds(time);
    DoorSprite.sprite = Open;
    yield return new WaitForSeconds(time);
 }
}
