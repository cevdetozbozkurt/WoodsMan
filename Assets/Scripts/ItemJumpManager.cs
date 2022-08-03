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
    public void AddNewWoodForSawmill(Transform itemToAdd, Transform itemHolderPoint, float jumpPosition, int numOfWood, float yPosition)
    {

        itemToAdd.DOJump(itemHolderPoint.position + new Vector3(0, jumpPosition, 0), 1f, 1, 1).OnComplete(
            () => {
                itemToAdd.SetParent(itemHolderPoint, true);
                itemToAdd.localPosition = new Vector3(0, (jumpPosition * numOfWood)%6, 0);
                itemToAdd.localRotation = Quaternion.identity;
            }
        );
        if(numOfWood%6 == 0)
        {
            yPosition += 0.65f;
        }
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
                itemToAdd.localPosition = new Vector3(jumpPosition * numOfTimber,0, 0);
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
}
