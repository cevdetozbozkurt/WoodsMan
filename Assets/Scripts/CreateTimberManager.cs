using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateTimberManager : MonoBehaviour
{
    public List<GameObject> timberList = new List<GameObject>();
    public GameObject itemPrefab;
    public Transform exitPoint;
    public Vector3 itemRotation;
    public float increasedPositionY;
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
        increasedPositionY = exitPoint.position.y;
        while(true){
            if(isWorking){
                if(((float)timberList.Count%5) == 0) increasedPositionY += increasedValueY;
                GameObject temp = Instantiate(itemPrefab);
                temp.transform.position = new Vector3(exitPoint.position.x, increasedPositionY, exitPoint.position.z + (0.16f * ((float)timberList.Count%5)));
                temp.transform.localRotation = Quaternion.Euler(itemRotation);
                temp.transform.SetParent(exitPoint);
                timberList.Add(temp);
                if(timberList.Count > itemCountForWorking - 1) {isWorking = false;}
            }
            else if(timberList.Count < itemCountForWorking - 1) {isWorking = true;}
            yield return new WaitForSecondsRealtime(0.5f);
        }
    }

}
