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

			names.ToList().ForEach((name) =>
			{
				players.Add(Helper.CreatePlayer(name));
			});

			var index = 1;

			players.ForEach((p) =>
			{
				var row = new TableRow(this);
				row.Id = index;
				var playerName = new TextView(this);
				playerName.Text = p.Name;
				row.AddView(playerName);

				var maal = new EditText(this);
				maal.Id = index;
				row.AddView(maal);

				var gameWon = new CheckBox(this);
				gameWon.Id = index;
				gameWon.Checked = p.GameWon;
				row.AddView(gameWon);

				var maalSeen = new CheckBox(this);
				maalSeen.Id = index;
				maalSeen.Checked = p.MaalSeen;
				row.AddView(maalSeen);


				var dubliee = new CheckBox(this);
				dubliee.Id = index;
				dubliee.Checked = p.Dubliee;
				row.AddView(dubliee);


				table.AddView(row);
				index += 1;
			});

			// logic for calculating maal
			var calculateButton = FindViewById<Button>(Resource.Id.calculate);

			calculateButton.Click += (sender, e) =>
			{
				var resultPlayers = new List<Player>();
				players.ForEach((player) =>
				{
					var row = (TableRow)FindViewById(players.IndexOf(player) + 1);
					var name = ((TextView)row.GetChildAt(0)).Text;
					var maal = ((TextView)row.GetChildAt(1)).Text;
					var gameWon = ((CheckBox)row.GetChildAt(2)).Checked;
					var maalSeen = ((CheckBox)row.GetChildAt(3)).Checked;
					var dubliee = ((CheckBox)row.GetChildAt(4)).Checked;

					int result = 0;
					var newPlayer = new Player
					{
						Name = name,
						Mall = int.TryParse(maal, out result) ? result : 0,
						GameWon = gameWon,
						MaalSeen = maalSeen,
						Dubliee = dubliee,
						Points = 10,
						Money = 20
					};
					resultPlayers.Add(newPlayer);
				});

				var resultTable = FindViewById<TableLayout>(Resource.Id.resultTable);
				resultTable.RemoveAllViews();

				var titlerow = new TableRow(this);
				var playerNameText = new TextView(this);
				playerNameText.SetPadding(0, 0, 100, 0);
				playerNameText.Text = "Name";
				titlerow.AddView(playerNameText);

			
				var pointsText = new TextView(this);
				pointsText.Text = "Points";
				pointsText.SetPadding(0, 0, 100, 0);
				titlerow.AddView(pointsText);

			
				var moneyText = new TextView(this);
				moneyText.Text = "Money";
				moneyText.SetPadding(0, 0, 100, 0);
				titlerow.AddView(moneyText);

				resultTable.AddView(titlerow);

				resultPlayers.ForEach((p) =>
					{
						var row = new TableRow(this);

						var playerName = new TextView(this);
						playerName.Text = p.Name;
						row.AddView(playerName);

						var points = new TextView(this);
						points.Text = p.Points.ToString();
						row.AddView(points);

						var money = new TextView(this);
						money.Text = p.Money.ToString();
						row.AddView(money);

						resultTable.AddView(row);
						index += 1;
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

		public Player()
		{

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
		public static Player CreatePlayer(string name)
		{
			return new Player(name);
		}
	}
}
