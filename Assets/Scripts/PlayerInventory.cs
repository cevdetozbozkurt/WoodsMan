using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;
public class PlayerInventory : MonoBehaviour
{
    [SerializeField]
    public Transform woodHolderTransform;
    [SerializeField]
    public Transform timberHolderTransform;
    [SerializeField]
    public int numOfWoodHolding = 0;
    [SerializeField]
    public int numOfTimberHolding = 0;
    [SerializeField]
    public Transform ChairHolderTransform;
    [SerializeField]
    public Dictionary<string, int> items = new Dictionary<string, int>();
    public List<GameObject> Wood = new List<GameObject>();
    public List<GameObject> Timber = new List<GameObject>();
    public List<GameObject> Chair = new List<GameObject>();
    public int numOfCoins {get; private set;}
    public UnityEvent<PlayerInventory> OnCoinCollected;
    private void Start()
    {
        items.Add("Wood", 0);
        items.Add("Timber", 0);
        items.Add("Chair", 0);
    }

    public void CoinCollected(){
        numOfCoins++;
        OnCoinCollected.Invoke(this);
    }

    public void AddNewWood(Transform woodToAdd)
    {

        woodToAdd.DOJump(woodHolderTransform.position + new Vector3(0, 0, 0.25f * numOfWoodHolding), 2f, 1, 1).OnComplete(
            () =>{
                woodToAdd.SetParent(woodHolderTransform, true);
                woodToAdd.localPosition = new Vector3(0, 0, 0.25f * items["Wood"]);
                woodToAdd.localRotation = Quaternion.identity;
                items["Wood"]++;
            }
            );
    }
    
    public void AddNewTimber(Transform timberToAdd)
    {

        timberToAdd.DOJump(timberHolderTransform.position + new Vector3(0, 0, 0.05f * items["Timber"]), 2f, 1, 1).OnComplete(
            () => {
                timberToAdd.SetParent(timberHolderTransform, true);
                timberToAdd.localPosition = new Vector3(0, 0, 0.05f * items["Timber"]);
                timberToAdd.localRotation = Quaternion.identity;
                items["Timber"]++;
            }
            );
    }

    public void AddNewChair(Transform timberToAdd)
    {
        timberToAdd.DOJump(ChairHolderTransform.position + new Vector3(0, 0, 0.05f * items["Chair"]), 2f, 1, 1).OnComplete(
            () => {
                timberToAdd.SetParent(ChairHolderTransform, true);
                timberToAdd.localPosition = new Vector3(0, 0, 0.05f * items["Chair"]);
                timberToAdd.localRotation = Quaternion.identity;
                items["Chair"]++;
            }
            );
    }
    private void Update() {

    }
    
}
