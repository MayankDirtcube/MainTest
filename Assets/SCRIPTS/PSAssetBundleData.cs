using UnityEngine;
using System;
using System.Collections.Generic;

namespace PlayStarz
{
    namespace AssetBundleLoading
    {
        [CreateAssetMenu(fileName = "AssetBundleData", menuName = "AssetBundleData/New AssetBundleData", order = 1)]
        public class PSAssetBundleData : ScriptableObject
        {
            public AssetBundleObject _assetBundleObject;
        }

        [Serializable]
        public struct AssetBundleObject
        {

            // public float assetBundleVersion;
            public List<string> assetBundleURL_ios;
            public List<string> assetBundleURL_Android;

           // public string assetBundleURL_ios, assetBundleURL_Android;
            public bool shouldTriggerItself;
            public List<string> sceneNames;
            //public string sceneName;
            public GameName _gameName;
        }
        public enum GameName
        {
            TestGame_1,
            TestGame_2,
            NinjaHelix
        }
    }
}
