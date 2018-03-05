using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Nikola.Munchy.MunchyAPI;
using System.IO;
using System.Diagnostics;

namespace Nikola.Munchy.MunchyAPI
{
    public class ShoppingList
    {
        public List<string> USFoodsToBuy = new List<string>();
        public List<string> BGFoodsToBuy = new List<string>();

        static string m_LocalAppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        static string m_ProgramFolder = System.IO.Path.Combine(m_LocalAppDataPath, "Munchy");
        string m_ShoppingListFile = System.IO.Path.Combine(m_ProgramFolder, "shoppinglist.txt");


        public void AddToShoppingList(string USFoodToAdd, string BGFoodToAdd)
        {
            USFoodsToBuy.Add(USFoodToAdd);
            BGFoodsToBuy.Add(BGFoodToAdd);
        }

        public void RemoveFromShoppingList(int positionToRemoveAt)
        {
            USFoodsToBuy.RemoveAt(positionToRemoveAt);
            BGFoodsToBuy.RemoveAt(positionToRemoveAt);
        }

        public void ClearList()
        {
            USFoodsToBuy.Clear();
            BGFoodsToBuy.Clear();
        }

        public void PrintShoppingList(string lang)
        {
            if (lang == "US")
            {
                string contentsUS = string.Join(",", USFoodsToBuy);
                File.WriteAllText(m_ShoppingListFile, contentsUS);
            }
            else
            {
                string contentsBG = string.Join(",", BGFoodsToBuy);
                File.WriteAllText(m_ShoppingListFile, contentsBG);
            }

            ProcessStartInfo info = new ProcessStartInfo();
            info.Verb = "print";
            info.FileName = m_ShoppingListFile;
            info.CreateNoWindow = true;
            info.WindowStyle = ProcessWindowStyle.Normal;

            Process p = new Process();
            p.StartInfo = info;
            p.Start();

            p.WaitForInputIdle();
            System.Threading.Thread.Sleep(3000);

            File.Delete(m_ShoppingListFile);
            
        }
    }
}

