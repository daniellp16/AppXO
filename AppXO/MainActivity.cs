using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Graphics;
using Android.Views;

namespace AppXO
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        public const int N = 3;
        private Button[,] arr;
        private char player;
        private Button buttonRestart;
        private LinearLayout linearLayoutBoard;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            buttonRestart = (Button)this.FindViewById(Resource.Id.buttonRestart);
            //buttonRestart.Visibility = ViewStates.Invisible;
            this.buttonRestart.Click += ButtonRestart_Click;
            this.linearLayoutBoard =(LinearLayout) this.FindViewById(Resource.Id.linearLayoutBoard);
            this.player = 'X';
            this.BuildBoard();
            this.DrawBoard();
        }

        private void ButtonRestart_Click(object sender, System.EventArgs e)
        {
            this.NewGame();
        }

        public void NewGame()
        {
            this.linearLayoutBoard.RemoveAllViews();
            this.player = 'X';
            this.BuildBoard();
            this.DrawBoard();
        }
        private void DrawBoard()
        {
            ViewGroup.LayoutParams layoutParamsLine = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.WrapContent);
            for (int row = 0; row < N; row++)
            {
                LinearLayout linearLayoutLine = new LinearLayout(this);
                linearLayoutLine.LayoutParameters = layoutParamsLine;
                linearLayoutLine.Orientation = Orientation.Horizontal;
                for (int col = 0; col < N; col++)
                {
                    linearLayoutLine.AddView(this.arr[row, col]);
                }
                this.linearLayoutBoard.AddView(linearLayoutLine);
            }

        }
        public void BuildBoard()
        {
            this.arr = new Button[N, N];
            Point screenSize = new Point();
            this.WindowManager.DefaultDisplay.GetSize(screenSize);//מקבל את הגודל של המסך
            int buttonSize = screenSize.X / N;//גודל המסך חלקי מספר הכפתורים
            ViewGroup.LayoutParams layoutParams = new ViewGroup.LayoutParams(buttonSize, buttonSize);//קובע עצם חדש שבו יכנס כפתור, קובע גודל להיות זהה
            for (int row = 0; row < N; row++)
            {
                for (int col = 0; col < N; col++)
                {
                    this.arr[row, col] = new Button(this);
                    this.arr[row, col].SetTextColor(Color.Black);
                    this.arr[row, col].SetBackgroundResource(Resource.Drawable.btnBackground);
                    this.arr[row, col].LayoutParameters = layoutParams;
                    this.arr[row, col].SetHighlightColor(Color.White);
                    this.arr[row, col].Text = " ";
                    this.arr[row, col].Click += Button_Click;
                    this.arr[row, col].TextSize=screenSize.X / 12;
                }
            }
        }

        private void Button_Click(object sender, System.EventArgs e)
        {
            if (this.Win())
            {
                Toast.MakeText(this, string.Format("{0} Wins!", this.player), ToastLength.Long).Show();
                return;
            }
            Button button = (Button)sender;
            if (button.Text!=" ")
            {
                Toast.MakeText(this, "That's word because you know - You can't touch this", ToastLength.Long).Show();
                return;
            }
            button.Text = this.player.ToString();
            if (this.Win())
            {
                Toast.MakeText(this, string.Format("{0} Wins!", this.player), ToastLength.Long).Show();
                return;
            }
            if (this.player == 'X')
            {
                this.player='○';
            }
            else
            {
                this.player = 'X';
            }
               
        }
        public bool Win ()
        {
            if(this.CheckRows()==true)
            {
                return true;
            }
            if (this.CheckCols())
            {
                return true;
            }
            if (this.MainDiagonal())
            {
                return true;
            }
            if (this.SecondDiagonal())
            {
                return true;
            }
            return false;
        }
        public bool CheckCols()
        {
            for (int col = 0; col < N; col++)
            {
                if (this.CheckCol(col))
                {
                    return true;
                }
            }
            return false;
        }
        public bool CheckRows()
        {
            for (int row = 0; row < N; row++)
            {
                if (this.CheckRow(row))
                {
                    return true;
                }
            }
            return false;
        }
        public bool CheckCol(int col)
        {
            for (int i = 0; i < N; i++)
            {
                if (arr[i, col].Text != player.ToString())
                {
                    return false;
                }
            }
            return true;
        }
        public bool CheckRow( int row)
        {
            for (int j = 0; j < N; j++)
            {
                if (arr[row, j].Text != player.ToString())
                {
                    return false;
                }
            }
            return true;
        }

        public bool SecondDiagonal()
        {
            int i;
            for (i = 0; i < N; i++)
            {
                if (arr[i, N-1-i].Text != player.ToString())
                {
                    return false;
                }
            }
            return true;
        }


        public bool MainDiagonal ()
        {
            for (int i = 0; i < N; i++)
            {
                if(arr[i,i].Text!=player.ToString())
                {
                    return false;
                }
            }
            return true;
        }
    }
}