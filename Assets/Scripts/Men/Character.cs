using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Character : MonoBehaviour
{
    [SerializeField] private Animator animator;

    [SerializeField] private Bag bag;
    [SerializeField] private Knife knife;
    [SerializeField] private Joystick joystick;

    [SerializeField] private float speedMove = 100;
    [SerializeField] private int bagMaxValue = 40;

    private Rigidbody rigidbody;

    private bool IsBagEmpty;

    public event Action<int> ChangeBambooValueEvent;

    public int CurrentBambooValue { get => currentBambooValue;}
    private int currentBambooValue;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        currentBambooValue = 0;
        bag.SetMaxValue(bagMaxValue);
    }

    private void OnEnable()
    {
        knife.OnCutEvent += Knife_OnCutEvent;
    }

    private void OnDisable()
    {
        knife.OnCutEvent -= Knife_OnCutEvent;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Knife_OnCutEvent()
    {
        AddBamboo();
    }

    private void Move()
    {
        var vector = joystick.Direction;
        if (vector.sqrMagnitude < 0.05f)
        {
            animator.SetBool("Move", false);
            rigidbody.velocity = Vector3.zero;
            return;
        }
        
        animator.SetBool("Move", true);
        vector.Normalize();
        var globalVector = new Vector3(vector.x, 0, vector.y);
        rigidbody.AddForce(speedMove * Time.fixedDeltaTime * globalVector, ForceMode.Impulse);

        var angle = Vector3.SignedAngle(Vector3.forward, globalVector, Vector3.up);
        var angularVelocity = angle * Vector3.up;
        rigidbody.rotation = Quaternion.Euler(angularVelocity);
    }

    private void OnTriggerEnter(Collider other)
    {
        var field = other.gameObject.GetComponent<FieldPosition>();
        if (field == null)
            return;
        animator.SetBool("Cut", true);
    }

    private void OnTriggerExit(Collider other)
    {
        var field = other.gameObject.GetComponent<FieldPosition>();
        if (field == null)
            return;
        animator.SetBool("Cut", false);
    }

    private void AddBamboo()
    {
        currentBambooValue++;
        if (currentBambooValue > bagMaxValue)
        {
            currentBambooValue = bagMaxValue;
            return;
        }
        ChangeBambooValueEvent?.Invoke(currentBambooValue);
        bag.AddValue();
        IsBagEmpty = false;
    }

    public void RemoveBamboo()
    {
        StartCoroutine(RemoveBambooCouratine());
    }

    private IEnumerator RemoveBambooCouratine()
    {
        yield return null;
        
        while (!IsBagEmpty)
        {
            currentBambooValue--;
            if (currentBambooValue < 0)
            {
                IsBagEmpty = true;
                currentBambooValue = 0;
                yield break;
            }
            ChangeBambooValueEvent?.Invoke(currentBambooValue);
            bag.RemoveValue();
            yield return new WaitForSeconds(0.01f);
        }
    }
}
