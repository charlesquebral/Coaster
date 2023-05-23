using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointControl : MonoBehaviour
{
    public bool ctrlMode;
    public Bezier bez;
    public bool isClosed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ctrlMode = !ctrlMode;
        }

        if (bez != null)
        {
            for (int i = 0; i < bez.objects.Length; i++)
            {
                if (i % 3 == 0)
                {
                    bez.objects[i].GetComponentInChildren<MeshRenderer>().enabled = ctrlMode;

                    AutoSet(i);
                }
            }
        }

        if (ctrlMode)
        {

        }
    }

    void AutoSet(int anchorIndex)
    {
        Vector3 anchorPos = bez.objects[anchorIndex].transform.position;
        Vector3 dir = Vector3.zero;
        Vector3 dir2 = Vector3.zero;
        float[] neighborDistances = new float[2];

        if (anchorIndex - 1 >= 0)
        {
            Vector3 offset = bez.objects[anchorIndex - 3].transform.position - anchorPos;
            dir += offset.normalized;
            neighborDistances[0] = offset.magnitude;

            dir.Normalize();

            bez.objects[anchorIndex - 1].transform.position = anchorPos + dir * neighborDistances[0] * .5f;
        }
        if (anchorIndex + 1 < bez.objects.Length)
        {
            Vector3 offset2 = bez.objects[anchorIndex + 3].transform.position - anchorPos;
            dir2 -= offset2.normalized;
            neighborDistances[1] = -offset2.magnitude;

            dir2.Normalize();

            bez.objects[anchorIndex + 1].transform.position = anchorPos + dir * neighborDistances[1] * .5f;
        }

    }
}
