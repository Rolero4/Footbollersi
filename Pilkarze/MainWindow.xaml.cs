using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
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
using System.Xml.Serialization;

namespace Pilkarze
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        ObservableCollection<Player> player_list = new ObservableCollection<Player>();


        public MainWindow()
        {
            InitializeComponent();
            List<int> combo_list = new List<int>();

            for (int i=15; i<40; i++)
            {
                combo_list.Add(i);
            }
            combobox.ItemsSource = combo_list;
            combobox.SelectedItem = combo_list[0];

            deserialize();
            listBox.ItemsSource = player_list;
        }





        private void add_btn_Click(object sender, RoutedEventArgs e)
        {
            int age;
            double weight;
            string name, surname;

            age = (int)combobox.SelectedItem;
            weight = weight_slider.Value;
            name = name_box.Text;
            surname = surname_box.Text;

            Player player = new Player(age, weight, name, surname);
            adding_to_listbox(player);
        }

        private void adding_to_listbox(Player player)
        {
            bool error = true;
            for (int i = 0; i < player_list.Count; i++)
            {
                if (player.Age == player_list[i].Age && player.Weight == player_list[i].Weight && player.Name == player_list[i].Name && player.Surname == player_list[i].Surname)
                    error = false;
            }
            if(error)
            {
                player_list.Add(player);
                serialize();
            }
        }

        private void delete_btn_Click(object sender, RoutedEventArgs e)
        {
            player_list.RemoveAt(listBox.Items.IndexOf(listBox.SelectedItem));
            serialize();
            
        }

        private void modify_btn_Click(object sender, RoutedEventArgs e)
        {
            int age;
            double weight;
            string name, surname;

            age = (int)combobox.SelectedItem;
            weight = weight_slider.Value;
            name = name_box.Text;
            surname = surname_box.Text;


            player_list[listBox.Items.IndexOf(listBox.SelectedItem)].Age = age;
            player_list[listBox.Items.IndexOf(listBox.SelectedItem)].Weight = weight;
            player_list[listBox.Items.IndexOf(listBox.SelectedItem)].Name = name;
            player_list[listBox.Items.IndexOf(listBox.SelectedItem)].Surname = surname;
            serialize();
            listBox.Items.Refresh();

        }


        public void serialize()
        {
            Stream stream = File.Create(Environment.CurrentDirectory + "\\listOfPlayers.txt");
            XmlSerializer xmlser = new XmlSerializer(typeof(ObservableCollection<Player>));
            xmlser.Serialize(stream, player_list);
            stream.Close();
        }

        private void deserialize()
        {
            XmlSerializer xmlser = new XmlSerializer(typeof(ObservableCollection<Player>));
            using (FileStream fileStream = new FileStream(Environment.CurrentDirectory + "\\listOfPlayers.txt", FileMode.Open))
            {
                 player_list = (ObservableCollection<Player>)xmlser.Deserialize(fileStream);
            }
        }

    }
}
