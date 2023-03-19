using EzySlice;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bamboo : MonoBehaviour
{
    [SerializeField] private SliceBamboo sliceBamboo;
    [SerializeField] public Material sliceMaterial;

    public event Action<Bamboo> OnDeadEvent;

    public int p { get; private set; }
    public int o { get; private set; }


    private void OnEnable()
    {
        sliceBamboo.KnifeColiderEvent += SliceBamboo_KnifeColiderEvent;
    }

    private void SliceBamboo_KnifeColiderEvent(Vector3 worldPosition)
    {
        StartCoroutine(BabooCouratine(worldPosition));
        sliceBamboo.KnifeColiderEvent -= SliceBamboo_KnifeColiderEvent;
    }

    private IEnumerator BabooCouratine(Vector3 worldPosition)
    {
        yield return null;

        var objectToSlice = sliceBamboo.gameObject;

        var hulk = Slice(objectToSlice, worldPosition, Vector3.up);
        var obj1 = hulk.CreateLowerHull(objectToSlice, sliceMaterial);
        var obj2 = hulk.CreateUpperHull(objectToSlice, sliceMaterial);

        obj1.transform.position = this.transform.position + new Vector3(0, -4, 0);
        obj2.transform.position = this.transform.position + new Vector3(0, -4, 0);
        MakeItPhysical(obj1);
        MakeItPhysical(obj2);

        Destroy(objectToSlice);

        yield return new WaitForSeconds(3f);

        OnDeadEvent?.Invoke(this);
        Destroy(obj2);
        Destroy(obj1);
        Destroy(this.gameObject);
    }

    private SlicedHull Slice(GameObject gameObject, Vector3 planeWorldPosition, Vector3 planeWorldDirection)
    {
        return gameObject.Slice(planeWorldPosition, planeWorldDirection, sliceMaterial);
    }

    private void MakeItPhysical(GameObject obj)
    {

        obj.AddComponent<MeshCollider>().convex = true;
        var rb = obj.AddComponent<Rigidbody>();
        rb.AddForce(Vector3.forward * 100);
    }

    public void SetPos(int p, int o)
    {
        this.o = o;
        this.p = p;
    }
}
