using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SawMillManager : MonoBehaviour
{
    public List<GameObject> sawMillWoods = new List<GameObject>();
    IEnumerator RemoveLast(){
        while(true){
            if(sawMillWoods.Count > 0){
                Destroy(sawMillWoods.Last());
                sawMillWoods.Remove(sawMillWoods.Last());
            }
            yield return new WaitForSeconds(0.5f);
        }
    }
}
