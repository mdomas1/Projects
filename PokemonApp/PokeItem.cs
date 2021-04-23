using System;
using System.Collections.Generic;
using System.Text;

//Define your PokeItem model which will have a Name, and a Url.
namespace PokemonApp
{
    //Make your class public, since by default it is internal.
    public class PokeItem
    {
        //Define the constructor of your PokeItem which is the same name as class, and is not returning anything.
        //Will take a string name
        public PokeItem(string name, string type1,  string id, string abilities, string baseex, string weight, string height)
        {
            Name = name;
            Type1 = type1;
          //  Type2 = type2;
            Id = id;
            Abilities = abilities;
           
            BaseEx = baseex;
            Weight = weight;
            Height = height;
          
            
        //    DataGrid1.Items.Add(new PokeItem() { Name = name, Type1 = "2012, 8, 15", Finich = "2012, 9, 15" });
        }
        //Your Properties are auto-implemented.
        public string Name { get; set; }
       
        public string Type1 { get; set; }
        public string Type2 { get;  set; }
        public string Id { get; set; }
        public string Abilities { get;  set; }
       
        public string BaseEx { get; set; }
        public string Weight { get; set; }
        public string Height { get; set; }
       
    }
}