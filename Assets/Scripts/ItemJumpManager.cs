using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ItemJumpManager : MonoBehaviour
{
    public int itemCount = 0;
    public void AddNewItem(Transform itemToAdd, Transform itemHolderPoint,float jumpPosition)
    {

        itemToAdd.DOJump(itemHolderPoint.position + new Vector3(0, jumpPosition * itemCount, 0), 1f, 1, 1).OnComplete(
            () =>{
                itemToAdd.SetParent(itemHolderPoint, true);
                itemToAdd.localPosition = new Vector3(0, jumpPosition * itemCount, 0);
                itemToAdd.localRotation = Quaternion.identity;
                itemCount++;
            }
        );
    }
}
