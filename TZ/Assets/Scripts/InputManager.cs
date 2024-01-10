using Core.MessengerStatic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.EventSystems;
using View;

public class InputManager : MonoBehaviour
{
    [SerializeField] private Camera _main;

    private RaycastHit _hit;

    private void Update()
    {
        if (!Input.GetMouseButtonDown(0))
        {
            return;
        }
        
        if (EventSystem.current.IsPointerOverGameObject()) return;

        if (GameController.CurrentGameState != GameController.GameState.PlayerTurn)
        {
            return;
        }
        
        Ray ray = _main.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out _hit, 1000.0f))
        {
            return;
        }
        Debug.DrawLine(ray.origin, ray.origin+ ray.direction*1000, Color.red, 5);
        
        var movementBox = _hit.transform.GetComponent<WorldBoxView>();
        if (movementBox == null)
        {
            return;
        }
        
        Messenger<WorldBox>.Broadcast(Constnts.Events.Game.BoxClick, movementBox.GetWorldBox());
    }
}