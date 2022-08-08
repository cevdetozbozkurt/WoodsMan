using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateDeskManager : MonoBehaviour
{
    public List<GameObject> itemList = new List<GameObject>();
    public GameObject itemPrefab;
    public Transform exitPoint;
    public Vector3 itemRotation;
    public float increasedPositionX;
    public float increasedValueX = 0.2f;
    public int itemCountForWorking = 20;
    public bool isWorking;
    public DeskManager deskManager;
    

    private void Start() {
        StartCoroutine(CreateItem());
    }

    public void RemoveLast(List<GameObject> list){
        if(list.Count > 0){
            Destroy(list[list.Count-1]);
            list.RemoveAt(list.Count -1);
        }
    }

    IEnumerator CreateItem(){
        increasedPositionX = exitPoint.position.x - 0.5f;
        while(true){
            if(isWorking && deskManager.timberList.Count >= 10 && itemList.Count < 5){
                GameObject temp = Instantiate(itemPrefab);
                temp.transform.position = new Vector3(increasedPositionX, exitPoint.position.y, exitPoint.position.z - (1f * (itemList.Count % 5)));
                temp.transform.localRotation = Quaternion.Euler(itemRotation);
                temp.transform.SetParent(exitPoint);
                itemList.Add(temp);
                if ((itemList.Count % 5) == 0) increasedPositionX -= increasedValueX;
                if (itemList.Count % 1 == 0)
                {
                    for(int i = 0; i < 10; i++)
                    {
                        RemoveLast(deskManager.timberList);
                    }
                }
            }
            yield return new WaitForSecondsRealtime(0.5f);
        }
    }
}
