using System.Collections.Generic;

namespace SectionedRecyclerViewAdapter.Demo
{
    /// <summary>
    /// Dummy data provider for MovieInfo
    /// </summary>
    public static class MovieInfoRepository
    {
        private static List<MovieInfo> movies = new List<MovieInfo>()
        {
            new MovieInfo { Name="The Shawshank Redemption", Year=1994 , Rank = 1},
            new MovieInfo { Name="The Godfather ", Year=1972 , Rank = 2},
            new MovieInfo { Name="The Godfather: Part II", Year=1974 , Rank = 3},
            new MovieInfo { Name="The Dark Knight", Year=2008 , Rank = 4},
            new MovieInfo { Name="Schindler's List ", Year=1993 , Rank = 5},
            new MovieInfo { Name="12 Angry Man", Year=1957 , Rank = 6},
            new MovieInfo { Name="Pulp Fiction", Year=1994 , Rank = 7},
            new MovieInfo { Name="The Lord of the Rings: The Return of the King", Year=2003 , Rank = 8},
            new MovieInfo { Name="Il buono, il brutto, il cattivo", Year=1996 , Rank = 9},
            new MovieInfo { Name="Fight Club", Year=1979 , Rank = 10},
            new MovieInfo { Name="The Lord of the Rings: The Fellowship of the Ring", Year=2001, Rank = 11},
            new MovieInfo { Name="Star Wars: Episode V - The Empire Strikes Back", Year=1980, Rank = 12},
            new MovieInfo { Name="Forrest Gump", Year=1994, Rank = 13},
            new MovieInfo { Name="Inception", Year=2010, Rank = 14},
        };

        public static List<MovieInfo> GetMovies()
        {
            return movies;
        }
    }

}