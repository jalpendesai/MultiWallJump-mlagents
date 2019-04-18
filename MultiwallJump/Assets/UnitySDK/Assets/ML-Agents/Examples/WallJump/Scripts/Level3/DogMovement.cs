using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogMovement : MonoBehaviour
{
    public float speed = 1.2f;

    Transform detection;

    private void Start()
    {
        // detection = gameObject.transform.GetChild(0);
        //     this.transform.eulerAngles = new Vector3(-90, Random.Range(0, 360), 0);

    }
    private void FixedUpdate()
    {
        // this.transform.Translate(Vector3.down * Time.deltaTime * speed);

        // if (!Physics.Raycast(detection.position, Vector3.down, 5))
        // {
        //     transform.Rotate(Vector3.right, 10 * Time.deltaTime);
        // }

        // Debug.DrawRay(detection.transform.position, Vector3.down * 5, Color.red);
        // if (this.transform.position.y < 0)
        // {
        //     this.transform.position = new Vector3(Random.Range(-16, 13), 1, Random.Range(-18, 7));
        //     this.transform.eulerAngles = new Vector3(-90, Random.Range(0, 360), 0);
        //     // transform.rotation = Quaternion.Euler(-90, Random.Range(0, 360), 0);
        // }
    }
}
