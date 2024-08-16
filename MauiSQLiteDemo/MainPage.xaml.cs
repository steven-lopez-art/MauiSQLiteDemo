namespace MauiSQLiteDemo
{
    public partial class MainPage : ContentPage
    {
        private readonly LocalDbService _dbService;
        private int _editClientesId;

        public MainPage(LocalDbService dbService)
        {
            InitializeComponent();
            _dbService = dbService;
            Task.Run(async()=>listview.ItemsSource = await _dbService.GetClientes());
        }

        private async void savebutton_Clicked(object sender, EventArgs e)
        {
            if (_editClientesId==0)
            {
                await _dbService.Create(new Clientes
                {
                    NombreCliente = nombreentryfield.Text,
                    Email = emailentryfield.Text,
                    Movil = movilentryfield.Text

                });
            }
            else
            {
                await _dbService.Update(new Clientes
                {
                    Id = _editClientesId,
                    NombreCliente = nombreentryfield.Text,
                    Email = emailentryfield.Text,
                    Movil = movilentryfield.Text
                });
                _editClientesId = 0;
            }

            nombreentryfield.Text = string.Empty;
            emailentryfield.Text = string.Empty;
            movilentryfield.Text = string.Empty;

            listview.ItemsSource = await _dbService.GetClientes();

        }

        private async void listview_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var clientes = (Clientes)e.Item;
            var action = await DisplayActionSheet("Action", "Cancel", null, "Edit", "Delete");

            switch(action)
            {
                case "Edit":
                    _editClientesId = clientes.Id;
                    nombreentryfield.Text =  clientes.NombreCliente;
                    emailentryfield.Text = clientes.Email;
                    movilentryfield.Text = clientes.Movil;
                    break;

                case "Delete":
                    await _dbService.Delete(clientes);
                    listview.ItemsSource = await _dbService.GetClientes();
                    break;
            }
        }
    }

}
