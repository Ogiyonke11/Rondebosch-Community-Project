using System.Diagnostics;
using System.DirectoryServices.ActiveDirectory;

namespace Rondebosch_Community_Project
{
    public partial class RondeboschCommunityMarket : Form
    {
        //Array for the combobox items
        string[] foodType = { "Vegetable", "Can foods", "Protein foods", "Carbohydrate foods" };
        decimal[] foodPrices = { 5m, 10m, 15m, 10m };
        int count = 0;

        //Array  for tickets
        string[] ticketTypes = { "Orchestra", "Mezzanine", "General", "Balcony" };
        decimal[] ticketPrices = { 500m, 300m, 150m, 50m };
        int totalTicketsSold = 0;


        //Global variables 
        int quantity;
        decimal taxRate = 0;
        double subtotal;
        double totalPrice;

        public RondeboschCommunityMarket()
        {
            InitializeComponent();
        }

        private void RondeboschCommunityMarket_Load(object sender, EventArgs e)
        {
            //Changing the visibility of the radiobuttons and the groupbox, when it starts running, they should not appear only when clicked on yes
            rdbtnYes.Checked = false;
            rdbtnNo.Checked = false;
            OrdersGroupBox.Visible = false;
            rdbtnPrepaid.Checked = false;
            rdbtnLaybuy.Checked = false;
            rdbtnCash.Checked = false;

            //Adding the array and prices of the ticket
            for (int i = 0; i < ticketTypes.Length; i++)
            {
                lstbxTickets.Items.Add($"{ticketTypes[i]}  - R{ticketPrices[i]}");
            }

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            //exit the form
            Application.Exit();
        }

        private void btnClear_Click_1(object sender, EventArgs e)
        {
            //Clear everything
            txtName.Clear();
            txtTelephone.Clear();
            txtQuantity.Clear();
            txtTicketPrice.Clear();
            txtTicketsSold.Clear(); 
            rdbtnNo.Checked = false;
            rdbtnYes.Checked = false;
            rdbtnPrepaid.Checked = false;
            rdbtnLaybuy.Checked = false;
            rdbtnCash.Checked = false;
            cmbBoxCatalog.Text = string.Empty;

            //Clicking clear removes the groupbox as well and focuses on the Name textbox
            OrdersGroupBox.Visible = false;
            txtName.Focus();

        }

        private void rdbtnNo_CheckedChanged(object sender, EventArgs e)
        {
            //Clicking the No radioButton clears everything and doesn't continue further with the programme
            txtName.Clear();
            txtTelephone.Clear();
            txtQuantity.Clear();

            //changing the visibility of the checked radiobutton
            rdbtnNo.Visible = true;

            //Displaying an information radiobutton to administator if they are not spaza shop owners
            string message = "This seasonal sale is for spaza shop only. We thank you for your interest in our program but if you are not a spaza shop owner, we cannot provide you with any assistance. ";
            MessageBox.Show(message, "Not Available", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //Focusing the cursor on the Name textbox
            txtName.Focus();
        }

        private void rdbtnYes_CheckedChanged_1(object sender, EventArgs e)
        {
            //Clicking yes makes the groupBox appear and the user can continue with their order
            OrdersGroupBox.Visible = true;
            cmbBoxCatalog.Items.Clear();
            rdbtnPrepaid.Visible = true;
            rdbtnLaybuy.Visible = true;
            rdbtnCash.Visible = true;
            //Ading the items and prices of food on the combo box
            for (int i = 0; i < foodType.Length; i++)
            {
                cmbBoxCatalog.Items.Add($"{foodType[i]}  - R{foodPrices[i]}");
            }

        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Allows user to change the font
            fontDialog1.Font = txtName.Font;
            fontDialog1.Font = txtTelephone.Font;
            fontDialog1.ShowDialog();
            txtName.Font = fontDialog1.Font;
            txtName.Font = fontDialog1.Font;
        }

        private void colorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Allows user to change the color
            colorDialog1.Color = txtName.ForeColor;
            colorDialog1.Color = txtTelephone.ForeColor;
            colorDialog1.ShowDialog();
            txtName.ForeColor = colorDialog1.Color;
            txtTelephone.ForeColor = colorDialog1.Color;
        }

