using CoopUisraelSQlite.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CoopUisraelSQlite
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConsultaRegistro : ContentPage
    {
        private SQLiteAsyncConnection _conn;
        private ObservableCollection<Estudiante> _Tablaestudiante;  //contenedor de datos 


        public ConsultaRegistro()
        {
            InitializeComponent();
            _conn = DependencyService.Get<DataBase>().GetConnection(); 
            NavigationPage.SetHasBackButton(this, false); 
        }

        protected async override void OnAppearing()
        {
            var ResulRegistros = await _conn.Table<Estudiante>().ToListAsync(); // creo una variable de regitros
            _Tablaestudiante = new ObservableCollection<Estudiante>(ResulRegistros);
            ListaUsuarios.ItemsSource = _Tablaestudiante;
            base.OnAppearing();
        }


        void OnSelection(object sender, SelectedItemChangedEventArgs e)
        {
            var Obj = (Estudiante)e.SelectedItem;
            var item = Obj.Id.ToString();
            int ID = Convert.ToInt32(item);
            try
            {
                Navigation.PushAsync(new Elemento(ID)); // abre una nueva ventana de ese campo elemento
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}