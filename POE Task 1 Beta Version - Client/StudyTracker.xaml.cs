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
using System.Windows.Shapes;
using POE_Task_1__Beta_Version;//Import statement

namespace POE_Task_1_Beta_Version___Client
{
    //NIKSHAY LALLA
    //20109946
    //PROG6212- POE TASK 1
    //DATE: 20/09/2021
    public partial class StudyTracker : Window
    {
       public static List<int> weeklyStudiedHours = new List<int>();
       public static string module;
       public static DateTime dateHours;
       public static int hours;
       public static DateTime startDate;
       public static int numweeks;
       public static double SelfStudyHours;

        public StudyTracker()
        {
            InitializeComponent();
            //when the form loads the combo box gets populated with the module code from the modules page
            foreach (Module item in Utility.module)
            {
                cmbxModules.Items.Add(item.ModuleCode);//Adds items to the combo box
            }

        }

        //Submit button, allowing user to display information
        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
           
            //Capturing the users information from textboxes
            string module = cmbxModules.SelectedItem.ToString();
            int hours = Convert.ToInt32(txtHours.Text);
            DateTime dateHours = Convert.ToDateTime(dtpDateHours.SelectedDate.Value.ToString());
            int numweeks = Convert.ToInt32(MainWindow.numOfWeeks);
            DateTime startDate = Convert.ToDateTime(MainWindow.startDate);
            double SelfStudyHours = 0;

            //Error handling
            /********************************Code attribution*******************************************
             Author: CodeProject.com, BalaMurugan1989             
             Date: 20 November 2012
             Link: https://www.codeproject.com/Questions/496674/EmptyplusTextboxplusValidationplusinplusC-23 */
            //This code ensures the textbox is not empty
            if (txtHours.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please enter number of hours");

            }

            //This code displays the module code and self study hours 
            foreach (Module item in Utility.module)
            {
                if (item.ModuleCode.Equals(module))
                {
                    SelfStudyHours = item.SelfStudyHours;

                }
            }

            //This code takes the list of type int weeks and makes it equal to the populate list methods
            List<int> weeklyStudiedHours = PopulateList(numweeks, Convert.ToInt32(SelfStudyHours));
            Tracker study = new Tracker(hours, module, dateHours, weeklyStudiedHours, startDate);

            //getting weeks
            foreach (Tracker item in StudyHours())
            {
                if (item.module.Equals(module))
                {
                    weeklyStudiedHours = item.weeks;

                }
            }
            //The number of weeks is equal to tothe week from date time picker
            study.weeks = weeklyStudiedHours;
            //Adds to the list of type tracker
            Utility.studies.Add(study);

            //foreach loop that loops through tracker list 
            foreach (Tracker t in Utility.studies)
            {
                //This code takes the weeklystudiedhours and checks it with the weeks and minus it from hours the user enters
                weeklyStudiedHours[Convert.ToInt32(WeekCheck(dateHours, startDate))] = weeklyStudiedHours[Convert.ToInt32(WeekCheck(dateHours, startDate))] - hours;
                //setting weeks
                foreach (Tracker item in StudyHours())
                {
                    if (item.module.Equals(module))
                    {

                        item.weeks = weeklyStudiedHours;

                    }
                }

                //Displaying information in the listbox 
                foreach (Tracker item in Utility.studies)
                {
                    //clears the list
                    lstbxTracker.Items.Clear();
                    //displays in the listbox
                    lstbxTracker.Items.Add("Study Tracker Information: " + "\n" + "------------------------------------------------------"
                                                                        + "\n" + " Module is: " + item.module + "\n" + " Module hour(s) are: "
                                                                        + weeklyStudiedHours[Convert.ToInt32(WeekCheck(dateHours, startDate))] + " hours " + "\n" + " Module date is: " + item.date + "\n" + "Week: "
                                                                        + WeekCheck(dateHours, startDate) + "\n" + "Semester start date is: " + item.semStartDate);
                }

            }

        }

        //This code populates the list of type int with weeks and hours
        public List<int> PopulateList(int numweeks, int hours)
        {
            //Creating a list of type int, so variables can be stored in this list. 
            List<int> weeklyStudiedHours = new List<int>();

            //clears the list
            weeklyStudiedHours.Clear();

            //A for loop that is looping throught he weeks, so week 1 and all that is displayed for that week
            for (int i = 0; i < numweeks; i++)
            {
                //As the weeks changes so does the hours that remain for that certian module, so module called PROG had 6 hours remaing for week 1
                weeklyStudiedHours.Add(hours);

            }
            return weeklyStudiedHours; //return statement
        }

        /*This code is using lINQ method syntax, so it joins to lists which is module and studie(tracker), 
        to display the hours the user entered or used for the module*/
        public List<Tracker> StudyHours()
        {
            /*List tracker is where my information is being saved and data is being pulled from, calling it hours used
            from x(variable name) list of type module and joins it with list of type studies 
            where they share the same variable modulecode*/
            List<Tracker> hoursUsed = (from x in Utility.module
                                       join y in Utility.studies
                                       on x.ModuleCode equals y.module
                                       select y).ToList();

            return hoursUsed;//returns the hours used. 
        }

        //This code checks the weeks, so it converts the days to weeks from the datetime pickers
        public int WeekCheck(DateTime dateHours, DateTime startDate)
        {
            //This captures the users information from the date time pickers and divides by 7 to get weeks, the +1 add one to the list as the default is zero
            int Week = Convert.ToInt32((dateHours - startDate).TotalDays / 7) + 1;

            return Week;//return te weeks
        }

        /********************************Code attribution*******************************************
            Author: programmingistheway.wordpress.com            
            Date: 17 February 2017
            Link:  https://programmingistheway.wordpress.com/2017/02/17/only-numbers-in-a-wpf-textbox-with-regular-expressions/ */
        //this code does not allow users to enter text only allowing for numbers
        private void ValidateHoursWorked(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);

        }

        //This code will display when no item is selected from combo box 
        private void cmbxModules_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbxModules.SelectedIndex == -1)
            {
                MessageBox.Show("Please select an item from the combo box");
            }

        }

        //Back button allowing the user to go to the mainwindow
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


