using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExternalLinks : MonoBehaviour
{
    public void VisitFlowPrivacyPolicy() {
        Application.OpenURL("https://www.freeprivacypolicy.com/privacy/view/a74170b2b38de6a4e0993da2e2022d2c");
    }

    public void VisitUnityPrivacyPolicy() {
        Application.OpenURL("https://unity3d.com/legal/privacy-policy");
    }
}
