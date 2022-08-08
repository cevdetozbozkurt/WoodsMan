using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ItemJumpManager : MonoBehaviour
{
    public void AddNewWood(Transform itemToAdd, Transform itemHolderPoint,float jumpPosition,int numOfWood)
    {

        itemToAdd.DOJump(itemHolderPoint.position + new Vector3(0, jumpPosition, 0), 1f, 1, 1).OnComplete(
            () =>{
                itemToAdd.SetParent(itemHolderPoint, true);
                itemToAdd.localPosition = new Vector3(0, jumpPosition * numOfWood, 0);
                itemToAdd.localRotation = Quaternion.identity;
            }
        );
    }
    public void AddNewWoodForSawmill(Transform itemToAdd, Transform itemHolderPoint, float jumpPosition, int numOfWood)
    {
        
        itemToAdd.DOJump(itemHolderPoint.position + new Vector3(0, jumpPosition, 0), 1f, 1, 1).OnComplete(
            () => {
                itemToAdd.SetParent(itemHolderPoint, true);
                itemToAdd.localPosition = new Vector3(0, 0, (numOfWood * 0.27f));
                itemToAdd.localRotation = Quaternion.identity;
            }
        );
    }
    public void AddNewTimber(Transform itemToAdd, Transform itemHolderPoint, float jumpPosition, int numOfTimber)
    {

        itemToAdd.DOJump(itemHolderPoint.position + new Vector3(0, 0, jumpPosition), 1f, 1, 1).OnComplete(
            () => {
                itemToAdd.SetParent(itemHolderPoint, true);
                itemToAdd.localPosition = new Vector3(0, 0,jumpPosition * numOfTimber);
                itemToAdd.localRotation = Quaternion.identity;
            }
        );
    }

    public void AddNewTimberForFurniture(Transform itemToAdd, Transform itemHolderPoint, float jumpPosition, int numOfTimber)
    {

        itemToAdd.DOJump(itemHolderPoint.position + new Vector3(0, jumpPosition, 0 ), 1f, 1, 1).OnComplete(
            () => {
                itemToAdd.SetParent(itemHolderPoint, true);
                itemToAdd.localPosition = new Vector3(numOfTimber * 0.3f,0, 0);
                itemToAdd.localRotation = Quaternion.identity;
            }
        );
    }

    public void AddNewChair(Transform itemToAdd, Transform itemHolderPoint, float jumpPosition, int numOfChair)
    {

        itemToAdd.DOJump(itemHolderPoint.position + new Vector3(0, jumpPosition, 0), 1f, 1, 1).OnComplete(
            () => {
                itemToAdd.SetParent(itemHolderPoint, true);
                itemToAdd.localPosition = new Vector3(0, jumpPosition * numOfChair, 0);
                itemToAdd.localRotation = Quaternion.identity;
            }
        );
    }
    public void AddNewDesk(Transform itemToAdd, Transform itemHolderPoint, float jumpPosition, int numOfChair)
    {

        itemToAdd.DOJump(itemHolderPoint.position + new Vector3(0, jumpPosition, 0), 1f, 1, 1).OnComplete(
            () => {
                itemToAdd.SetParent(itemHolderPoint, true);
                itemToAdd.localPosition = new Vector3(0, jumpPosition * numOfChair, 0);
                itemToAdd.localRotation = Quaternion.identity;
            }
        );
    }
}
