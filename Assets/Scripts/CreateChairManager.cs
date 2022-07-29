using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateChairManager : MonoBehaviour
{
    public List<GameObject> itemList = new List<GameObject>();
    public GameObject itemPrefab;
    public Transform exitPoint;
    public Vector3 itemRotation;
    public float increasedPositionZ;
    public float increasedValueY = 0.2f;
    public int itemCountForWorking = 20;
    bool isWorking = true;
    

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
        increasedPositionZ = exitPoint.position.z;
        while(true){
            if(isWorking){
                if(((float)itemList.Count%5) == 0) increasedPositionZ -= increasedValueY;
                GameObject temp = Instantiate(itemPrefab);
                temp.transform.position = new Vector3(exitPoint.position.x+ (1f * ((float)itemList.Count%5)), exitPoint.position.y, increasedPositionZ);
                temp.transform.localRotation = Quaternion.Euler(itemRotation);
                temp.transform.SetParent(exitPoint);
                itemList.Add(temp);
                if(itemList.Count > itemCountForWorking - 1) {isWorking = false;}
            }
            else if(itemList.Count < itemCountForWorking - 1) {isWorking = true;}
            yield return new WaitForSecondsRealtime(0.5f);
        }
    }
}
