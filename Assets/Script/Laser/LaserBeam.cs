using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LaserBeam : MonoBehaviour
{
    [SerializeField] LineRenderer beam;
    [SerializeField] float maxDistance = 100f;
    [SerializeField] Transform startPoint;
    public Material laserMat;

    [SerializeField] float damage;
    public LayerMask uiLayer;

    // Start is called before the first frame update
    void Start()
    {
        beam = this.gameObject.GetComponent<LineRenderer>();
        beam.material = laserMat;
        beam.useWorldSpace = true;
        beam.enabled = false;
    }

    void Activate()
    {
        beam.enabled = true;
    }
    void DeActivate()
    {
        beam.enabled = false;

        beam.SetPosition(0, startPoint.position);
        beam.SetPosition(1, startPoint.position);

    }
    // Update is called once per frame
    void Update()
    {
       
        if (Input.GetMouseButtonDown(0))
        {

            beam.enabled = true;
            Activate();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            beam.enabled = false;
            DeActivate();
        }
        
    }
    private void FixedUpdate()
    {
        if (!beam.enabled)
        {
            return;
        }
        Ray ray = new Ray(startPoint.position, startPoint.forward);
        RaycastHit hit;
        bool cast = Physics.Raycast(ray, out hit, maxDistance, ~uiLayer);
        Vector3 hitPosition = cast ? hit.point : startPoint.position + startPoint.forward * maxDistance;//new Vector3(startPoint.position.x , startPoint.position.y - 3f, startPoint.position.z * maxDistance);//startPoint.up * maxDistance;

        beam.SetPosition(0, startPoint.position + new Vector3(0, 0, 1.1f));
        beam.SetPosition(1, hitPosition);
        
        if (cast && hit.collider.TryGetComponent(out Damageable damageable))
        {
            Debug.Log("Destroy ship by damagable");
            //damageable.ApplyDamage(damage);
        }

    }
    
}
