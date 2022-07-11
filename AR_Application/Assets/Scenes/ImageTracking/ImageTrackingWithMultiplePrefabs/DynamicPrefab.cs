using System;
using System.Text;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace UnityEngine.XR.ARFoundation.Samples
{
    /// <summary>
    /// Change the prefab for the first image in library at runtime.
    /// </summary>
    [RequireComponent(typeof(ARTrackedImageManager))]
    public class DynamicPrefab : MonoBehaviour
    {
        GameObject m_OriginalPrefab;

        [SerializeField]
        GameObject m_AlternativePrefab;

        GameObject m_GOInstance;

        public GameObject alternativePrefab
        {
            get => m_AlternativePrefab;
            set => m_AlternativePrefab = value;
        }

/*
        enum State
        {
            OriginalPrefab,
            ChangeToOriginalPrefab,
            AlternativePrefab,
            ChangeToAlternativePrefab,
            Error
        }

        State m_State;
*/

        string m_InfoMessage = "";

/*
        enum MoveInstanceInDirection
        {
            MoveInPositiveX,
            ChangeToMoveInPositiveX,
            MoveInNegativeX,
            ChangeToMoveInNegativeX,
            MoveInPositiveY,
            ChangeToMoveInPositiveY,
            MoveInNegativeY,
            ChangeToMoveInNegativeY,
            MoveInPositiveZ,
            ChangeToMoveInPositiveZ,
            MoveInNegativeZ,
            ChangeToMoveInNegativeZ,
            Error
        }


        MoveInstanceInDirection m_MoveInstanceInDirection;
*/        

        public Vector3 distanceLeft;
        const float DISTANCE_INCREMENT = 0.01f;

        // bool firstTime = true;

        public enum Guide
        {
            FirstGuide,
            ChangeToFirstGuide,
            SecondGuide,
            ChangeToSecondGuide,
            ThirdGuide,
            ChangeToThirdGuide,
            FourthGuide,
            ChangeToFourthGuide
        }

        public Guide m_Guide;        

        void Start()
        {
            GUILayout.Label("Scan the 1st tracker.");
        }

        void OnGUI()
        {        
                            
            var fontSize = 50;
            GUI.skin.button.fontSize = fontSize;
            GUI.skin.label.fontSize = fontSize;
            // GUI.skin.label.alignment = 

            float margin = 150;                        

            GUILayout.BeginArea(new Rect(margin, margin, Screen.width - margin * 2, Screen.height - margin * 2));
            /*
            switch (m_State)
            {
                case State.OriginalPrefab:
                {
                    if (GUILayout.Button($"Alternative Prefab for {GetComponent<PrefabImagePairManager>().imageLibrary[0].name}"))
                    {
                        m_State = State.ChangeToAlternativePrefab;
                    }

                    break;
                }
                case State.AlternativePrefab:
                {
                    if (GUILayout.Button($"Original Prefab for {GetComponent<PrefabImagePairManager>().imageLibrary[0].name}"))
                    {
                        m_State = State.ChangeToOriginalPrefab;
                    }

                    break;
                }
                case State.Error:
                { */
                    GUILayout.Label(m_InfoMessage);
                    /*
                    break;
                }
            }
            */
            GUILayout.EndArea();

            

            /*

            GUILayout.BeginArea(new Rect(margin, margin * 2.5f, Screen.width - margin * 2, Screen.height - margin * 2));

            switch (m_MoveInstanceInDirection)
            {
                case MoveInstanceInDirection.MoveInPositiveX:
                    {
                        if (GUILayout.Button($"Move in the Negative X direction"))
                        {
                            m_MoveInstanceInDirection = MoveInstanceInDirection.ChangeToMoveInNegativeX;                            
                        }

                        break;
                    }
                case MoveInstanceInDirection.MoveInNegativeX:
                    {
                        if (GUILayout.Button($"Move in the Positive Y direction"))
                        {
                            m_MoveInstanceInDirection = MoveInstanceInDirection.ChangeToMoveInPositiveY;                            
                        }

                        break;
                    }
                case MoveInstanceInDirection.MoveInPositiveY:
                    {
                        if (GUILayout.Button($"Move in the Negative Y direction"))
                        {
                            m_MoveInstanceInDirection = MoveInstanceInDirection.ChangeToMoveInNegativeY;
                        }

                        break;
                    }
                case MoveInstanceInDirection.MoveInNegativeY:
                    {
                        if (GUILayout.Button($"Move in the Positive Z direction"))
                        {
                            m_MoveInstanceInDirection = MoveInstanceInDirection.ChangeToMoveInPositiveZ;
                        }

                        break;
                    }
                case MoveInstanceInDirection.MoveInPositiveZ:
                    {
                        if (GUILayout.Button($"Move in the Negative Z direction"))
                        {
                            m_MoveInstanceInDirection = MoveInstanceInDirection.ChangeToMoveInNegativeZ;
                        }

                        break;
                    }
                case MoveInstanceInDirection.MoveInNegativeZ:
                    {
                        if (GUILayout.Button($"Move in the Positive X direction"))
                        {
                            m_MoveInstanceInDirection = MoveInstanceInDirection.ChangeToMoveInPositiveX;
                        }

                        break;
                    }
                case MoveInstanceInDirection.Error:
                    {
                        GUILayout.Label(m_InfoMessage);
                        break;
                    }
            }
            GUILayout.EndArea();      

            */ 
        }

/*
        void SetError(string errorMessage)
        {
            m_State = State.Error;
            m_InfoMessage = $"Error: {errorMessage}";
        }
*/

        public void SetInfo(string errorMessage)
        {
            // m_State = State.Error;
            m_InfoMessage = $"{errorMessage}";
        }

        void Update()
        {

            /*
            switch (m_State)
            {
                case State.ChangeToAlternativePrefab:
                {
                    if (!alternativePrefab)
                    {
                        SetError("No alternative prefab is given.");
                        break;
                    }

                    var manager = GetComponent<PrefabImagePairManager>();
                    if (!manager)
                    {
                        SetError($"No {nameof(PrefabImagePairManager)} available.");
                        break;
                    }

                    var library = manager.imageLibrary;
                    if (!library)
                    {
                        SetError($"No image library available.");
                        break;
                    }

                    if (!m_OriginalPrefab)
                        m_OriginalPrefab = manager.GetPrefabForReferenceImage(library[0]);

                    manager.SetPrefabForReferenceImage(library[0], alternativePrefab);
                    m_State = State.AlternativePrefab;
                    break;
                }

                case State.ChangeToOriginalPrefab:
                {
                    if (!m_OriginalPrefab)
                    {
                        SetError("No original prefab is given.");
                        break;
                    }

                    var manager = GetComponent<PrefabImagePairManager>();
                    if (!manager)
                    {
                        SetError($"No {nameof(PrefabImagePairManager)} available.");
                        break;
                    }

                    var library = manager.imageLibrary;
                    if (!library)
                    {
                        SetError($"No image library available.");
                        break;
                    }

                    manager.SetPrefabForReferenceImage(library[0], m_OriginalPrefab);
                    m_State = State.OriginalPrefab;
                    break;
                }
            }



            switch (m_MoveInstanceInDirection)
            {
                case MoveInstanceInDirection.ChangeToMoveInPositiveX:
                    {                        
                        if(firstTime)
                        {
                            distanceLeft = new Vector3(DISTANCE_TO_GO, distanceLeft.y, distanceLeft.z);
                            firstTime = false;
                        }

                        var manager = GetComponent<PrefabImagePairManager>();
                        if (!manager)
                        {
                            SetInfo($"No {nameof(PrefabImagePairManager)} available.");
                            break;
                        }

                        var library = manager.imageLibrary;
                        if (!library)
                        {
                            SetInfo($"No image library available.");
                            break;
                        }

                        if (!m_GOInstance)
                            m_GOInstance = manager.getInstanceForReferenceImage(library[3]);

                        if(m_GOInstance)
                        {
                            Debug.Log("AymaneShadow: Should move in positive X.");
                            Debug.Log("AymaneShadow: firstTime = " + firstTime);
                            Vector3 oldPosition = m_GOInstance.transform.position;
                            Debug.Log("AymaneShadow: Old Position = " + oldPosition);
                            Debug.Log("AymaneShadow: Old distanceLeft = " + distanceLeft);
                            m_GOInstance.transform.position = new Vector3(m_GOInstance.transform.position.x + DISTANCE_INCREMENT, m_GOInstance.transform.position.y, m_GOInstance.transform.position.z);                            
                            distanceLeft = new Vector3(distanceLeft.x - DISTANCE_INCREMENT, distanceLeft.y, distanceLeft.z);
                            Vector3 newPosition = m_GOInstance.transform.position;
                            Debug.Log("AymaneShadow: New Position = " + newPosition);
                            Debug.Log("AymaneShadow: New distanceLeft = " + distanceLeft);
                            SetInfo($"Moving in positive X. Old Position = " + oldPosition + " New Position = " + newPosition);                            
                        }

                        if(distanceLeft.x <= 0)
                        {
                            m_MoveInstanceInDirection = MoveInstanceInDirection.MoveInPositiveX;
                            firstTime = true;
                        }
                            
                        break;
                    }
                case MoveInstanceInDirection.ChangeToMoveInNegativeX:
                    {
                        if (firstTime)
                        {
                            distanceLeft = new Vector3(DISTANCE_TO_GO, distanceLeft.y, distanceLeft.z);
                            firstTime = false;
                        }                        

                        var manager = GetComponent<PrefabImagePairManager>();
                        if (!manager)
                        {
                            SetInfo($"No {nameof(PrefabImagePairManager)} available.");
                            break;
                        }

                        var library = manager.imageLibrary;
                        if (!library)
                        {
                            SetInfo($"No image library available.");
                            break;
                        }

                        if (!m_GOInstance)
                            m_GOInstance = manager.getInstanceForReferenceImage(library[3]);

                        if (m_GOInstance)
                        {                            
                            Debug.Log("AymaneShadow: Should move in negative X.");
                            Debug.Log("AymaneShadow: firstTime = " + firstTime);
                            Vector3 oldPosition = m_GOInstance.transform.position;
                            Debug.Log("AymaneShadow: Old Position = " + oldPosition);
                            Debug.Log("AymaneShadow: Old distanceLeft = " + distanceLeft);
                            m_GOInstance.transform.position = new Vector3(m_GOInstance.transform.position.x - DISTANCE_INCREMENT, m_GOInstance.transform.position.y, m_GOInstance.transform.position.z);
                            distanceLeft = new Vector3(distanceLeft.x - DISTANCE_INCREMENT, distanceLeft.y, distanceLeft.z);
                            Vector3 newPosition = m_GOInstance.transform.position;
                            Debug.Log("AymaneShadow: New Position = " + newPosition);
                            Debug.Log("AymaneShadow: New distanceLeft = " + distanceLeft);
                            SetInfo($"Moving in negative X. Old Position = " + oldPosition + " New Position = " + newPosition);
                        }

                        if (distanceLeft.x <= 0)
                        {
                            m_MoveInstanceInDirection = MoveInstanceInDirection.MoveInNegativeX;
                            firstTime = true;
                        }

                        break;
                    }
                case MoveInstanceInDirection.ChangeToMoveInPositiveY:
                    {
                        if (firstTime)
                        {
                            distanceLeft = new Vector3(distanceLeft.x, DISTANCE_TO_GO, distanceLeft.z);
                            firstTime = false;
                        }                        
                        

                        var manager = GetComponent<PrefabImagePairManager>();
                        if (!manager)
                        {
                            SetInfo($"No {nameof(PrefabImagePairManager)} available.");
                            break;
                        }

                        var library = manager.imageLibrary;
                        if (!library)
                        {
                            SetInfo($"No image library available.");
                            break;
                        }

                        if (!m_GOInstance)
                            m_GOInstance = manager.getInstanceForReferenceImage(library[3]);

                        if (m_GOInstance)
                        {
                            Debug.Log("AymaneShadow: Should move in positive Y.");
                            Debug.Log("AymaneShadow: firstTime = " + firstTime);
                            Vector3 oldPosition = m_GOInstance.transform.position;
                            Debug.Log("AymaneShadow: Old Position = " + oldPosition);
                            Debug.Log("AymaneShadow: Old distanceLeft = " + distanceLeft);
                            m_GOInstance.transform.position = new Vector3(m_GOInstance.transform.position.x, m_GOInstance.transform.position.y + DISTANCE_INCREMENT, m_GOInstance.transform.position.z);
                            distanceLeft = new Vector3(distanceLeft.x, distanceLeft.y - DISTANCE_INCREMENT, distanceLeft.z);
                            Vector3 newPosition = m_GOInstance.transform.position;
                            Debug.Log("AymaneShadow: New Position = " + newPosition);
                            Debug.Log("AymaneShadow: New distanceLeft = " + distanceLeft);
                            SetInfo($"Moving in positive Y. Old Position = " + oldPosition + " New Position = " + newPosition);
                        }

                        if (distanceLeft.y <= 0)
                        {
                            m_MoveInstanceInDirection = MoveInstanceInDirection.MoveInPositiveY;
                            firstTime = true;
                        }

                        break;
                    }
                case MoveInstanceInDirection.ChangeToMoveInNegativeY:
                    {
                        if (firstTime)
                        {
                            distanceLeft = new Vector3(distanceLeft.x, DISTANCE_TO_GO, distanceLeft.z);
                            firstTime = false;
                        }                                                

                        var manager = GetComponent<PrefabImagePairManager>();
                        if (!manager)
                        {
                            SetInfo($"No {nameof(PrefabImagePairManager)} available.");
                            break;
                        }

                        var library = manager.imageLibrary;
                        if (!library)
                        {
                            SetInfo($"No image library available.");
                            break;
                        }

                        if (!m_GOInstance)
                            m_GOInstance = manager.getInstanceForReferenceImage(library[3]);

                        if (m_GOInstance)
                        {
                            Debug.Log("AymaneShadow: Should move in negative Y.");
                            Debug.Log("AymaneShadow: firstTime = " + firstTime);
                            Vector3 oldPosition = m_GOInstance.transform.position;
                            Debug.Log("AymaneShadow: Old Position = " + oldPosition);
                            Debug.Log("AymaneShadow: Old distanceLeft = " + distanceLeft);
                            m_GOInstance.transform.position = new Vector3(m_GOInstance.transform.position.x, m_GOInstance.transform.position.y - DISTANCE_INCREMENT, m_GOInstance.transform.position.z);
                            distanceLeft = new Vector3(distanceLeft.x, distanceLeft.y - DISTANCE_INCREMENT, distanceLeft.z);
                            Vector3 newPosition = m_GOInstance.transform.position;
                            Debug.Log("AymaneShadow: New Position = " + newPosition);
                            Debug.Log("AymaneShadow: New distanceLeft = " + distanceLeft);
                            SetInfo($"Moving in negative Y. Old Position = " + oldPosition + " New Position = " + newPosition);
                        }

                        if (distanceLeft.y <= 0)
                        {
                            m_MoveInstanceInDirection = MoveInstanceInDirection.MoveInNegativeY;
                            firstTime = true;
                        }

                        break;
                    }
                case MoveInstanceInDirection.ChangeToMoveInPositiveZ:
                    {
                        if (firstTime)
                        {
                            distanceLeft = new Vector3(distanceLeft.x, distanceLeft.y, DISTANCE_TO_GO);
                            firstTime = false;
                        }                                                

                        var manager = GetComponent<PrefabImagePairManager>();
                        if (!manager)
                        {
                            SetInfo($"No {nameof(PrefabImagePairManager)} available.");
                            break;
                        }

                        var library = manager.imageLibrary;
                        if (!library)
                        {
                            SetInfo($"No image library available.");
                            break;
                        }

                        if (!m_GOInstance)
                            m_GOInstance = manager.getInstanceForReferenceImage(library[3]);

                        if (m_GOInstance)
                        {
                            Debug.Log("AymaneShadow: Should move in positive Z.");
                            Debug.Log("AymaneShadow: firstTime = " + firstTime);
                            Vector3 oldPosition = m_GOInstance.transform.position;
                            Debug.Log("AymaneShadow: Old Position = " + oldPosition);
                            Debug.Log("AymaneShadow: Old distanceLeft = " + distanceLeft);
                            m_GOInstance.transform.position = new Vector3(m_GOInstance.transform.position.x, m_GOInstance.transform.position.y, m_GOInstance.transform.position.z + DISTANCE_INCREMENT);
                            distanceLeft = new Vector3(distanceLeft.x, distanceLeft.y, distanceLeft.z - DISTANCE_INCREMENT);
                            Vector3 newPosition = m_GOInstance.transform.position;
                            Debug.Log("AymaneShadow: New Position = " + newPosition);
                            Debug.Log("AymaneShadow: New distanceLeft = " + distanceLeft);
                            SetInfo($"Moving in positive Z. Old Position = " + oldPosition + " New Position = " + newPosition);
                        }

                        if (distanceLeft.z <= 0)
                        {
                            m_MoveInstanceInDirection = MoveInstanceInDirection.MoveInPositiveZ;
                            firstTime = true;
                        }

                        break;
                    }
                case MoveInstanceInDirection.ChangeToMoveInNegativeZ:
                    {
                        if (firstTime)
                        {
                            distanceLeft = new Vector3(distanceLeft.x, distanceLeft.y, DISTANCE_TO_GO);
                            firstTime = false;
                        }

                        var manager = GetComponent<PrefabImagePairManager>();
                        if (!manager)
                        {
                            SetInfo($"No {nameof(PrefabImagePairManager)} available.");
                            break;
                        }

                        var library = manager.imageLibrary;
                        if (!library)
                        {
                            SetInfo($"No image library available.");
                            break;
                        }

                        if (!m_GOInstance)
                            m_GOInstance = manager.getInstanceForReferenceImage(library[3]);

                        if (m_GOInstance)
                        {
                            Debug.Log("AymaneShadow: Should move in negative Z.");
                            Debug.Log("AymaneShadow: firstTime = " + firstTime);
                            Vector3 oldPosition = m_GOInstance.transform.position;
                            Debug.Log("AymaneShadow: Old Position = " + oldPosition);
                            m_GOInstance.transform.position = new Vector3(m_GOInstance.transform.position.x, m_GOInstance.transform.position.y, m_GOInstance.transform.position.z - DISTANCE_INCREMENT);
                            distanceLeft = new Vector3(distanceLeft.x, distanceLeft.y, distanceLeft.z - DISTANCE_INCREMENT);
                            Vector3 newPosition = m_GOInstance.transform.position;                            
                            Debug.Log("AymaneShadow: New Position = " + newPosition);
                            SetInfo($"Moving in negative Z. Old Position = " + oldPosition + " New Position = " + newPosition);
                        }

                        if (distanceLeft.z <= 0)
                        {
                            m_MoveInstanceInDirection = MoveInstanceInDirection.MoveInNegativeZ;
                            firstTime = true;
                        }

                        break;
                    }
            }

            */

            switch (m_Guide)
            {
                // case Guide.FirstGuide:
                // {
                //     SetInfo($"Scan the 2nd tracker.");
                //     m_Guide = Guide.ChangeToSecondGuide;  
                //     break;
                // }
                // case Guide.SecondGuide:
                // {
                //     SetInfo($"Scan the 3rd tracker.");
                //     m_Guide = Guide.ChangeToThirdGuide;  
                //     break;
                // }
                // case Guide.ThirdGuide:
                // {
                //     SetInfo($"Scan the 4th tracker.");
                //     m_Guide = Guide.ChangeToFourthGuide;  
                //     break;
                // }
                // case Guide.FourthGuide:
                // {
                //     SetInfo($"Congrats! you found the treasure!");
                //     break;
                // }                  
                case Guide.ChangeToSecondGuide:
                {
                    m_GOInstance = null;
                    var manager = GetComponent<PrefabImagePairManager>();
                    if (!manager)
                    {
                        SetInfo($"No {nameof(PrefabImagePairManager)} available.");
                        break;
                    }

                    var library = manager.imageLibrary;
                    if (!library)
                    {
                        SetInfo($"No image library available.");
                        break;
                    }

                    if (!m_GOInstance)
                        m_GOInstance = manager.getInstanceForReferenceImage(library[0]);

                    Debug.LogError("AymaneShadow: m_GOInstance.name = " + m_GOInstance.name);  


                    SetInfo($"The Red Ghost is guiding you.");

                    checkDistanceLeft(1, m_GOInstance);

                    if (distanceLeft.x <= 0)
                    {
                        SetInfo("Scan the 2nd tracker.");
                        m_Guide = Guide.SecondGuide;
                    }                    
                                        
                    break;
                }
                case Guide.ChangeToThirdGuide:
                {
                    m_GOInstance = null;
                    var manager = GetComponent<PrefabImagePairManager>();
                    if (!manager)
                    {
                        SetInfo($"No {nameof(PrefabImagePairManager)} available.");
                        break;
                    }

                    var library = manager.imageLibrary;
                    if (!library)
                    {
                        SetInfo($"No image library available.");
                        break;
                    }

                    if (!m_GOInstance)
                        m_GOInstance = manager.getInstanceForReferenceImage(library[1]);

                    Debug.LogError("AymaneShadow: m_GOInstance.name = " + m_GOInstance.name);                          

                    SetInfo($"The Pink Ghost is guiding you.");

                    checkDistanceLeft(-1, m_GOInstance);

                    if (distanceLeft.z <= 0)
                    {
                        SetInfo("Scan the 3rd tracker.");
                        m_Guide = Guide.ThirdGuide;
                    }                    
                    
                    break;
                }
                case Guide.ChangeToFourthGuide:
                {
                    m_GOInstance = null;
                    var manager = GetComponent<PrefabImagePairManager>();
                    if (!manager)
                    {
                        SetInfo($"No {nameof(PrefabImagePairManager)} available.");
                        break;
                    }

                    var library = manager.imageLibrary;
                    if (!library)
                    {
                        SetInfo($"No image library available.");
                        break;
                    }

                    if (!m_GOInstance)
                        m_GOInstance = manager.getInstanceForReferenceImage(library[2]);

                    Debug.LogError("AymaneShadow: m_GOInstance.name = " + m_GOInstance.name);  

                    SetInfo($"The Yellow Ghost is guiding you.");

                    checkDistanceLeft(1, m_GOInstance);

                    if (distanceLeft.x <= 0)
                    {
                        SetInfo("Scan the 4th tracker.");
                        m_Guide = Guide.FourthGuide;
                    }                        
                    
                    break;
                }                                               
                case Guide.ChangeToFirstGuide:
                {
                    m_GOInstance = null;
                    var manager = GetComponent<PrefabImagePairManager>();
                    if (!manager)
                    {
                        SetInfo($"No {nameof(PrefabImagePairManager)} available.");
                        break;
                    }

                    var library = manager.imageLibrary;
                    if (!library)
                    {
                        SetInfo($"No image library available.");
                        break;
                    }

                    if (!m_GOInstance)
                        m_GOInstance = manager.getInstanceForReferenceImage(library[3]);

                    Debug.LogError("AymaneShadow: m_GOInstance.name = " + m_GOInstance.name);  

                    SetInfo($"The Brown Ghost is guiding you.");

                    checkDistanceLeft(1, m_GOInstance);

                    if (distanceLeft.z <= 0)
                    {
                        SetInfo("Congrats! You found the treasure!");
                        m_Guide = Guide.FirstGuide;
                    }                    
                    
                    break;
                }                 
            }     

        }

        void checkDistanceLeft(int isPositiveDirection, GameObject m_GOInstance)
        {
            if(distanceLeft.x > 0)
            {
                Debug.Log("AymaneShadow: Should move in X.");
                // Debug.Log("AymaneShadow: firstTime = " + firstTime);
                Vector3 oldPosition = m_GOInstance.transform.position;
                Debug.Log("AymaneShadow: Old Position = " + oldPosition);
                Debug.Log("AymaneShadow: Old distanceLeft = " + distanceLeft);
                m_GOInstance.transform.position = new Vector3(m_GOInstance.transform.position.x + DISTANCE_INCREMENT * isPositiveDirection, m_GOInstance.transform.position.y, m_GOInstance.transform.position.z);                            
                distanceLeft = new Vector3(distanceLeft.x - DISTANCE_INCREMENT, distanceLeft.y, distanceLeft.z);
                Vector3 newPosition = m_GOInstance.transform.position;
                Debug.Log("AymaneShadow: New Position = " + newPosition);
                Debug.Log("AymaneShadow: New distanceLeft = " + distanceLeft);  
            }
            else if(distanceLeft.y > 0)
            {
                Debug.Log("AymaneShadow: Should move in Y.");
                // Debug.Log("AymaneShadow: firstTime = " + firstTime);
                Vector3 oldPosition = m_GOInstance.transform.position;
                Debug.Log("AymaneShadow: Old Position = " + oldPosition);
                Debug.Log("AymaneShadow: Old distanceLeft = " + distanceLeft);
                m_GOInstance.transform.position = new Vector3(m_GOInstance.transform.position.x, m_GOInstance.transform.position.y + DISTANCE_INCREMENT * isPositiveDirection, m_GOInstance.transform.position.z);                            
                distanceLeft = new Vector3(distanceLeft.x, distanceLeft.y - DISTANCE_INCREMENT, distanceLeft.z);
                Vector3 newPosition = m_GOInstance.transform.position;
                Debug.Log("AymaneShadow: New Position = " + newPosition);
                Debug.Log("AymaneShadow: New distanceLeft = " + distanceLeft);  
            }
            else if(distanceLeft.z > 0)
            {
                Debug.Log("AymaneShadow: Should move in Z.");
                // Debug.Log("AymaneShadow: firstTime = " + firstTime);
                Vector3 oldPosition = m_GOInstance.transform.position;
                Debug.Log("AymaneShadow: Old Position = " + oldPosition);
                Debug.Log("AymaneShadow: Old distanceLeft = " + distanceLeft);
                m_GOInstance.transform.position = new Vector3(m_GOInstance.transform.position.x, m_GOInstance.transform.position.y, m_GOInstance.transform.position.z + DISTANCE_INCREMENT * isPositiveDirection);                            
                distanceLeft = new Vector3(distanceLeft.x, distanceLeft.y, distanceLeft.z - DISTANCE_INCREMENT);
                Vector3 newPosition = m_GOInstance.transform.position;
                Debug.Log("AymaneShadow: New Position = " + newPosition);
                Debug.Log("AymaneShadow: New distanceLeft = " + distanceLeft);  
            }              
        }
    }
}
