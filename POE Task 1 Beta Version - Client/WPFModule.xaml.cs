using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using System.Text.RegularExpressions;
using POE_Task_1__Beta_Version;

namespace POE_Task_1_Beta_Version___Client
{
    //NIKSHAY LALLA
    //20109946
    //PROG6212- POE TASK 1
    //DATE: 20/09/2021
    public partial class WPFModule : Window
    {
        public static int numberOfWeeks = MainWindow.numOfWeeks;
      
        public WPFModule()
        {
            InitializeComponent();

        }

        //Button for submitting information in my module class
        private void btnSubmitInfo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Capturing the users input from textboxes
                string code = txtCode.Text;
                string name = txtName.Text;
                int credits = Convert.ToInt32(txtCredits.Text);
                double hours = Convert.ToDouble(txtHours.Text);
                double selfStudy = Utility.Calculation(credits, MainWindow.numOfWeeks, hours);

                //Saving the users data in a list
                Module mod = new Module(code, name, credits, hours, selfStudy);

                //Adding information to a list
                Utility.module.Add(mod);

                //Foreach loop that loops through the users input and displays it in the listbox. 
                foreach (Module item in Utility.module)
                {
                    //Clears the list box, so it does not allow for duplicates
                    lstbxModule.Items.Clear();

                    //Display information in a listbox called lstbxModule
                    lstbxModule.Items.Add("Module Information " + "\n" + "--------------------------------------------------------------");
                    lstbxModule.Items.Add("Module Code: " + item.ModuleCode + "\n" + "Module Name: " + item.ModuleName + "\n" + "Module Credits: "
                        + item.ModuleCredits + "\n" + "Module hours per week: "
                        + item.ModuleHrsPerWeek + "\n" + "Self - study hours: " + item.SelfStudyHours);
                }

            }
            catch (Exception)
            {

                MessageBox.Show("Please ensure all textboxes are filled in");
            }
            

            //Error handling
            /********************************Code attribution*******************************************
             Author: CodeProject.com, BalaMurugan1989             
             Date: 20 November 2012
             Link: https://www.codeproject.com/Questions/496674/EmptyplusTextboxplusValidationplusinplusC-23 */

            if (txtName.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please enter module Name");
               
            }

            /********************************Code attribution*******************************************
            Author: CodeProject.com, BalaMurugan1989             
            Date: 20 November 2012
            Link: https://www.codeproject.com/Questions/496674/EmptyplusTextboxplusValidationplusinplusC-23 */
            if (txtCode.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please enter module code");
                
            }

        }

        /********************************Code attribution*******************************************
            Author: programmingistheway.wordpress.com            
            Date: 17 February 2017
            Link:  https://programmingistheway.wordpress.com/2017/02/17/only-numbers-in-a-wpf-textbox-with-regular-expressions/ */
        //this code does not allow users to enter text only allowing for numbers
        private void ValidateCredits(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
           
        }

        /********************************Code attribution*******************************************
           Author: programmingistheway.wordpress.com            
           Date: 17 February 2017
           Link:  https://programmingistheway.wordpress.com/2017/02/17/only-numbers-in-a-wpf-textbox-with-regular-expressions/ */
        //this code does not allow users to enter text only allowing for numbers
        private void ValidateHours(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
            // MessageBox.Show("You have entered " + e.Text + " Please enter a valid number ");
        }

        //back button, where the user will go to the mainwindow
        private void btnback_Click(object sender, RoutedEventArgs e)
        {
            //Create a new instance of my main mindow
            MainWindow main = new MainWindow();
            Hide();//hide my module form
            main.Show();//shows main window form

            //When the user clicks the back button the grid semeseter details will be hidden and the grid heading will be shown. 
            main.gridSemesterDetails.Visibility = Visibility.Hidden;
            main.gridHeading.Visibility = Visibility.Visible;

            //When the user clicks the back button the labels will be shown below and the grid will dissapear.
            main.lblNumWeeks.Content = MainWindow.numOfWeeks;
            main.lblStartDate.Content = MainWindow.startDate;

            //the button to save the weeks and start date will be disabled so the user cannot enter information again. 
            main.btnCapture.IsEnabled = false;
        }

    }
}
//END


