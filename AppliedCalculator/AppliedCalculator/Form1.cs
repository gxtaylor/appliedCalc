using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppliedCalculator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void claimCounter_ValueChanged(object sender, EventArgs e)
        {
            newClaim();
        }

        public void removeClaim()
        {
            
        }

        public void newClaim()
        {
            var x = 90;
            var y = 50;

            for (var i=0; i< claimCounter.Value; i++)
            {
                var date = new DateTimePicker
                {
                    Location = new Point(x, y),
                    Name = $@"Claim: {i + 1}",
                };

                var claimLabel = new Label
                {
                    Location = new Point(x - 60, y + 2),
                    Text = $@"Claim: {i + 1}",
                    Size = new Size(60, 20)
                };

                splitContainer1.Panel2.Controls.Add(claimLabel);
                splitContainer1.Panel2.Controls.Add(date);
                y += 20;
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            var nowDate = DateTime.Now.Date;
            var policyStartDate = dateTimePicker1.Value.Date;

            if(policyStartDate < nowDate)
            {
                MessageBox.Show("Please select a future date");
            }
        }

        public double CalculatePremium()
        {
            var startingPremium = 500;
            double premiumResult = 0.0;
            var driver = getDriver();
            var driverAge = GetYears(driver.DOB.Date);

            if (driverAge >= 21 && driverAge <= 25)
            {
                premiumResult = startingPremium + (startingPremium * 20 / 100);
            }
            else if (driverAge > 25 && driverAge <= 75)
            {
                premiumResult = startingPremium - (startingPremium * 10 / 100);
            }
            else if (driverAge < 21)
            {
                ErrorLabel.Text = "One of your Drivers is too young";
                ErrorLabel.Visible = true;
                
                premiumResult = 0;
            }
            else if (driverAge > 75)
            {
                ErrorLabel.Text = "One of your Drivers is too old";
                ErrorLabel.Visible = true;

                premiumResult = 0;
            }

            if (Job.Text == "Accountant")
            {
                premiumResult = premiumResult - (startingPremium * 10 / 100);
            }
            else if (Job.Text == "Chauffeur")
            {
                premiumResult = premiumResult + (startingPremium * 10 / 100);
            }

            foreach (var claim in driver.Claims)
            {
                if (claim.ClaimDate.Date < DateTime.Now)
                {
                    var years = claimYears(driver.PolicyStart.Date, claim.ClaimDate.Date);
                    if (years <= 1)
                    {
                        premiumResult = premiumResult + (startingPremium * 20 / 100);
                    }
                    else if (years >= 2 && years <= 5)
                    {
                        premiumResult = premiumResult + (startingPremium * 10 / 100);
                    }

                    if (driver.Claims.Count > 2)
                    {
                        ErrorLabel.Text = "Your drivers have too many claims";
                        ErrorLabel.Visible = true;

                        premiumResult = 0;
                    }
                }
                return premiumResult;
            }

            return premiumResult;
        }

        public int GetYears(DateTime date)
        {
            DateTime now = DateTime.Today;
            int years = now.Year - date.Year;

            return years;
        }

        public int claimYears(DateTime policyDate, DateTime claimDate)
        {
            int years = policyDate.Year - claimDate.Year;

            return years;
        }

        public Details getDriver()
        {
            var driver = new Details
            {
                Name = PolicyHolder.Text,
                Occupation = Job.SelectedText,
                DOB = dobPicker.Value.Date,
                PolicyStart = dateTimePicker1.Value.Date,
                Claims = new List<Claim>()
            };

            foreach (Control control in splitContainer1.Panel2.Controls)
            {
                if (control is DateTimePicker picker && control.Name != "")
                {
                    var claim = new Claim { ClaimDate = picker.Value.Date };
                    if (claim != null && claim.ClaimDate.Date >= DateTime.Now.Date)
                    {
                        driver.Claims.Add(claim);
                    }
                }
            }

            foreach (Control control in Driver2.Panel2.Controls)
            {
                if (control is DateTimePicker picker && control.Name != "")
                {
                    var claim = new Claim { ClaimDate = picker.Value.Date };
                    if (claim != null && claim.ClaimDate.Date >= DateTime.Now.Date)
                    {
                        driver.Claims.Add(claim);
                    }
                }
            }

            foreach (Control control in Driver3.Panel2.Controls)
            {
                if (control is DateTimePicker picker && control.Name != "")
                {
                    var claim = new Claim { ClaimDate = picker.Value.Date };
                    if (claim != null && claim.ClaimDate.Date >= DateTime.Now.Date)
                    {
                        driver.Claims.Add(claim);
                    }
                }
            }

            foreach (Control control in Driver4.Panel2.Controls)
            {
                if (control is DateTimePicker picker && control.Name != "")
                {
                    var claim = new Claim { ClaimDate = picker.Value.Date };
                    if (claim != null && claim.ClaimDate.Date >= DateTime.Now.Date)
                    {
                        driver.Claims.Add(claim);
                    }
                }
            }
            return driver;
        }

        public void getQuote_Click(object sender, EventArgs e)
        {
            getDriver();
            CalculatePremium();
            quote.Text = $@"{CalculatePremium()}";
        }

        private void numberOfDrivers_ValueChanged(object sender, EventArgs e)
        {
            switch (numberOfDrivers.Value)
            {
                case 1:
                    Driver2.Visible = true;
                    break;
                case 2:
                    Driver2.Visible = true;
                    Driver3.Visible = true;
                    break;
                case 3:
                    Driver2.Visible = true;
                    Driver3.Visible = true;
                    Driver4.Visible = true;
                    break;
                case 4:
                    Driver2.Visible = true;
                    Driver3.Visible = true;
                    Driver4.Visible = true;
                    Driver5.Visible = true;
                    break;
            }
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {
           
        }

        private void Driver2_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        public void d1Claims_ValueChanged(object sender, EventArgs e)
        {
            for (var i = 0; i < d1Claims.Value; i++)
            {
                newSubClaim1();
            }
        }

        public void newSubClaim1()
        {
            var x1 = 90;
            var y1 = 50;

            for (var i = 0; i < d1Claims.Value; i++)
            {
                var date = new DateTimePicker
                {
                    Location = new Point(x1, y1),
                    Name = $@"Claim: {i + 1}",
                };

                var claimLabel = new Label
                {
                    Location = new Point(x1 - 60, y1 + 2),
                    Text = $@"Claim: {i + 1}",
                    Size = new Size(60, 20)
                };

                Driver2.Panel2.Controls.Add(claimLabel);
                Driver2.Panel2.Controls.Add(date);
                y1 += 20;
            }
        }

        private void d2claims_ValueChanged(object sender, EventArgs e)
        {
            for (var i = 0; i < d2claims.Value; i++)
            {
                newSubClaim2();
            }
        }

        public void newSubClaim2 ()
        {
            var x1 = 90;
            var y1 = 50;

            for (var i = 0; i < d2claims.Value; i++)
            {
                var date = new DateTimePicker
                {
                    Location = new Point(x1, y1),
                    Name = $@"Claim: {i + 1}",
                };

                var claimLabel = new Label
                {
                    Location = new Point(x1 - 60, y1 + 2),
                    Text = $@"Claim: {i + 1}",
                    Size = new Size(60, 20)
                };

                Driver3.Panel2.Controls.Add(claimLabel);
                Driver3.Panel2.Controls.Add(date);
                y1 += 20;
            }
        }

        private void d3Claims_ValueChanged(object sender, EventArgs e)
        {
            newSubClaim3();
        }

        public void newSubClaim3()
        {
            var x1 = 90;
            var y1 = 50;

            for (var i = 0; i < d3Claims.Value; i++)
            {
                var date = new DateTimePicker
                {
                    Location = new Point(x1, y1),
                    Name = $@"Claim: {i + 1}",
                };

                var claimLabel = new Label
                {
                    Location = new Point(x1 - 60, y1 + 2),
                    Text = $@"Claim: {i + 1}",
                    Size = new Size(60, 20)
                };

                Driver4.Panel2.Controls.Add(claimLabel);
                Driver4.Panel2.Controls.Add(date);
                y1 += 20;
            }
        }

        private void d4Claims_ValueChanged(object sender, EventArgs e)
        {
            newSubClaim4();
        }

        public void newSubClaim4()
        {
            var x1 = 90;
            var y1 = 50;

            for (var i = 0; i < d4Claims.Value; i++)
            {
                var date = new DateTimePicker
                {
                    Location = new Point(x1, y1),
                    Name = $@"Claim: {i + 1}",
                };

                var claimLabel = new Label
                {
                    Location = new Point(x1 - 60, y1 + 2),
                    Text = $@"Claim: {i + 1}",
                    Size = new Size(60, 20)
                };

                Driver5.Panel2.Controls.Add(claimLabel);
                Driver5.Panel2.Controls.Add(date);
                y1 += 20;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 NewForm = new Form1();
            NewForm.Show();
            this.Dispose(false);
        }
    }
}

