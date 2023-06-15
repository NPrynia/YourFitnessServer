using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YourFitnessServer
{
    public class Appdata
    {
        public static Models.YourFitnessEntities4 Context { get; set; } = new Models.YourFitnessEntities4();

        public static void refreshChanges() 
        {
            Context = new Models.YourFitnessEntities4();
        }
    }
}