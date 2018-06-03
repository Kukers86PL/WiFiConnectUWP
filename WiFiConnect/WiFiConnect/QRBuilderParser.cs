using System;
using Windows.UI.Xaml.Media.Imaging;
using ZXing;
using ZXing.Mobile;
using ZXing.QrCode;

public class QRBuilderParser
{
    public static String INIT_STRING = "*WiFiConnect*0*";

    static public WriteableBitmap EncodeAsBitmap(String str)
    {
        var options = new QrCodeEncodingOptions()
        {
            DisableECI = true,
            CharacterSet = "UTF-8",
            Width = 800,
            Height = 800
        };

        BarcodeWriter writer = new BarcodeWriter();
        writer.Format = BarcodeFormat.QR_CODE;
        writer.Options = options;
        return writer.Write(str);
    }

    static public String BuildQR(String SSID, String pass)
    {
        return INIT_STRING + SSID + "*" + pass + "*";
    }

    static private String ParseSSID(String QR)
    {
        if (QR.Length >= INIT_STRING.Length)
        {
            String init = QR.Substring(0, INIT_STRING.Length);
            if (init.Equals(INIT_STRING))
            {
                if (QR.Length > INIT_STRING.Length)
                {
                    int index = QR.IndexOf("*", INIT_STRING.Length);
                    if (index != -1)
                    {
                        return QR.Substring(INIT_STRING.Length, index);
                    }
                }
            }
        }

        return "";
    }

    static private String ParsePass(String QR)
    {
        if (QR.Length >= INIT_STRING.Length)
        {
            String init = QR.Substring(0, INIT_STRING.Length);
            if (init.Equals(INIT_STRING))
            {
                if (QR.Length > INIT_STRING.Length)
                {
                    int index1 = QR.IndexOf("*", INIT_STRING.Length);
                    if ((index1 != -1) && (QR.Length - index1) > 0)
                    {
                        int index2 = QR.IndexOf("*", index1 + 1);
                        if (index2 != -1)
                        {
                            return QR.Substring(index1 + 1, index2);
                        }
                    }
                }
            }
        }

        return "";
    }

}