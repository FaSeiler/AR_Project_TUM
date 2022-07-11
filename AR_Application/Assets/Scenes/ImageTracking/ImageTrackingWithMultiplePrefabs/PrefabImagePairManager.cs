﻿using System;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.ARFoundation;

namespace UnityEngine.XR.ARFoundation.Samples
{
    /// <summary>
    /// This component listens for images detected by the <c>XRImageTrackingSubsystem</c>
    /// and overlays some prefabs on top of the detected image.
    /// </summary>
    [RequireComponent(typeof(ARTrackedImageManager))]
    public class PrefabImagePairManager : MonoBehaviour, ISerializationCallbackReceiver
    {
        /// <summary>
        /// Used to associate an `XRReferenceImage` with a Prefab by using the `XRReferenceImage`'s guid as a unique identifier for a particular reference image.
        /// </summary>
        [Serializable]
        struct NamedPrefab
        {
            // System.Guid isn't serializable, so we store the Guid as a string. At runtime, this is converted back to a System.Guid
            public string imageGuid;
            public GameObject imagePrefab;

            public NamedPrefab(Guid guid, GameObject prefab)
            {
                imageGuid = guid.ToString();
                imagePrefab = prefab;
            }
        }

        [SerializeField]
        [HideInInspector]
        List<NamedPrefab> m_PrefabsList = new List<NamedPrefab>();

        Dictionary<Guid, GameObject> m_PrefabsDictionary = new Dictionary<Guid, GameObject>();
        Dictionary<Guid, GameObject> m_Instantiated = new Dictionary<Guid, GameObject>();
        ARTrackedImageManager m_TrackedImageManager;

        [SerializeField]
        [Tooltip("Reference Image Library")]
        XRReferenceImageLibrary m_ImageLibrary;

        /// <summary>
        /// Get the <c>XRReferenceImageLibrary</c>
        /// </summary>
        public XRReferenceImageLibrary imageLibrary
        {
            get => m_ImageLibrary;
            set => m_ImageLibrary = value;
        }

        public void OnBeforeSerialize()
        {
            m_PrefabsList.Clear();
            foreach (var kvp in m_PrefabsDictionary)
            {
                m_PrefabsList.Add(new NamedPrefab(kvp.Key, kvp.Value));
            }
        }

        public void OnAfterDeserialize()
        {
            m_PrefabsDictionary = new Dictionary<Guid, GameObject>();
            foreach (var entry in m_PrefabsList)
            {
                m_PrefabsDictionary.Add(Guid.Parse(entry.imageGuid), entry.imagePrefab);
            }
        }

        void Awake()
        {
            m_TrackedImageManager = GetComponent<ARTrackedImageManager>();
        }

        void OnEnable()
        {
            m_TrackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
        }

        void OnDisable()
        {
            m_TrackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
        }

        void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
        {
            foreach (var trackedImage in eventArgs.added)
            {
                // Give the initial image a reasonable default scale
                var minLocalScalar = Mathf.Min(trackedImage.size.x, trackedImage.size.y) / 2;
                trackedImage.transform.localScale = new Vector3(minLocalScalar, minLocalScalar, minLocalScalar);
                AssignPrefab(trackedImage);
            }
        }

