using UnityEngine;

public class RoomCollider : MonoBehaviour
{
    [Header("References")]
    [Space(16.0f)]
    [SerializeField] RoomManager _roomManager;

    [Header("Parameters")]
    [Space(16.0f)]
    [SerializeField] string _name;

    void Start()
    {

    }

    void Update()
    {

    }

    void OnTriggerStay(Collider other)
    {
        _roomManager.roomStates[_name] = true;
    }

    void OnTriggerExit(Collider other)
    {
        _roomManager.roomStates[_name] = false;
    }
}
