using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class IdleMovement : MonoBehaviour
{
    public Transform item;
    Vector3 targetPosition;

    Vector3 upLoc;
    Vector3 downLoc;

    // Start is called before the first frame update
    void Start()
    {
        downLoc = item.transform.position;
        item.transform.position = downLoc;
        upLoc = downLoc + Vector3.up * 2;

        item.transform.DOMove(upLoc, 2).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(item.transform.position, upLoc) < 0.2f)
        {
            targetPosition = downLoc;
        }
        else if (Vector3.Distance(item.transform.position, downLoc) < 0.2f)
        {
            targetPosition = upLoc;
        }

        item.rotation = item.rotation * Quaternion.Euler(0, 0.2f, 0);
    }

    //on trigger enter disables the item and it activates the green arrow to indicate the player where he has to go in order to spawn the item
    //it also sets the boolean that is used to check whether that specific item has been collected
    private void OnTriggerEnter(Collider other)
    {
        gameObject.SetActive(false);
        ItemPuzzle.Instance.showArrow = true;

        if (gameObject.name == "VikingShield")
        {
            ItemPuzzle.Instance.shieldVikingCollected = true;
        }
        else if (gameObject.name == "VikingAxe")
        {
            ItemPuzzle.Instance.axeVikingCollected = true;
        }
        else if (gameObject.name == "VikingBody")
        {
            ItemPuzzle.Instance.bodyVikingCollected = true;
        }
        else if (gameObject.name == "DragonSkull")
        {
            ItemPuzzle.Instance.dragonSkullCollected = true;
        }
        else if (gameObject.name == "HumanSkull")
        {
            ItemPuzzle.Instance.humanSkullCollected = true;
        }
        else if (gameObject.name == "KnightShield")
        {
            ItemPuzzle.Instance.knightShieldCollected = true;
        }
        else if (gameObject.name == "YellowTreasure")
        {
            ItemPuzzle.Instance.yellowTreasureCollected = true;
        }
        else if (gameObject.name == "DungeonRocks")
        {
            ItemPuzzle.Instance.dungeonRocksCollected = true;
        }
        else if (gameObject.name == "BoneAxe")
        {
            ItemPuzzle.Instance.boneAxeCollected = true;
        }
    }
}
