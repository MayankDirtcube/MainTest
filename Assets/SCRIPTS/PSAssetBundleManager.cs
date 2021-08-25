/*
 * date:-2021/07/16
 * author:-sayat 
 * co-Author:-sanjay
 * handles the asset loading
 */
using System.Collections;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections.Generic;

namespace PlayStarz
{
    namespace AssetBundleLoading
    {
        public class PSAssetBundleManager : MonoBehaviour
        {
            public static PSAssetBundleManager instance;
            //varibale to store the loading image.
            // public Image loadingImage;
            //Text to visulize the loading
            //  public Text loadingPercentage;

            // public GameObject BG;

            //private WWW www;
            private UnityWebRequest uwr;
            private bool loadingStart = false;

            //Since dont have the external data keeping it 1 by default.
            private int _currentABVersion = 0;

            private string playerPrefKey = "AssetBundleVersionIdentifier";

            private AssetBundleDownloadType _downloadType;

            private int gameIndex;

            //Bool to check the internet connectivity.
            private bool _internetStatus;

            [SerializeField] private List<PSAssetBundleData> _assetBundleData;

            private AssetBundle _currentAssetBundle;

            private string path = "AssetBundleData";

            public AssetBundleWantUI assetBundleWantUI = AssetBundleWantUI.no;

            private void Awake()
            {
                PlayerPrefs.DeleteAll();
                DontDestroyOnLoad(this);
                CheckInstance();
                LoadAssetBundleData();
  
            }

            //Checking the instance.
            private void CheckInstance()
            {
                if (instance == null)
                {
                    instance = this;
                }
                else
                {
                    Destroy(gameObject);
                }
            }

            private void LoadAssetBundleData()
            {
                var PSAssetBundleObject = Resources.LoadAll<PSAssetBundleData>(path);

                if (PSAssetBundleObject != null)
                {
                    foreach (var item in PSAssetBundleObject)
                    {
                        _assetBundleData.Add(item);
                    }
                }
            }

            //Function to load the AssetBundle
            public void LoadAssetBundle(int i)
            {
                Debug.Log("The asset bundle id is--" + i);
                _currentABVersion = GetABVersionNumber();

                gameIndex = i;
                CheckDownloadType();
            }

            

            // Update is called once per frame
            void Update()
            {
                /* if (loadingStart && assetBundleWantUI==AssetBundleWantUI.yes)
                 {
                     BG.SetActive(true);

                     double v = uwr.downloadProgress;
                     loadingImage.fillAmount = (float)v;
                     v = System.Math.Round(v, 2);
                     v *= 100;
                     loadingPercentage.text = "Loading" + v + "%";
                 }*/
            }

            private void CheckDownloadType()
            {
                /*switch (_downloadType)
                {
                    case AssetBundleDownloadType.local:
                        StartCoroutine(SceneLoadFromLocal(gameIndex));
                        break;

                    case AssetBundleDownloadType.server:
                        StartCoroutine(SceneLoadFromServer(gameIndex));
                        break;
                    default:
                        break;
                }*/

                StartCoroutine(SceneLoadFromServer(gameIndex));
            }
            //To load the assetbundle from Local.
            private IEnumerator SceneLoadFromLocal(int gameIndex)
            {
                _currentABVersion = GetABVersionNumber();
                while (!Caching.ready)
                   
#if UNITY_IPHONE
                using(uwr=UnityWebRequestAssetBundle.GetAssetBundle(_assetBundleData[gameIndex]._assetBundleObject.assetBundleURL_ios))
#endif
#if UNITY_ANDROID
                using (uwr = UnityWebRequestAssetBundle.GetAssetBundle(_assetBundleData[0]._assetBundleObject.assetBundleURL_Android[gameIndex]))
#endif

                {
                    loadingStart = true;
                    yield return uwr.SendWebRequest();
                    if (!string.IsNullOrEmpty(uwr.error))
                    {
                        Debug.Log(uwr.error);
                        yield return null;
                    }
                    else
                    {
                        Debug.Log("Download success");
                    }
                    _currentAssetBundle = DownloadHandlerAssetBundle.GetContent(uwr);

                }
                loadingStart = false;
                if (_currentABVersion != 0)
                {
                    string[] scenes = _currentAssetBundle.GetAllScenePaths();
                    foreach (string s in scenes)
                    {
                        print(Path.GetFileNameWithoutExtension(s));
                        if (Path.GetFileNameWithoutExtension(s) == _assetBundleData[0]._assetBundleObject.sceneNames[gameIndex])
                        {
                            LoadScene(Path.GetFileNameWithoutExtension(s));
                        }
                    }
                }
                else
                {
                    Debug.Log("get me internet");
                }
            }

            //To load the assetbundle from server.
            private IEnumerator SceneLoadFromServer(int gameIndex)
            {
                //done to make the deafault 1 for now since dont have external data.
                _currentABVersion = 1;
                SetAssetBundleValue(_currentABVersion);
                while (!Caching.ready)
                    yield return null;

#if UNITY_IPHONE
                using(uwr=UnityWebRequestAssetBundle.GetAssetBundle(_assetBundleData[gameIndex]._assetBundleObject.assetBundleURL_ios))
#endif
#if UNITY_ANDROID
                using (uwr = UnityWebRequestAssetBundle.GetAssetBundle(_assetBundleData[0]._assetBundleObject.assetBundleURL_Android[gameIndex]))
#endif

                {
                    loadingStart = true;
                    yield return uwr.SendWebRequest();

                    while (!uwr.isDone)
                    {
                        yield return null;
                    }

                    if (!string.IsNullOrEmpty(uwr.error))
                    {
                        Debug.Log(uwr.error);
                        yield return null;
                    }
                    else
                    {
                        Debug.Log("Download success");

                    }
                    _currentAssetBundle = DownloadHandlerAssetBundle.GetContent(uwr);
                }
                loadingStart = false;
                string[] scenes = _currentAssetBundle.GetAllScenePaths();

                foreach (string s in scenes)
                {
                    print(Path.GetFileNameWithoutExtension(s));
                    if (Path.GetFileNameWithoutExtension(s) == _assetBundleData[0]._assetBundleObject.sceneNames[gameIndex])
                    {
                        LoadScene(Path.GetFileNameWithoutExtension(s));
                    }
                }
            }

            public void LoadScene(string name)
            {
                //BG.SetActive(false);
                //SceneManager.LoadScene(name);
                SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);
               
                Debug.Log("The scene name is--" + name);
                _currentAssetBundle.UnloadAsync(false);
            }

            /*private float GetAssetBundleGameVersion(int index)
            {
                float version = _assetBundleData[index]._assetBundleObject.assetBundleVersion;
                return version;
            }*/

            private int GetABVersionNumber()
            {
                _currentABVersion = PlayerPrefs.GetInt(playerPrefKey);
                return _currentABVersion;
            }

            private void SetAssetBundleValue(int value)
            {
                PlayerPrefs.SetInt(playerPrefKey, value);
            }
        }

        public enum AssetBundleDownloadType
        {
            local,
            server
        }

        public enum AssetBundleWantUI
        {
            yes,
            no
        }
    }
}