using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using POE_Task_1__Beta_Version;//Import statement

namespace POE_Task_1_Beta_Version___Client
{
    //NIKSHAY LALLA
    //20109946
    //PROG6212- POE TASK 1
    //DATE: 20/09/2021
    public partial class MainWindow : Window
    {
        //Global variables for weeks and start date, so it is accesible anywhere throughout the program
        public static int numOfWeeks;
        public static string startDate;


        public MainWindow()
        {
            InitializeComponent();
            //When the form loads the grid heading is disabled
            gridHeading.Visibility = Visibility.Hidden;
            //The display button is enabled to be seen
            btnDisplay.IsEnabled = true;
        }

        //Save button, this will allow the user to save the number of weeks and start date 
        private void btnCapture_Click(object sender, RoutedEventArgs e)
        {
            //Error handling for the textbox called weeks
            //So if the textbox is empty a message will appear
            if (!(txtWeeks.Text.Length == 0))
            {
                numOfWeeks = Convert.ToInt32(txtWeeks.Text);

            }
            else
            {
                MessageBox.Show("Please enter number of weeks");

            }

            //So if the datetime picker is empty a message will appear
            /************************Code Attribution***************************
            Author: Stackoverflow.com
            Date: 11 September, 2017
            Link: https://stackoverflow.com/questions/35957822/check-if-datepicker-value-is-null/35957881 */
            if (!(dtpDate.Text.Length == 0))
            {
                startDate = dtpDate.SelectedDate.Value.ToString();
            }
            else
            {
                MessageBox.Show("Please select a date");
            }

            //Displays the numbner of weeks and start date into label
            lblNumWeeks.Content = numOfWeeks;
            lblStartDate.Content = startDate;

        }

        //Error handling 
        /********************************Code attribution*******************************************
           Author: programmingistheway.wordpress.com            
           Date: 17 February 2017
           Link:  https://programmingistheway.wordpress.com/2017/02/17/only-numbers-in-a-wpf-textbox-with-regular-expressions/ */
        //This code does not allow users to enter text only allowing for numbers
        private void ValidateWeeks(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);

        }

        //Add a module button, this will take the user to a new page called WPFModule
        private void btnAddModule_Click(object sender, RoutedEventArgs e)
        {
            //Creates a new instance of the form
            WPFModule WPFModule = new WPFModule();
            Hide();//Hides the current form
            WPFModule.ShowDialog(); //displays the wpfmodule class

        }

        //Add a study tracker button, this will take the user to a new page called Study Tracker
        private void btnStudyTracker_Click(object sender, RoutedEventArgs e)
        {
            //Creates a new instance of the form
            StudyTracker WPFStudyTracker = new StudyTracker();
            Hide();//Hides the current form
            WPFStudyTracker.ShowDialog(); //displays the study tracker form

        }
        //Display button, when user clicks on it, the information is displayed into a listbox
        private void btnDisplay_Click(object sender, RoutedEventArgs e)
        {
            //Displays the module information
            foreach (Module item in Utility.module)
            {
                lstbxSummary.Items.Add("Module Information " + "\n" + "---------------------------------------");
                lstbxSummary.Items.Add("Module Code: " + item.ModuleCode + "\n" + "Module Name: " + item.ModuleName + "\n" + "Module Credits: "
                    + item.ModuleCredits + "\n" + "Module hours per week: "
                    + item.ModuleHrsPerWeek + "\n" + "Self - study hours: " + item.SelfStudyHours + "\n" + "*************************************");
            }

            //Disableds the button. so user cannot duplicate inofrmation
            btnDisplay.IsEnabled = false;

        }

        //Exit button closes the form that is open
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Close();//Closes the form and the application 
        }

    }

}

