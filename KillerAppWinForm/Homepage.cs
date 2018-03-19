using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KillerAppClassLibrary.Classes;
using KillerAppClassLibrary.Context;
using KillerAppClassLibrary.Logic;
using KillerAppClassLibrary.Logic.Repositories;

namespace KillerAppWinForm
{
    public partial class Homepage : Form
    {

        private readonly Controller controller = new Controller();

        public Homepage()
        {
            InitializeComponent();
            tabControl.SelectedIndex = 1;
            tabControl.ItemSize = new Size(tabControl.ItemSize.Width, 1);
            dgvAllTracks.AutoGenerateColumns = true;
            UpdateDatabindings(dgvAllTracks);
            UpdateLatestReleases(lbLatestReleases);
            UpdateMostDownloaded(lbMostDownloaded);
        }

        #region HoverColorChanges

        private void lbViewAll_MouseEnter(object sender, EventArgs e)
        {
            lbViewAll.ForeColor = Color.Gray;
        }

        private void lbViewAll_MouseLeave(object sender, EventArgs e)
        {
            lbViewAll.ForeColor = Color.White;
        }

        private void lbLoginOrCreate_MouseEnter(object sender, EventArgs e)
        {
            lbLoginOrCreate.ForeColor = Color.Gray;
        }

        private void lbLoginOrCreate_MouseLeave(object sender, EventArgs e)
        {
            lbLoginOrCreate.ForeColor = Color.White;
        }

        private void lbAdminPage_MouseEnter(object sender, EventArgs e)
        {
            lbAdminPage.ForeColor = Color.Gray;
        }

        private void lbAdminPage_MouseLeave(object sender, EventArgs e)
        {
            lbAdminPage.ForeColor = Color.White;
        }

        #endregion

        private void lbViewAll_Click(object sender, EventArgs e)
        {
            tabControl.SelectedIndex = 1;
            UpdateDatabindings(dgvAllTracks);
        }


        private void lbLoginOrCreate_Click(object sender, EventArgs e)
        {
            tabControl.SelectedIndex = 2;

            if (controller.UserIsLoggedIn())
            {
                lbCurrentBalance.Text = controller.GetCurrentUser().Fund + ",-";
            }
        }

        private void btCheckout_Click(object sender, EventArgs e)
        {
            tabControl.SelectedIndex = 3;
            var currentShoppingCart = controller.GetShoppingCartItems(controller.GetCurrentUser());

            Updateshoppingcart(currentShoppingCart.ToList());
        }



        private void lbAdminPage_Click(object sender, EventArgs e)
        {

            if (!controller.UserIsLoggedIn())
            {
                MessageBox.Show("You must be logged in as an admin to use this feature.");
                return;
            }
            if (controller.GetCurrentUser().IsAdmin)
            {
                tabControl.SelectedIndex = 4;
                UpdateDatabindings(dgvAdminPage);
            }
            else
            {
                MessageBox.Show("You must be logged in as an admin to use this feature.");
            }

        }

        private void btRegister_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
            controller.RegisterUser(new User(tbUsernameRegister.Text, tbEmailRegister.Text, tbPasswordRegister.Text));
        }

        private void btLogin_Click(object sender, EventArgs e)
        {
            try
            {
                controller.Login(tbEmailLogin.Text, tbPasswordLogin.Text);
                lbLoginOrCreate.Text = controller.GetCurrentUser().Username + " | Accountpage";
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }

            
        }

        private void AddToWallet_Click(object sender, EventArgs e)
        {
            var button = sender as Button;

            MessageBox.Show(controller.AddFund(Convert.ToInt32(button.Tag)));

            var user = controller.GetCurrentUser();

            lbCurrentBalance.Text = user.Fund + ",-";
        }

        private void dgvAllTracks_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            tabControl.SelectedIndex = 0;

            controller.CurrentTrack = GetTrackFromDataGrid(dgvAllTracks);

