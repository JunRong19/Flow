using UnityEngine;
using TMPro;

namespace Michsky.UI.ModernUIPack
{
    public class HamburgerMenuManager : MonoBehaviour
    {
        private Animator menuAnimator;

        [Header("RESOURCES")]
        public Animator animatedButton;
        public TextMeshProUGUI title;
        public GameObject shadow;

        [Header("SETTINGS")]
        public bool openAtStart = false;
        public bool hasText = false;
        public bool shouldButtonAnimate = false;
        public string titleAtStart;

        bool isOpen;

        void Start()
        {
            menuAnimator = gameObject.GetComponent<Animator>();

            if(hasText) {
                title.text = titleAtStart;
            }

            if(openAtStart == true)
            {
                menuAnimator.Play("Expand");

                if(shouldButtonAnimate) {
                    animatedButton.Play("HTE Expand");
                }

                shadow.SetActive(true);

                isOpen = true;
            }

            else
            {
                menuAnimator.Play("Minimize");

                if(shouldButtonAnimate) {
                    animatedButton.Play("HTE Hamburger");
                }

                shadow.SetActive(false);

                isOpen = false;
            }
        }

        public void Animate()
        {
            if (isOpen == true)
            {
                menuAnimator.Play("Minimize");
                if(shouldButtonAnimate) {
                    animatedButton.Play("HTE Hamburger");
                }

                shadow.SetActive(false);

                isOpen = false;
            }

            else
            {          
                menuAnimator.Play("Expand");
                if(shouldButtonAnimate) {
                    animatedButton.Play("HTE Exit");
                }

                shadow.SetActive(true);

                isOpen = true;
            }
        }

        public void ChangeText(string titleText)
        {
            if(hasText) {
                title.text = titleText;
            }
        }
    }
}