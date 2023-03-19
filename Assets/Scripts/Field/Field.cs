using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    [SerializeField] private Transform spawnerTranform;
    [SerializeField] private Bamboo bambooPrefab;
    [SerializeField] private float timeToRise = 10f;

    private Bamboo[,] bambooObjects;

    private void Start()
    {
        bambooObjects = new Bamboo[(int)(spawnerTranform.localScale.z), (int)spawnerTranform.localScale.x];
        SpawnBambooForest();
    }

    private void SpawnBambooForest()
    {
        for (int i = 0; i < spawnerTranform.localScale.x; i++)
        {
            for (int u = 0; u < spawnerTranform.localScale.z; u++)
            {
                SpawnBamboo(u, i);
            }
        }
    }

    public bool SpawnBamboo(int p, int o)
    {
        Vector3 pos = spawnerTranform.position - (spawnerTranform.localScale / 2) + new Vector3(o, 0, p)
                + new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
        Quaternion quaternion = new Quaternion(Random.Range(0, 0.1f), Random.Range(0, 0.1f), Random.Range(0, 0.1f), 1);
        if (bambooObjects[p, o] == null)
        {
            bambooObjects[p, o] = Instantiate(bambooPrefab, pos, quaternion, this.transform);
            bambooObjects[p, o].SetPos(p, o);
            bambooObjects[p, o].OnDeadEvent += Field_OnDeadEvent;
            return true;
        }
        return false;
    }

    private void Field_OnDeadEvent(Bamboo obj)
    {
        bambooObjects[obj.p, obj.o].OnDeadEvent -= Field_OnDeadEvent;
        bambooObjects[obj.p, obj.o] = null;
        StartCoroutine(SpawnOneBambooCouratine(obj.p, obj.o));
    }

    private IEnumerator SpawnOneBambooCouratine(int p, int o)
    {
        yield return new WaitForSeconds(Random.Range(timeToRise - timeToRise/4, timeToRise + timeToRise / 4));
        SpawnBamboo(p, o);
    }
}
