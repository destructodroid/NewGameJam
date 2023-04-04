using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterControl : MonoBehaviour
{
    [SerializeField]
    private float Speed = 500f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D)){
            transform.Translate(Time.deltaTime * Speed, 0, 0);
        }
        else if (Input.GetKey(KeyCode.A)){
            transform.Translate(-Time.deltaTime * Speed, 0, 0);
        }
    }
}
