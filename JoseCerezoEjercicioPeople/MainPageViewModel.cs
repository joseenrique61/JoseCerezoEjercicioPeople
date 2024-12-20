using JoseCerezoEjercicioPeople.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace JoseCerezoEjercicioPeople
{
	internal class MainPageViewModel : INotifyPropertyChanged
	{
		private string personName;

		private ObservableCollection<Person> allPeople;

		public string PersonName
		{
			get => personName;
			set
			{
				personName = value;
				OnPropertyChanged();
			}
		}

		public string StatusMessage => App.PersonRepo.StatusMessage;

		public ObservableCollection<Person> AllPeople
		{
			get => allPeople;
			set
			{
				allPeople = value;
				OnPropertyChanged();
			}
		}

		public ICommand AddPerson { get; }

		public ICommand GetAllPeople { get; }

		public ICommand Delete { get; }

		public MainPageViewModel()
		{
			AddPerson = new Command(() =>
			{
				App.PersonRepo.AddNewPerson(PersonName);
				PersonName = "";

				OnPropertyChanged(nameof(StatusMessage));
			});

			GetAllPeople = new Command(() =>
			{
				AllPeople = new ObservableCollection<Person>(App.PersonRepo.GetAllPeople());
			});

			Delete = new Command<int>((id) =>
			{
				App.PersonRepo.DeletePerson(id);
				
				OnPropertyChanged(nameof(StatusMessage));
			});
		}

		public event PropertyChangedEventHandler? PropertyChanged;

		public void OnPropertyChanged([CallerMemberName] string name = "") =>
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
	}
}
