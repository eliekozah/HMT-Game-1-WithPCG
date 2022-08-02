using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Pun.UtilityScripts;

public class PinningSystem : MonoBehaviour
{
    private Camera _mainCamera;

    private Ray _ray;
    private RaycastHit _hit;

    public GameObject _PinQuestionPrefab;
    public GameObject _PinNoticePrefab;
    public Image _pinUI;
    public Text _alertMessage;

    private Vector3 _pinPosition;
    private bool _isPinned;
    public static bool _IsPinnable;


    /*private LayerMask _myLayerMask = 6; -> not working */

    void Start()
    {
        _mainCamera = Camera.main;
        _isPinned = false;
        _IsPinnable = false;
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0)){
            Debug.Log("mouse clicked + " + Input.mousePosition);

            _ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

            if (!_isPinned)
            {
                if (Physics.Raycast(_ray, out _hit, 1000f, LayerMask.GetMask("Ground")))
                {
                    Debug.Log("isPinned, " + _hit.transform.gameObject.name + " " + _hit.transform.position);
                    _pinUI.transform.position = Input.mousePosition;
                    _pinUI.transform.gameObject.SetActive(true);
                    
                    _pinPosition = new Vector3(_hit.transform.position.x, 0, _hit.transform.position.z);
                    _isPinned = true;
                }
            }
        }
    }

    /* PinUI choice button clicked */
    public void cancel()
    {
        _pinUI.transform.gameObject.SetActive(false);
        _isPinned = false;
    }
    public void question()
    {
        PhotonNetwork.Instantiate(_PinQuestionPrefab.name, new Vector3(_hit.transform.position.x, 0, _hit.transform.position.z), Quaternion.Euler(0, 180, 0));
        PinWindow.instance.AddPing(new Vector3(_hit.transform.position.x, 0, _hit.transform.position.z), 0);
        cancel();
    }
    public void notice()
    {
        PhotonNetwork.Instantiate(_PinNoticePrefab.name, new Vector3(_hit.transform.position.x, 0, _hit.transform.position.z), Quaternion.Euler(0, 180, 0));
        PinWindow.instance.AddPing(new Vector3(_hit.transform.position.x, 0, _hit.transform.position.z), 1);
        cancel();
    }

    /* Alert messsage if not pinnable */

    private IEnumerator AlertMessage()
    {
        _alertMessage.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        _alertMessage.gameObject.SetActive(false);
    }
}
