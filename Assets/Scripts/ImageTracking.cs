using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ImageTracking : MonoBehaviour
{
    // No idea wtf a serialized field is but this is one
    [SerializeField]
    ARTrackedImageManager m_TrackedImageManager;

    // Declared array of prefabs
    public GameObject[] ArPrefabs;

    private readonly Dictionary<string, GameObject> _instantiatePrefabs = new Dictionary<string, GameObject>

    // When is this called?
    // According to docs MonoBehavior.OnEnable() is called when the game object is
    // "enabled". Im not sure what enabled actually
    // means and im also not sure when this particular gameobjectis enabled.
    void OnEnable() => m_TrackedImageManager.trackedImagesChanged += OnChanged;

    // When is this called?
    // When the object is disabled... same conundrum as above
    void OnDisable() => m_TrackedImageManager.trackedImagesChanged -= OnChanged;

    void OnChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var newImage in eventArgs.added)
        {
            //What happens when a new image is added?
        }

        foreach (var updatedImage in eventArgs.updated)
        {
            // Handle updated event
        }

        foreach (var removedImage in eventArgs.removed)
        {
            // Handle removed event
        }
    }
}
