namespace TestGoogleAPI
{
    public partial class Form1 : Form
    {
        private GoogleHelper helper;

        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            this.helper = new GoogleHelper(Properties.Settings.Default.GoogleToken,
                Properties.Settings.Default.SheetFileName);

            bool success = this.helper.Start().Result;

            SetButton.Enabled = GetButton.Enabled = success;
        }

        private void SetButton_Click(object sender, EventArgs e)
        {
            this.helper.Set(cellName: txtCellNameSet.Text, value: txtCellValue.Text);
        }

        private void GetButton_Click(object sender, EventArgs e)
        {
            var result = this.helper.Get(cellName: txtCellNameGet.Text);

            txtCellGetValue.Text = result;
        }

        private void CreateButton_Click(object sender, EventArgs e)
        {
            this.helper.Create(sheetName: txtCreateSheetName.Text);
        }
    }
}