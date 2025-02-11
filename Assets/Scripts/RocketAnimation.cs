using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketAnimation : MonoBehaviour
{
    [Header("Animator")]
    public Animator animator;

    // Start is called before th awd dw ww w  www w e first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        animator.SetFloat("HAxis", horizontal);

    }
}
