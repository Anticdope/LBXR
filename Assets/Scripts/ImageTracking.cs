using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using TMPro;

public class ImageTracking : MonoBehaviour
{
    // No idea wtf a serialized field is but this is one
    [SerializeField]
    ARTrackedImageManager m_TrackedImageManager;

    // Declared array of prefabs
    public GameObject[] ArPrefabs;
    // Text game object
    public TMP_Text Text;
    // Dictionary of instantiated prefabs
    private readonly Dictionary<string, GameObject> _instantiatePrefabs = new Dictionary<string, GameObject>();

    // When is this called?
    // According to docs MonoBehavior.OnEnable() is called when the game object is
    // "enabled". Im not sure what enabled actually
    // means and im also not sure when this particular gameobject is enabled.
    void OnEnable() => m_TrackedImageManager.trackedImagesChanged += OnChanged;

    // When is this called?
    // When the object is disabled... same conundrum as above
    void OnDisable() => m_TrackedImageManager.trackedImagesChanged -= OnChanged;

    //Called at the "start" (When the app runs)
    void Start()
    {
        foreach (var prefab in ArPrefabs)
        {
            var instance = Instantiate(prefab);
            instance.SetActive(false);
            _instantiatePrefabs.Add(prefab.name, instance);
        }
    }

    void OnChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var newImage in eventArgs.added)
        {
            // Instantiate a prefab when a new image is detected
            if (_instantiatePrefabs.ContainsKey(newImage.referenceImage.name))
            {
                Text.text = "Tracking";
                var prefab = _instantiatePrefabs[newImage.referenceImage.name];
                prefab.SetActive(true);
                prefab.transform.position = newImage.transform.position;
                prefab.transform.rotation = newImage.transform.rotation;
            }
        }

        foreach (var updatedImage in eventArgs.updated)
        {
            // Update the prefab position and rotation when an existing image is updated
            if (_instantiatePrefabs.ContainsKey(updatedImage.referenceImage.name))
            {
                Text.text = "Still Tracking";
                var prefab = _instantiatePrefabs[updatedImage.referenceImage.name];
                prefab.SetActive(updatedImage.trackingState == TrackingState.Tracking);
                prefab.transform.position = updatedImage.transform.position;
                prefab.transform.rotation = updatedImage.transform.rotation;
            }
        }

        foreach (var removedImage in eventArgs.removed)
        {
            // Disable the prefab when an image is removed
            if (_instantiatePrefabs.ContainsKey(removedImage.referenceImage.name))
            {
                Text.text = "Not Tracking";
                var prefab = _instantiatePrefabs[removedImage.referenceImage.name];
                prefab.SetActive(false);
            }
        }
    }
}
