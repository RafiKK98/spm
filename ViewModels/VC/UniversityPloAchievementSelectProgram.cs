using System.Collections.Generic;
using SpmsApp.Models;
using SpmsApp.ViewModels.VC;

namespace SpmsApp.ViewModels.VC
{
    public class UniversityPloAchievementSelectProgram
    {
        public TopbarViewModel TopbarViewModel { get; set; }
        public List<Program> Programs { get; set; }
        public int SelectedProgram { get; set; }

        public VC_Data Data { get; set; }
    }

    public class VC_Data
    {
        public List<string> Labels { get; set; }
        public List<Dataset> Datasets { get; set; }
    }

    public class Dataset
    {
        public string Label { get; set; }
        public List<int> Data { get; set; }
        public string BackgroundColor { get; set; }
        public string BorderColor { get; set; }
        public string PointBackgroundColor { get; set; }
        public string PointBorderColor { get; set; }
        public string PointHoverBackgroundColor { get; set; }
        public string PointHoverBorderColor { get; set; }
    }
}