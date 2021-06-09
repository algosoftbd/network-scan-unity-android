using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IFEN
{

    public class IfenHud : MonoBehaviour
    {
        public void OpenIfenUrl()
        {
            Application.OpenURL("https://www.neurofeedback-info.de/en/");
        }

        public void OpenNeurofeedbackPartnerUrl()
        {
            Application.OpenURL("https://www.neurofeedback-partner.de/");
        }

        public void OpenCapitoUrl()
        {
            Application.OpenURL("https://capito.muenchen-neurofeedback.de/index.php/en/");
        }

        public void OpenFreeCapOrderUrl()
        {
            Application.OpenURL("https://www.neurofeedback-partner.de/product_info.php?language=en&info=p199_free-cap-19-kanal-mit-gesinterten-silber-silber-chlorid-ag-agcl-elektroden.html");
        }


        public void OpenProZProtocolBuyUrl()
        {
            Application.OpenURL("https://www.neurofeedback-partner.de/product_info.php?language=en&info=p321_ifen-pro-z-protocol-for-discovery.html");
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}