using Android.App;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using Android.Content;

namespace cards
{
	[Activity(Label = "21cards", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);

			var addPlayerButton = FindViewById<Button>(Resource.Id.addPlayer);
			var playerName = FindViewById<EditText>(Resource.Id.playerName);
			var startButton = FindViewById<Button>(Resource.Id.startGame);
			var result = FindViewById<TextView>(Resource.Id.result);

			var players = new List<string>();
			playerName.Click += (sender, e) => {
				playerName.Text = "";
			};

			addPlayerButton.Click += (object sender, System.EventArgs e) => {
				players.Add(playerName.Text);
				var playersName = "";
				startButton.Enabled = true;
				players.ForEach((obj) => {
					playersName = playersName + obj + "     ";
				});
				playerName.Text = "";
				result.Text = playersName;
			};

			//start calculate page
			startButton.Click += (sender, e) => {
				var intent = new Intent(this, typeof(CalculateActivity));

				//temp so that I don't have to keep adding player
				players.Add("Raj");
				players.Add("Aista");
				players.Add("Sumi");
				players.Add("Dipesh");

				intent.PutStringArrayListExtra("players", players);

				StartActivity(intent);
			};
		}
	}
}

