using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColletableItemCtrl : MonoBehaviour
{
    bool isAlreadyCollected = false;

    [SerializeField]
    private PlayerInventoryyy cltr;


    private void OnTriggerEnter(Collider other)
    {
        if (isAlreadyCollected) return;

        if (other.CompareTag("Player"))
        {
            cltr = other.GetComponent<PlayerInventoryyy>();
            if(this.tag == "Wood")
            {
                cltr.Wood.Add(this.gameObject);
                cltr.AddNewWood(this.transform);
                this.GetComponent<CapsuleCollider>().enabled = false;
                isAlreadyCollected = true;
            }
            
            if(this.CompareTag("Timber"))
            {
                cltr = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventoryyy>();
                cltr.AddNewTimber(this.transform);
                cltr.Timber.Add(this.gameObject);
                this.GetComponent<BoxCollider>().enabled = false;
                isAlreadyCollected = true;
            }
            if(this.CompareTag("Chair")){
                cltr = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventoryyy>();
                cltr.AddNewChair(this.transform);
                cltr.Chair.Add(this.gameObject);
                this.GetComponent<SphereCollider>().enabled = false;
                this.GetComponent<MeshCollider>().enabled = false;
                this.GetComponent<Rigidbody>().useGravity = false;
                this.GetComponent<Rigidbody>().isKinematic = true;
                isAlreadyCollected = true;
            }
        }        
    }
   
}
