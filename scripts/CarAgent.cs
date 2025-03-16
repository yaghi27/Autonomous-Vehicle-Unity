using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class CarAgent : Agent
{
    public float speed = 10f;  // Speed of the car
    public float turnSpeed = 85f; // Turning speed of the car

    //[SerializeField] public Transform target;
    private int checkpointCount = 0;
    // Raycast parameters
    public LayerMask obstacleLayer;  // Layer to consider for obstacles (e.g., walls, other cars)
    private Rigidbody rb;
    private RayPerceptionSensorComponent3D rayPerceptionSensor;

    public override void Initialize()
    {
        rb = GetComponent<Rigidbody>();
        rayPerceptionSensor = GetComponent<RayPerceptionSensorComponent3D>();
    }


    public override void OnEpisodeBegin()
    {
        transform.localPosition = new Vector3(0f, 0f, 0f);
        transform.rotation = Quaternion.Euler(0, 0, 0);

        //transform.rotation = Quaternion.identity;
        checkpointCount = 0;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // Position of car and target
        sensor.AddObservation(transform.localPosition);
        //sensor.AddObservation(target.localPosition);

        // Velocity of the car
        sensor.AddObservation(rb);

        // Car's rotation (orientation)
        sensor.AddObservation(transform.rotation);

    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        float moveForward = Mathf.Abs(actions.ContinuousActions[0]); // Forward/backward movement
        float turn = actions.ContinuousActions[1]; // Steering

        // Move forward or backward
        //transform.localPosition += transform.forward * moveZ * Time.deltaTime * speed;

        rb.MovePosition(transform.position + transform.forward * moveForward * Time.fixedDeltaTime * speed);

        // Apply rotation only when there is movement along the Z-axis
        if (Mathf.Abs(moveForward) > 0.2f)
        {
            transform.Rotate(Vector3.up, turn * turnSpeed * Time.deltaTime);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Wall>(out Wall wall))
        {
            float crashSpeed = rb.velocity.magnitude;
            AddReward(-20f * crashSpeed);
            EndEpisode();

        }
        else if (other.TryGetComponent<Checkpoint>(out Checkpoint checkpoint))
        {
            checkpointCount++;
            AddReward(1f);
        }
        else if (other.TryGetComponent<ExtraCheckPoint>(out ExtraCheckPoint extraCheckPoint))
        {
            checkpointCount++;
            AddReward(5f);
        }
        else if (other.TryGetComponent<WrongSide>(out WrongSide WrongSide))
        {
            checkpointCount++;
            AddReward(-5f);
        }
        else if (other.TryGetComponent<Goal>(out Goal goal))
        {
            AddReward(100f);
            EndEpisode();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            float crashSpeed = rb.velocity.magnitude;
            AddReward(-20f * crashSpeed);
            EndEpisode();
        }
    }


    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> actions = actionsOut.ContinuousActions;
        actions[0] = Input.GetAxisRaw("Vertical"); // Forward/backward
        actions[1] = Input.GetAxisRaw("Horizontal"); // Left/right steering
    }

    private void FixedUpdate()
    {
        RequestDecision();
    }
}
