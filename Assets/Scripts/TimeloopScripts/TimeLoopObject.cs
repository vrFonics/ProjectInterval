using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeLoopObject : MonoBehaviour
{

    private List<Vector3> _positions;
    private List<Quaternion> _rotations;

    public SwitchInteractMode switchInteractMode;
    public GrabbableObject grabbableObject;
    public GrabHandler grabHandler;
    public SpriteRenderer spriteRenderer;
    public GameObject progressBarPrefab;

    private GameObject _progressBarInstance;
    private UnityEngine.UI.Image _progressBarImage;

    public Rigidbody2D rb;

    private Vector3 _targetPosition;
    private Quaternion _targetRotation;

    public bool recorded = false;
    private bool _recording = false;
    private bool _playbackF = false;
    private bool _playbackB = false;

    public float recordDuration;
    public float recordCurTime = 0f;

    public int currentPosRot = 0;

    private void Start()
    {
        switchInteractMode = FindObjectOfType<SwitchInteractMode>();
        grabHandler = FindObjectOfType<GrabHandler>();
        grabbableObject = GetComponent<GrabbableObject>();
        _positions = new List<Vector3>();
        _rotations = new List<Quaternion>();
    }

    private void Update()
    {
        if (switchInteractMode.interactState == 2 || recorded)
        {
            spriteRenderer.material.SetVector("Visible", new Vector4(1, 1, 1, 1));
        }
        else
        {
            spriteRenderer.material.SetVector("Visible", new Vector4(0, 0, 0, 0));
        }
    }

    public void SetRecording()
    {
        if (!recorded)
        {
            _recording = true;
            recorded = true;
        }
        CreateProgressBar();
    }

    public void ResetRecording()
    {
        if (recorded)
        {
            recorded = false;
            _recording = false;
            grabbableObject.enabled = true;
            _playbackB = false;
            _playbackF = false;
            rb.isKinematic = false;
            grabHandler.UpdateGrabbableObjects();
            recordCurTime = 0f;
            currentPosRot = 0;
            Destroy(_progressBarInstance);
            _positions = new List<Vector3>();
            _rotations = new List<Quaternion>();
        }
    }

    public void HandleRecord()
    {
        if (recordCurTime >= recordDuration)
        {
            _recording = false;
            grabbableObject.enabled = false;
            grabHandler.UpdateGrabbableObjects();
            recordCurTime = 0f;
            currentPosRot = _positions.Count - 1;
            _playbackB = true;
            //rb.isKinematic = true;
            Destroy(_progressBarInstance);
        }
        if (_recording)
        {
            Record();
        }
    }

    public void HandlePlayBack()
    {
        if (_playbackF)
        {
            PlayForwards();
        }
        if (_playbackB)
        {
            PlayBackwards();
        }
    }

    private void FixedUpdate()
    {
        HandleRecord();
        HandlePlayBack();
        if (_recording)
        {
            _progressBarInstance.transform.position = transform.position;
        }
    }

    private void Record()
    {
        _positions.Add(transform.position);
        _rotations.Add(transform.rotation);
        recordCurTime += Time.deltaTime;
        _progressBarImage.transform.localScale = new Vector2(1 - (1 / (recordDuration / recordCurTime)), 1f);
    }

    //TODO make object stop playing through loop if blocked and resume when unblocked
    private void PlayForwards()
    {
        //old way
        //rb.velocity = Vector2.zero;
        //transform.position = _positions[currentPosRot];
        //transform.rotation = _rotations[currentPosRot];


        rb.MovePosition(_positions[currentPosRot]);
        rb.MoveRotation(_rotations[currentPosRot]);
        _targetPosition = _positions[currentPosRot];
        _targetRotation = _rotations[currentPosRot];

            if (currentPosRot == _positions.Count - 1)
            {
                _playbackB = true;
                _playbackF = false;
            }
            else
            {
                currentPosRot++;
            }
    }

    private void PlayBackwards()
    {
        //old way
        //transform.position = _positions[currentPosRot];
        //transform.rotation = _rotations[currentPosRot];

        rb.MovePosition(_positions[currentPosRot]);
        rb.MoveRotation(_rotations[currentPosRot]);
        _targetPosition = _positions[currentPosRot];
        _targetRotation = _rotations[currentPosRot];

            if (currentPosRot == 0)
            {
                _playbackF = true;
                _playbackB = false;
            }
            else
            {
                currentPosRot--;
            }
    }

    private void CreateProgressBar()
    {
        _progressBarInstance = Instantiate(progressBarPrefab);
        _progressBarImage = _progressBarInstance.GetComponentInChildren<UnityEngine.UI.Image>();
    }
}
