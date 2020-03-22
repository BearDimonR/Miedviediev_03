using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Miedviediev_03.Models;
using Miedviediev_03.ViewModels;

namespace Miedviediev_03.Sorting
{
    public class DataGridSortBehaviour
    {
        public static readonly DependencyProperty AllowCustomSortProperty =
            DependencyProperty.RegisterAttached("AllowCustomSort", typeof(bool),
                typeof(DataGridSortBehaviour), new UIPropertyMetadata(false, OnAllowCustomSortChanged));

        public static readonly DependencyProperty SortField =
            DependencyProperty.RegisterAttached("SortField", typeof(SortField),
                typeof(DataGridSortBehaviour), null);

        public static SortField GetSortField(DataGridColumn grid)
        {
            return (SortField) grid.GetValue(SortField);
        }

        public static void SetSortField(DataGrid grid, SortField value)
        {
            grid.SetValue(SortField, value);
        }

        public static bool GetAllowCustomSort(DataGrid grid)
        {
            return (bool) grid.GetValue(AllowCustomSortProperty);
        }

        public static void SetAllowCustomSort(DataGrid grid, bool value)
        {
            grid.SetValue(AllowCustomSortProperty, value);
        }

        private static void OnAllowCustomSortChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var existing = d as DataGrid;
            if (existing == null) return;

            var oldAllow = (bool) e.OldValue;
            var newAllow = (bool) e.NewValue;

            if (!oldAllow && newAllow)
            {
                existing.Sorting += HandleSorting;
            }
            else
            {
                existing.Sorting -= HandleSorting;
            }
        }

        private static void HandleSorting(object sender, DataGridSortingEventArgs e)
        {
            var dataGrid = sender as DataGrid;
            if (dataGrid == null || !GetAllowCustomSort(dataGrid)) return;

            e.Handled = true;

            SortField sortField = GetSortField(e.Column);

            ListSortDirection? sortDirection = e.Column.SortDirection;

            ObservableCollection<Person> persons = (ObservableCollection<Person>) dataGrid.ItemsSource;
            IOrderedEnumerable<Person> result;
            switch (sortField)
            {
                case Sorting.SortField.Name:
                    if(sortDirection == ListSortDirection.Ascending)
                        result = from u in persons
                            orderby u.Name
                            select u;
                    else 
                        result = from u in persons
                            orderby u.Name descending 
                            select u;
                    break;
                case Sorting.SortField.Surname:
                    if(sortDirection == ListSortDirection.Ascending)
                        result = from u in persons
                            orderby u.Surname
                            select u;
                    else 
                        result = from u in persons
                            orderby u.Surname descending
                            select u;
                    break;
                case Sorting.SortField.Email:
                    if(sortDirection == ListSortDirection.Ascending)
                        result = from u in persons
                            orderby u.Email
                            select u;
                    else 
                        result = from u in persons
                            orderby u.Email descending
                            select u;
                    break;
                case Sorting.SortField.Birthday:
                    if(sortDirection == ListSortDirection.Ascending)
                        result = from u in persons
                            orderby u.Birthday
                            select u;
                    else 
                        result = from u in persons
                            orderby u.Birthday descending 
                            select u;
                    break;
                case Sorting.SortField.IsAdult:
                    if(sortDirection == ListSortDirection.Ascending)
                        result = from u in persons
                            orderby u.IsAdult
                            select u;
                    else 
                        result = from u in persons
                            orderby u.IsAdult descending 
                            select u;
                    break;
                case Sorting.SortField.IsBirthday:
                    if(sortDirection == ListSortDirection.Ascending)
                        result = from u in persons
                            orderby u.IsBirthday
                            select u;
                    else 
                        result = from u in persons
                            orderby u.IsBirthday descending 
                            select u;
                    break;
                case Sorting.SortField.WesternZodiac:
                    if(sortDirection == ListSortDirection.Ascending)
                        result = from u in persons
                            orderby u.WesternZodiac
                            select u;
                    else 
                        result = from u in persons
                            orderby u.WesternZodiac descending 
                            select u;
                    break;
                case Sorting.SortField.ChineseZodiac:
                    if(sortDirection == ListSortDirection.Ascending)
                        result = from u in persons
                            orderby u.ChineseSign
                            select u;
                    else 
                        result = from u in persons
                            orderby u.ChineseSign descending 
                            select u;
                    break;
                default:
                    throw new ArgumentException("Indefined SortingField!");
            }
            ((DataGridVm)dataGrid.DataContext).Persons = new ObservableCollection<Person>(result);
            e.Column.SortDirection = sortDirection == ListSortDirection.Descending || sortDirection == null ? ListSortDirection.Ascending : ListSortDirection.Descending;
        }
    }
}