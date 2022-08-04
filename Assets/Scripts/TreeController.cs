using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeController : MonoBehaviour
{
    [SerializeField]
    public int treeHealt = 3;
    [SerializeField]
    public GameObject tree;
    [SerializeField]
    private GameObject wood;

    [SerializeField]
    public MeshRenderer treeAfterFirstDamage;

    [SerializeField]
    public MeshRenderer treeBeforeLastDamage;


    public Coroutine treeHealtCoroutine;
    bool crrunning;

   

    IEnumerator SetTreeHealt()
    {
        crrunning = true;
        yield return new WaitForSeconds(10f);
        treeHealt = 3;
        gameObject.GetComponent<Collider>().enabled = true;
        gameObject.GetComponentInParent<MeshRenderer>().enabled = true;
        transform.parent.gameObject.GetComponent<Collider>().enabled = true;
        GameObject.Find("Tree After First Damage").GetComponent<MeshRenderer>().enabled = false;
        GameObject.Find("Tree Before Last Damage").GetComponent<MeshRenderer>().enabled = false;
        crrunning = false;
    }

    public IEnumerator createWood(AnimsController anims)
    {
        while(treeHealt != 0 && anims.isPlayerIn)
        {
            float woodPositionx = transform.position.x;
            float woodPositionz = transform.position.z;

            Vector3 woodRange = new Vector3(Random.Range(woodPositionx + 1, woodPositionx - 1), 1.54836f, Random.Range(woodPositionz + 1, woodPositionz - 1));

            yield return new WaitForSeconds(1.5f);

            if (anims.isPlayerIn)
            {
                Instantiate(wood, woodRange, Quaternion.identity);
                treeHealt--;
                if (treeHealt == 3)
                {
                    tree.GetComponent<MeshRenderer>().enabled = true;
                    treeAfterFirstDamage.enabled = false;
                    treeBeforeLastDamage.enabled = false;
                }
                if (treeHealt == 2)
                {
                    tree.GetComponent<MeshRenderer>().enabled = false;
                    treeAfterFirstDamage.enabled = true;
                    treeBeforeLastDamage.enabled = false;
                }
                if (treeHealt == 1)
                {
                    tree.GetComponent<MeshRenderer>().enabled = false;
                    treeAfterFirstDamage.enabled = false;
                    treeBeforeLastDamage.enabled = true;
                }
            }
        }
        if (treeHealt == 0)
        {
            anims.isPlayerIn = false;
            if (anims.GetComponent<Animator>().GetBool("isTree"))
            {
                anims.GetComponent<Animator>().SetBool("isTree", false);
            }
            gameObject.GetComponent<Collider>().enabled = false;
            transform.parent.gameObject.GetComponent<MeshRenderer>().enabled = false;
            transform.parent.gameObject.GetComponent<Collider>().enabled = false;
            treeAfterFirstDamage.enabled = false;
            treeBeforeLastDamage.enabled = false;
            if (crrunning)
            {
                StopCoroutine(treeHealtCoroutine);
            }
            else
            {
                treeHealtCoroutine = StartCoroutine(SetTreeHealt());
            }
        }
        
    }


}
