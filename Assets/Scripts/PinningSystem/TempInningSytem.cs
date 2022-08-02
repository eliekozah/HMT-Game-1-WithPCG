using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TempInningSytem : MonoBehaviour
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


    /*private LayerMask _myLayerMask = 6; -> not working */

    void Start()
    {
        _mainCamera = Camera.main;
        _isPinned = false;
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
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
        Instantiate(_PinQuestionPrefab, new Vector3(_hit.transform.position.x, 0, _hit.transform.position.z), Quaternion.identity);
        PinWindow.instance.AddPing(new Vector3(_hit.transform.position.x, 0, _hit.transform.position.z),0);
        cancel();
    }
    public void notice()
    {
        Instantiate(_PinNoticePrefab, new Vector3(_hit.transform.position.x, 0, _hit.transform.position.z), Quaternion.identity);
        PinWindow.instance.AddPing(new Vector3(_hit.transform.position.x, 0, _hit.transform.position.z),1);
        cancel();
    }

}
