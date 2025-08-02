using ImTipsyDude.InstantECS;
using UnityEngine;
using UnityEngine.UI;

namespace ImTipsyDude.BolaBoom
{
    public class EnMonstoGuage : IECSEntity
    {
        public Slider Slider;
        public Image FillImage;
        public Gradient Gradient;
    }
}