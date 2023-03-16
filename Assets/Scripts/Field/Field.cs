using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    [SerializeField] private Transform spawnerTranform;
    [SerializeField] private Bamboo bambooPrefab;
    [Range(0,1)]
    [SerializeField] private float fieldFull;

    private Bamboo[,] bambooObjects;

    private float timeToRise = 10f;
    private float timeToRiseOne;

    private void Start()
    {
        timeToRiseOne = timeToRise / (spawnerTranform.localScale.z * spawnerTranform.localScale.x);
        bambooObjects = new Bamboo[(int)(spawnerTranform.localScale.z), (int)spawnerTranform.localScale.x];
        StartCoroutine(SpawnBambooCouratine());
    }

    private IEnumerator SpawnBambooCouratine()
    {
        yield return null;

        while (fieldFull < 0.95)
        {
            int o = Random.Range(0, (int)spawnerTranform.localScale.x);
            int p = Random.Range(0, (int)spawnerTranform.localScale.z);
            if (SpawnBamboo(p, o))
            {
                yield return new WaitForSeconds(timeToRiseOne);
            }
        }
        yield return null;
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
            fieldFull += 1 / (spawnerTranform.localScale.z * spawnerTranform.localScale.x);
            return true;
        }
        return false;
    }

    private void Field_OnDeadEvent(Bamboo obj)
    {
        bambooObjects[obj.p, obj.o] = null;
        fieldFull -= 1 / (spawnerTranform.localScale.z * spawnerTranform.localScale.x);
        StartCoroutine(SpawnOneBambooCouratine(obj.p, obj.o));
    }

    private IEnumerator SpawnOneBambooCouratine(int p, int o)
    {
        yield return new WaitForSeconds(Random.Range(8f, 12f));
        SpawnBamboo(p, o);
    }
}
