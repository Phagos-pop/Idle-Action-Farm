using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Character : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Bag bag;

    [SerializeField] private Joystick joystick;
    [SerializeField] private float speedMove = 100;
    [SerializeField] private float torqueSpeed = 20;

    private Rigidbody rigidbody;

    private bool IsBagEmpty;
    public bool IsCut;

    public event Action<int> ChangeBambooValueEvent;

    public int CurrentBambooValue { get => currentBambooValue;}
    private int currentBambooValue;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        bag.FullBagEvent += Bag_FullBagEvent;
        bag.EmptyBagEvent += Bag_EmptyBagEvent;
        IsBagEmpty = false;
        IsCut = false;
        currentBambooValue = 0;
    }

    private void Bag_EmptyBagEvent()
    {
        IsBagEmpty = true;
    }

    private void OnDisable()
    {
        bag.FullBagEvent -= Bag_FullBagEvent;
        bag.EmptyBagEvent -= Bag_EmptyBagEvent;
    }

    private void Bag_FullBagEvent()
    {
        animator.SetBool("Cut", false);
    }

    private void FixedUpdate()
    {
        Move();
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
        IsCut = true;
        animator.SetBool("Cut", true);
    }

    private void OnTriggerExit(Collider other)
    {
        var field = other.gameObject.GetComponent<FieldPosition>();
        if (field == null)
            return;
        IsCut = false;
        animator.SetBool("Cut", false);
    }

    public void AddBamboo()
    {
        currentBambooValue++;
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
            ChangeBambooValueEvent?.Invoke(currentBambooValue);
            bag.RemoveValue();
            yield return new WaitForSeconds(0.1f);
        }
    }
}
