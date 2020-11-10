using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_project1
{
    public class UncompressedStep : Step
    {
        public string StepNumber { get; set; }
        //Methods

        public static BindingList<UncompressedStep> ToUncrompressStepList(BindingList<Step> stepsList)
        {
            var result = new BindingList<UncompressedStep>();
            for (int i = 0; i < stepsList.Count; i++)
            {
                var stepnumber = new StringBuilder();
                stepnumber.Append("Bước " + (i + 1).ToString());
                result.Add(new UncompressedStep() { Content = stepsList[i].Content, ImgSource = stepsList[i].ImgSource, StepNumber = stepnumber.ToString() });
            }
            return result;
        }
    }
}
