using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoloSharpTest
{
    public class ModelPath
    {
        public string ConfigPath { get; private set; }
        public string WeightsPath { get; private set; }
        public string NamesPath { get; private set; }
        public float FixedAspectRatio { get; private set; } = -1f;
        public bool Found { get; private set; } = true;
        public ModelPath(string modelPath)
        {
            GetModelFiles(modelPath);
        }
        private void GetModelFiles(string modelPath)
        {
            try
            {
                ConfigPath = Directory.GetFiles(modelPath, "*.cfg")[0];
                WeightsPath = Directory.GetFiles(modelPath, "*.weights")[0];
                NamesPath = Directory.GetFiles(modelPath, "*.names")[0];

                string aspect = Path.Combine(modelPath,"fixed.aspect");
                float result;
                if (File.Exists(aspect)){
                    string data = File.ReadAllText(aspect).Trim();
                    if(float.TryParse(data, out result))
                    {
                        FixedAspectRatio = result;
                    }
                }
            }
            catch
            {
                Found = false;
            }
        }
    }
}
