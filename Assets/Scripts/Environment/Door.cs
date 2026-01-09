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
    [SerializeField] BoxCollider2D doorHitbox;
    [SerializeField] float OpenTime;

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OpenDoor() {
        if (doorHitbox.IsTouchingLayers(LayerMask.GetMask("Player"))) {
            StartCoroutine(OpenTheDoor(OpenTime));
        }
    }



 private IEnumerator OpenTheDoor(float time) {
    DoorSprite.sprite = PartClosed;
    yield return new WaitForSeconds(time);
    DoorSprite.sprite = PartOpen;
    yield return new WaitForSeconds(time);
    DoorSprite.sprite = Open;
    yield return new WaitForSeconds(time);
 }
}
