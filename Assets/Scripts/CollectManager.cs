using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CollectManager : MonoBehaviour
{
    public List<GameObject> timberList = new List<GameObject>();
    public List<GameObject> chairList = new List<GameObject>();
    public List<GameObject> woodList = new List<GameObject>();
    //public List<GameObject> deskList = new List<GameObject>();

    [SerializeField]
    private CreateTimberManager createTimberManager;
    [SerializeField]
    private CreateChairManager createChair;

    [SerializeField]
    private TriggerManager triggerManager;
    [SerializeField]
    private SawMillManager sawMillManager;
    [SerializeField]
    private FurnitureManager furniture;

    private ItemJumpManager itemJumpManager = new ItemJumpManager();
    public GameObject woodPref,timberPref,chairPref;
    public Transform woodPoint,timberPoint,chairPoint;
    public Transform giveWoodPoint,giveTimberPoint;
    public int timberLimit = 30, woodLimit = 5, chairLimit = 2, deskLimit = 1;

    public void RemoveLast(List<GameObject> list){
        if(list.Count > 0){
            Destroy(list.Last());
            list.Remove(list.Last());
        }
    }

    private void OnEnable() {
        triggerManager.OnTimberCollect += GetTimber;
        triggerManager.OnGiveWood += GiveWood;
        triggerManager.OnGiveTimber += GiveTimber;
        triggerManager.OnChairCollect += GetChair;
        
    }

    private void OnDisable() {
        triggerManager.OnTimberCollect -= GetTimber;
        triggerManager.OnGiveWood -= GiveWood;
        triggerManager.OnGiveTimber -= GiveTimber;
        triggerManager.OnChairCollect -= GetChair;
    }


    void GetTimber(){
        if(createTimberManager.timberList.Count != 0)
        {
            timberList.Add(createTimberManager.timberList.Last());
            createTimberManager.timberList.RemoveAt(createTimberManager.timberList.Count - 1);
            itemJumpManager.AddNewTimber(timberList[timberList.Count -1].transform,timberPoint,0.065f,timberList.Count);
        }
    }

    public void GetWood(){
        if(woodList.Count != woodLimit){
            woodList.Add(triggerManager.wood);
            triggerManager.wood.GetComponent<CapsuleCollider>().enabled = false;
            itemJumpManager.AddNewWood(triggerManager.wood.transform,woodPoint,0.25f,woodList.Count);
        }
    }

    public void GetChair()
    {
        if(chairList.Count != chairLimit && createChair.itemList.Count != 0)
        {
            chairList.Add(createChair.itemList.Last());
            createChair.itemList.Remove(createChair.itemList.Last());
            chairList.Last().GetComponent<SphereCollider>().enabled = false;
            chairList.Last().GetComponent<MeshCollider>().enabled = false;
            chairList.Last().GetComponent<Rigidbody>().useGravity = false;
            chairList.Last().GetComponent<Rigidbody>().isKinematic = true;
            itemJumpManager.AddNewChair(chairList.Last().transform, chairPoint, 1f, chairList.Count);
        }
    }

    void GiveWood(){
        if(woodList.Count > 0){
            itemJumpManager.AddNewWoodForSawmill(woodList[woodList.Count-1].transform,giveWoodPoint,0.85f,sawMillManager.sawMillWoods.Count,0);
            sawMillManager.sawMillWoods.Add(woodList[woodList.Count - 1]);
            woodList.Remove(woodList.Last());
        }
    }

    void GiveTimber()
    {
        if (timberList.Count > 0)
        {
            itemJumpManager.AddNewTimberForFurniture(timberList.Last().transform, giveTimberPoint, 0.3f, furniture.FurnitureTimber.Count);
            furniture.FurnitureTimber.Add(timberList.Last());
            timberList.Remove(timberList.Last());
        }
    }

}
