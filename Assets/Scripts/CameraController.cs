using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject objeto;
    void Start()
    {
        var transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        var t = objeto.GetComponent<Transform>();
        var x = t.position.x;
        var y = t.position.y;
        transform.position = new Vector3(x, y, transform.position.z);
    }
}
