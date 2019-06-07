using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform player;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var velocity = Input.GetAxis("Horizontal") * Time.deltaTime;
        player.Translate(velocity, 0, 0);
        animator.SetFloat("Velocity", velocity);
        Debug.Log(velocity);
    }
}
