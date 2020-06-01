using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

using System.IO;

using AForge.Imaging.Filters;

namespace prot1
{
    class Gesture
    {
        private static List<Bitmap> gestures = new List<Bitmap>();
        private static List<string> gestures_name = new List<string>();

        public List<string> Gestures_name
        {
            get { return gestures_name; }
        }

        public List<Bitmap> Gestures
        {
            get { return gestures; }
        }

        public Gesture(string path, float similarity_limit)
        {
            Data.Path = path;
            Data.Similarity_limit = similarity_limit;

            Update_Base();
        }

        public Gesture()
        {
            Update_Base();
        }

        public static void Update_Base()
        {
            Grayscale FilterGrayscale = new Grayscale(Data.Red, Data.Green, Data.Blue);

            string[] files = Directory.GetFiles(Data.Path, "hand*.jpg", SearchOption.TopDirectoryOnly);

            gestures.Clear();
            gestures_name.Clear();

            Image_Comparison cmp = new Image_Comparison();

            foreach (string file in files)
            {
                Bitmap bmp = new Bitmap(file);
                gestures.Add(FilterGrayscale.Apply(bmp)); //??
                gestures_name.Add(Path.GetFileNameWithoutExtension(file));
            }
        }

        public string Search(Bitmap img)
        {
            float max_similarity = 0;
            float similarity;
            int i = 0;

            string name_of_gesture = null;

            Image_Comparison cmp = new Image_Comparison();

            foreach (Bitmap img2 in gestures)
            {
                similarity = cmp.Overlay_Comparison(img, img2);

                if (similarity > max_similarity)
                {
                    max_similarity = similarity;
                    name_of_gesture = gestures_name[i];
                }

                i++;
            }

            return name_of_gesture;
        }

        public void Create(Bitmap img, string name)
        {
            Bitmap bmp = (Bitmap)img.Clone();

            Grayscale FilterGrayscale = new Grayscale(Data.Red, Data.Green, Data.Blue);
            SobelEdgeDetector FilterSobelEdgeDetector = new SobelEdgeDetector();

            bmp = FilterGrayscale.Apply(bmp);
            bmp = FilterSobelEdgeDetector.Apply(bmp);

            bmp.Save(Data.Path + "/" + name + ".jpg", ImageFormat.Jpeg);

            Gesture.Update_Base();
        }
    }
}
