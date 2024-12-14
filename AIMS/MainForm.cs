using AIMS.Models.Entities;
using AIMS.Services;
using AIMS.Views;
using AIMS.Views.Cart;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AIMS
{
    public partial class MainForm : Form
    {
        public static MainForm Instance;
        public MainForm()
        {
            InitializeComponent();
            Instance = this;
            CartView cartView = new CartView();
            MainForm.Instance.mainFormPanel.Controls.Clear();
            MainForm.Instance.mainFormPanel.Controls.Add(cartView);
            cartView.Visible = true;
            cartView.BringToFront();
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            HomeView homeViewUC = new HomeView();
            mainFormPanel.Controls.Add(homeViewUC);
            homeViewUC.Visible = true;
            homeViewUC.BringToFront();
        }
        private async void button1_Click_1(object sender, EventArgs e)
        {
            MediaService mediaService = new MediaService();
            List<(Media, Book, CD, DVD)> mediaItems = new List<(Media, Book, CD, DVD)>()
    {
        // Book examples
        (
            new Media { category = "Book", type = "Paperback", price = 22, quantity = 18, title = "The Catcher in the Rye", imgURL = "catcher.jpg", rush_support = false },
            new Book { author = "J.D. Salinger", coverType = "Soft cover", publisher = "Little, Brown and Company", publishDate = new DateTime(1951, 7, 16), numberOfPages = 277, language = "English" },
            null,
            null
        ),
        (
            new Media { category = "Book", type = "Hardcover", price = 35, quantity = 10, title = "The Lord of the Rings", imgURL = "lotr2.jpg", rush_support = true },
            new Book { author = "J.R.R. Tolkien", coverType = "Hard cover", publisher = "George Allen & Unwin", publishDate = new DateTime(1954, 7, 29), numberOfPages = 1178, language = "English" },
            null,
            null
        ),
        (
            new Media { category = "Book", type = "Ebook", price = 12, quantity = 40, title = "And Then There Were None", imgURL = "andthen.jpg", rush_support = false },
            new Book { author = "Agatha Christie", coverType = "Digital", publisher = "Collins Crime Club", publishDate = new DateTime(1939, 11, 6), numberOfPages = 256, language = "English" },
            null,
            null
        ),
        (
            new Media { category = "Book", type = "Paperback", price = 16, quantity = 22, title = "The Da Vinci Code", imgURL = "davinci.jpg", rush_support = true },
            new Book { author = "Dan Brown", coverType = "Soft cover", publisher = "Doubleday", publishDate = new DateTime(2003, 3, 18), numberOfPages = 454, language = "English" },
            null,
            null
        ),
        (
            new Media { category = "Book", type = "Audiobook", price = 28, quantity = 15, title = "Becoming", imgURL = "becoming.jpg", rush_support = true },
            new Book { author = "Michelle Obama", coverType = "Audio", publisher = "Crown", publishDate = new DateTime(2018, 11, 13), numberOfPages = null, language = "English" },
            null,
            null
        ),

        // CD examples
        (
            new Media { category = "CD", type = "Album", price = 15, quantity = 35, title = "Rumours", imgURL = "rumours.jpg", rush_support = true },
            null,
            new CD { artist = "Fleetwood Mac", recordLabel = "Warner Bros.", tracklist = "Second Hand News, Dreams, ...", releaseDate = new DateTime(1977, 2, 4) },
            null
        ),
        (
            new Media { category = "CD", type = "Single", price = 7, quantity = 50, title = "Smells Like Teen Spirit", imgURL = "smells.jpg", rush_support = false },
            null,
            new CD { artist = "Nirvana", recordLabel = "DGC", tracklist = "Smells Like Teen Spirit, Even in His Youth", releaseDate = new DateTime(1991, 9, 10) },
            null
        ),

        // DVD examples
        (
            new Media { category = "DVD", type = "Movie", price = 18, quantity = 20, title = "Pulp Fiction", imgURL = "pulpfiction.jpg", rush_support = true },
            null,
            null,
            new DVD { discType = "DVD", director = "Quentin Tarantino", runtime = 154, studio = "Miramax Films", subtitle = "English, Spanish, French", releaseDate = new DateTime(1994, 10, 14) }
        ),
        (
            new Media { category = "DVD", type = "TV Series", price = 35, quantity = 12, title = "Game of Thrones", imgURL = "got.jpg", rush_support = false },
            null,
            null,
            new DVD { discType = "DVD", director = "David Benioff, D.B. Weiss", runtime = 57, studio = "HBO", subtitle = "English, French, Spanish", releaseDate = new DateTime(2011, 4, 17) }
        ),
        (
            new Media { category = "DVD", type = "Documentary", price = 12, quantity = 18, title = "Free Solo", imgURL = "freesolo.jpg", rush_support = true },
            null,
            null,
            new DVD { discType = "DVD", director = "Jimmy Chin, Elizabeth Chai Vasarhelyi", runtime = 100, studio = "National Geographic", subtitle = "English", releaseDate = new DateTime(2018, 8, 31) }
        )
    };

            foreach (var (media, book, cd, dvd) in mediaItems)
            {
                await mediaService.AddMediaAsync(media, book, cd, dvd);
                Console.WriteLine($"Added media item: {media.title}");
            }
        }
    }
}
