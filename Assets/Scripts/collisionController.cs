using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class collisionController : MonoBehaviour
{

    [Tooltip("Obstacle Layermask ")] [SerializeField] LayerMask obstacleMask;
    
    void OnCollisionEnter(Collision other){
        Debug.Log("Collided");

        if(other.gameObject.layer == LayerMask.NameToLayer("Wall")){
            StartCoroutine(death());
           
        }
    }

    IEnumerator death(){
        yield return new WaitForSeconds(0.5f);
        Debug.Log("Coroutine started");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }



}
