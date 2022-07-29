using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;
public class FactoryController : MonoBehaviour
{
    [SerializeField]
    private PlayerInventoryyy cltr;
    [SerializeField]
    private Transform takePosition; //alınan wood 
    [SerializeField]
    private Transform backFactory; //verilen timber
    [SerializeField]
    private Transform takeWoodForProcess; //işlenen woodun positionı
    [SerializeField]
    private ColletableItemCtrl itemCtrl;
    [SerializeField]
    public GameObject Timber;
    [SerializeField]
    private List<GameObject> itemsInFactory = new List<GameObject>();
    [SerializeField]
    private List<GameObject> timberInFactory = new List<GameObject>();
    [SerializeField]
    public float WoodStackJumpDuration = 0.1f;
    [SerializeField]
    public float WoodProcessingJumpDuration = 0.5f;
    [SerializeField]
    public float WoodProcessingDuration = 0.5f;
    private float timberPositionY = 0;
    private int numOfWoodInFactory = 0;
    private float woodPositionY = 0;
    private Coroutine coroutineForStacking;
    private Coroutine coroutineForTimber;

    //%5 kullanarak kalanı hesapla 0 sa y değişkenini değiştirsin. timber koyarken.

    public void JumpToFactory(Transform WoodToAddForFactory){
        WoodToAddForFactory.DOJump(takePosition.position + new Vector3(0, 0, 0.93f *(takePosition.childCount % 7)), 2f, 1, 1).OnComplete(
            () =>{
                WoodToAddForFactory.SetParent(takePosition, true);
                WoodToAddForFactory.localPosition = new Vector3(0, woodPositionY, 0.93f * (takePosition.childCount%7));
                WoodToAddForFactory.localRotation = Quaternion.identity;
                if(takePosition.childCount % 7 == 0) woodPositionY += 0.88f;
            }
        );
    }

    public void JumpToInsideFactory(Transform WoodToAddForInsideFactory){
        WoodToAddForInsideFactory.DOJump(takeWoodForProcess.position, 2f, 1, WoodProcessingJumpDuration).OnComplete(
            () =>{
                WoodToAddForInsideFactory.SetParent(takeWoodForProcess, true);
                WoodToAddForInsideFactory.localPosition = Vector3.zero;
                WoodToAddForInsideFactory.localRotation = Quaternion.identity;
            }
        );
    }

    IEnumerator StackingWood(){
        while(cltr.Wood.Count> 0){
            //JumpToFactory(cltr.Wood.Last().transform);
            itemsInFactory.Add(cltr.Wood.Last());
            cltr.Wood.Last().transform.SetParent(takePosition);
            cltr.Wood.Remove(cltr.Wood.Last());
            cltr.items["Wood"]--;
            yield return new WaitForSeconds(WoodStackJumpDuration);
        }
    }
    

    IEnumerator CreatingTimber(){
        yield return new WaitForSeconds(.5f);
        while(itemsInFactory.Count > 0 ){
            yield return new WaitForSeconds(0.1f);
            JumpToInsideFactory(itemsInFactory.Last().transform);
            yield return new WaitForSeconds(WoodProcessingDuration);
            itemsInFactory.Remove(itemsInFactory.Last());
            yield return new WaitForSeconds(0.1f);
            Instantiate(Timber, backFactory.position + new Vector3(0, timberPositionY, 0.167f * (backFactory.childCount % 6)),backFactory.rotation,backFactory);
            yield return new WaitForSeconds(WoodProcessingDuration);
            Instantiate(Timber, backFactory.position + new Vector3(0, timberPositionY, 0.167f * (backFactory.childCount % 6)),backFactory.rotation,backFactory);
            numOfWoodInFactory--;
            if(backFactory.childCount % 6 == 0){
                timberPositionY += 0.03f;
            }
        }
    }
//takeposition daki jump için yeni funct yaz
//hızlı atsın oraya işleme yavaş olsun

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            coroutineForStacking = StartCoroutine(StackingWood());
            coroutineForTimber = StartCoroutine(CreatingTimber());
        }
    }
    private void OnTriggerExit(Collider other) {
        if(other.CompareTag("Player")){
            
        }
    }

    private void Update() {

    }
}
