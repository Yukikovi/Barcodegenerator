using Aspose.BarCode.Generation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
namespace BarCodeGenerator
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnGenerate_Click(object sender, RoutedEventArgs e)
        {
            var imageType = "Png";

            if (rbPng.IsChecked == true)
            {
                imageType = rbPng.Content.ToString();
            }
            else if (rbBmp.IsChecked == true)
            {
                imageType = rbBmp.Content.ToString();
            }
            else if (rbJpg.IsChecked == true)
            {
                imageType = rbJpg.Content.ToString();
            }

            //Выбраный формат изоображения 
            var imageFormat = (BarCodeImageFormat)Enum.Parse(typeof(BarCodeImageFormat), imageType.ToString());
            //значение по умолчанию
            var encodeType = EncodeTypes.Code128;
            //
            if (!string.IsNullOrEmpty(comboBarcodeType.Text))
            {
                switch (comboBarcodeType.Text)
                {
                    case "Code128":
                        encodeType = EncodeTypes.Code128;
                        break;

                    case "ITF14":
                        encodeType = EncodeTypes.ITF14;
                        break;

                    case "EAN13":
                        encodeType = EncodeTypes.EAN13;
                        break;

                    case "Datamatrix":
                        encodeType = EncodeTypes.DataMatrix;
                        break;

                    case "Code32":
                        encodeType = EncodeTypes.Code32;
                        break;

                    case "Code11":
                        encodeType = EncodeTypes.Code11;
                        break;

                    case "PDF417":
                        encodeType = EncodeTypes.Pdf417;
                        break;

                    case "EAN8":
                        encodeType = EncodeTypes.EAN8;
                        break;

                    case "QR":
                        encodeType = EncodeTypes.QR;
                        break;
                }
            }

            Barcode barcode = new Barcode
            {
                Text = tbCodeText.Text,
                BarcodeType = encodeType,
                ImageType = imageFormat
            };

            try
            {
                string imagePath = "";

                if (cbGenerateWithOptions.IsChecked == true)
                    return;
                else
                    imagePath = GenerateBarcode(barcode);

                Uri fileUri = new Uri(System.IO.Path.GetFullPath(imagePath));
                imgDynamic.Source = new BitmapImage(fileUri);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private string GenerateBarcode(Barcode barcode)
        {
            string imagePath = comboBarcodeType.Text + "." + barcode.ImageType;

            BarcodeGenerator generator = new BarcodeGenerator(barcode.BarcodeType, barcode.Text);

            generator.Save(imagePath, barcode.ImageType);

            return imagePath;
        }
    }
}
