using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : GenericSingleton<ObjectPooler>
{
    private Dictionary<string, Queue<GameObject>> objectPool = new Dictionary<string, Queue<GameObject>>();

    public GameObject GetObjectFromPool(GameObject gameObject)
    {
        if (objectPool.TryGetValue(gameObject.name, out Queue<GameObject> objectList))
        {
            if (objectList.Count == 0)
                return CreateNewObject(gameObject);
            else
            {
                GameObject pooledobject = objectList.Dequeue();
                pooledobject.SetActive(true);

                return pooledobject;
            }
        }
        else
            return CreateNewObject(gameObject);
    }

    public void ReturnGameObjectToPool(GameObject gameObject)
    {
        if (objectPool.TryGetValue(gameObject.name, out Queue<GameObject> objectList))
            objectList.Enqueue(gameObject);
        else
        {
            Queue<GameObject> newObjectQueue = new Queue<GameObject>();
            newObjectQueue.Enqueue(gameObject);
            objectPool.Add(gameObject.name, newObjectQueue);
        }

        gameObject.SetActive(false);
    }

    private GameObject CreateNewObject(GameObject gameObject)
    {
        GameObject newGO = Instantiate(gameObject);
        newGO.name = gameObject.name;

        return newGO;
    }
}
