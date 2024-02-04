using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ArrowScript : MonoBehaviour
{
    public GameObject itemArrow;
    public MeshRenderer arrowMesh;
    Vector3 targetPosition;

    Vector3 upLoc;
    Vector3 downLoc;

    // Start is called before the first frame update
    void Start()
    {
        downLoc = itemArrow.transform.position;
        itemArrow.transform.position = downLoc;
        upLoc = downLoc + Vector3.up * 2;

        itemArrow.transform.DOMove(upLoc, 2).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(itemArrow.transform.position, upLoc) < 0.2f)
        {
            targetPosition = downLoc;
        }
        else if (Vector3.Distance(itemArrow.transform.position, downLoc) < 0.2f)
        {
            targetPosition = upLoc;
        }

        itemArrow.transform.rotation = itemArrow.transform.rotation * Quaternion.Euler(0, 0.2f, 0);

        if (ItemPuzzle.Instance.showArrow == true)
        {
            arrowMesh.enabled = true;

        }
        else if (ItemPuzzle.Instance.showArrow == false)
        {
            arrowMesh.enabled = false;
        }
    }
}
