/*****************************************
 * Program: Assignment 2
 * Programmer: Sam Rutledge & Andrew Madden
 * zID: z1584845 & z1784580
 * Due Date: 2/16/18
 ****************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assign2
{
    public partial class Assign2 : Form
    {
        List<Person> personList = new List<Person>(); //List to hold Persons
        public static string name, office, phone; //data members

        public class Person : IComparable<Person>
        {
            //private data members
            private string name;
            private string officeNum;
            private string phoneNum;

            //constructor
            public Person(string n, string o, string p)
            {
                name = n;
                officeNum = o;
                phoneNum = p;
            }

            //set & get method for Name
            public string Name
            {
                get { return name; }
                set { name = value; }
            }

            //set & get method for OfficeNum
            public string OfficeNum
            {
                get { return officeNum; }
                set { officeNum = value; }
            }

            //set & get method for PhoneNum
            public string PhoneNum
            {
                get { return phoneNum; }
                set { phoneNum = value; }
            }

            //compare function for IComparable interface
            public int CompareTo(Person other)
            {
                return name.CompareTo(other.name);
            }
        }

        public Assign2()
        {
            InitializeComponent();
        }

        /***************************************************************
        Function: void Form1_Load()

        Use: Unchecks all radio buttons, while opening a file stream
        and adds Persons to personList. It then readsthe information
        from the data text file.

        Parameters: object sender, EventArgs e

        Returns: Nothing
        ***************************************************************/
        private void Form1_Load(object sender, EventArgs e)
        {
            //unchecks all radio buttons
            radioButton1.Checked = true;

            //open file stream and add Persons to personList
            using (StreamReader SR = new StreamReader("data1.txt"))
            {
                //read information from data1.txt
                while ((name = SR.ReadLine()) != null)
                {
                    office = SR.ReadLine();
                    phone = SR.ReadLine();

                    personList.Add(new Person(name, office, phone));
                }
            }
        }

        /***************************************************************
        Function: void button1_Click()
        Use:  
        Parameters: object sender, EventArgs e
        Returns: Nothing
        ***************************************************************/
        private void button1_Click(object sender, EventArgs e)
        {

        }

        /***************************************************************
        Function: void groupBox1_Enter()
        Use:  
        Parameters: object sender, EventArgs e
        Returns: Nothing
        ***************************************************************/
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        /***************************************************************
        Function: void button1_Click1()
        Use: When the user chooses the "clear all button," it clears
        all the text fields.
        Parameters: object sender, EventArgs e
        Returns: Nothing
        ***************************************************************/
        private void button1_Click_1(object sender, EventArgs e)
        {
            //Clears out text boxes & ListBox
            NameBox.Clear();
            PhoneNumbox.Clear();
            OfficeNumBox.Clear();
            ListBox.Items.Clear();
        }

        /***************************************************************
        Function: void Name_TextChanged()
        Use:  
        Parameters: object sender, EventArgs e
        Returns: Nothing
        ***************************************************************/
        private void Name_TextChanged(object sender, EventArgs e)
        {

        }

        /***************************************************************
        Function: void label4_Click()
        Use:  
        Parameters: object sender, EventArgs e
        Returns: Nothing
        ***************************************************************/
        private void label4_Click(object sender, EventArgs e)
        {

        }

        /***************************************************************
        Function: void label7_Click()
        Use:  
        Parameters: object sender, EventArgs e
        Returns: Nothing
        ***************************************************************/
        private void label7_Click(object sender, EventArgs e)
        {

        }

        /***************************************************************
        Function: void textBox1_TextChanged()
        Use:  
        Parameters: object sender, EventArgs e
        Returns: Nothing
        ***************************************************************/
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        /***************************************************************
        Function: void radioButtons_CheckedChanged()

        Use: This method checks which radio button was selected by the
        user - it is the event handler for all of the radio buttons.
        Depending on the selection, a specific task is done, 
        whether it be create a new entry, search for a person, office or
        phone number. Our data is changed and output differs
        depending on which radio button is selected.

        Parameters: 2 parameters - object sender, EventArgs e

        Returns: Nothing

        Notes: We use a series of condition statements to check which
        radio button is selected. After a radio button is selected, the
        button is cleared so the user can choose to complete another 
        task, or quit the form.
        ***************************************************************/
        private void radioButtons_CheckedChanged(object sender, EventArgs e)
        {
            //populate Listbox from personList
            if (radioButton1.Checked)
            {
                //Make sure Listbox is cleared before printing
                ListBox.Items.Clear();

                //Print list out 
                for (int i = 0; i < personList.Count; i++)
                {
                    ListBox.Items.Add(personList[i].Name.PadRight(15) + " " + personList[i].PhoneNum.PadRight(15) + " " + personList[i].OfficeNum.PadRight(15));
                }

                radioButton1.Checked = false;
            }

            //add name
            else if (radioButton2.Checked)
            {
                //flag for checking duplicate names
                bool nameMatch = false;

                //if name is not empty, check for duplicate before adding
                if (!String.IsNullOrEmpty(NameBox.Text))
                {
                    //check for duplicate name & print error message if found
                    for (int i = 0; i < personList.Count; i++)
                    {
                        if (NameBox.Text == personList[i].Name)
                        {
                            ListBox.Items.Clear();
                            ListBox.Items.Add("Error: that name already exists");

                            //clear boxes
                            NameBox.Clear();
                            PhoneNumbox.Clear();
                            OfficeNumBox.Clear();

                            nameMatch = true;
                        }
                    }

                    //if there is no duplicate name add person to list
                    if (!nameMatch)
                    {
                        personList.Add(new Person(NameBox.Text, OfficeNumBox.Text, PhoneNumbox.Text));

                        //clear boxes
                        NameBox.Clear();
                        PhoneNumbox.Clear();
                        OfficeNumBox.Clear();
                        ListBox.Items.Clear();
                    }

                }
                else
                {
                    ListBox.Items.Clear();
                    ListBox.Items.Add("Error: Name cannot be blank");
                }

                radioButton2.Checked = false;
            }

            //Search for a name
            else if (radioButton3.Checked)
            {
                //holds value user wants to find
                string searchName;
                bool found = false;

                //set search name
                searchName = NameBox.Text;

                //check to see if Name is blank
                if (String.IsNullOrEmpty(searchName))
                {
                    ListBox.Items.Clear();
                    ListBox.Items.Add("Error: Name cannot be blank");
                }
                else
                {
                    //loop through list looking for searchName match
                    for (int i = 0; i < personList.Count; i++)
                    {
                        //if name is found print that Person
                        if (searchName == personList[i].Name)
                        {
                            ListBox.Items.Clear();
                            ListBox.Items.Add(personList[i].Name.PadRight(15) + " " + personList[i].PhoneNum.PadRight(15) + " " + personList[i].OfficeNum.PadRight(15));
                            found = true;
                        }
                    }

                    //if that Person is not found then print message
                    if (!found)
                    {
                        ListBox.Items.Clear();
                        ListBox.Items.Add("That person does not exist");
                    }
                }


                //clear radio buttons
                radioButton3.Checked = false;
            }//Search for an office number
            else if (radioButton4.Checked)
            {
                //hold the office number of person being searched
                string searchOffice;
                bool found = false;

                //set search office
                searchOffice = OfficeNumBox.Text;

                //check to see if searchOffice is empty first
                if (!String.IsNullOrEmpty(searchOffice))
                {

                    //search through array for match
                    for (int i = 0; i < personList.Count; i++)
                    {
                        if (searchOffice == personList[i].OfficeNum)
                        {
                            ListBox.Items.Clear();
                            ListBox.Items.Add(personList[i].Name.PadRight(15) + " " + personList[i].PhoneNum.PadRight(15) + " " + personList[i].OfficeNum.PadRight(15));
                            found = true;
                        }
                    }

                    //if there is no match
                    if (!found)
                    {
                        ListBox.Items.Clear();
                        ListBox.Items.Add("That office number does not exist.");
                    }

                    //clear radio buttons and text field
                    OfficeNumBox.Clear();
                }
                else
                {
                    ListBox.Items.Clear();
                    ListBox.Items.Add("Error: Office number cannot be blank");
                }

                radioButton4.Checked = false;
            }

            //Search phone numbers
            else if (radioButton5.Checked)
            {
                string searchPhone;
                bool found = false;

                //set search name
                searchPhone = PhoneNumbox.Text;

                if (!String.IsNullOrEmpty(searchPhone))
                {
                    //loop through list looking for searchName match
                    for (int i = 0; i < personList.Count; i++)
                    {
                        //if name is found print that Person
                        if (searchPhone == personList[i].PhoneNum)
                        {
                            ListBox.Items.Clear();
                            ListBox.Items.Add(personList[i].Name.PadRight(15) + " " + personList[i].PhoneNum.PadRight(15) + " " + personList[i].OfficeNum.PadRight(15));
                            found = true;
                        }
                    }

                    //if that Person is not found then print message
                    if (!found)
                    {
                        ListBox.Items.Clear();
                        ListBox.Items.Add("That phone number does not exist");
                    }
                }
                else
                {
                    ListBox.Items.Clear();
                    ListBox.Items.Add("Error: Phone number cannot be blank");
                }
                //clear radio buttons and text field
                radioButton5.Checked = false;
                PhoneNumbox.Clear();

            }

            //Change office number
            else if (radioButton6.Checked)
            {
                string name;
                string newOfficeNum;
                bool found = false;

                name = NameBox.Text;
                newOfficeNum = OfficeNumBox.Text;

                //check to see that name and office are not empty
                if (!String.IsNullOrEmpty(name) && !String.IsNullOrEmpty(newOfficeNum))
                {

                    //search through array for name, then update office number
                    for (int i = 0; i < personList.Count; i++)
                    {
                        if (name == personList[i].Name)
                        {
                            found = true;
                            personList[i].OfficeNum = newOfficeNum;
                            ListBox.Items.Clear();
                            ListBox.Items.Add("The office number has been updated!");
                        }
                    }
                    //if the name cannot be found
                    if (!found)
                    {
                        ListBox.Items.Clear();
                        ListBox.Items.Add("Office number could not be changed. Name not found.");
                    }

                    //clear radio buttons and text fields
                    NameBox.Clear();
                    OfficeNumBox.Clear();
                }
                else
                {
                    ListBox.Items.Clear();
                    ListBox.Items.Add("Error: Name and office number cannot be blank");
                }

                radioButton6.Checked = false;
            }

            //Sort the list of records
            else if (radioButton7.Checked)
            {
                //sort the list
                personList.Sort();

                ListBox.Items.Clear();
                ListBox.Items.Add("The records have been sorted!");

                //clear the radio buttons
                radioButton7.Checked = false;

            }

            //Quit the application
            else if (radioButton8.Checked)
            {
                Application.Exit();
            }
        }
    }
}