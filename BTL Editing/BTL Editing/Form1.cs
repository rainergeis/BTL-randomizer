using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;
using SoulsFormats;
using System;

namespace BTL_Editing
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            var dialog = new FolderBrowserDialog(); 
            //forces user to select a folder

            if (dialog.ShowDialog() != DialogResult.OK) return; 
            //okay if the user selects OK

            foreach (string file in Directory.EnumerateFiles(dialog.SelectedPath, ".", SearchOption.AllDirectories))
            {
                if (Path.GetExtension(file) != ".dcx") continue;
                BTL btl = BTL.Read(file);
                RandomizeBTLLightProperties(btl);
                btl.Write($"{Directory.GetParent(file)}/{Path.GetFileNameWithoutExtension(Path.GetFileName(file))}.dcx", DCX.Type.DCX_KRAK);
            }
        }

        private int GenerateRandomNum(int min, int max) //random number generator between two ints
        {
            Random sugma = new Random();
            int randomNum = sugma.Next(min, max);
            return randomNum;
        }
        private void RandomizeBTLLightProperties(BTL btlFile)
        {
            foreach (BTL.Light light in btlFile.Lights)
            {
                List<int> colorValues = new List<int>();
                for (int i = 0; i < 3; i++)
                {
                    colorValues.Add(GenerateRandomNum(0,255));
                }
                Color newColor = Color.FromArgb((byte)colorValues[0], (byte)colorValues[1], (byte)colorValues[2]);
                light.DiffuseColor = newColor;
                light.SpecularColor = newColor;
                light.SpecularPower = GenerateRandomNum(5, 40);
                light.DiffusePower = GenerateRandomNum(7, 100);
                light.Type = BTL.LightType.Point;
                light.FlickerIntervalMin = (float)0.1;
                light.FlickerIntervalMax = (float)0.1;
                light.FlickerBrightnessMult = GenerateRandomNum(3, 100);
                light.CastShadows = true;
                light.Sharpness = GenerateRandomNum(1, 10);    

            }
        }
    }
}
