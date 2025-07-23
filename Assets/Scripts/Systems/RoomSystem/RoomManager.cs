using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [HideInInspector]
    public Dictionary<string, bool> roomStates = new Dictionary<string, bool>()
    {
        {"Corridor", false},
        {"Closet", false},
        {"Bathroom", false},
        {"Kitchen", false},
        {"Living Room", false},
        {"Bedroom", false},
        {"Balcony", false},
    };

    void Start()
    {

    }

    void Update()
    {
        
    }
}
