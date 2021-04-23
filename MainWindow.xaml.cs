using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfAnimatedGif;
using System.Threading;
using System.Windows.Media.Animation;
using System.Windows.Controls.Primitives;
using System.ComponentModel;
using System.Net.Http.Handlers;
using DataGridExtensions;

namespace PokemonApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
      
            this.DataContext = this;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            worker.WorkerReportsProgress = true;
            worker.DoWork += worker_DoWork;
            worker.ProgressChanged += worker_ProgressChanged;



            buttonFactory.SetValue(Control.BackgroundProperty, System.Windows.Media.Brushes.Transparent);
            buttonFactory.SetValue(Control.BorderBrushProperty, System.Windows.Media.Brushes.Black);

            buttonTemplate.VisualTree = buttonFactory;

            buttonFactory.AddHandler(ButtonBase.ClickEvent, new RoutedEventHandler(ShowHideDetails));
            buttonFactory.SetValue(ContentProperty, "Show");
            buttonColumn.CellTemplate = buttonTemplate;
            DataGrid2.Columns.RemoveAt(0);


        }
        //initialize all namespace variables
        public static DataTable newtable = new DataTable();
        private readonly BackgroundWorker worker = new BackgroundWorker();
        public List<PokeItem> pokeList = new List<PokeItem>();
        public int pokeId;
        public string abilities = null;
        public string type2 = null;
        public List<string> abilist = new List<string>();
        public string ability2 = null;
        List<string> typelist = new List<string> { "Bug", "Dragon", "Electric", "Fighting", "Fire", "Flying", "Ghost", "Grass", "Ground", "Ice", "Normal", "Poison", "Psychic", "Rock", "Water" };
        public string ID{ get { return (string)GetValue(ActiveProperty); } set { SetValue(ActiveProperty, value); } }
        public static readonly DependencyProperty ActiveProperty = DependencyProperty.Register("ID", typeof(string), typeof(MainWindow));
        private DataGridTemplateColumn buttonColumn = new DataGridTemplateColumn();
        DataTemplate buttonTemplate = new DataTemplate();
        FrameworkElementFactory buttonFactory = new FrameworkElementFactory(typeof(Button));


        //all xmal component methods
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadButton.Visibility = Visibility.Hidden;

            DataGrid2.ItemsSource = null;
            GetPokemon();
            progressBar1.Visibility = Visibility.Visible;
            progressborder.Visibility = Visibility.Visible;
            FileNameLabel.Visibility = Visibility.Visible;
            pika.Visibility = Visibility.Visible;
            DataGrid2.Visibility = Visibility.Hidden;
            newtable.Columns.Add("ID");
            newtable.Columns.Add("Name");
            newtable.Columns.Add("Type");

            newtable.Columns.Add("Abilities");


            int x = Int32.Parse(text1.Text);

            GetAllPokemon(x);
            text1.Visibility = Visibility.Hidden;
            LoadButton.Visibility = Visibility.Hidden;

        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        public void ShowHideDetails(object sender, RoutedEventArgs e)
        {
            try
            {
                //gets the id and name from the row of which the button was pressed
                DataRowView dataRowView = (DataRowView)((Button)e.Source).DataContext;
                String id = dataRowView[0].ToString();
                String name = dataRowView[1].ToString();
                for (int i = 0; i < abilist.Count; i++)
                {
                    if (abilist[i].Contains(" ID: " + id + "\n Base Experience:"))
                    {

                        MessageBox.Show(abilist[i], name, MessageBoxButton.OK, MessageBoxImage.Information);
                    }

                }

            }
            catch (Exception a)
            {
                MessageBox.Show("No description given", "Sorry", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void text1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            text1.Text = " ";
            text1.FontSize = 25;
            text1.FontWeight = FontWeights.ExtraBlack;
        }

        private void CliClear(object sender, RoutedEventArgs args)
        {

            try
            {
                newtable.Columns.Clear();
                newtable.Rows.Clear();
                DataGrid2.ItemsSource = null;
                DataGrid2.Items.Clear();
                DataGrid2.Items.Refresh();
                DataGrid2.Visibility = Visibility.Hidden;
                text1.Visibility = Visibility.Visible;
                LoadButton.Visibility = Visibility.Visible;
                progressBar1.Value = 0;
                DataGrid2.Columns.Remove(buttonColumn);
            }
            catch (Exception e)
            {
                return;
            }
        }

        //worker thread methods
        private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }
        private void worker_DoWork(object sender, DoWorkEventArgs a)
        {


            this.Dispatcher.Invoke(() =>
             {
                 progressBar1.Visibility = Visibility.Visible;
                 progressborder.Visibility = Visibility.Visible;
                 FileNameLabel.Visibility = Visibility.Visible;
                 pika.Visibility = Visibility.Visible;
                 DataGrid2.Visibility = Visibility.Hidden;

             });
            //only way to establish these without getting a thread exception

            for (int pokeId = 1; pokeId < 151; pokeId++)
            {
                Thread.Sleep(1000);

            }


        }
       
        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //once the worker is complete, the loading bar fills
          
        }
   

        //api call methods
        public static async void GetPokemon()
        {
            //Define your baseUrl
            string baseUrl = "http://pokeapi.co/api/v2/pokemon/";
            //Have your using statements within a try/catch block
            try
            {
                //We will now define your HttpClient with your first using statement which will implements a IDisposable interface.
                using (HttpClient client = new HttpClient())
                {
                    //In the next using statement you will initiate the Get Request, use the await keyword so it will execute the using statement in order.
                    using (HttpResponseMessage res = await client.GetAsync(baseUrl))
                    {
                        //Then get the content from the response in the next using statement, then within it you will get the data, and convert it to a c# object.
                        using (HttpContent content = res.Content)
                        {
                            //Now assign your content to your data variable, by converting into a string using the await keyword.
                            var data = await content.ReadAsStringAsync();
                            //If the data isn't null return log convert the data using newtonsoft JObject Parse class method on the data.
                          
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("Exception Hit------------" + exception);
                // Console.WriteLine(exception);
            }
        }
        //Define your static method which will make the method become part of the class
        //Also make it asynchronous meaning it is retrieving data from a api.
        //Have it void since your are logging the result into the console.
        //Which would take a integar as a argument.
        public async void GetAllPokemon(int total)
        {
            total = total + 1;
            int progress = 0;
            type2 = null;
            ability2 = null;
   
            for (int pokeId = 1; pokeId < total; pokeId++)
            {
                //Define your base url
                string baseURL = $"http://pokeapi.co/api/v2/pokemon/{pokeId}/";
              
                //Have your api call in try/catch block.
                try
                {
                    //Now we will have our using directives which would have a HttpClient 
                    using (HttpClient client = new HttpClient())
                    {
                        //Now get your response from the client from get request to baseurl.
                        //Use the await keyword since the get request is asynchronous, and want it run before next asychronous operation.
                        using (HttpResponseMessage res = await client.GetAsync(baseURL))
                        {
                            //Now we will retrieve content from our response, which would be HttpContent, retrieve from the response Content property.
                            using (HttpContent content = res.Content)
                            {
                                //Retrieve the data from the content of the response, have the await keyword since it is asynchronous.
                                string data = await content.ReadAsStringAsync();
                                //If the data is not null, parse the data to a C# object, then create a new instance of PokeItem.
                                if (data != null)
                                {
                                    // MessageBox.Show("NO Data");
                                    //Parse your data into a object.
                                    var dataObj = JObject.Parse(data);
                                    //Then create a new instance of PokeItem, and string interpolate your name property to your JSON object
                                    //Which will convert it to a string, since each property value is a instance of JToken.
                                    PokeItem pokeItem = new PokeItem(name: $"{dataObj["name"]}", type1: $"{dataObj["types"][0]["type"]["name"]}", id: $"{dataObj["id"]}", abilities: $"{dataObj["abilities"][0]["ability"]["name"]}", baseex: $"{dataObj["base_experience"]}", weight: $"{dataObj["weight"]}", height: $"{dataObj["height"]}");

                                    Task.Run(async () => { await CheckType2(pokeId); }).Wait();
                                    Task.Run(async () => { await CheckAbility2(pokeId); }).Wait();
                               
                                    pokeItem.Abilities = Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(pokeItem.Abilities);
                                    abilities = pokeItem.Abilities;
                                    if (type2 != null)
                                    {
                                        pokeItem.Type1 = pokeItem.Type1 + " & " + type2;

                                    }
                                    if (ability2 != null)
                                    {
                                        abilities = pokeItem.Abilities + " & " + ability2;

                                    }
                                    progress = (pokeId * 100) / total;
                                    progressBar1.Value = progress;
                                    abilist.Add(" ID: " + pokeItem.Id + "\n" + " Base Experience: " + pokeItem.BaseEx + "\n " + "Weight: " + pokeItem.Weight + " \n" + " Height: " + pokeItem.Height);
                                    //  setImageSource(pokeId);
                                    populateDatagrid(pokeItem.Name, pokeItem.Type1, pokeItem.Id, abilities);

                                }
                                else
                                {
                                    //If data is null log it into console.
                                    MessageBox.Show("Data is null!");
                                }
                            }
                        }

                    }
                }

                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                }

            }
            progressBar1.Visibility = Visibility.Hidden;
            progressborder.Visibility = Visibility.Hidden;
            FileNameLabel.Visibility = Visibility.Hidden;
            pika.Visibility = Visibility.Hidden;

            buttonColumn.Header = "Misc Info";
            buttonColumn.Width = 80;
            buttonColumn.Visibility = Visibility.Visible;

            DataGrid2.Columns.Add(buttonColumn);

            DataGrid2.Visibility = Visibility.Visible;
            DataGrid2.Columns[0].SetIsFilterVisible(false);

        }


        public async Task<string> CheckType2(int pokeId)
        {

            //Define your base url
            string baseURL = $"http://pokeapi.co/api/v2/pokemon/{pokeId}/";

            //Have your api call in try/catch block.
            try
            {
                //Now we will have our using directives which would have a HttpClient 
                using (HttpClient client = new HttpClient())
                {
                    //Now get your response from the client from get request to baseurl.
                    //Use the await keyword since the get request is asynchronous, and want it run before next asychronous operation.
                    using (HttpResponseMessage res = await client.GetAsync(baseURL))
                    {
                        //Now we will retrieve content from our response, which would be HttpContent, retrieve from the response Content property.
                        using (HttpContent content = res.Content)
                        {
                            // Task t
                            //Retrieve the data from the content of the response, have the await keyword since it is asynchronous.
                            string data = await content.ReadAsStringAsync();
                            //If the data is not null, parse the data to a C# object, then create a new instance of PokeItem.
                            if (data != null)
                            {
                              
                                //Parse your data into a object.
                                var dataObj = JObject.Parse(data);
                                //Then create a new instance of PokeItem, and string interpolate your name property to your JSON object.

                                type2 = $"{dataObj["types"][1]["type"]["name"]}";
                                type2 = Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(type2);
                                if (!typelist.Contains(type2))
                                {
                                    type2 = null;
                                }
                                return type2;

                            }
                            else
                            {
                                //If data is null log it into console.
                                MessageBox.Show("Data is null!");
                                return type2;
                            }
                        }
                    }

                }
            }

            catch (Exception exception)
            {
                Console.WriteLine(exception);

            }

            return type2;

        }
        public async Task<string> CheckAbility2(int pokeId)
        {

            //Define your base url
            string baseURL = $"http://pokeapi.co/api/v2/pokemon/{pokeId}/";

            //Have your api call in try/catch block.
            try
            {
                //Now we will have our using directives which would have a HttpClient 
                using (HttpClient client = new HttpClient())
                {
                    //Now get your response from the client from get request to baseurl.
                    //Use the await keyword since the get request is asynchronous, and want it run before next asychronous operation.
                    using (HttpResponseMessage res = await client.GetAsync(baseURL))
                    {
                        //Now we will retrieve content from our response, which would be HttpContent, retrieve from the response Content property.
                        using (HttpContent content = res.Content)
                        {
                        
                            //Retrieve the data from the content of the response, have the await keyword since it is asynchronous.
                            string data = await content.ReadAsStringAsync();
                            //If the data is not null, parse the data to a C# object, then create a new instance of PokeItem.
                            if (data != null)
                            {
                             
                                //Parse your data into a object.
                                var dataObj = JObject.Parse(data);
                                //Then create a new instance of PokeItem, and string interpolate your name property to your JSON object.

                                ability2 = $"{dataObj["abilities"][1]["ability"]["name"]}";
                                ability2 = Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(ability2);

                                return ability2;

                            }
                            else
                            {
                                //If data is null log it into console.
                                MessageBox.Show("Data is null!");
                                return ability2;
                            }
                        }
                    }

                }
            }

            catch (Exception exception)
            {
                Console.WriteLine(exception);

            }

            return type2;

        }


    

        public void populateDatagrid(String name, string type, string id, string abilities)
        {
       
            ID = id;
            List<string> list = new List<string>();
        
            name = Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(name);
            type = Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(type);
            list.Add(id);
            list.Add(name);
            list.Add(type);
            list.Add(abilities);
            String[] str = list.ToArray();
            DataRow dr = newtable.NewRow();

            for (int i = 0; i < 4; i++)
            {

                dr[i] = str[i];

            }


            newtable.Rows.Add(dr);
            DataGrid2.ItemsSource = new ListCollectionView(newtable.DefaultView);
      

        }

       
    }
}