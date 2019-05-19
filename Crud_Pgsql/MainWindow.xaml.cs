using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TutoDeveloppez;

namespace Crud_Pgsql
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MyPersons per = null;
        public MainWindow()
        {
            InitializeComponent();
            Refresh();
        }
        private void Refresh()
        {
            per = new MyPersons();
            DataTable MyData = new DataTable();
            MyData = per.SelectAllPerson();
            TablePersonne.Items.Clear();

            foreach (DataRow row in MyData.Rows)
            {
                var data = new ClTablePersonne { id = row[0].ToString(), nom = row[1].ToString(), prenom = row[2].ToString() };
                TablePersonne.Items.Add(data);
            }
        
        }
        private void ClicRefreshPerson(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ClicAddPerson(object sender, RoutedEventArgs e)
        {
            per = new MyPersons();
            TablePersonne.Items.Clear();
            //Appel de la méthode InsertPsersons créee dans
            per.InsertPersons(TextName.Text, TextPrenom.Text, TextTelephone.Text, TextAdresse.Text);
            Refresh();
        }
        public class ClTablePersonne
        {
            public string id { get; set; }
            public string nom { get; set; }
            public string prenom { get; set; }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
        private void ClicDelete(object sender, RoutedEventArgs e)
        {
            if (TablePersonne.SelectedIndex != -1)
            {
                //string myId = TablePersonne.SelectedItems = "{Binding Path=id}";
                var data = new ClTablePersonne {};
                string myId= (this.DataContext as ClTablePersonne).Tostring;
                int id = int.Parse(myId);
                per.DeletePersonneById(id);
            }
            else
            {
                MessageBox.Show("Vous devez sélectionner une ligne dans le tableau pour pouvoir supprimer un enregistrement !");   
            }
        }
    }
}
