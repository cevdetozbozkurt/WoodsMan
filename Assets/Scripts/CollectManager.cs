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

    [SerializeField]
    private TriggerManager triggerManager;
    private ItemJumpManager itemJumpManager = new ItemJumpManager();
    public GameObject woodPref,timberPref,chairPref;
    //public GameObject deskPref;
    public Transform woodPoint,timberPoint,chairPoint;
    public Transform giveWoodPoint;
    //public Transform deskPoint;
    public int timberLimit = 30, woodLimit = 5, chairLimit = 2, deskLimit = 1;


    private void OnEnable() {
        TriggerManager.OnTimberCollect += GetTimber; 
        TriggerManager.OnGiveWood += GiveWood;
    }

    private void OnDisable() {
        TriggerManager.OnTimberCollect -= GetTimber;
        TriggerManager.OnGiveWood -= GiveWood;
    }

    void GetTimber(){
        if(timberList.Count <= timberLimit){
            timberList.Add(createTimberManager.timberList[createTimberManager.timberList.Count-1]);
            createTimberManager.timberList.RemoveAt(createTimberManager.timberList.Count - 1);
            itemJumpManager.AddNewItem(timberList[timberList.Count -1].transform,timberPoint,0.065f);
        }
    }

    public void GetWood(){
        if(woodList.Count <= woodLimit){
            woodList.Add(triggerManager.wood);
            triggerManager.wood.GetComponent<CapsuleCollider>().enabled = false;
            itemJumpManager.AddNewItem(triggerManager.wood.transform,woodPoint,0.25f);
        }
    }

    void GiveWood(){
        if(woodList.Count > 0){
            itemJumpManager.AddNewItem(woodList[woodList.Count-1].transform,giveWoodPoint,0.25f);
            
        }
    }

}
