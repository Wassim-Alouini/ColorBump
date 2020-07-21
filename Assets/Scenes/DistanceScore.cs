using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistanceScore : MonoBehaviour
{
    Transform player;
    [SerializeField] Slider DistanceSlider;

    float initDistance;
    private void Start()
    {
        player = GameObject.Find("Ball").transform;

        initDistance = transform.position.z - player.position.z;
    }
    private void Update()
    {
        float currentDistance = transform.position.z - player.position.z;
        DistanceSlider.value = (initDistance - currentDistance) / initDistance;
    }


}
