﻿using Npgsql;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KelLynItTermProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Business class that stores all the variables for the business.
        /// </summary>
        public class Business
        {
            /// <summary>
            /// 
            /// </summary>
            public string business_id { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string name { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string address { get; set; }
            /// <summary>
            /// state string.
            /// </summary>
            public string state { get; set; }

            /// <summary>
            /// city string.
            /// </summary>
            public string city { get; set; }

            /// <summary>
            /// zipcode string.
            /// </summary>
            public string zipcode { get; set; }

            /// <summary>
            /// latitude string.
            /// </summary>
            public double latitude { get; set; }

            /// <summary>
            /// longitude string.
            /// </summary>
            public double longitude { get; set; }

            /// <summary>
            /// number of stars.
            /// </summary>
            public double stars { get; set; }

            /// <summary>
            /// number of reviews.
            /// </summary>
            public int reviewCount { get; set; }

            /// <summary>
            /// 1 for when business is open 0 for closed.
            /// </summary>
            public int isOpen { get; set; }

            /// <summary>
            /// number of check-ins.
            /// </summary>
            public int numCheckIns { get; set; }

            /// <summary>
            /// review rating.
            /// </summary>
            public double reviewRating { get; set; }

        }

        public class Friend
        {
            public string friendName { get; set; }

            public string Yelping_since
            {
                get; set;
            }

            public double FriendStars { get; set; }
        }

        public class Reviews
        {
            public string ReviewUserName { get; set; }

            public string ReviewBusinessName { get; set; }

            public string ReviewCity { get; set; }

            public string ReviewText { get; set; }
        }
        /// <summary>
        /// Builder for string to connect to the database.
        /// </summary>
        /// <returns>string to connect to database.</returns>
        private string buildConnString()
        {
            return "Host=localhost; Username=postgres; Password=Abigail1; Database=projectTest;";
        }

        public MainWindow()
        {
            InitializeComponent();
            AddStates();
            AddColumns2FriendsGrid();
            AddColumns2ReviewsGrid();
            addColumns2BusinessGrid();
        }

        /// <summary>
        /// Add the columns to the business grid.
        /// </summary>
        public void addColumns2BusinessGrid()
        {
            DataGridTextColumn column = new DataGridTextColumn();
            column.Header = "BusinessName";
            column.Binding = new Binding("name");
            resultsGrid.Columns.Add(column);

            DataGridTextColumn column1 = new DataGridTextColumn();
            column1.Header = "Address";
            column1.Binding = new Binding("address");
            resultsGrid.Columns.Add(column1);

            DataGridTextColumn column2 = new DataGridTextColumn();
            column2.Header = "City";
            column2.Binding = new Binding("city");
            resultsGrid.Columns.Add(column2);

            DataGridTextColumn column3 = new DataGridTextColumn();
            column3.Header = "State";
            column3.Binding = new Binding("state");
            resultsGrid.Columns.Add(column3);

            DataGridTextColumn column4 = new DataGridTextColumn();
            column4.Header = "Distance (miles)";
            column4.Binding = new Binding("distance");
            resultsGrid.Columns.Add(column4);

            DataGridTextColumn column5 = new DataGridTextColumn();
            column5.Header = "Stars";
            column5.Binding = new Binding("stars");
            resultsGrid.Columns.Add(column5);

            DataGridTextColumn column6 = new DataGridTextColumn();
            column6.Header = "# of Reviews";
            column6.Binding = new Binding("reviewCount");
            resultsGrid.Columns.Add(column6);

            DataGridTextColumn column7 = new DataGridTextColumn();
            column7.Header = "Avg Review Rating";
            column7.Binding = new Binding("reviewRating");
            resultsGrid.Columns.Add(column7);

            DataGridTextColumn column8 = new DataGridTextColumn();
            column8.Header = "Total CheckIns";
            column8.Binding = new Binding("numCheckIns");
            resultsGrid.Columns.Add(column8);
        }

        /// <summary>
        /// Adds the columns to the friends grid.
        /// </summary>
        public void AddColumns2FriendsGrid()
        {
            DataGridTextColumn name = new DataGridTextColumn();
            name.Header = "Name";
            name.Binding = new Binding("friendName");
            FriendsDataGrid.Columns.Add(name);

            DataGridTextColumn avgStars = new DataGridTextColumn();
            avgStars.Header = "Avg Stars";
            avgStars.Binding = new Binding("FriendStars");
            FriendsDataGrid.Columns.Add(avgStars);

            DataGridTextColumn yelpingSince = new DataGridTextColumn();
            yelpingSince.Header = "Yelping Since";
            yelpingSince.Binding = new Binding("Yelping_since");
            FriendsDataGrid.Columns.Add(yelpingSince);
        }

        public void AddColumns2ReviewsGrid()
        {
            DataGridTextColumn userName = new DataGridTextColumn();
            userName.Header = "User Name";
            userName.Binding = new Binding("ReviewUserName");
            ReviewsByFriendsDataGrid.Columns.Add(userName);

            DataGridTextColumn business = new DataGridTextColumn();
            business.Header = "Business";
            business.Binding = new Binding("ReviewBusinessName");
            ReviewsByFriendsDataGrid.Columns.Add(business);

            DataGridTextColumn city = new DataGridTextColumn();
            city.Header = "City";
            city.Binding = new Binding("ReviewCity");
            ReviewsByFriendsDataGrid.Columns.Add(city);

            DataGridTextColumn text = new DataGridTextColumn();
            text.Header = "Text";
            text.Binding = new Binding("ReviewText");
            ReviewsByFriendsDataGrid.Columns.Add(text);
        }

        /// <summary>
        /// Add the states to the state combo box.
        /// </summary>
        public void AddStates()
        {
            using (var comm = new NpgsqlConnection(buildConnString()))
            {
                comm.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = comm;
                    cmd.CommandText = "SELECT DISTINCT state_code FROM business ORDER BY state_code";
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            stateList.Items.Add(reader.GetString(0));
                        }
                    }
                }
                comm.Close();
            }
        }

        /// <summary>
        /// Add the cities from the database for the selected state.
        /// </summary>
        public void AddCitiesWhenStateSelected()
        {
            using (var comm = new NpgsqlConnection(buildConnString()))
            {
                cityListBox.Items.Clear();
                comm.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = comm;
                    cmd.CommandText = "SELECT DISTINCT city FROM business WHERE state_code = '" + stateList.SelectedItem.ToString() + "'ORDER BY city;";
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cityListBox.Items.Add(reader.GetString(0));
                        }
                    }
                }
                comm.Close();
            }
        }

        /// <summary>
        /// Add zip codes from database that are in selected city.
        /// </summary>
        public void AddZipCodesWhenCitySelected()
        {
            using (var comm = new NpgsqlConnection(buildConnString()))
            {
                zipCodeListBox.Items.Clear();
                comm.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = comm;
                    cmd.CommandText = "SELECT DISTINCT postal_code FROM business WHERE state_code = '" + stateList.SelectedItem.ToString() + "' and city='" + cityListBox.SelectedItem.ToString() + "' ORDER BY postal_code;";
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            zipCodeListBox.Items.Add(reader.GetString(0));
                        }
                    }
                }
                comm.Close();
            }
        }

        /// <summary>
        /// Add the business categories from the database that are in the selected zipcode. 
        /// </summary>
        public void AddBusinessCategoryWhenZipCodeSelected()
        {
            using (var comm = new NpgsqlConnection(buildConnString()))
            {
                categoryListBox.Items.Clear();
                comm.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = comm;
                    cmd.CommandText = "SELECT DISTINCT category_name FROM business, categories WHERE state_code = '" + stateList.SelectedItem.ToString() + "' and city='" + cityListBox.SelectedItem.ToString() + "' " +
                        "and postal_code = '" + zipCodeListBox.SelectedItem.ToString() + "' and business.business_id = categories.business_id  ORDER BY category_name;";
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            categoryListBox.Items.Add(reader.GetString(0));
                        }
                    }
                }
                comm.Close();
            }
        }

        /// <summary>
        /// add cities to the list when a state is selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void stateList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            resultsGrid.Items.Clear();
            if (stateList.SelectedIndex > -1)
            {
                AddCitiesWhenStateSelected();
            }
        }

        /// <summary>
        /// add zip codes to the list when a city is selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cityListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            resultsGrid.Items.Clear();
            if (cityListBox.SelectedIndex > -1)
            {
                AddZipCodesWhenCitySelected();
            }
        }

        /// <summary>
        /// add business categories to the list when a zipcode is selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void zipCodeListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            resultsGrid.Items.Clear();
            if (zipCodeListBox.SelectedIndex > -1)
            {
                AddBusinessCategoryWhenZipCodeSelected();
            }
        }

        /// <summary>
        /// get the businesses that have the selected category in the selected state, city, and zipcode
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void search_Click(object sender, RoutedEventArgs e)
        {
            resultsGrid.Items.Clear();

            using (var comm = new NpgsqlConnection(buildConnString()))
            {
                comm.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = comm;
                    cmd.CommandText = "SELECT * FROM business, (SELECT DISTINCT business_id as busID FROM categories WHERE category_name = '" + categoryListBox.SelectedItem.ToString() + "') a " +
                        "WHERE state_code='" + stateList.SelectedItem.ToString() + "' and city='" + cityListBox.SelectedItem.ToString() + "' " +
                        "and postal_code = '" + zipCodeListBox.SelectedItem.ToString() + "' and a.busID = business.business_id; ";
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            resultsGrid.Items.Add(new Business()
                            {
                                business_id = reader.GetString(0),
                                name = reader.GetString(1),
                                address = reader.GetString(2),
                                city = reader.GetString(3),
                                state = reader.GetString(4),
                                zipcode = reader.GetString(5),
                                latitude = reader.GetDouble(6),
                                longitude = reader.GetDouble(7),
                                stars = reader.GetDouble(8),
                                reviewCount = reader.GetInt32(9),
                                isOpen = reader.GetInt32(10),
                                numCheckIns = reader.GetInt32(11),
                                reviewRating = reader.GetDouble(12)
                            });
                        }
                    }
                }
                comm.Close();
            }
            numBusinessesResult.Text = resultsGrid.Items.Count.ToString();
        }

        private void userTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            userIDListBox.Items.Clear();
            FriendsDataGrid.Items.Clear();
            ReviewsByFriendsDataGrid.Items.Clear();

            using (var comm = new NpgsqlConnection(buildConnString()))
            {
                comm.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = comm;
                    cmd.CommandText = "SELECT user_id FROM users WHERE name = '" + userTextBox.Text.ToString() + "';";
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            userIDListBox.Items.Add(reader.GetString(0));
                        }
                    }
                }
                comm.Close();
            }
        }

        private void userIDListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (userIDListBox.SelectedIndex > -1)
            {
                FriendsDataGrid.Items.Clear();
                ReviewsByFriendsDataGrid.Items.Clear();
                using (var comm = new NpgsqlConnection(buildConnString()))
                {
                    comm.Open();
                    using (var cmd = new NpgsqlCommand())
                    {
                        cmd.Connection = comm;
                        cmd.CommandText = "SELECT name, average_stars, fans, yelping_since, funny, cool, useful FROM users WHERE user_id = '" + userIDListBox.SelectedItem.ToString() + "';";
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                UserNameTextBox.Text = reader.GetString(0);
                                UserStarsTextBox.Text = reader.GetDouble(1).ToString();
                                UserFansTextBox.Text = reader.GetInt32(2).ToString();
                                yelpingSinceTextBox.Text = reader.GetDate(3).ToString();
                                FunnyVotesTextBox.Text = reader.GetInt32(4).ToString();
                                CoolVotesTextBox.Text = reader.GetInt32(5).ToString();
                                UsefulVotesTextBox.Text = reader.GetInt32(6).ToString();
                            }
                        }
                        cmd.CommandText = "SELECT distinct name, average_stars, yelping_since FROM users, (SELECT DISTINCT friend_id FROM friends " +
                            "WHERE user_id = '" + userIDListBox.SelectedItem.ToString() +"') as a WHERE a.friend_id = users.user_id; ";
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                FriendsDataGrid.Items.Add(new Friend { friendName = reader.GetString(0), FriendStars = reader.GetDouble(1), Yelping_since = (reader.GetDate(2)).ToString() });
                            }
                        }
                        cmd.CommandText = "SELECT users.name, business.name, business.city, review.text FROM users, business, review, (SELECT distinct user_id " +
                            "FROM users, (SELECT DISTINCT friend_id FROM friends WHERE user_id = '" + userIDListBox.SelectedItem.ToString() + "') as a WHERE a.friend_id = users.user_id) as b " +
                            "WHERE b.user_id = users.user_id and business.business_id = review.business_id and review.user_id = b.user_id ORDER BY users.name;";
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ReviewsByFriendsDataGrid.Items.Add(new Reviews
                                {
                                    ReviewUserName = reader.GetString(0),
                                    ReviewBusinessName = reader.GetString(1),
                                    ReviewCity = reader.GetString(2),
                                    ReviewText = reader.GetString(3)
                                });
                            }
                        }
                    }
                    comm.Close();
                }
            }
        }
    }
}
