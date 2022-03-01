using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour
{
    private bool isOpen = false;
    public GameObject collectible;
    public Vector3 offset;

    public void OpenChest(){
        if (!isOpen){
            int pointCount = Random.Range(1, 4);
            for (int i = 0; i < pointCount; i++){
                Vector3 spawnPostion = new Vector3 (
                    transform.position.x + offset.x + i, transform.position.y + offset.y, transform.position.z + offset.z);
                Instantiate(collectible, spawnPostion, Quaternion.identity);
            }
            isOpen = true;
        }
    }
}
