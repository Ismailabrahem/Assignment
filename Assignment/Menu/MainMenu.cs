namespace Assignment.Menu;

internal class MainMenu
{
    private readonly ClientMenu _clientMenu;
    private readonly CompanyMenu _companyMenu;

    public MainMenu(ClientMenu clientMenu, CompanyMenu companyMenu)
    {
        _clientMenu = clientMenu;
        _companyMenu = companyMenu;
    }

    public async Task StartAsync()
    {
        do
        {
            Console.Clear();
            Console.WriteLine("Main Menu");
            Console.WriteLine("1. Manage Clients");
            Console.WriteLine("2. Manage Companies");
            Console.WriteLine("0. Exit");
            Console.WriteLine("Choose one of the options");
            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    await _clientMenu.ManageClients();
                    break;

                case "2":
                    await _companyMenu.ManageCompanies();
                    break;

                case "0":
                    Environment.Exit(0);
                    break;

            }

        }
        while (true);
    }



}
