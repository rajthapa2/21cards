using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.OS;
using Android.Widget;

namespace cards
{
	[Activity(Label = "Calculate")]
	public class CalculateActivity : Activity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.Calculate);

			var names = Intent.Extras.GetStringArrayList("players") ?? new List<string>();

			TableLayout table = FindViewById<TableLayout>(Resource.Id.tableLayout1);

			var players = new List<Player>();

			names.ToList().ForEach((name) => {
				players.Add(Helper.CreatePlayer(name));
			});


			players.ForEach((p) => { 
				var row = new TableRow(this);
				var playerName = new TextView(this);
				playerName.Text = p.Name;
				row.AddView(playerName);

				var maal = new EditText(this);
				row.AddView(maal);

				var gameWon = new CheckBox(this);
				gameWon.Checked = p.GameWon;
				row.AddView(gameWon);

				var maalSeen = new CheckBox(this);
				maalSeen.Checked = p.MaalSeen;
				row.AddView(maalSeen);


				var dubliee = new CheckBox(this);
				dubliee.Checked = p.Dubliee;
				row.AddView(dubliee);


				table.AddView(row);
			});

			// logic for calculating maal
			var calculateButton = FindViewById<Button>(Resource.Id.calculate);

			calculateButton.Click += (sender, e) => {
				players.ForEach((player) => {
					player.Points = 20;
					player.Money = 10;
				});
			};

		}
	}

	public class Player
	{
		public Player(string name)
		{
			Name = name;
		}

		public string Name { get; set; }
		public int Mall { get; set; }
		public bool GameWon { get; set; }
		public bool MaalSeen { get; set; }
		public bool Dubliee { get; set; }
		public int Points { get; set; }
		public int Money { get; set; }
	}


	public static class Helper
	{
		public static Player CreatePlayer(string name) {
			return new Player(name);
		}
	}
}
