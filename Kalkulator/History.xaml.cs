namespace Kalkulator;

public partial class History : ContentPage
{
    string _fileName = Path.Combine(FileSystem.AppDataDirectory, "history.txt");

    public History()
	{
		InitializeComponent();
        LoadHistory();
	}

    protected override void OnAppearing()
    {
        LoadHistory();
    }

    public void LoadHistory()
    {
        if (File.Exists(_fileName))
        {
            HistoryField.Text = File.ReadAllText(_fileName);
        }
    }

    void ClearHistory(object sender, EventArgs e)
    {
        File.WriteAllText(_fileName, "");
        LoadHistory();
    }
}