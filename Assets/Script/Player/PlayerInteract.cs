using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private Camera _cam;
    private CharacterDataSO _characterData;
    [SerializeField]
    private LayerMask _layerMask;
    private PlayerUI _playerUI;
    private InputManager _inputManager;

    // Start is called before the first frame update
    void Start()
    {
        _cam = GetComponent<PlayerLook>().GetCam();
        _characterData = GetComponent<PlayerData>().CharacterData;
        _playerUI = GetComponent<PlayerUI>();
        _inputManager = GetComponent<InputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        _playerUI.UpdateText(string.Empty)
;
        Ray ray = new Ray(_cam.transform.position, _cam.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * _characterData.interactRangeCharacter);

        RaycastHit hitInfo;

        if(Physics.Raycast(ray, out hitInfo, _characterData.interactRangeCharacter, _layerMask))
        {
            if (hitInfo.collider.GetComponent<Interactable>() != null)
            {
                Interactable interactable = hitInfo.collider.GetComponent<Interactable>();

                _playerUI.UpdateText(interactable.prompMessage);
                if (_inputManager.onFoot.Interact.triggered)
                {
                    interactable.interact();
                }
            }
        }
    }
}
