using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    private void Update()
    {
        transform.Rotate(new Vector3(1, 1, 0), 50 * Time.deltaTime);
    }


}