        void AssignPrefab(ARTrackedImage trackedImage)
        {
            //Debug.Log("AymaneShadow: 1");   
            //Debug.LogWarning("AymaneShadow: 1");   
            //Debug.LogError("AymaneShadow: 1");   

            if (m_PrefabsDictionary.TryGetValue(trackedImage.referenceImage.guid, out var prefab))
            {
                m_Instantiated[trackedImage.referenceImage.guid] = Instantiate(prefab, trackedImage.transform);
                // Debug.Log("AymaneShadow: Instantiated " + prefab.name + " at " + trackedImage.transform.position + " oriented " + trackedImage.transform.rotation);
                //Vector3 pos = new Vector3(0, 0, 0);
                // Vector3 pos = trackedImage.transform.position;
                // Quaternion rot = Quaternion.identity;

                //Debug.Log("AymaneShadow: Transform of tracked image is " + trackedImage.transform);
                //Debug.Log("AymaneShadow: Position of tracked image is " + trackedImage.transform.position);
                //Debug.Log("AymaneShadow: Rotation of tracked image is " + trackedImage.transform.rotation);

                //m_Instantiated[trackedImage.referenceImage.guid] = Instantiate(prefab, pos, rot);   
                Debug.LogWarning("AymaneShadow: Instantiated " + prefab.name + ".");

                // if(prefab.name == "Balloon_Ghost(Mixed)")
                // {
                //     Debug.Log("AymaneShadow: This is actually a Ghost");

                //     // Debug.Log("AymaneShadow: Ghost type is " + m_Instantiated[trackedImage.referenceImage.guid].GetType());
                // }                

                if (prefab.name.StartsWith("Balloon_Ghost(Mixed)_red"))
                {
                    Debug.Log("AymaneShadow: This is a red Balloon Ghost. It is the first Guide");
                    
                    GetComponent<DynamicPrefab>().m_Guide = DynamicPrefab.Guide.ChangeToSecondGuide; 
                    GetComponent<DynamicPrefab>().distanceLeft = new Vector3(1.5f, GetComponent<DynamicPrefab>().distanceLeft.y, GetComponent<DynamicPrefab>().distanceLeft.z);   

                    // m_Instantiated[trackedImage.referenceImage.guid].GetComponent<Break_Ghost>().nextPosition = new Vector3(0, 0, 5);

                    // Debug.Log("AymaneShadow: Next real position of " + prefab.name + " is " + m_Instantiated[trackedImage.referenceImage.guid].GetComponent<Break_Ghost>().nextPosition);
                }

                if (prefab.name.StartsWith("Balloon_Ghost(Mixed)_pink"))
                {
                    Debug.Log("AymaneShadow: This is a pink Balloon Ghost. It is the second Guide");

                    GetComponent<DynamicPrefab>().m_Guide = DynamicPrefab.Guide.ChangeToThirdGuide; 
                    GetComponent<DynamicPrefab>().distanceLeft = new Vector3(GetComponent<DynamicPrefab>().distanceLeft.x, GetComponent<DynamicPrefab>().distanceLeft.y, 1.0f);                       

                    foreach (var instantiatedObject in m_Instantiated)
                    {
                        if(instantiatedObject.Value.name.StartsWith("Balloon_Ghost(Mixed)_red"))
                        {
                            Debug.Log("AymaneShadow: Red Balloon Ghost Found.");
                            instantiatedObject.Value.GetComponent<Break_Ghost>().break_Ghost();
                        }
                    }                    

                    // m_Instantiated[trackedImage.referenceImage.guid].GetComponent<Break_Ghost>().nextPosition = new Vector3(0, 0, 5);

                    // Debug.Log("AymaneShadow: Next real position of " + prefab.name + " is " + m_Instantiated[trackedImage.referenceImage.guid].GetComponent<Break_Ghost>().nextPosition);
                }        

                if (prefab.name.StartsWith("Balloon_Ghost(Mixed)_yellow"))
                {
                    Debug.Log("AymaneShadow: This is a yellow Balloon Ghost. It is the third Guide");

                    GetComponent<DynamicPrefab>().m_Guide = DynamicPrefab.Guide.ChangeToFourthGuide; 
                    GetComponent<DynamicPrefab>().distanceLeft = new Vector3(1.5f, GetComponent<DynamicPrefab>().distanceLeft.y, GetComponent<DynamicPrefab>().distanceLeft.z);                        

                    foreach (var instantiatedObject in m_Instantiated)
                    {
                        if(instantiatedObject.Value.name.StartsWith("Balloon_Ghost(Mixed)_pink"))
                        {
                            Debug.Log("AymaneShadow: pink Balloon Ghost Found.");
                            instantiatedObject.Value.GetComponent<Break_Ghost>().break_Ghost();
                        }
                    }    
                }                          

                if (prefab.name.StartsWith("Balloon_Ghost(Mixed)_brown"))
                {
                    Debug.Log("AymaneShadow: This is a brown Balloon Ghost. It is the fourth Guide");

                    GetComponent<DynamicPrefab>().m_Guide = DynamicPrefab.Guide.ChangeToFirstGuide; 
                    GetComponent<DynamicPrefab>().distanceLeft = new Vector3(GetComponent<DynamicPrefab>().distanceLeft.x, GetComponent<DynamicPrefab>().distanceLeft.y, 2f);                      

                    foreach (var instantiatedObject in m_Instantiated)
                    {
                        if(instantiatedObject.Value.name.StartsWith("Balloon_Ghost(Mixed)_yellow"))
                        {
                            Debug.Log("AymaneShadow: yellow Balloon Ghost Found.");
                            instantiatedObject.Value.GetComponent<Break_Ghost>().break_Ghost();
                        }
                    }    
                }              
            }
                
        }

