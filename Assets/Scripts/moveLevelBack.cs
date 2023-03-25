using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveLevelBack : MonoBehaviour
{

    [Tooltip("The ground object")] [SerializeField] Transform player;
    [Tooltip("Offset on the z axis")] [SerializeField] float zOffset = 1f;

    [SerializeField] bool canMove = true;

    void Update(){
        
        
        if(Mathf.RoundToInt(player.transform.position.z) % 100 == 0 && canMove){
            Debug.Log("Have Moved");
            StartCoroutine(moveCooldown());
            transform.position = new Vector3(transform.position.x, transform.position.y, player.transform.position.z + zOffset);
        }
    }

    IEnumerator moveCooldown(){
        canMove = false;
        yield return new WaitForSeconds(3);
        canMove = true;

    }
}
