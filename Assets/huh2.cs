using UnityEngine;

public class huh2 : MonoBehaviour
{
    public Rigidbody a;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        a.AddForce(Vector3.right * 2000);
    }

    // Update is called once per frame
    void Update()
    {
        //a.AddForce(Vector3.right*10);
    }
}
