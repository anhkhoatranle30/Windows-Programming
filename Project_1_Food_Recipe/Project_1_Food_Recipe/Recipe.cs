using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_project1
{
    //interface
    public interface IRecipeBuilder
    {
        IRecipeBuilder setRecipeID(int recipeID);
        IRecipeBuilder setTitle(string title);
        IRecipeBuilder setDesPicture(string desPicture);
        IRecipeBuilder setDescription(string description);
        IRecipeBuilder setVideoLink(string videoLink);
        IRecipeBuilder setStepsList(BindingList<Step> stepList);
        IRecipeBuilder setIsFavorite(bool isFavorite);
        Recipe Build();
    }
    //builder
    public class ConcreteRecipeBuilder : IRecipeBuilder
    {

        int RecipeID;
        string Title;
        string DesPicture;
        string Description;
        string VideoLink;
        BindingList<Step> StepsList;
        bool IsFavorite;
        public IRecipeBuilder setRecipeID(int recipeID)
        {
            this.RecipeID = recipeID;
            return this;
        }
        public IRecipeBuilder setTitle(string title)
        {
            this.Title = title;
            return this;
        }
        public IRecipeBuilder setDesPicture(string desPicture)
        {
            this.DesPicture = desPicture;
            return this;
        }
        public IRecipeBuilder setDescription(string description)
        {
            this.Description = description;
            return this;
        }
        public IRecipeBuilder setVideoLink(string videoLink)
        {
            this.VideoLink = videoLink;
            return this;
        }
        public IRecipeBuilder setStepsList(BindingList<Step> stepsList)
        {
            this.StepsList = stepsList;
            return this;
        }
        public IRecipeBuilder setIsFavorite(bool isFavorite)
        {
            this.IsFavorite = isFavorite;
            return this;
        }
        public Recipe Build()
        {
            return new Recipe() { Title = this.Title, Description = this.Description, DesPicture = this.DesPicture, IsFavorite = this.IsFavorite, RecipeID = this.RecipeID, StepsList = this.StepsList, VideoLink = this.VideoLink };
        }
        public Recipe BuildFromString(string line)
        {
            var result = new Recipe();
            var quantity = new RecipesQuantityDAOTextFile().GetAll();

            var tokens = line.Split(new string[] { "*" }, StringSplitOptions.None);
            if (tokens.Length > 0)
            {
                var recipeBuilder = new ConcreteRecipeBuilder();
                int recipeID = int.Parse(tokens[0]);
                recipeBuilder.setRecipeID(recipeID);
                recipeBuilder.setTitle(tokens[1]);

                if (recipeID < quantity.Default)
                {
                    tokens[2] = PathString.toAbsolutePath(tokens[2]);
                }
                recipeBuilder.setDesPicture(tokens[2]);
                recipeBuilder.setDescription(tokens[3]);
                //Video link
                tokens[4] = PathString.TransformToEmbedYoutubeLink(tokens[4]);
                recipeBuilder.setVideoLink(tokens[4]);
                recipeBuilder.setIsFavorite(bool.Parse(tokens[5]));
                //StepsList
                var steplist = new StepDAOTextFile().GetAll(recipeID);
                recipeBuilder.setStepsList(steplist);
                //Build to recipe
                //Recipe
                result = recipeBuilder.Build();
            }

            return result;
        }
    }
    //definition 
    public class Recipe
    {
        //Properties
        public int RecipeID { get; set; }

        public string Title { get; set; }
        public string DesPicture { get; set; } //absolute path
        public string Description { get; set; }
        public string VideoLink { get; set; }
        public BindingList<Step> StepsList { set; get; }
        public bool IsFavorite { get; set; }

        //Method
        public override string ToString()
        {
            var result = new StringBuilder();
            result.Append(this.RecipeID.ToString());
            result.Append('*');
            result.Append(this.Title);
            result.Append('*');
            result.Append(this.DesPicture);
            result.Append('*');
            result.Append(this.Description);
            result.Append('*');
            result.Append(this.VideoLink);
            result.Append('*');
            result.Append(this.IsFavorite.ToString());
            //result.Append('\n');
            return result.ToString();
        }

        
    }
    //abstract DAO
    public abstract class RecipeDAO
    {
        public abstract BindingList<Recipe> GetAll();

        public abstract BindingList<Recipe> GetAll(int productsPerPage, ref int pageNumber, ref int noPages);

        public abstract void Add(Recipe recipe);

        public abstract void Delete(Recipe recipe);

        public abstract BindingList<Recipe> Search(String searchedString);
    }
    //DAO Text File
    public class RecipeDAOTextFile : RecipeDAO
    {
        /// <summary>
        /// Hàm lấy tất cả dữ liệu
        /// </summary>
        /// <returns>Trả về BindingList các món ăn</returns>
        public override BindingList<Recipe> GetAll()
        {
            var recipesQuantityDAOTextFile = new RecipesQuantityDAOTextFile();
            var quantity = recipesQuantityDAOTextFile.GetAll();
            //
            //Initialize list
            //
            var result = new BindingList<Recipe>();
            var fullList = new BindingList<Recipe>();

            //
            //Read file txt
            //
            var path = new StringBuilder();
            path.Append(AppDomain.CurrentDomain.BaseDirectory);
            path.Append("Database.txt");

            var lines = File.ReadAllLines(path.ToString());
            //recipes
            foreach (var line in lines)
            {
                var tokens = line.Split(new string[] { "*" }, StringSplitOptions.None);
                if (tokens.Length > 0)
                {
                    var recipeBuilder = new ConcreteRecipeBuilder();
                    int recipeID = int.Parse(tokens[0]);
                    recipeBuilder.setRecipeID(recipeID);
                    recipeBuilder.setTitle(tokens[1]);

                    if (recipeID < quantity.Default)
                    {
                        tokens[2] = PathString.toAbsolutePath(tokens[2]);
                    }
                    recipeBuilder.setDesPicture(tokens[2]);
                    recipeBuilder.setDescription(tokens[3]);
                    //Video link
                    tokens[4] = PathString.TransformToEmbedYoutubeLink(tokens[4]);
                    recipeBuilder.setVideoLink(tokens[4]);
                    recipeBuilder.setIsFavorite(bool.Parse(tokens[5]));
                    //StepsList
                    var steplist = new StepDAOTextFile().GetAll(recipeID);
                    recipeBuilder.setStepsList(steplist);
                    //Build to recipe
                    //Recipe
                    var recipe = recipeBuilder.Build();
                    //Add to list
                    fullList.Add(recipe);
                }
            }
            //add favorite to head of the list
            foreach (var recipe in fullList)
            {
                if (recipe.IsFavorite == true)
                {
                    result.Add(recipe);
                }
            }
            //add unfavorite to tail of the list
            foreach (var recipe in fullList)
            {
                if (recipe.IsFavorite == false)
                {
                    result.Add(recipe);
                }
            }

            //return
            return result;
        }

        /// <summary>
        /// Hàm hiển thị số trang (normal type)
        /// </summary>
        /// <param name="productsPerPage"></param>
        /// <param name="page"></param>
        /// <param name="noPages"></param>
        /// <returns></returns>
        public override BindingList<Recipe> GetAll(int productsPerPage, ref int pageNumber, ref int noPages)
        {
            var result = new BindingList<Recipe>();
            var fullList = new BindingList<Recipe>();
            fullList = GetAll();
            var total = fullList.Count();
            noPages = (total / productsPerPage) + ((total % productsPerPage == 0) ? 0 : 1);
            //page
            pageNumber = pageNumber % noPages + ((pageNumber % noPages == 0) ? noPages : 0);
            //end page
            for (int i = productsPerPage * (pageNumber - 1); i < productsPerPage * pageNumber && i < total; i++)
            {
                result.Add(fullList[i]);
            }
            return result;
        }

        /// <summary>
        /// Hàm thêm công thức nấu ăn vào database
        /// </summary>
        /// <param name="recipe"></param>
        public override void Add(Recipe recipe)
        {
            //throw new NotImplementedException();
            //db file path
            var path = new StringBuilder();
            path.Append(AppDomain.CurrentDomain.BaseDirectory);
            path.Append("Database.txt");
            //quantity file path
            var quantityFilePath = new StringBuilder();
            quantityFilePath.Append(AppDomain.CurrentDomain.BaseDirectory);
            quantityFilePath.Append("Database_quantity.txt");
            //end file path

            //
            //Get quantity
            //
            var quantity = new RecipesQuantityDAOTextFile().GetAll();

            //Check if new dish has ID
            if (recipe.RecipeID != quantity.Total)
            {
                recipe.RecipeID = quantity.Total++;
                foreach(var step in recipe.StepsList)
                {
                    step.RecipeID = recipe.RecipeID;
                }
            }
            //Write recipe to Database.txt
            var newRecipeEncoded = recipe.ToString();
            using (StreamWriter sw = File.AppendText(path.ToString()))
            {
                sw.WriteLine(newRecipeEncoded);
            }
            //Write Steps to Database_step.txt
            foreach(var step in recipe.StepsList)
            {
                new StepDAOTextFile().Add(step);
            }
            //Write to Database_quantity.txt
            using (StreamWriter sw = File.CreateText(quantityFilePath.ToString()))
            {
                sw.WriteLine(quantity.Default);
                sw.Write(quantity.Total);
            }
        }

        /// <summary>
        /// Hàm xóa một món ăn ra khỏi db
        /// </summary>
        /// <param name="recipe"></param>
        public override void Delete(Recipe recipe)
        {
            //throw new NotImplementedException();
            var lineToCompare = recipe.RecipeID.ToString();
            lineToCompare += "*";
            lineToCompare += recipe.Title;
            //file name
            var path = new StringBuilder();
            path.Append(AppDomain.CurrentDomain.BaseDirectory);
            path.Append("Database.txt");
            var filename = path.ToString();

            //Anonymous path
            var path2 = new StringBuilder();
            path2.Append(AppDomain.CurrentDomain.BaseDirectory);
            path2.Append("Database2.txt");
            var tempFilename = path2.ToString();
            // Initial values

            int lineNumber = 0;
            //int linesRemoved = 0;

            //
            // Read file
            //
            using (var sr = new StreamReader(filename))
            {
                //
                // Write new file
                //
                using (var sw = new StreamWriter(tempFilename))
                {
                    //
                    // Read lines
                    //
                    String line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        lineNumber++;
                        // if its not the line we re looking for
                        if (!line.Contains(lineToCompare))
                        {
                            sw.WriteLine(line);
                        }
                        else
                        {
                            // Meet the line that needs to be deleted
                            //Just ignore it and do nothing
                        }
                    }
                }
            }
            // Delete original file
            File.Delete(filename);

            // ... and put the temp file in its place.
            File.Move(tempFilename, filename);
        }

        /// <summary>
        /// Hàm tìm kiếm món ăn
        /// </summary>
        /// <param name="searchString">chuỗi cần tìm</param>
        /// <returns></returns>
        public override BindingList<Recipe> Search(string searchedString)
        {
            var result = new BindingList<Recipe>();

            var recipes = GetAll();
            var maxPriority = SearchString.CalcMaxPriority(searchedString);
            for (var i = maxPriority; i > 0; i--)
            {
                foreach (var recipe in recipes)
                {
                    var toSearchString = recipe.Title;
                    var priority = SearchString.CalcPriority(searchedString, toSearchString);
                    if (priority == i)
                    {
                        result.Add(recipe);
                    }
                }
            }

            return result;
        }
    }
}
