using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectManager : MonoBehaviour
{
    public List<GameObject> timberList = new List<GameObject>();
    public List<GameObject> chairList = new List<GameObject>();
    public List<GameObject> woodList = new List<GameObject>();
    //public List<GameObject> deskList = new List<GameObject>();

    [SerializeField]
    private CreateTimberManager createTimberManager;
    public GameObject woodPref,timberPref,chairPref;
    //public GameObject deskPref;
    public Transform woodPoint,timberPoint,chairPoint;
    //public Transform deskPoint;
    public int timberLimit = 30, woodLimit = 30, chairLimit = 2, deskLimit = 1;


    private void OnEnable() {
        TriggerManager.OnTimberCollect += GetTimber; 
    }

    private void OnDisable() {
        TriggerManager.OnTimberCollect -= GetTimber;
    }

    void GetTimber(){
        if(timberList.Count <= timberLimit){
            timberList.Add(createTimberManager.timberList[createTimberManager.timberList.Count-1]);
            createTimberManager.timberList.RemoveAt(createTimberManager.timberList.Count - 1);
            timberList[timberList.Count - 1].transform.SetParent(timberPoint);
            timberList[timberList.Count - 1].transform.position = new Vector3(timberPoint.position.x,timberPoint.position.y+ (0.065f * timberList.Count),timberPoint.position.z);
            timberList[timberList.Count - 1].transform.localRotation = Quaternion.identity;
            createTimberManager.itemCountForWorking--;
        }
    }

}
