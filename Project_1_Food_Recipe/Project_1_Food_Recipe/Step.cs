using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_project1
{
    //interface
    public interface IStepBuilder
    {
        IStepBuilder setContent(string content);
        IStepBuilder addImgSource(string imgsource);
        IStepBuilder setImgSourceList(BindingList<string> imgsourceList);
        Step Build();
    }
    //builder
    public class ConcreteStepBuilder : IStepBuilder
    {
        int RecipeID;
        int StepID;
        BindingList<string> ImgSource;
        string Content;
        public IStepBuilder setRecipeID(int recipeID)
        {
            this.RecipeID = recipeID;
            return this;
        }
        public IStepBuilder setStepID(int stepID)
        {
            this.StepID = stepID;
            return this;
        }
        public IStepBuilder setContent(string content)
        {
            this.Content = content;
            return this;
        }
        public IStepBuilder addImgSource(string imgsource)
        {
            if(ImgSource == null)
            {
                this.ImgSource = new BindingList<string>();
            }
            this.ImgSource.Add(imgsource);
            return this;
        }
        public IStepBuilder setImgSourceList(BindingList<string> imgsourceList)
        {
            this.ImgSource = imgsourceList;
            return this;
        }
        public Step Build()
        {
            return new Step() { RecipeID = this.RecipeID, StepID = this.StepID, ImgSource = this.ImgSource, Content = this.Content };
        }
    }
    //definition
    public class Step
    {
        //properties
        public int RecipeID { get; set; } //PK
        public int StepID { get; set; } //weak key
        public BindingList<string> ImgSource { get; set; }
        public string Content { get; set; }
        //method
        public override string ToString()
        {
            var result = new StringBuilder();
            foreach(var image in this.ImgSource)
            {
                result.Append(this.RecipeID);
                result.Append("*");
                result.Append(this.StepID);
                result.Append("*");
                result.Append("image");
                result.Append("*");
                result.Append(image);
                result.Append("\n");
            }
            //content
            result.Append(this.RecipeID);
            result.Append("*");
            result.Append(this.StepID);
            result.Append("*");
            result.Append("content");
            result.Append("*");
            result.Append(this.Content);
            return result.ToString();
        }
    }
    //abstract DAO
    public abstract class StepDAO
    {
        public abstract BindingList<Step> GetAll(int recipeID);
        public abstract void Add(Step step);
    }
    //DAO Text File
    public class StepDAOTextFile : StepDAO
    {
        public override BindingList<Step> GetAll(int recipeID)
        {
            //result
            var result = new BindingList<Step>();
            //StepBuilder
            var stepBuilder = new ConcreteStepBuilder();


            //
            //Read Database_quantity.txt
            //
            var quantityPath = new StringBuilder();
            quantityPath.Append(AppDomain.CurrentDomain.BaseDirectory);
            quantityPath.Append("Database_quantity.txt");
            var quantityLines = File.ReadAllLines(quantityPath.ToString());
            int noSystemRecipes = int.Parse(quantityLines[0]);
            //
            //Read file txt
            //
            var path = new StringBuilder();
            path.Append(AppDomain.CurrentDomain.BaseDirectory);
            path.Append("Database_step.txt");

            var lines = File.ReadAllLines(path.ToString());
            
            //recipes
            foreach (var line in lines)
            {
                Debug.WriteLine(line);
                

                var tokens = line.Split(new String[] { "*" }, StringSplitOptions.None);
                
                if(recipeID == int.Parse(tokens[0]))
                {
                    var resultStep = new Step();
                    stepBuilder.setRecipeID(recipeID);
                    stepBuilder.setStepID(int.Parse(tokens[1]));
                    if (tokens[2] == "image")
                    {
                        if(recipeID < noSystemRecipes)
                        {
                            stepBuilder.addImgSource(PathString.toAbsolutePath(tokens[3]));
                        }
                        else
                        {
                            stepBuilder.addImgSource(tokens[3]);
                        }
                    }
                    else //"content"
                    {
                        stepBuilder.setContent(tokens[3]);
                        resultStep = stepBuilder.Build();
                        result.Add(resultStep);
                        //reset Builder
                        stepBuilder = new ConcreteStepBuilder();
                    }
                }
            }
            return result;
        }
        public override void Add(Step step)
        {
            var newStepEncoded = step.ToString();
            //db file path
            var path = new StringBuilder();
            path.Append(AppDomain.CurrentDomain.BaseDirectory);
            path.Append("Database_step.txt");
            using (StreamWriter sw = File.AppendText(path.ToString()))
            {
                sw.WriteLine(newStepEncoded);
            }
        }
    }
}
