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
        public Reward reward;
        GameObject m_GOInstance;

        string m_InfoMessage = "";

        public Vector3 distanceLeft;
        const float DISTANCE_INCREMENT = 0.01f;

        // bool firstTime = true;

        public enum Guide
        {
            FirstGuide,
            // ChangeToFirstGuide,
            SecondGuide,
            ChangeToSecondGuide,
            ThirdGuide,
            ChangeToThirdGuide,
            FourthGuide,
            ChangeToFourthGuide,
            TreasureFound,
            ChangeToTreasureFound,
        }

        public Guide m_Guide;

        void Start()
        {
            m_InfoMessage = "Scan the 1st marker.";
        }

        void OnGUI()
        {
            var fontSize = 50;
            GUI.skin.button.fontSize = fontSize;
            GUI.skin.label.fontSize = fontSize;
            GUI.skin.label.alignment = TextAnchor.MiddleCenter;

            float margin = 100;

            GUILayout.BeginArea(new Rect(margin, margin + 50, Screen.width - margin * 2, Screen.height - margin * 2));

            // GUILayout.Label(m_InfoMessage);

            GUILayout.Button($"{m_InfoMessage}");

            GUILayout.EndArea();
        }

        public void SetInfo(string infoMessage)
        {
            m_InfoMessage = $"{infoMessage}";
        }

        void Update()
        {
            m_GOInstance = null;
            var manager = GetComponent<PrefabImagePairManager>();
            if (!manager)
            {
                SetInfo($"No {nameof(PrefabImagePairManager)} available.");
            }

            var library = manager.imageLibrary;
            if (!library)
            {
                SetInfo($"No image library available.");
            }

            switch (m_Guide)
            {
                case Guide.ChangeToSecondGuide:
                {
                    if (!m_GOInstance)
                        m_GOInstance = manager.getInstanceForReferenceImage(library[0]);

                    SetInfo($"The Red Ghost is guiding you.");

                    if(checkDistanceLeft(m_GOInstance))
                    {
                        SetInfo("Scan the 2nd marker.");
                        m_Guide = Guide.SecondGuide;                        
                    }

                    break;
                }
                case Guide.ChangeToThirdGuide:
                {
                    if (!m_GOInstance)
                        m_GOInstance = manager.getInstanceForReferenceImage(library[1]);

                    SetInfo($"The Pink Ghost is guiding you.");

                    if (checkDistanceLeft(m_GOInstance))
                    {
                        SetInfo("Scan the 3rd marker.");
                        m_Guide = Guide.ThirdGuide;
                    }

                    break;
                }
                case Guide.ChangeToFourthGuide:
                {
                    if (!m_GOInstance)
                        m_GOInstance = manager.getInstanceForReferenceImage(library[2]);

                    SetInfo($"The Yellow Ghost is guiding you.");

                    if (checkDistanceLeft(m_GOInstance))
                    {
                        SetInfo("Scan the 4th marker.");
                        m_Guide = Guide.FourthGuide;
                    }

                    break;
                }
                case Guide.ChangeToTreasureFound:
                {
                    if (!m_GOInstance)
                        m_GOInstance = manager.getInstanceForReferenceImage(library[3]);

                    SetInfo($"The Brown Ghost is guiding you.");

                    if (checkDistanceLeft(m_GOInstance))
                    {
                        SetInfo("Congrats! You found the treasure!");
                        m_Guide = Guide.TreasureFound;

                        m_GOInstance.GetComponent<Break_Ghost>().break_Ghost();

                        reward.ShowReward(m_GOInstance.transform);
                    }

                    break;
                }
            }

        }

        bool checkDistanceLeft(GameObject m_GOInstance)
        {
            Vector3 oldPosition = m_GOInstance.transform.position;
            Vector3 newPosition = new Vector3();
            Vector3 oldDistanceLeft = distanceLeft;
            Vector3 newDistanceLeft = new Vector3();

            Debug.Log("AymaneShadow: ========================================================");
            Debug.Log("AymaneShadow: Old Position = " + oldPosition);
            Debug.Log("AymaneShadow: Old distanceLeft = " + oldDistanceLeft);            

            if(distanceLeft.x != 0)
            {
                if(distanceLeft.x > 0)
                {
                    Debug.Log("AymaneShadow: Should move in positive X.");

                    newPosition = new Vector3(m_GOInstance.transform.position.x + DISTANCE_INCREMENT, m_GOInstance.transform.position.y, m_GOInstance.transform.position.z);
                    m_GOInstance.transform.position = newPosition;

                    newDistanceLeft = new Vector3(distanceLeft.x - DISTANCE_INCREMENT, distanceLeft.y, distanceLeft.z);
                    distanceLeft = newDistanceLeft;

                    if (distanceLeft.x <= 0)
                    {
                        return true;
                    }                    
                }
                else
                {
                    Debug.Log("AymaneShadow: Should move in negative X.");

                    newPosition = new Vector3(m_GOInstance.transform.position.x - DISTANCE_INCREMENT, m_GOInstance.transform.position.y, m_GOInstance.transform.position.z);
                    m_GOInstance.transform.position = newPosition;

                    newDistanceLeft = new Vector3(distanceLeft.x + DISTANCE_INCREMENT, distanceLeft.y, distanceLeft.z);
                    distanceLeft = newDistanceLeft;

                    if (distanceLeft.x >= 0)
                    {
                        return true;
                    }                    
                }
            }

            else if(distanceLeft.y != 0)
            {
                if(distanceLeft.y > 0)
                {
                    Debug.Log("AymaneShadow: Should move in positive Y.");

                    newPosition = new Vector3(m_GOInstance.transform.position.x, m_GOInstance.transform.position.y + DISTANCE_INCREMENT, m_GOInstance.transform.position.z);
                    m_GOInstance.transform.position = newPosition;

                    newDistanceLeft = new Vector3(distanceLeft.x, distanceLeft.y - DISTANCE_INCREMENT, distanceLeft.z);
                    distanceLeft = newDistanceLeft;

                    if (distanceLeft.y <= 0)
                    {
                        return true;
                    }                    
                }
                else
                {
                    Debug.Log("AymaneShadow: Should move in negative Y.");

                    newPosition = new Vector3(m_GOInstance.transform.position.x, m_GOInstance.transform.position.y - DISTANCE_INCREMENT, m_GOInstance.transform.position.z);
                    m_GOInstance.transform.position = newPosition;

                    newDistanceLeft = new Vector3(distanceLeft.x, distanceLeft.y + DISTANCE_INCREMENT, distanceLeft.z);
                    distanceLeft = newDistanceLeft;

                    if (distanceLeft.y >= 0)
                    {
                        return true;
                    }                    
                }
            }

            else if(distanceLeft.z != 0)
            {                
                if(distanceLeft.z > 0)
                {
                    Debug.Log("AymaneShadow: Should move in positive Z.");

                    newPosition = new Vector3(m_GOInstance.transform.position.x, m_GOInstance.transform.position.y, m_GOInstance.transform.position.z + DISTANCE_INCREMENT);
                    m_GOInstance.transform.position = newPosition;

                    newDistanceLeft = new Vector3(distanceLeft.x, distanceLeft.y, distanceLeft.z - DISTANCE_INCREMENT);
                    distanceLeft = newDistanceLeft;

                    if (distanceLeft.z <= 0)
                    {
                        return true;
                    }                    
                }
                else
                {
                    Debug.Log("AymaneShadow: Should move in negative Z.");

                    newPosition = new Vector3(m_GOInstance.transform.position.x, m_GOInstance.transform.position.y, m_GOInstance.transform.position.z - DISTANCE_INCREMENT);
                    m_GOInstance.transform.position = newPosition;

                    newDistanceLeft = new Vector3(distanceLeft.x, distanceLeft.y, distanceLeft.z + DISTANCE_INCREMENT);
                    distanceLeft = newDistanceLeft;

                    if (distanceLeft.z >= 0)
                    {
                        return true;
                    }                    
                }                            
            }                        

            Debug.Log("AymaneShadow: --------------------------------------------------------");
            Debug.Log("AymaneShadow: New Position = " + newPosition);
            Debug.Log("AymaneShadow: New distanceLeft = " + newDistanceLeft);            

            return false;
        }
    }
}
