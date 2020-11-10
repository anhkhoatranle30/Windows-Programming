using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_project1
{
    //definition
    public class FavoriteRecipe : Recipe
    {

    }
    //abstract DAO 
    //DAO Text File
    public class FavoriteRecipeDAOTextFile : RecipeDAO
    {
        public override BindingList<Recipe> GetAll()
        {
            //throw new NotImplementedException();
            var result = new BindingList<Recipe>();
            var fullRecipesList = new RecipeDAOTextFile().GetAll();
            foreach(var recipe in fullRecipesList)
            {
                if(recipe.IsFavorite == true)
                {
                    result.Add(recipe);
                }
            }
            return result;

        }
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
        public override void Add(Recipe recipe)
        {
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
                            // Meet the line that needs to be editted
                            //recipe.IsFavorite = true;
                            //var tempLine = recipe.ToString();
                            //sw.WriteLine(tempLine);
                            var tokens = line.Split(new String[] { "*" }, StringSplitOptions.None);
                            var toAddLine = new StringBuilder();
                            for (int i = 0; i < tokens.Length - 1; i++)
                            {
                                toAddLine.Append(tokens[i]);
                                toAddLine.Append("*");
                            }
                            toAddLine.Append("True");
                            sw.WriteLine(toAddLine);
                        }
                    }
                }
            }
            // Delete original file
            File.Delete(filename);

            // ... and put the temp file in its place.
            File.Move(tempFilename, filename);
        }
        public override void Delete(Recipe recipe)
        {
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
                            // Meet the line that needs to be editted
                            //recipe.IsFavorite = false;
                            //var tempLine = recipe.ToString();
                            //sw.WriteLine(tempLine);
                            var tokens = line.Split(new String[] { "*" }, StringSplitOptions.None);
                            var toAddLine = new StringBuilder();
                            for (int i = 0; i < tokens.Length - 1; i++)
                            {
                                toAddLine.Append(tokens[i]);
                                toAddLine.Append("*");
                            }
                            toAddLine.Append("False");
                            sw.WriteLine(toAddLine);
                        }
                    }
                }
            }
            // Delete original file
            File.Delete(filename);

            // ... and put the temp file in its place.
            File.Move(tempFilename, filename);
        }
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
