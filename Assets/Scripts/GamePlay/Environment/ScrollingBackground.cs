using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ScrollingBackground : MonoBehaviour
{
    public float ParallaxSpeed;

    private Transform cameraTransform;
    private float lastCameraX;
    private Vector3 viewZone;
    private Transform[] layers;
    private Vector3[] backgroundSize;
    private int leftIndex;
    private int rightIndex;


    // Start is called before the first frame update
    void Start()
    {
        Camera camera = Camera.main;
        cameraTransform = camera.transform;

        // Find out the correct view zone size based on the aspect ratio
        float halfHeight = camera.orthographicSize;
        float halfWidth = camera.aspect * halfHeight;
        viewZone = new Vector3(halfWidth, halfHeight, 0);

        // Setting up the layers which must be defined as sub children
        layers = new Transform[transform.childCount];
        backgroundSize = new Vector3[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            layers[i] = transform.GetChild(i);
            SpriteRenderer sr = layers[i].GetComponent<SpriteRenderer>();
            if (sr == null) 
            {
                throw new System.Exception("Every children must use a SpriteRenderer.");
            }
            backgroundSize[i] = sr.bounds.size;
        }

        if (layers.Length < 3) 
        {
            throw new System.Exception("The GameObject must have at least 3 children to work properly.");
        }

        leftIndex = 0;
        rightIndex = layers.Length - 1;
    }

    // Update is called once per frame
    void Update()
    {
        float deltaX = cameraTransform.position.x - lastCameraX;
        transform.position += Vector3.right * (deltaX * ParallaxSpeed);
        lastCameraX = cameraTransform.position.x;
        
        if (cameraTransform.position.x < (layers[leftIndex].transform.position.x + viewZone.x))
        {
            ScrollLeft();
        }
        else if (cameraTransform.position.x > (layers[rightIndex].transform.position.x - viewZone.x))
        {
            ScrollRight();
        }
    }

    private void ScrollLeft()
    {
        int lastRight = rightIndex;
        layers[rightIndex].position = Vector3.right * (layers[leftIndex].position.x - backgroundSize[rightIndex].x);
        leftIndex = rightIndex;
        rightIndex--;
        if (rightIndex < 0)
        {
            rightIndex = layers.Length - 1;
        }   
    }

    private void ScrollRight()
    {
        int lastLeft = leftIndex;
        layers[leftIndex].position = Vector3.right * (layers[rightIndex].position.x + backgroundSize[rightIndex].x);
        rightIndex = leftIndex;
        leftIndex++;
        if (leftIndex == layers.Length)
        {
            leftIndex = 0;
        }   
    }
}
