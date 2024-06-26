using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Arrow : XRGrabInteractable
{
    public static event System.Action OnArrowHitRope;
    [SerializeField] private float _speed = 10f;
    [SerializeField] private Transform tip;

    [Space(2)]
    [Header("Extra settings")]
    [SerializeField] private float arrowTimeTillDespawn = 5f;

    private Rigidbody _rigidbody;
    private bool _inAir = false;
    private Vector3 _lastPosition = Vector3.zero;
    private bool arrowNotched = false;

    protected override void Awake()
    {
        base.Awake();
        _rigidbody = GetComponent<Rigidbody>();
        //selectExited.AddListener((args) => { Release(0); });
        PullInteraction.PullActionReleased += Release;

        //Stop();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        //selectExited.RemoveListener((args) => { Release(0); });
        PullInteraction.PullActionReleased -= Release;
    }


    public void Notch()
    {
        arrowNotched = true;
    }

    private void Release(float value)
    {
        if (arrowNotched)
        {
            PullInteraction.PullActionReleased -= Release;
            gameObject.transform.parent = null;
            _inAir = true;
            SetPhysics(true);

            Vector3 force = transform.forward * value * _speed;
            _rigidbody.AddForce(force, ForceMode.Impulse);

            StartCoroutine(RotateWithVelocity());
            _lastPosition = tip.position;
            arrowNotched = false;
        }
        Destroy(gameObject, arrowTimeTillDespawn);
    }

    private IEnumerator RotateWithVelocity()
    {
        yield return new WaitForFixedUpdate();
        while (_inAir)
        {
            Quaternion newRotation = Quaternion.LookRotation(_rigidbody.velocity, transform.up);
            transform.rotation = newRotation;
            yield return null;
        }
    }

    private void FixedUpdate()
    {
        if (_inAir)
        {
            CheckCollision();
            _lastPosition = tip.position;
        }
    }

    private void CheckCollision()
    {
        if(Physics.Linecast(_lastPosition, tip.position, out RaycastHit hitInfo))
        {
            if(hitInfo.transform.gameObject.layer != 8 && hitInfo.transform.gameObject.layer != 11) // IGNORE the body of the player
            {
                if(hitInfo.transform.TryGetComponent(out Rigidbody body))
                {
                    _rigidbody.interpolation = RigidbodyInterpolation.None;
                    transform.SetParent(hitInfo.collider.transform);
                    body.AddForce(new Vector3(0.01f,0.01f), ForceMode.Impulse);
                }
                if (hitInfo.transform.gameObject.layer == 12) // EQUIVALENT: LayerMask.GetMask("Rope")
                {
                    OnArrowHitRope?.Invoke();
                    hitInfo.transform.GetComponent<Joint>().breakForce = 0;
                }
                Stop();
            }
        }
    }

    private void Stop()
    {
        _inAir = false;
        SetPhysics(false);
    }

    private void SetPhysics(bool usePhysics)
    {
        _rigidbody.useGravity = usePhysics;
        _rigidbody.isKinematic = !usePhysics;
    }
}
