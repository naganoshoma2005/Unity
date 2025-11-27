
using UnityEngine;
using Unity.Cinemachine;

public class PlayerTransformation : MonoBehaviour
{
    [Header("Cinemachine")]
    public CinemachineCamera MainCamera;

    [Header("変身設定")]
    public float transformationDuration = 10.0f;
   

    private GameObject normalModel;
    private GameObject currentActiveModel;
    private MoveFish normalController;

   
    private float activeVerticalOffset = 0f; 

    void Start()
    {
        
        normalModel = this.gameObject;
        currentActiveModel = normalModel;
        
      
        normalController = GetComponent<MoveFish>();
    }

    
    public void StartTransformation(GameObject modelToActivate)
    {
        
        
        
        FishTransformationData fishData = modelToActivate.GetComponent<FishTransformationData>();
        if (fishData != null)
        {
            activeVerticalOffset = fishData.verticalOffset;
        } 
        else
        {
            activeVerticalOffset = 0f; 
        }

      
        if (normalController != null)
        {
            normalController.enabled = false;
        }

        Vector3 position = currentActiveModel.transform.position;
        Quaternion rotation = currentActiveModel.transform.rotation;
        currentActiveModel.SetActive(false);

      
        modelToActivate.transform.position = new Vector3(position.x, position.y - activeVerticalOffset, position.z);
        modelToActivate.transform.rotation = rotation;

        modelToActivate.SetActive(true);
        currentActiveModel = modelToActivate;
        
      
        MoveFish fishController = currentActiveModel.GetComponent<MoveFish>();
        if (fishController != null)
        {
            fishController.enabled = true;
        }
        
        if (MainCamera != null)
        {
            MainCamera.Follow = currentActiveModel.transform;
            MainCamera.LookAt = currentActiveModel.transform;
        }

       
        if (TransformationTimer.Instance != null)
        {
            TransformationTimer.Instance.StartTransformationTimer(this, transformationDuration);
        }
    }

   
    public void RevertTransformation()
    {
       
        if (TransformationTimer.Instance != null)
        {
            TransformationTimer.Instance.StopTransformationTimer();
        }

      
        
        MoveFish fishController = currentActiveModel.GetComponent<MoveFish>();
        if (fishController != null)
        {
            fishController.enabled = false;
        }

        Vector3 position = currentActiveModel.transform.position;
        Quaternion rotation = currentActiveModel.transform.rotation;
        currentActiveModel.SetActive(false);

       
        normalModel.transform.position = new Vector3(position.x, position.y + activeVerticalOffset, position.z);
        normalModel.transform.rotation = rotation;
        
        normalModel.SetActive(true);
        currentActiveModel = normalModel;

        
        if (normalController != null)
        {
            normalController.enabled = true;
        }

        if (MainCamera != null)
        {
            MainCamera.Follow = currentActiveModel.transform;
            MainCamera.LookAt = currentActiveModel.transform;
        }
        
      
        activeVerticalOffset = 0f;
    }
}