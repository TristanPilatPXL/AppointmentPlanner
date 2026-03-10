using System.Windows;
using AppointmentPlanner.Domain;
using AppointmentPlanner.Infrastructure;
using AppointmentPlanner.Application;

namespace AppointmentPlanner.Presentation
{
    public partial class MainWindow : Window
    {
        private readonly PlannerService _service;

        public MainWindow()
        {
            InitializeComponent();

            AppointmentJsonRepository jsonRepository = new AppointmentJsonRepository();
            RoomCsvRepository csvRepository = new RoomCsvRepository();
            _service = new PlannerService(jsonRepository, csvRepository);

            // Laad de rooms in de ComboBox bij het opstarten
            LoadRooms();
            RefreshList();
        }

        // ===== ROOMS LADEN =====
        private void LoadRooms()
        {
            RoomComboBox.ItemsSource = _service.GetAllRooms();
        }

        // ===== ADD APPOINTMENT =====
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Haal de gekozen datum op uit de DatePicker
                DateTime selectedDate = DayPicker.SelectedDate
                    ?? throw new ArgumentException("Selecteer een datum.");

                // Combineer de datum met de ingevoerde tijd
                DateTime startTime = selectedDate.Date + TimeSpan.Parse(StartTimeTextBox.Text);
                DateTime endTime = selectedDate.Date + TimeSpan.Parse(EndTimeTextBox.Text);

                string title = TitleTextBox.Text;
                int participants = int.Parse(ParticipantsTextBox.Text);

                // Voeg toe via de service
                _service.AddAppointment(title, startTime, endTime, participants);

                MessageBox.Show("Appointment toegevoegd!");
                RefreshList();
                ClearFields();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Ongeldige invoer", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Fout", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Er ging iets mis:\n" + ex.Message, "Onverwachte fout", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // ===== CANCEL APPOINTMENT =====
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Haal de geselecteerde appointment op uit de ListBox
                Appointment selected = appointmentsListBox.SelectedItem as Appointment
                    ?? throw new InvalidOperationException("Selecteer een appointment om te annuleren.");

                _service.CancelAppointment(selected);

                MessageBox.Show("Appointment geannuleerd!");
                RefreshList();
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Fout", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Er ging iets mis:\n" + ex.Message, "Onverwachte fout", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // ===== REFRESH =====
        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            RefreshList();
        }

        private void RefreshList()
        {
            appointmentsListBox.ItemsSource = null;
            appointmentsListBox.ItemsSource = _service.GetAllAppointments();
        }

        // ===== INVOERVELDEN LEEGMAKEN =====
        private void ClearFields()
        {
            TitleTextBox.Text = "";
            StartTimeTextBox.Text = "";
            EndTimeTextBox.Text = "";
            ParticipantsTextBox.Text = "";
        }
    }
}