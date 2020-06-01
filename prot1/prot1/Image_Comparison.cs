using System.Drawing;

using AForge.Imaging;
using AForge.Imaging.Filters;

namespace prot1
{
    class Image_Comparison
    {
        public Image_Comparison(float limit)
        {
            Data.Similarity_limit = limit;
        }

        public Image_Comparison() { }

        public float Overlay_Comparison(Bitmap main_img, Bitmap sample_img)
        {
            ExhaustiveTemplateMatching tm = new ExhaustiveTemplateMatching(Data.Similarity_limit);
            TemplateMatch[] matchings;

            Bitmap overlay = (Bitmap)main_img.Clone();

            Add AddFilter = new Add(overlay);
            overlay = AddFilter.Apply(sample_img);

            matchings = tm.ProcessImage(main_img, overlay);
            if (matchings.Length > 0) return matchings[0].Similarity;

            return 0;
        }
    }
}