        public GameObject GetPrefabForReferenceImage(XRReferenceImage referenceImage)
            => m_PrefabsDictionary.TryGetValue(referenceImage.guid, out var prefab) ? prefab : null;

        public void SetPrefabForReferenceImage(XRReferenceImage referenceImage, GameObject alternativePrefab)
        {
            m_PrefabsDictionary[referenceImage.guid] = alternativePrefab;
            if (m_Instantiated.TryGetValue(referenceImage.guid, out var instantiatedPrefab))
            {
                m_Instantiated[referenceImage.guid] = Instantiate(alternativePrefab, instantiatedPrefab.transform.parent);
                Destroy(instantiatedPrefab);
            }
        }

        public GameObject getInstanceForReferenceImage(XRReferenceImage referenceImage)
        {
            if (m_Instantiated.TryGetValue(referenceImage.guid, out var instantiatedPrefab))
            {
                return m_Instantiated[referenceImage.guid];
            }
            else
                return null;
        }

#if UNITY_EDITOR
        /// <summary>
        /// This customizes the inspector component and updates the prefab list when
        /// the reference image library is changed.
        /// </summary>
        [CustomEditor(typeof(PrefabImagePairManager))]
        class PrefabImagePairManagerInspector : Editor
        {
            List<XRReferenceImage> m_ReferenceImages = new List<XRReferenceImage>();
            bool m_IsExpanded = true;

            bool HasLibraryChanged(XRReferenceImageLibrary library)
            {
                if (library == null)
                    return m_ReferenceImages.Count == 0;

                if (m_ReferenceImages.Count != library.count)
                    return true;

                for (int i = 0; i < library.count; i++)
                {
                    if (m_ReferenceImages[i] != library[i])
                        return true;
                }

                return false;
            }

            public override void OnInspectorGUI()
            {
                //customized inspector
                var behaviour = serializedObject.targetObject as PrefabImagePairManager;

                serializedObject.Update();
                using (new EditorGUI.DisabledScope(true))
                {
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("m_Script"));
                }

                var libraryProperty = serializedObject.FindProperty(nameof(m_ImageLibrary));
                EditorGUILayout.PropertyField(libraryProperty);
                var library = libraryProperty.objectReferenceValue as XRReferenceImageLibrary;

                //check library changes
                if (HasLibraryChanged(library))
                {
                    if (library)
                    {
                        var tempDictionary = new Dictionary<Guid, GameObject>();
                        foreach (var referenceImage in library)
                        {
                            tempDictionary.Add(referenceImage.guid, behaviour.GetPrefabForReferenceImage(referenceImage));
                        }
                        behaviour.m_PrefabsDictionary = tempDictionary;
                    }
                }

                // update current
                m_ReferenceImages.Clear();
                if (library)
                {
                    foreach (var referenceImage in library)
                    {
                        m_ReferenceImages.Add(referenceImage);
                    }
                }

                //show prefab list
                m_IsExpanded = EditorGUILayout.Foldout(m_IsExpanded, "Prefab List");
                if (m_IsExpanded)
                {
                    using (new EditorGUI.IndentLevelScope())
                    {
                        EditorGUI.BeginChangeCheck();

                        var tempDictionary = new Dictionary<Guid, GameObject>();
                        foreach (var image in library)
                        {
                            var prefab = (GameObject) EditorGUILayout.ObjectField(image.name, behaviour.m_PrefabsDictionary[image.guid], typeof(GameObject), false);
                            tempDictionary.Add(image.guid, prefab);
                        }

                        if (EditorGUI.EndChangeCheck())
                        {
                            Undo.RecordObject(target, "Update Prefab");
                            behaviour.m_PrefabsDictionary = tempDictionary;
                            EditorUtility.SetDirty(target);
                        }
                    }
                }

                serializedObject.ApplyModifiedProperties();
            }
        }
#endif
    }
}