            UpdateTrackInfo();
        }

        private void lbLatestReleases_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            controller.CurrentTrack = GetListboxTrack(lbLatestReleases);
            tabControl.SelectedIndex = 0;
            UpdateTrackInfo();
        }

        private void btBuyCurrent_Click(object sender, EventArgs e)
        {
            try
            {
                controller.PutInShoppingCart(controller.CurrentTrack, controller.GetCurrentUser());
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void Homepage_Load(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
        }

        private void btCreate_Click(object sender, EventArgs e)
        {
            try
            {
                throw controller.AddNewTrack(tbArtistName.Text, tbTrackName.Text, tbLabel.Text, Convert.ToInt32(nmPrice.Value));
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);

                UpdateDatabindings(dgvAdminPage);
                UpdateLatestReleases(lbLatestReleases);
            }
        }

        private void btRemove_Click(object sender, EventArgs e)
        {
            try
            {
                controller.RemoveTrack(GetTrackFromDataGrid(dgvAdminPage));
                UpdateDatabindings(dgvAdminPage);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }


        }

        private void btEdit_Click(object sender, EventArgs e)
        {
            if (dgvAdminPage.SelectedRows.Count > 0) return;

            var track = GetTrackFromDataGrid(dgvAdminPage);

            tbArtistName.Text = track.ArtistName;
            tbTrackName.Text = track.TrackName;
            tbLabel.Text = track.Label;
            nmPrice.Value = track.Price;
            lbId.Text = track.Id.ToString();
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            try
            {
                controller.EditTrack(new Track(Convert.ToInt32(lbId.Text), tbArtistName.Text, tbTrackName.Text, tbLabel.Text, Convert.ToInt32(nmPrice.Value)));
                UpdateDatabindings(dgvAdminPage);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }


        }

        private void pbPositive_Click(object sender, EventArgs e)
        {
            try
            {
                controller.CastVote(controller.GetCurrentUser(), controller.CurrentTrack, 1);
                lbScore.Text = controller.GetScore(controller.CurrentTrack).ToString();
                UpdateVoteIndicators();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                
            }
        }

        private void pbNegative_Click(object sender, EventArgs e)
        {
            try
            {
                controller.CastVote(controller.GetCurrentUser(), controller.CurrentTrack, -1);
                lbScore.Text = controller.GetScore(controller.CurrentTrack).ToString();
                UpdateVoteIndicators();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }

        }

        private void btPay_Click(object sender, EventArgs e)
        {
            controller.CreateOrder(controller.GetShoppingCartItems(controller.GetCurrentUser()),
                controller.GetCurrentUser());

            UpdateMostDownloaded(lbMostDownloaded);
        }

        private void lbMostDownloaded_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            controller.CurrentTrack = GetListboxTrack(lbMostDownloaded);
            tabControl.SelectedIndex = 0;
            UpdateTrackInfo();
        }

        private void btLogout_Click(object sender, EventArgs e)
        {
            MessageBox.Show(controller.Logout());

            if (!controller.UserIsLoggedIn())
            {
                lbLoginOrCreate.Text = "Login | Create account";
            }
        }

        private void Remove_Click(object sender, EventArgs e)
        {
            var currentShoppingCart = controller.GetShoppingCartItems(controller.GetCurrentUser()).ToList();

            var label = sender as Label;

            var shoppingCart = currentShoppingCart;

            if (shoppingCart.Count < Convert.ToInt32(label.Tag) - 1) return;

            var track = shoppingCart[Convert.ToInt32(label.Tag) - 1];

            controller.RemoveItemFromShoppingCart(controller.GetCurrentUser(), track);
            Updateshoppingcart(currentShoppingCart);
        }

        #region Methods

        /// <summary>
        /// Gets the double clicked track from the specified DataGridView.
        /// </summary>
        /// <param name="grid">The grid.</param>
        /// <returns></returns>
        private Track GetTrackFromDataGrid(DataGridView grid)
        {
            return controller.GetAllTracks().ToList().Find(t => t.Id == Convert.ToInt32(grid.CurrentRow.Cells["ID"].Value));
        }

        /// <summary>
        /// Gets the track selected in the listbox you are using.
        /// </summary>
        /// <param name="listBox">The list box.</param>
        /// <returns></returns>
        private Track GetListboxTrack(ListBox listBox)
        {
            return listBox.SelectedItem as Track;
        }

        /// <summary>
        /// Updates the track information.
        /// </summary>
        private void UpdateTrackInfo()
        {
            LbTrackname.Text = $"{controller.CurrentTrack.ArtistName} " + $"- {controller.CurrentTrack.TrackName}"
                ;
            lbArtistname.Text = $"{controller.CurrentTrack.ArtistName}";
            lbLabel.Text = $"{controller.CurrentTrack.Label}";
            lbPrice.Text = $"{controller.CurrentTrack.Price},00";
            lbScore.Text = controller.GetScore(controller.CurrentTrack).ToString();

            UpdateVoteIndicators();
        }

        /// <summary>
        /// Updates the databindings for the specified grid.
        /// </summary>
        private void UpdateDatabindings(DataGridView grid)
        {
            grid.ReadOnly = false;

            var tracks = controller.GetAllTracks();
            var source = new BindingSource { DataSource = tracks };

            grid.DataSource = source;
            grid.Columns["ID"].Visible = false;

            grid.ReadOnly = true;
        }

        /// <summary>
        /// Updates the latest releases listbox.
        /// </summary>
        /// <param name="listBox">The list box.</param>
        private void UpdateLatestReleases(ListBox listBox)
        {
            listBox.Items.Clear();

            foreach (var track in controller.GetLatestReleases())
            {
                listBox.Items.Add(track);
                listBox.DisplayMember = "TrackName";
            }
        }

        /// <summary>
        /// Gets the most downloaded tracks and adds them to the specified listbox.
        /// </summary>
        /// <param name="listBox">The list box.</param>
        private void UpdateMostDownloaded(ListBox listBox)
        {
            listBox.Items.Clear();

            foreach (var track in controller.GetMostDownloadedTracks())
            {
                listBox.Items.Add(track);
                listBox.DisplayMember = "TrackName";
            }
        }

        /// <summary>
        /// Updates the color of the vote pictureboxes.
        /// </summary>
        private void UpdateVoteIndicators()
        {
            if (controller.GetCurrentUser() == null)
            {

            }
            else
            {
                if (!controller.HasVoted(controller.CurrentTrack, controller.GetCurrentUser()))
                {
                    pbNegative.Image = Properties.Resources.down;
                    pbPositive.Image = Properties.Resources.up;
                }

                if (controller.VoteIsPositive(controller.CurrentTrack, controller.GetCurrentUser()))

                {
                    pbPositive.Image = Properties.Resources.upGreen;
                    pbNegative.Image = Properties.Resources.down;
                }
                else
                {
                    pbNegative.Image = Properties.Resources.downRed;
                    pbPositive.Image = Properties.Resources.up;
                }
            }
        }

        /// <summary>
        /// Updates the text of the checkout. Only works for adding. Not removing.
        /// </summary>
        /// <param name="currentShoppingCart">The current shopping cart.</param>
        private void Updateshoppingcart(List<Track> currentShoppingCart)
        {
            if (currentShoppingCart == null) return;

            if (currentShoppingCart.Count > 0)
            {
                lbArtistCheckout1.Text = currentShoppingCart[0].ArtistName;
                lbLabelCheckout1.Text = currentShoppingCart[0].Label;
                lbTrackCheckout1.Text = currentShoppingCart[0].TrackName;
                lbPriceCheckout1.Text = currentShoppingCart[0].Price + ",-";
            }
            if (currentShoppingCart.Count > 1)
            {
                lbArtistCheckout2.Text = currentShoppingCart[1].ArtistName;
                lbLabelCheckout2.Text = currentShoppingCart[1].Label;
                lbTrackCheckout2.Text = currentShoppingCart[1].TrackName;
                lbPriceCheckout2.Text = currentShoppingCart[1].Price + ",-";
            }
            if (currentShoppingCart.Count > 2)
            {
                lbArtistCheckout3.Text = currentShoppingCart[2].ArtistName;
                lbLabelCheckout3.Text = currentShoppingCart[2].Label;
                lbTrackCheckout3.Text = currentShoppingCart[2].TrackName;
                lbPriceCheckout3.Text = currentShoppingCart[2].Price + ",-";
            }
            if (currentShoppingCart.Count > 3)
            {
                lbArtistCheckout4.Text = currentShoppingCart[3].ArtistName;
                lbLabelCheckout4.Text = currentShoppingCart[3].Label;
                lbTrackCheckout4.Text = currentShoppingCart[3].TrackName;
                lbPriceCheckout4.Text = currentShoppingCart[3].Price + ",-";
            }
            if (currentShoppingCart.Count > 4)
            {
                lbArtistCheckout5.Text = currentShoppingCart[4].ArtistName;
                lbLabelCheckout5.Text = currentShoppingCart[4].Label;
                lbTrackCheckout5.Text = currentShoppingCart[4].TrackName;
                lbPriceCheckout5.Text = currentShoppingCart[4].Price + ",-";

            }

            lbTotalPriceCheckout.Text = currentShoppingCart.Sum(track => track.Price) + ",-";
        }
        #endregion
    }
}
