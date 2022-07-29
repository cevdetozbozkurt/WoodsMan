using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;
public class FurnitureFactoryControl : MonoBehaviour
{
    [SerializeField]
    private Transform holdTimber;
    [SerializeField]
    private Transform transforminFactory;
    [SerializeField]
    private GameObject entryDoor;
    [SerializeField]
    private GameObject exitDoor;

    [SerializeField]
    private Transform newChairTransform;    

    [SerializeField]
    private GameObject Chair;
    [SerializeField]
    private GameObject insideFactory;
    [SerializeField]
    private Animator animator ;
    private PlayerInventoryyy playerInventory;
    private bool isTrigger = false;
    private int numOfTimberInFactory = 0;
    private float newChairTransformX = 0f;
    private int rotationSpeed = 25;
    float value = 180;
    float timberPositionY = 0 ;
    [SerializeField]
    private float creatingChairDuration = 0.5f;
    private bool isActive;
    private Coroutine coroutineStacking;
    private Coroutine coroutineCreateChair;
    public List<GameObject> TimberinFurniture = new List<GameObject>();
    public List<GameObject> Chairs = new List<GameObject>();

    private void Start() {
    }

    IEnumerator GetTimbers(){
        while(playerInventory.Timber.Count > 0){
            JumpToFactory(playerInventory.Timber.Last().transform);
            TimberinFurniture.Add(playerInventory.Timber.Last());
            playerInventory.Timber.Remove(playerInventory.Timber.Last());
            yield return new WaitForSeconds(0.2f);
            playerInventory.items["Timber"]--;
        }
    }

    private IEnumerator CreateChair(){
        while(TimberinFurniture.Count >= 5){
            isActive = true;
            for (int i = 0; i < 5; i++)
            {
                if(TimberinFurniture.Last() != null) JumpinFactory(TimberinFurniture.Last().transform);
                Destroy(TimberinFurniture.Last());
                TimberinFurniture.Remove(TimberinFurniture.Last());
                yield return new WaitForSeconds(0.1f);
            }
            Instantiate(Chair, newChairTransform.transform.position + new Vector3(0.95f*(newChairTransform.childCount %5), 0,newChairTransformX), Quaternion.identity, newChairTransform);
            yield return new WaitForSeconds(creatingChairDuration);
            Debug.Log("Calıştım dış");
            isActive = false;
            if(newChairTransform.childCount % 5 ==0) newChairTransformX -= 1.1f;
        }
        
    }

    public void JumpToFactory(Transform WoodToAddForFactory){
        WoodToAddForFactory.DOJump(holdTimber.position + new Vector3(0.1f * (holdTimber.childCount%10), timberPositionY,0), 2f, 1, 1).OnComplete(
            () =>{
                WoodToAddForFactory.SetParent(holdTimber, true);
                WoodToAddForFactory.localPosition = new Vector3(0.3f * (holdTimber.childCount%10), timberPositionY, 0);
                WoodToAddForFactory.localRotation = Quaternion.identity;
                numOfTimberInFactory++;
            }
        );
        if(holdTimber.childCount%10==0){
            timberPositionY += 0.03f;
        }
    }
    IEnumerator JumpinFactory(Transform WoodToAddForFactory){
        WoodToAddForFactory.DOJump(transforminFactory.position, 2f, 1, 1).OnComplete(
            () =>{
                WoodToAddForFactory.SetParent(transforminFactory, true);
                WoodToAddForFactory.localPosition = Vector3.zero;
                WoodToAddForFactory.localRotation = Quaternion.identity;
                numOfTimberInFactory++;
            }
        );
        yield return new WaitForSeconds(0.5f);
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")){
            playerInventory = other.GetComponent<PlayerInventoryyy>();
            animator.SetBool("isPlayerIn",true);
            coroutineStacking = StartCoroutine(GetTimbers());
            if(TimberinFurniture.Count>=5) isActive = false;
        }   
    }
    private void OnTriggerExit(Collider other) {
        if(other.CompareTag("Player")){
            isTrigger = false;
            animator.SetBool("isPlayerIn",false);
            StopCoroutine(coroutineStacking);
        }
    }

    private void Update() {
        if(TimberinFurniture.Count >=5 && !isActive){
            coroutineCreateChair = StartCoroutine(CreateChair());
        }
    }

}
