using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameObjectExtension 
{
    public static void SetActive(this IEnumerable<GameObject> gameObjects, bool active) {
        foreach (GameObject go in gameObjects) {
            if (go)  
                go.SetActive(active);
        }
    }
}