        //Method for calculating the pay
        private decimal CalculatePay()
        {
            try
            {
                //Prices that will be used to calculate the prices of items
                decimal[] prices = { 5m, 10m, 15m, 10m };

                //Parse quantity from textbox
                int quantity = int.Parse(txtQuantity.Text);
                int telephone = int.Parse(txtTelephone.Text);

                if (txtTelephone.Text.Length != 10)
                {
                    MessageBox.Show("Maximum ten digits");
                    return -1; //for error conditions
                }


                if (quantity > 20)
                {
                    MessageBox.Show("Maximum 20 units allowed.");
                    return -1;
                }

                if (cmbBoxCatalog.SelectedIndex == -1)
                {
                    MessageBox.Show("Select product.");
                    return -1;
                }

                //Radiobutttons return the tax rate for calculation
                if (rdbtnPrepaid.Checked)
                {
                    taxRate = 0.02m;
                }
                else if (rdbtnLaybuy.Checked)
                {
                    taxRate = 0.03m;
                }
                else if (rdbtnCash.Checked)
                {
                    taxRate = 0.00m;
                }
                else
                {
                    MessageBox.Show("Please select a payment method.");
                    return -1;
                }

                //Calculations
                int selectedIndex = cmbBoxCatalog.SelectedIndex;
                decimal Prices = foodPrices[selectedIndex];

                decimal subtotal = Prices * quantity;
                decimal taxAmount = subtotal * taxRate;
                decimal totalPrice = subtotal + taxAmount;
                MessageBox.Show($"Total price: R{totalPrice:F2}", "Order Total");

                return totalPrice; //Calculates the total price
            }
            catch (Exception)
            {
                MessageBox.Show("Ensure you entered the correct information.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            //Calls the method 
            CalculatePay();
        }

        private void totalCostinclTaxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Calculates the sum of the orders made
            double totalPrice = 0;
            totalPrice += totalPrice;
            MessageBox.Show($"Total Cost of Orders: R{totalPrice:F2}");
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Checking if the item is there before adding it to the combo box
            bool foodFound = false;
            int foodIndex = 0;

            if (!string.IsNullOrEmpty(cmbBoxCatalog.Text))
            {
                while (!foodFound && foodIndex < cmbBoxCatalog.Items.Count)
                {
                    if (cmbBoxCatalog.Text == cmbBoxCatalog.Items.ToString())
                    {
                        foodFound = true;
                    }
                    else
                    {
                        foodIndex++;
                    }
                }
                if (foodFound)
                {
                    MessageBox.Show("Duplicate Food type.", "Add failed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    cmbBoxCatalog.Items.Add(cmbBoxCatalog.Text);
                    cmbBoxCatalog.Text = "";
                }
            }
            else
            {
                MessageBox.Show("Enter a valid item", "Missing data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Deleting selcted item returning a message if item is not selected
            if (cmbBoxCatalog.SelectedIndex != -1)
            {
                cmbBoxCatalog.Items.RemoveAt(cmbBoxCatalog.SelectedIndex);
            }
            else
            {
                MessageBox.Show("Select the food type you would like to remove.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void numberOfItemsInCatalogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Show the total number of items in the combo box
            MessageBox.Show("There are " + cmbBoxCatalog.Items.Count + " Food types on the list.", " Food Type", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void clearCatalogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Clears the combo box and asks user to verify theirr decision
            DialogResult ClearDialogResult;

            ClearDialogResult = MessageBox.Show("Are you sure you want to clear  the catalog ?", "Clear Catalog", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (ClearDialogResult == DialogResult.Yes)
            {
                cmbBoxCatalog.Items.Clear();
            }
        }

        private void totalOrdersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Calculates the total orders 
            int count = 0;

            count++;
            MessageBox.Show($" Total Orders: {count} ");
        }

        private void calculatePriceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Calculates the total price of tickets purchased
            try
            {
                int selectedIndex = lstbxTickets.SelectedIndex;
                decimal Prices = ticketPrices[selectedIndex];

                lstbxTickets.Text = $"{Prices}";

                int totalTickets = 0;
                totalTickets++;
                //Display on the textbox
                txtTicketPrice.Text = lstbxTickets.Text;
                txtTicketsSold.Text = totalTickets.ToString();

            }
            catch (Exception)
            {
                MessageBox.Show(" Select an item ", "Select");
            }
        }

        private void lstbxTickets_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Printing the catalog
            printPreviewCatalogDialog1.Document = printCatalog;
            printPreviewCatalogDialog1.ShowDialog();
        }

        private void printCatalog_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            //Set up printer output
            Font printFont = new Font("Arial", 12);
            float lineHeight = printFont.GetHeight() + 2;
            float horizontalPrintLocation = e.MarginBounds.Left;
            float verticalPrintLocation = e.MarginBounds.Top;
            string printLine;

            //Programme name
            printLine = "Rondebosch Community Monthly Sale ";
            e.Graphics!.DrawString(printLine, printFont, Brushes.Black, horizontalPrintLocation, verticalPrintLocation);
            verticalPrintLocation += lineHeight * 2;

            using (Font headingFont = new Font("Arial", 14, FontStyle.Bold))
            {
                //Print a heading and then the list
                printLine = "Sale Catalogue";
                e.Graphics.DrawString(printLine, printFont, Brushes.Black, horizontalPrintLocation, verticalPrintLocation);
                verticalPrintLocation += lineHeight;
                printLine = "___________________";
                e.Graphics.DrawString(printLine, printFont, Brushes.Black, horizontalPrintLocation, verticalPrintLocation);
                verticalPrintLocation += lineHeight;

            }

            //Loop through combo box to print 
            for (int i = 0; i < cmbBoxCatalog.Items.Count; i++)
            {
                //set up a line
                printLine = cmbBoxCatalog.Items[i].ToString();
                //sending the line to the graphics page 
                e.Graphics.DrawString(printLine, printFont, Brushes.Black, horizontalPrintLocation, verticalPrintLocation);
                verticalPrintLocation += lineHeight;
            }

        }

        private void ticketsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //increments the number of tickets purchased
            int count = 0;

            count++;
            MessageBox.Show($" Total Tickets Purchased: {count} ");
        }
    }

}
